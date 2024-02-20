using HotelReservations.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace HotelReservations.Repository
{
    public class RoomTypeRepository : IRoomTypeRepository
    {
        public List<RoomType> GetAll()
        {
            var roomTypes = new List<RoomType>();
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                var commandText = "SELECT * FROM dbo.room_type";
                SqlDataAdapter adapter = new SqlDataAdapter(commandText, conn);

                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet, "room_type");

                foreach (DataRow row in dataSet.Tables["room_type"].Rows)
                {
                    var roomType = new RoomType()
                    {
                        Id = (int)row["room_type_id"],
                        Name = (string)row["room_type_name"],
                        IsActive = (bool)row["room_type_is_active"]
                    };

                    roomTypes.Add(roomType);
                }
            }

            return roomTypes;
        }
        public int Insert(RoomType roomType)
        {
            using (var conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();
                var command = new SqlCommand(@"
                INSERT INTO dbo.room_type (room_type_name, room_type_is_active)
                OUTPUT INSERTED.room_type_id
                VALUES (@Name, @IsActive)", conn);

                command.Parameters.AddWithValue("@Name", roomType.Name);
                command.Parameters.AddWithValue("@IsActive", roomType.IsActive);

                return (int)command.ExecuteScalar();
            }
        }
        public void Update(RoomType roomType)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();
                var command = new SqlCommand(@"
                    UPDATE dbo.room_type
                    SET room_type_name = @name, room_type_is_active = @isActive
                    WHERE room_type_id = @id", conn);

                command.Parameters.AddWithValue("@name", roomType.Name);
                command.Parameters.AddWithValue("@isActive", roomType.IsActive);
                command.Parameters.AddWithValue("@id", roomType.Id);

                command.ExecuteNonQuery();
            }
        }

        public void Save(List<RoomType> roomTypes)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        foreach (var roomType in roomTypes)
                        {
                            if (roomType.Id == 0) 
                            {
                                var insertCommand = new SqlCommand(@"
                            INSERT INTO dbo.room_type (room_type_name, room_type_is_active)
                            VALUES (@room_type_name, @room_type_is_active)", conn, transaction);

                                insertCommand.Parameters.AddWithValue("@room_type_name", roomType.Name);
                                insertCommand.Parameters.AddWithValue("@room_type_is_active", roomType.IsActive);

                                insertCommand.ExecuteNonQuery();
                            }
                            else
                            {
                                var updateCommand = new SqlCommand(@"
                            UPDATE dbo.room_type
                            SET room_type_name = @room_type_name, 
                                room_type_is_active = @room_type_is_active
                            WHERE room_type_id = @room_type_id", conn, transaction);

                                updateCommand.Parameters.AddWithValue("@room_type_name", roomType.Name);
                                updateCommand.Parameters.AddWithValue("@room_type_is_active", roomType.IsActive);
                                updateCommand.Parameters.AddWithValue("@room_type_id", roomType.Id);

                                updateCommand.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit(); 
                    }
                    catch
                    {
                        transaction.Rollback(); 
                        throw; 
                    }
                }
            }
        }

    }
}
