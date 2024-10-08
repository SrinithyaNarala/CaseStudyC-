using CarConnect.Model;
using CarConnect.utility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CarConnect.dao
{
    public class VehicleRepository : IVehicleRepository
    {
        public Vehicle GetVehicleById(int vehicleId)
        {
            using (SqlConnection sqlConnection = new SqlConnection(DatabaseContext.GetConnString()))
            {
                using (SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Vehicle WHERE VehicleID = @vehicleId", sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@vehicleId", vehicleId);
                    sqlConnection.Open();

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read(); // Read the first row
                            return new Vehicle()
                            {
                                VehicleId = (int)reader["VehicleID"],
                                Model = (string)reader["Model"],
                                Make = (string)reader["Make"],
                                Year = (int)reader["Year"],
                                Color = (string)reader["Color"],
                                RegistrationNumber = (string)reader["RegistrationNumber"],
                                Availability = (bool)reader["Availability"],
                                DailyRate = (decimal)reader["DailyRate"]
                            };
                        }
                    }
                }
            }
            return null; // Return null if no vehicle is found
        }

        public List<Vehicle> GetAvailableVehicles()
        {
            List<Vehicle> availableVehicles = new List<Vehicle>();

            using (SqlConnection sqlConnection = new SqlConnection(DatabaseContext.GetConnString()))
            {
                using (SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Vehicle WHERE Availability = @availability", sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@availability", 1);
                    sqlConnection.Open();

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            availableVehicles.Add(new Vehicle
                            {
                                VehicleId = (int)reader["VehicleID"],
                                Model = (string)reader["Model"],
                                Make = (string)reader["Make"],
                                Year = (int)reader["Year"],
                                Color = (string)reader["Color"],
                                RegistrationNumber = (string)reader["RegistrationNumber"],
                                Availability = (bool)reader["Availability"],
                                DailyRate = (decimal)reader["DailyRate"]
                            });
                        }
                    }
                }
            }

            return availableVehicles;
        }

        public int AddVehicle(Vehicle vehicle)
        {
            using (SqlConnection sqlConnection = new SqlConnection(DatabaseContext.GetConnString()))
            {
                using (SqlCommand sqlCommand = new SqlCommand("INSERT INTO Vehicle (Model, Make, Year, Color, RegistrationNumber, Availability, DailyRate) " +
                    "VALUES(@model, @make, @year, @color, @regisnum, @availability, @drate)", sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@model", vehicle.Model);
                    sqlCommand.Parameters.AddWithValue("@make", vehicle.Make);
                    sqlCommand.Parameters.AddWithValue("@year", vehicle.Year);
                    sqlCommand.Parameters.AddWithValue("@color", vehicle.Color);
                    sqlCommand.Parameters.AddWithValue("@regisnum", vehicle.RegistrationNumber);
                    sqlCommand.Parameters.AddWithValue("@availability", vehicle.Availability);
                    sqlCommand.Parameters.AddWithValue("@drate", vehicle.DailyRate);
                    sqlConnection.Open();

                    return sqlCommand.ExecuteNonQuery(); // Returns the number of rows affected
                }
            }
        }

        public int UpdateVehicle(int id, Vehicle vehicle)
        {
            using (SqlConnection sqlConnection = new SqlConnection(DatabaseContext.GetConnString()))
            {
                using (SqlCommand sqlCommand = new SqlCommand("UPDATE Vehicle SET Model = @model, Make = @make, Year = @year, Color = @color, " +
                    "RegistrationNumber = @regisnum, Availability = @availability, DailyRate = @drate WHERE VehicleID = @id", sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@model", vehicle.Model);
                    sqlCommand.Parameters.AddWithValue("@make", vehicle.Make);
                    sqlCommand.Parameters.AddWithValue("@year", vehicle.Year);
                    sqlCommand.Parameters.AddWithValue("@color", vehicle.Color);
                    sqlCommand.Parameters.AddWithValue("@regisnum", vehicle.RegistrationNumber);
                    sqlCommand.Parameters.AddWithValue("@availability", vehicle.Availability);
                    sqlCommand.Parameters.AddWithValue("@drate", vehicle.DailyRate);
                    sqlCommand.Parameters.AddWithValue("@id", id);
                    sqlConnection.Open();

                    return sqlCommand.ExecuteNonQuery(); // Returns the number of rows affected
                }
            }
        }

        public int RemoveVehicle(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(DatabaseContext.GetConnString()))
            {
                sqlConnection.Open();

                // First, delete related reservations
                using (SqlCommand sqlCommand = new SqlCommand("DELETE FROM Reservation WHERE VehicleID = @vehicleId", sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@vehicleId", id);
                    sqlCommand.ExecuteNonQuery();
                }

                // Now delete the vehicle
                using (SqlCommand sqlCommand = new SqlCommand("DELETE FROM Vehicle WHERE VehicleID = @vehicleId", sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@vehicleId", id);
                    return sqlCommand.ExecuteNonQuery(); // Returns the number of rows affected
                }
            }
        }

        public List<Vehicle> GetAllVehicles()
        {
            throw new NotImplementedException();
        }
    }
}
