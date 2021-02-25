using BAISTGolfClub.API.Interfaces;
using BAISTGolfClub.Data.DBContext;
using BAISTGolfClub.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BAISTGolfClub.API.Services
{
    public class ReservationService : IReservationService
    {
        private readonly BAISTGolfClubContext _context;

        public ReservationService(BAISTGolfClubContext context)
        {
            this._context = context;
        }

        public async Task<List<Reservation>> GetAllReservations()
        {
           return await this._context.Reservation.ToListAsync();
        }
    }
}
