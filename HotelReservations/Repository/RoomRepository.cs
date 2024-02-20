using HotelReservations.Exceptions;
using HotelReservations.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservations.Repository
{

    public class RoomRepository : IRoomRepository
    {
        public List<Room> GetAll()
        {
            var rooms = new List<Room>();
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                var commandText = "SELECT r.*, rt.* FROM dbo.room r\r\nINNER JOIN dbo.room_type rt ON r.room_type_id = rt.room_type_id";
                SqlDataAdapter adapter = new SqlDataAdapter(commandText, conn);

                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet, "room");

                foreach (DataRow row in dataSet.Tables["room"]!.Rows)
                {
                    var room = new Room()
                    {
                        Id = (int)row["room_id"],
                        RoomNumber = row["room_number"] as string,
                        HasTV = (bool)row["has_TV"],
                        HasMiniBar = (bool)row["has_mini_bar"],
                        RoomType = new RoomType()
                        {
                            Id = (int)row["room_type_id"],
                            Name = (string)row["room_type_name"],
                            IsActive = (bool)row["room_type_is_active"]
                        },
                        IsActive = (bool)row["room_is_active"],

                    };

                    rooms.Add(room);
                }
            }

            return rooms;
        }

        public int Insert(Room room)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();

                var command = conn.CreateCommand();
                command.CommandText = @"
                    INSERT INTO dbo.room (room_number, has_TV, has_mini_bar, room_type_id, room_is_active)
                    OUTPUT inserted.room_id
                    VALUES (@room_number, @has_TV, @has_mini_bar, @room_type_id, @room_is_active)
                ";

                command.Parameters.Add(new SqlParameter("room_number", room.RoomNumber));
                command.Parameters.Add(new SqlParameter("has_TV", room.HasTV));
                command.Parameters.Add(new SqlParameter("has_mini_bar", room.HasMiniBar));
                command.Parameters.Add(new SqlParameter("room_type_id", room.RoomType.Id));
                command.Parameters.Add(new SqlParameter("room_is_active", room.IsActive));



                int newRoomId = (int)command.ExecuteScalar();
                return newRoomId;
            }
        }

        public void Save(List<Room> roomList)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    var insertCommandText = @"
                INSERT INTO dbo.room (room_number, has_TV, has_mini_bar, room_type_id, room_is_active)
                VALUES (@room_number, @has_TV, @has_mini_bar, @room_type_id, @room_is_active);";

                    var updateCommandText = @"
                UPDATE dbo.room
                SET room_number = @room_number, has_TV = @has_TV, has_mini_bar = @has_mini_bar, 
                    room_type_id = @room_type_id, room_is_active = @room_is_active
                WHERE room_id = @room_id;";

                    foreach (var room in roomList)
                    {
                        SqlCommand cmd;

                        if (room.Id <= 0)
                        {
                            cmd = new SqlCommand(insertCommandText, conn, transaction);
                        }
                        else
                        {
                            cmd = new SqlCommand(updateCommandText, conn, transaction);
                            cmd.Parameters.AddWithValue("@room_id", room.Id);
                        }

                        cmd.Parameters.AddWithValue("@room_number", room.RoomNumber);
                        cmd.Parameters.AddWithValue("@has_TV", room.HasTV);
                        cmd.Parameters.AddWithValue("@has_mini_bar", room.HasMiniBar);
                        cmd.Parameters.AddWithValue("@room_type_id", room.RoomType.Id);
                        cmd.Parameters.AddWithValue("@room_is_active", room.IsActive);

                        cmd.ExecuteNonQuery();
                    }

                    try
                    {
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

    }
}
