using CarConnect.Model;
using CarConnect.utility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnect.dao
{
    public class CustomerRepository : ICustomerRepository
    {
        SqlConnection sqlConnection = null;
        SqlCommand sqlCommand = null;

        public CustomerRepository()
        {
            try
            {
                sqlConnection = new SqlConnection(DatabaseContext.GetConnString());
                sqlCommand = new SqlCommand();
            }
            catch (SqlException sqlEx) // Specifically catch SQL exceptions
            {
                // Throw a custom exception with the original exception as the inner exception
                throw new Exceptions.DatabaseConnectionException("Failed to initialize database connection.", sqlEx);
            }
            catch (Exception ex) // Catch any other general exceptions
            {
                // You may also wrap these in the custom exception
                throw new Exceptions.DatabaseConnectionException("An unexpected error occurred during database initialization.", ex);
            }
        }

        public List<Customer> GetAllCustomers()
        {
            List<Customer> customers = new List<Customer>();

            using (SqlConnection sqlConnection = new SqlConnection(DatabaseContext.GetConnString()))
            using (SqlCommand sqlCommand = new SqlCommand("select * from Customer", sqlConnection))
            {
                sqlConnection.Open();
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Customer customer = new Customer
                            {
                                CustomerId = (int)reader["CustomerID"],
                                FirstName = (string)reader["FirstName"],
                                LastName = (string)reader["LastName"],
                                Email = (string)reader["Email"],
                                PhoneNumber = (string)reader["PhoneNumber"],
                                RegistrationDate = (DateTime)reader["RegistrationDate"]
                            };
                            customers.Add(customer);
                        }
                    }
                }
            }
            return customers;
        }



        public Customer GetCustomerById(int Id)
        {
            Customer customer = null;

            using (SqlConnection sqlConnection = new SqlConnection(DatabaseContext.GetConnString()))
            using (SqlCommand sqlCommand = new SqlCommand("select * from Customer where CustomerID = @customerId", sqlConnection))
            {
                sqlCommand.Parameters.AddWithValue("@customerId", Id);
                sqlConnection.Open();

                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            customer = new Customer
                            {
                                CustomerId = reader.GetInt32(reader.GetOrdinal("CustomerID")),
                                FirstName = reader["FirstName"] != DBNull.Value ? (string)reader["FirstName"] : string.Empty,
                                LastName = reader["LastName"] != DBNull.Value ? (string)reader["LastName"] : string.Empty,
                                Email = reader["Email"] != DBNull.Value ? (string)reader["Email"] : string.Empty,
                                PhoneNumber = reader["PhoneNumber"] != DBNull.Value ? (string)reader["PhoneNumber"] : string.Empty,
                                Address = reader["Address"] != DBNull.Value ? (string)reader["Address"] : string.Empty,
                                Password = reader["Password"] != DBNull.Value ? (string)reader["Password"] : string.Empty,
                                RegistrationDate = reader["RegistrationDate"] != DBNull.Value ? (DateTime)reader["RegistrationDate"] : DateTime.MinValue
                            };
                        }
                    }
                }
            }
            return customer;
        }


        public Customer GetCustomerByUsername(string uname)
        {
            Customer customer = null;

            using (SqlConnection sqlConnection = new SqlConnection(DatabaseContext.GetConnString()))
            using (SqlCommand sqlCommand = new SqlCommand("select * from Customer where Username = @username1", sqlConnection))
            {
                sqlCommand.Parameters.Clear();
                sqlCommand.Parameters.AddWithValue("@username1", uname);

                sqlConnection.Open();

                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            customer = new Customer
                            {
                                CustomerId = (int)reader["CustomerID"],
                                FirstName = reader["FirstName"] != DBNull.Value ? (string)reader["FirstName"] : string.Empty,
                                LastName = reader["LastName"] != DBNull.Value ? (string)reader["LastName"] : string.Empty,
                                Email = reader["Email"] != DBNull.Value ? (string)reader["Email"] : string.Empty,
                                PhoneNumber = reader["PhoneNumber"] != DBNull.Value ? (string)reader["PhoneNumber"] : string.Empty,
                                Address = reader["Address"] != DBNull.Value ? (string)reader["Address"] : string.Empty,
                                Username = reader["Username"] != DBNull.Value ? (string)reader["Username"] : string.Empty,
                                Password = reader["Password"] != DBNull.Value ? (string)reader["Password"] : string.Empty,
                                RegistrationDate = reader["RegistrationDate"] != DBNull.Value ? (DateTime)reader["RegistrationDate"] : DateTime.MinValue
                            };
                        }
                    }
                }
            }
            return customer;
        }


        public int RegisterCustomer(Customer customer)
        {
            int registerStatus = 0;

            using (SqlConnection sqlConnection = new SqlConnection(DatabaseContext.GetConnString()))
            using (SqlCommand sqlCommand = new SqlCommand("Insert Into Customer (FirstName,LastName,Email,PhoneNumber,Address,Username,Password,RegistrationDate) values(@firstName,@lastName,@email,@ph,@address,@username,@password,@date)", sqlConnection))
            {
          //      sqlCommand.Parameters.AddWithValue("@customerId",customer.CustomerId);
                sqlCommand.Parameters.AddWithValue("@firstName", customer.FirstName);
                sqlCommand.Parameters.AddWithValue("@lastName", customer.LastName);
                sqlCommand.Parameters.AddWithValue("@Email", customer.Email);
                sqlCommand.Parameters.AddWithValue("@ph", customer.PhoneNumber);
                sqlCommand.Parameters.AddWithValue("@address", customer.Address);
                sqlCommand.Parameters.AddWithValue("@username", customer.Username);
                sqlCommand.Parameters.AddWithValue("@password", customer.Password);
                sqlCommand.Parameters.AddWithValue("@date", customer.RegistrationDate);

                sqlConnection.Open();
                registerStatus = sqlCommand.ExecuteNonQuery();
            }

            if (registerStatus <= 0)
            {
                throw new Exception("Failed to insert customer record.");
            }

            return registerStatus;
        }


        public int UpdateCustomer(int id, Customer customer)
        {
            using (SqlConnection sqlConnection = new SqlConnection(DatabaseContext.GetConnString()))
            using (SqlCommand sqlCommand = new SqlCommand("Update Customer set FirstName=@firstName,LastName=@lastName,Email=@email,PhoneNumber=@ph,Address=@address,Username=@username,Password=@password,RegistrationDate=@date where CustomerID=@id", sqlConnection))
            {
                sqlCommand.Parameters.Clear();
                sqlCommand.Parameters.AddWithValue("@id", id);
                sqlCommand.Parameters.AddWithValue("@firstName", customer.FirstName);
                sqlCommand.Parameters.AddWithValue("@lastName", customer.LastName);
                sqlCommand.Parameters.AddWithValue("@Email", customer.Email);
                sqlCommand.Parameters.AddWithValue("@ph", customer.PhoneNumber);
                sqlCommand.Parameters.AddWithValue("@address", customer.Address);
                sqlCommand.Parameters.AddWithValue("@username", customer.Username);
                sqlCommand.Parameters.AddWithValue("@password", customer.Password);
                sqlCommand.Parameters.AddWithValue("@date", customer.RegistrationDate);

                sqlConnection.Open();
                int registerStatus = sqlCommand.ExecuteNonQuery();
                return registerStatus;
            }
        }


        public int DeleteCustomer(int id)
        {
            sqlCommand.CommandText = "delete from Customer where CustomerID = @customerId";
            sqlCommand.Parameters.Clear();
            sqlCommand.Parameters.AddWithValue("@customerId", id);
            sqlCommand.Connection = sqlConnection;
            sqlConnection.Open();
            int registerStatus = sqlCommand.ExecuteNonQuery();
            return registerStatus;
        }

    }
}
