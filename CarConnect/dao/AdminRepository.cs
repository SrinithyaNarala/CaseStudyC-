using CarConnect.Model;
using CarConnect.utility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CarConnect.dao
{
    internal class AdminRepository : IAdminRepository
    {
        private SqlConnection sqlConnection;
        private SqlCommand sqlCommand;

        public AdminRepository()
        {
            sqlConnection = new SqlConnection(DatabaseContext.GetConnString());
            sqlCommand = new SqlCommand { Connection = sqlConnection };
        }

        public List<Admin> GetAllAdmins()
        {
            sqlCommand.CommandText = "SELECT * FROM Admin";
            List<Admin> adminList = new List<Admin>();

            try
            {
                sqlConnection.Open();

                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Admin admin = new Admin()
                        {
                            AdminId = (int)reader["AdminId"],
                            FirstName = reader["FirstName"] != DBNull.Value ? (string)reader["FirstName"] : string.Empty,
                            LastName = reader["LastName"] != DBNull.Value ? (string)reader["LastName"] : string.Empty,
                            Email = reader["Email"] != DBNull.Value ? (string)reader["Email"] : string.Empty,
                            PhoneNumber = reader["PhoneNumber"] != DBNull.Value ? (string)reader["PhoneNumber"] : string.Empty,
                            Username = reader["Username"] != DBNull.Value ? (string)reader["Username"] : string.Empty,
                            Password = reader["Password"] != DBNull.Value ? (string)reader["Password"] : string.Empty,
                            Role = reader["Role"] != DBNull.Value ? (string)reader["Role"] : string.Empty,
                            JoinDate = reader["JoinDate"] != DBNull.Value ? (DateTime)reader["JoinDate"] : DateTime.MinValue
                        };
                        adminList.Add(admin);
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Message}"); // Log SQL errors
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}"); // Log general errors
            }
            finally
            {
                sqlConnection.Close();
            }

            return adminList;
        }

        public Admin GetAdminById(int adminId)
        {
            sqlCommand.CommandText = "SELECT * FROM Admin WHERE AdminId = @adminId";
            sqlCommand.Parameters.Clear();
            sqlCommand.Parameters.AddWithValue("@adminId", adminId);

            Admin admin = null;

            try
            {
                sqlConnection.Open();
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            admin = new Admin
                            {
                                AdminId = (int)reader["AdminId"],
                                FirstName = reader["FirstName"] != DBNull.Value ? (string)reader["FirstName"] : string.Empty,
                                LastName = reader["LastName"] != DBNull.Value ? (string)reader["LastName"] : string.Empty,
                                Email = reader["Email"] != DBNull.Value ? (string)reader["Email"] : string.Empty,
                                PhoneNumber = reader["PhoneNumber"] != DBNull.Value ? (string)reader["PhoneNumber"] : string.Empty,
                                Username = reader["Username"] != DBNull.Value ? (string)reader["Username"] : string.Empty,
                                Password = reader["Password"] != DBNull.Value ? (string)reader["Password"] : string.Empty,
                                Role = reader["Role"] != DBNull.Value ? (string)reader["Role"] : string.Empty,
                                JoinDate = reader["JoinDate"] != DBNull.Value ? (DateTime)reader["JoinDate"] : DateTime.MinValue
                            };
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                sqlConnection.Close();
            }

            return admin;
        }

        public Admin GetAdminByUsername(string username) // Ensure correct method signature
        {
            sqlCommand.CommandText = "SELECT * FROM Admin WHERE Username = @username";
            sqlCommand.Parameters.Clear();
            sqlCommand.Parameters.AddWithValue("@username", username);

            Admin admin = null;

            try
            {
                sqlConnection.Open();
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            admin = new Admin
                            {
                                AdminId = (int)reader["AdminId"],
                                FirstName = reader["FirstName"] != DBNull.Value ? (string)reader["FirstName"] : string.Empty,
                                LastName = reader["LastName"] != DBNull.Value ? (string)reader["LastName"] : string.Empty,
                                Email = reader["Email"] != DBNull.Value ? (string)reader["Email"] : string.Empty,
                                PhoneNumber = reader["PhoneNumber"] != DBNull.Value ? (string)reader["PhoneNumber"] : string.Empty,
                                Username = reader["Username"] != DBNull.Value ? (string)reader["Username"] : string.Empty,
                                Password = reader["Password"] != DBNull.Value ? (string)reader["Password"] : string.Empty,
                                Role = reader["Role"] != DBNull.Value ? (string)reader["Role"] : string.Empty,
                                JoinDate = reader["JoinDate"] != DBNull.Value ? (DateTime)reader["JoinDate"] : DateTime.MinValue
                            };
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                sqlConnection.Close();
            }

            return admin;
        }

        public int RegisterAdmin(Admin admin)
        {
            sqlCommand.CommandText = "INSERT INTO Admin (FirstName, LastName, Email, PhoneNumber, Username, Password, Role, JoinDate) VALUES (@firstName, @lastName, @Email, @ph, @username, @password, @role, @date)";
            sqlCommand.Parameters.Clear();
            sqlCommand.Parameters.AddWithValue("@firstName", admin.FirstName);
            sqlCommand.Parameters.AddWithValue("@lastName", admin.LastName);
            sqlCommand.Parameters.AddWithValue("@Email", admin.Email);
            sqlCommand.Parameters.AddWithValue("@ph", admin.PhoneNumber);
            sqlCommand.Parameters.AddWithValue("@username", admin.Username);
            sqlCommand.Parameters.AddWithValue("@password", admin.Password); // Consider hashing this
            sqlCommand.Parameters.AddWithValue("@role", admin.Role); // Ensure this is included
            sqlCommand.Parameters.AddWithValue("@date", admin.JoinDate);

            int registerStatus = 0;

            try
            {
                sqlConnection.Open();
                registerStatus = sqlCommand.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                sqlConnection.Close();
            }

            return registerStatus;
        }

        public int UpdateAdmin(int id, Admin admin)
        {
            sqlCommand.CommandText = "UPDATE Admin SET FirstName = @firstName, LastName = @lastName, Email = @Email, PhoneNumber = @ph, Username = @username, Password = @password, Role = @role, JoinDate = @date WHERE AdminId = @id";
            sqlCommand.Parameters.Clear();
            sqlCommand.Parameters.AddWithValue("@firstName", admin.FirstName);
            sqlCommand.Parameters.AddWithValue("@lastName", admin.LastName);
            sqlCommand.Parameters.AddWithValue("@Email", admin.Email);
            sqlCommand.Parameters.AddWithValue("@ph", admin.PhoneNumber);
            sqlCommand.Parameters.AddWithValue("@username", admin.Username);
            sqlCommand.Parameters.AddWithValue("@password", admin.Password); // Consider hashing this
            sqlCommand.Parameters.AddWithValue("@role", admin.Role);
            sqlCommand.Parameters.AddWithValue("@date", admin.JoinDate);
            sqlCommand.Parameters.AddWithValue("@id", id);

            int updateStatus = 0;

            try
            {
                sqlConnection.Open();
                updateStatus = sqlCommand.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                sqlConnection.Close();
            }

            return updateStatus;
        }

        public int DeleteAdmin(int adminId)
        {
            sqlCommand.CommandText = "DELETE FROM Admin WHERE AdminId = @adminId";
            sqlCommand.Parameters.Clear();
            sqlCommand.Parameters.AddWithValue("@adminId", adminId);

            int deleteStatus = 0;

            try
            {
                sqlConnection.Open();
                deleteStatus = sqlCommand.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                sqlConnection.Close();
            }

            return deleteStatus;
        }

        public Admin GetAdminByUsername(object username)
        {
            throw new NotImplementedException();
        }
    }
}
