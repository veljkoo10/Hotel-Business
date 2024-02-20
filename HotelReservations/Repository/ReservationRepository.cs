using HotelReservations.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;

namespace HotelReservations.Repository
{
    public class ReservationRepository : IReservationRepository
    {
        public List<Reservation> Load()
        {
            var reservations = new List<Reservation>();
            using (var conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();
                var command = new SqlCommand("SELECT * FROM reservation", conn);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var reservation = new Reservation()
                        {
                            Id = (int)reader["reservation_id"],
                            RoomNumber = reader["roomNumber"].ToString(), 
                            ReservationType = (ReservationType)Enum.Parse(typeof(ReservationType), reader["reservation_type"].ToString()),
                            Guests = new List<Guest>(), 
                            StartDateTime = (DateTime)reader["start_date_time"],
                            EndDateTime = (DateTime)reader["end_date_time"],
                            TotalPrice = (decimal)reader["total_price"],
                            IsActive = (bool)reader["is_active"]
                        };
                        reservations.Add(reservation);
                    }
                }
            }

            return reservations;
        }


        public void Save(List<Reservation> reservationList)
        {
            using (var conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    foreach (var reservation in reservationList)
                    {
                        Debug.WriteLine($"Čuvanje rezervacije sa ID: {reservation.Id}");
                        SqlCommand cmd;
                        if (reservation.Id <= 0)
                        {
                            cmd = new SqlCommand(@"
                            INSERT INTO reservation (room_id, reservation_type, guest, start_date_time, end_date_time, total_price, is_active)
                            VALUES (@RoomId, @ReservationType, @Guest, @StartDateTime, @EndDateTime, @TotalPrice, @IsActive)", conn, transaction);


                        }
                        else
                        {
                            cmd = new SqlCommand(@"
                            UPDATE reservation
                            SET room_id = @RoomId, reservation_type = @ReservationType, 
                                guest = @Guest, start_date_time = @StartDateTime, 
                                end_date_time = @EndDateTime, total_price = @TotalPrice, 
                                is_active = @IsActive
                            WHERE reservation_id = @ReservationId", conn, transaction);

                            
                        }

                        
                    }

                    try
                    {
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

        public int Insert(Reservation reservation)
        {
            using (var conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();
                string guestNames = String.Join(", ", reservation.Guests.Select(g => g.Name));

                var command = new SqlCommand(@"
        INSERT INTO reservation (roomNumber, reservation_type, guest, start_date_time, end_date_time, total_price, is_active)
        OUTPUT INSERTED.reservation_id
        VALUES (@RoomNumber, @ReservationType, @Guest, @StartDateTime, @EndDateTime, @TotalPrice, @IsActive)", conn);

                command.Parameters.AddWithValue("@RoomNumber", reservation.RoomNumber);
                command.Parameters.AddWithValue("@ReservationType", reservation.ReservationType.ToString());
                command.Parameters.AddWithValue("@Guest", guestNames);
                command.Parameters.AddWithValue("@StartDateTime", reservation.StartDateTime);
                command.Parameters.AddWithValue("@EndDateTime", reservation.EndDateTime);
                command.Parameters.AddWithValue("@TotalPrice", reservation.TotalPrice);
                command.Parameters.AddWithValue("@IsActive", reservation.IsActive);

                var insertedId = (int)command.ExecuteScalar();
                Debug.WriteLine($"Ubacivanje izvršeno sa ID: {insertedId}");
                return insertedId;
            }
        }





        private int GetRoomIdByRoomNumber(string roomNumber)
        {
            using (var conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();
                var command = new SqlCommand("SELECT room_id FROM room WHERE room_number = @RoomNumber", conn);
                command.Parameters.AddWithValue("@RoomNumber", roomNumber);

                var result = command.ExecuteScalar();
                if (result != null)
                {
                    return (int)result;
                }
                else
                {
                    throw new ArgumentException("Room with the specified number does not exist.");
                }
            }
        }




        public void Update(Reservation reservation)
        {
            using (var connection = new SqlConnection(Config.CONNECTION_STRING))
            {
                connection.Open();
                var command = new SqlCommand("UPDATE reservation SET is_active = @IsActive WHERE reservation_id = @Id", connection);
                command.Parameters.AddWithValue("@IsActive", reservation.IsActive);
                command.Parameters.AddWithValue("@Id", reservation.Id);
                command.ExecuteNonQuery();
            }
        }

    }
}
