using HotelReservations.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace HotelReservations.Repository
{
    internal class GuestRepository : IGuestRepository
    {
        public List<Guest> Load()
        {
            var guests = new List<Guest>();
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();
                var command = new SqlCommand("SELECT * FROM guest", conn);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var guest = new Guest()
                        {
                            Id = (int)reader["guest_id"],
                            Name = reader["name"].ToString(),
                            Surname = reader["surname"].ToString(),
                            Jbmg = reader["jmbg"].ToString(),
                            IsActive = (bool)reader["is_active"]
                        };
                        guests.Add(guest);
                    }
                }
            }

            return guests;
        }

        public void Save(List<Guest> guestList)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    foreach (var guest in guestList)
                    {
                        SqlCommand cmd;
                        if (guest.Id <= 0)
                        {
                            cmd = new SqlCommand(@"
                                INSERT INTO guest (name, surname, jmbg, is_active)
                                VALUES (@Name, @Surname, @Jbmg, @IsActive)", conn, transaction);
                        }
                        else
                        {
                            cmd = new SqlCommand(@"
                                UPDATE guest
                                SET name = @Name, surname = @Surname, jmbg = @Jbmg, is_active = @IsActive
                                WHERE guest_id = @GuestId", conn, transaction);
                            cmd.Parameters.AddWithValue("@GuestId", guest.Id);
                        }

                        cmd.Parameters.AddWithValue("@Name", guest.Name);
                        cmd.Parameters.AddWithValue("@Surname", guest.Surname);
                        cmd.Parameters.AddWithValue("@Jbmg", guest.Jbmg);
                        cmd.Parameters.AddWithValue("@IsActive", guest.IsActive);

                        cmd.ExecuteNonQuery();
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
        public int Insert(Guest guest)
        {
            if (string.IsNullOrEmpty(guest.Jbmg))
            {
                throw new ArgumentException("JMBG cannot be null or empty.");
            }

            using (var conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();

                var command = new SqlCommand(@"
            INSERT INTO guest (name, surname, jmbg, is_active)
            OUTPUT INSERTED.guest_id
            VALUES (@Name, @Surname, @Jbmg, @IsActive)", conn);

                command.Parameters.AddWithValue("@Name", guest.Name);
                command.Parameters.AddWithValue("@Surname", guest.Surname);
                command.Parameters.AddWithValue("@Jbmg", guest.Jbmg);
                command.Parameters.AddWithValue("@IsActive", guest.IsActive);

                return (int)command.ExecuteScalar();
            }
        }



        public void Update(Guest guest)
        {
            using (var conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();

                var command = new SqlCommand(@"
                    UPDATE guest
                    SET name = @Name, surname = @Surname, jmbg = @Jbmg, is_active = @IsActive
                    WHERE guest_id = @GuestId", conn);

                command.Parameters.AddWithValue("@GuestId", guest.Id);
                command.Parameters.AddWithValue("@Name", guest.Name);
                command.Parameters.AddWithValue("@Surname", guest.Surname);
                command.Parameters.AddWithValue("@Jbmg", guest.Jbmg);
                command.Parameters.AddWithValue("@IsActive", guest.IsActive);

                command.ExecuteNonQuery();
            }
        }
    }
}

