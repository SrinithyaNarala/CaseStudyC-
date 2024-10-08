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
    internal class ReservationRepository : IReservationRepository
    {

        SqlConnection sqlConnection = null;
        SqlCommand sqlCommand = null;
        public ReservationRepository()
        {
            sqlConnection = new SqlConnection(DatabaseContext.GetConnString());
            sqlCommand = new SqlCommand();
        }

        public Reservation GetReservationById(int reserveid)
        {
            Reservation reservation = null;
            sqlCommand.CommandText = "select * from Reservation where ReservationId = @reservationId";
            sqlCommand.Parameters.Clear();
            sqlCommand.Parameters.AddWithValue("@reservationId", reserveid);
            sqlCommand.Connection = sqlConnection;
            sqlConnection.Open();
            using (SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        reservation = new Reservation()
                        {
                            ReservationId = (int)reader["ReservationId"],
                            CustomerId = (int)reader["CustomerId"],
                            VehicleId = (int)reader["VehicleId"],
                            StartDate = (DateTime)reader["StartDate"],
                            EndDate = (DateTime)reader["EndDate"],
                            TotalCost = (decimal)reader["TotalCost"],
                            Status = (string)reader["Status"]
                        };
                    }
                }
            }

            sqlConnection.Close();
            return reservation;
        }

        public List<Reservation> GetReservationByCustomerId(int customerid)
        {
            List<Reservation> reservations = new List<Reservation>();
            sqlCommand.CommandText = "select * from Reservation where CustomerID = @customerId";
            sqlCommand.Parameters.Clear();
            sqlCommand.Parameters.AddWithValue("@customerId", customerid);
            sqlCommand.Connection = sqlConnection;
            sqlConnection.Open();
            using (SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Reservation reservation = new Reservation()
                        {
                            ReservationId = (int)reader["ReservationId"],
                            CustomerId = (int)reader["CustomerId"],
                            VehicleId = (int)reader["VehicleId"],
                            StartDate = (DateTime)reader["StartDate"],
                            EndDate = (DateTime)reader["EndDate"],
                            TotalCost = (decimal)reader["TotalCost"],
                            Status = (string)reader["Status"]
                        };
                        reservations.Add(reservation);
                    }
                }
            }
            sqlConnection.Close();
            return reservations;
        }

        public int CreateReservation(Reservation reservation)
        {
            Vehicle vehicle = new Vehicle();
            vehicle.VehicleId = reservation.VehicleId;

            // Using statement for the connection
            using (SqlConnection sqlConnection = new SqlConnection(DatabaseContext.GetConnString()))
            {
                // Prepare the command to get the daily rate
                sqlCommand.CommandText = "SELECT DailyRate, Availability FROM Vehicle WHERE VehicleId = @vehicleId";
                sqlCommand.Parameters.Clear();
                sqlCommand.Parameters.AddWithValue("@vehicleId", reservation.VehicleId);
                sqlCommand.Connection = sqlConnection;

                sqlConnection.Open();

                // Execute the command to get the daily rate
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        // If no record is found for the vehicle, throw VehicleNotFoundException
                        throw new Exceptions.VehicleNotFoundException($"Vehicle with ID {reservation.VehicleId} not found.");
                    }
                    vehicle.DailyRate = (decimal)reader["DailyRate"]; // Retrieve the daily rate
                    bool isAvailable = (bool)reader["Availability"];
                    if (!isAvailable)
                    {
                        // If the vehicle is not available, throw ReservationException
                        throw new Exceptions.ReservationException("Vehicle is already reserved for the selected dates.");
                    }
                }

                // Prepare to insert the reservation
                sqlCommand.CommandText = "INSERT INTO Reservation (CustomerId, VehicleId, StartDate, EndDate, TotalCost, Status) VALUES (@customerId, @vehicleId1, @startDate, @endDate, @totalCost, @status)";
                sqlCommand.Parameters.Clear();
                sqlCommand.Parameters.AddWithValue("@customerId", reservation.CustomerId);
                sqlCommand.Parameters.AddWithValue("@vehicleId1", reservation.VehicleId);
                sqlCommand.Parameters.AddWithValue("@startDate", reservation.StartDate);
                sqlCommand.Parameters.AddWithValue("@endDate", reservation.EndDate);

                int diff = (reservation.EndDate - reservation.StartDate).Days;
                decimal totalCost = vehicle.DailyRate * diff;
                sqlCommand.Parameters.AddWithValue("@totalCost", totalCost);
                sqlCommand.Parameters.AddWithValue("@status", "Pending");

                // Execute the insert command
                int registerStatus = sqlCommand.ExecuteNonQuery();
                Console.WriteLine($"\nThe Total cost is: {totalCost}\n");
                return registerStatus;
            }
        }


        public int UpdateReservation(int id, Reservation reservation)
        {
            sqlCommand.CommandText = @"UPDATE Reservation 
            SET CustomerId = @customerId,
                VehicleId = @vehicleId,
                StartDate = @startDate,
                EndDate = @endDate,
                TotalCost = @totalCost,
                Status = @status
            WHERE ReservationId = @id";
            sqlCommand.Parameters.Clear();

            sqlCommand.Parameters.AddWithValue("@customerId", reservation.CustomerId);
            sqlCommand.Parameters.AddWithValue("@vehicleId", reservation.VehicleId);
            sqlCommand.Parameters.AddWithValue("@startDate", reservation.StartDate);
            sqlCommand.Parameters.AddWithValue("@endDate", reservation.EndDate);
            sqlCommand.Parameters.AddWithValue("@totalCost", reservation.TotalCost);
            sqlCommand.Parameters.AddWithValue("@status", reservation.Status);
            sqlCommand.Parameters.AddWithValue("@id", id);

            sqlCommand.Connection = sqlConnection;
            sqlConnection.Open();

            int registerStatus = sqlCommand.ExecuteNonQuery();
            return registerStatus;
        }
        public int CancelReservation(int reservationId)
        {
            using (SqlConnection sqlConnection = new SqlConnection(DatabaseContext.GetConnString()))
            using (SqlCommand sqlCommand = new SqlCommand("DELETE FROM Reservation WHERE ReservationID = @reservationId", sqlConnection))
            {
                sqlCommand.Parameters.AddWithValue("@reservationId", reservationId);

                try
                {
                    sqlConnection.Open();
                    int status = sqlCommand.ExecuteNonQuery();

                    return status; // Return the status indicating the success of the deletion
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"SQL Error: {ex.Message}");
                    throw;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    throw;
                }
            }




        //    int IReservationRepository.CancelReservation(int reservationId)
        //{
        //        throw new NotImplementedException();
        //    }
        }
    }
}
