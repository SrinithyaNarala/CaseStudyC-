using CarConnect.doa;
using CarConnect.Model;
using CarConnect.dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace CarConnect.Service
{
    internal class ReservationService : IReservationService
    {
        readonly IReservationRepository reservationRepository;
        readonly NotificationService notificationService;

        public ReservationService(utility.DatabaseContext dbContext)
        {
            reservationRepository = new ReservationRepository();
            notificationService = new NotificationService();
        }

        public ReservationService()
        {
            reservationRepository = new ReservationRepository();
            notificationService = new NotificationService();
        }

        public Reservation GetReservationById(int id)
        {
            Reservation reservation = new Reservation();
            reservation = reservationRepository.GetReservationById(id);
            return reservation;
        }

        public List<Reservation> GetReservationByCustomerId(int customerId)
        {
            List<Reservation> reservations = new List<Reservation>();
            reservations = reservationRepository.GetReservationByCustomerId(customerId);
            return reservations;
        }

        public void CreateReservation(Reservation reservation)
        {
            int addStatus = reservationRepository.CreateReservation(reservation);
            if (addStatus > 0)
            {
                Console.WriteLine("Reservation added Successfully");
            }
            else
            {
                Console.WriteLine("Addition Failed");
            }
        }

        public void UpdateReservation(int id, Reservation reservation)
        {
            int Status = reservationRepository.UpdateReservation(id, reservation);
            if (Status > 0)
            {
                Console.WriteLine("Reservation updated Successfully");
            }
            else
            {
                Console.WriteLine("Updation Failed");
            }
        }

        public void CancelReservation(int reservationId)
        {
            try
            {
                int status = reservationRepository.CancelReservation(reservationId);
                if (status > 0)
                {
                    Console.WriteLine("Reservation canceled successfully.");
                }
                else
                {
                    Console.WriteLine("Cancellation failed. No matching reservation found.");
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Message}");
                // Handle SQL errors (e.g., log them)
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                // Handle other exceptions (e.g., log them)
            }
        }

        public List<Reservation> GetReservationsByCustomerId(int customerId)
        {
            throw new NotImplementedException();
        }
        public void SendReservationConfirmation(string email, Reservation reservation)
        {
            string subject = "Reservation Confirmation";
            string body = $"Dear {reservation.CustomerId},\n\nYour reservation for {reservation.VehicleId} is confirmed.\n\nDetails:\nReservation ID: {reservation.ReservationId}\nDate: {reservation.StartDate.ToShortDateString()}\n\nThank you!";

            notificationService.SendEmail(email, subject, body);
        }
    }
}
