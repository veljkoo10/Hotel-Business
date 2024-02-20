using HotelReservations.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace HotelReservations.Repository
{
    internal class UsersRepository : IUsersRepository
    {
        public List<User> LoadAll()
        {
            var users = new List<User>();
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();
                var command = new SqlCommand("SELECT * FROM [dbo].[user]", conn);


                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var user = new User(reader["user_type"].ToString())
                        {
                            Id = (int)reader["user_id"],
                            Name = reader["name"].ToString(),
                            Surname = reader["surname"].ToString(),
                            JMBG = reader["jmbg"].ToString(),
                            Username = reader["username"].ToString(),
                            Password = reader["password"].ToString(),
                            IsActive = (bool)reader["is_active"]
                        };
                        users.Add(user);
                    }
                }
            }

            return users;
        }

        public int Insert(User user)
        {
            try
            {
                using (var conn = new SqlConnection(Config.CONNECTION_STRING))
                {
                    conn.Open();

                    var command = new SqlCommand(@"
            INSERT INTO [dbo].[user] (name, surname, jmbg, username, password, user_type, is_active)
            OUTPUT INSERTED.user_id
            VALUES (@Name, @Surname, @JMBG, @Username, @Password, @UserType, @IsActive)", conn);

                    command.Parameters.AddWithValue("@Name", user.Name);
                    command.Parameters.AddWithValue("@Surname", user.Surname);
                    command.Parameters.AddWithValue("@JMBG", user.JMBG);
                    command.Parameters.AddWithValue("@Username", user.Username);
                    command.Parameters.AddWithValue("@Password", user.Password);
                    command.Parameters.AddWithValue("@UserType", user.UserType);
                    command.Parameters.AddWithValue("@IsActive", user.IsActive);

                    return (int)command.ExecuteScalar();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
                throw;
            }
        }



        public void Save(List<User> userList)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    foreach (var user in userList)
                    {
                        SqlCommand cmd;
                        if (user.Id <= 0)
                        {
                            cmd = new SqlCommand(@"
                            INSERT INTO [dbo].[user] (name, surname, jmbg, username, password, user_type, is_active)
                            VALUES (@Name, @Surname, @JMBG, @Username, @Password, @UserType, @IsActive)", conn, transaction);
                        }
                        else
                        {
                            cmd = new SqlCommand(@"
                            UPDATE [dbo].[user]
                            SET name = @Name, surname = @Surname, jmbg = @JMBG, username = @Username, 
                                password = @Password, user_type = @UserType, is_active = @IsActive
                            WHERE user_id = @UserId", conn, transaction);
                            cmd.Parameters.AddWithValue("@UserId", user.Id);
                        }

                        cmd.Parameters.AddWithValue("@Name", user.Name);
                        cmd.Parameters.AddWithValue("@Surname", user.Surname);
                        cmd.Parameters.AddWithValue("@JMBG", user.JMBG);
                        cmd.Parameters.AddWithValue("@Username", user.Username);
                        cmd.Parameters.AddWithValue("@Password", user.Password);
                        cmd.Parameters.AddWithValue("@UserType", user.UserType);
                        cmd.Parameters.AddWithValue("@IsActive", user.IsActive);

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
        public void Update(User user)
        {
            using (var conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();

                var command = new SqlCommand(@"
            UPDATE [dbo].[user]
            SET name = @Name, surname = @Surname, jmbg = @JMBG, username = @Username, 
                password = @Password, user_type = @UserType, is_active = @IsActive
            WHERE user_id = @UserId", conn);

                command.Parameters.AddWithValue("@UserId", user.Id);
                command.Parameters.AddWithValue("@Name", user.Name);
                command.Parameters.AddWithValue("@Surname", user.Surname);
                command.Parameters.AddWithValue("@JMBG", user.JMBG);
                command.Parameters.AddWithValue("@Username", user.Username);
                command.Parameters.AddWithValue("@Password", user.Password);
                command.Parameters.AddWithValue("@UserType", user.UserType);
                command.Parameters.AddWithValue("@IsActive", user.IsActive);

                command.ExecuteNonQuery();
            }
        }


    }
}
