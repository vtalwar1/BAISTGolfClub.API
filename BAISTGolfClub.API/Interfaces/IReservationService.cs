using BAISTGolfClub.Data.DTO;
using BAISTGolfClub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BAISTGolfClub.API.Interfaces
{
    public interface IReservationService
    {
        Task<List<Reservation>> GetAllReservations(bool activeOnly);
        Task<bool> CreateReservation(ReservationDTO reservationData);
        Task<List<Reservation>> GetAllReservationsByUserType(Guid isStaff);
    }
}
