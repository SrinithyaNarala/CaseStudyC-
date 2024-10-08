using CarConnect.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnect.dao
{
    internal interface IReservationRepository
    {
        Reservation GetReservationById(int reservationId);
        List<Reservation> GetReservationByCustomerId(int customerId);
        int CreateReservation(Reservation reservation);
        int UpdateReservation(int id, Reservation reservation);
        int CancelReservation(int reservationId);
    }
}
