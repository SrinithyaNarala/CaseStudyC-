using CarConnect.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnect.doa
{
    internal interface IReservationService
    {
        Reservation GetReservationById(int reservationId);
        List<Reservation> GetReservationsByCustomerId(int customerId);
        void CreateReservation(Reservation reservationData);
        void UpdateReservation(int reservationId,Reservation reservationData);
        void CancelReservation(int reservationId);
        List<Reservation> GetReservationByCustomerId(int customerId);
    }
}
