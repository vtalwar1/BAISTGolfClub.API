using BAISTGolfClub.API.Interfaces;
using BAISTGolfClub.Data.DBContext;
using BAISTGolfClub.Data.DTO;
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

        public async Task<bool> CreateReservation(ReservationDTO reservationData)
        {
            if (reservationData.ReservationType == "S")
            {
                return await CreateStandingReservation(reservationData);
            }
            else
            {
                return await CreateOneTimeReservation(reservationData);
            }
        }

        public async Task<List<Reservation>> GetAllReservations(bool activeOnly)
        {
            List<Reservation> reservations;
            if (activeOnly)
            {
                reservations = await this._context.Reservation.Where(x => x.StartDate >= DateTime.UtcNow).ToListAsync();
            }
            else
            {
                reservations = await this._context.Reservation.ToListAsync();
            }


            foreach (var reservation in reservations)
            {
                reservation.StartDate = ConvertToMountainTime(reservation.StartDate);
                reservation.EndDate = ConvertToMountainTime(reservation.EndDate);
                reservation.CreatedDateTime = ConvertToMountainTime(reservation.CreatedDateTime);
            }

            return reservations;
        }

        private DateTimeOffset ConvertToMountainTime(DateTimeOffset utc)
        {
            return TimeZoneInfo.ConvertTimeBySystemTimeZoneId
                (utc, "Mountain Standard Time");
        }

        public async Task<bool> CreateOneTimeReservation(ReservationDTO reservationData)
        {
            Reservation reservation = new Reservation()
            {
                ReservationId = Guid.NewGuid(),
                UserId = reservationData.User.UserId,
                StartDate = reservationData.StartDate,
                EndDate = reservationData.EndDate,
                Notes = reservationData.Notes,
                NumberOfPlayers = reservationData.NumberOfPlayers,
                CreatedBy = reservationData.CreatedBy,
                CreatedDateTime = DateTime.UtcNow,
            };

            await _context.Reservation.AddAsync(reservation);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CreateStandingReservation(ReservationDTO reservationData)
        {
            StandingReservation standingReservation = new StandingReservation()
            {
                StandingReservationId = Guid.NewGuid(),
                UserId = reservationData.User.UserId,
                StartDate = reservationData.StartDate,
                EndDate = reservationData.EndDate,
                CreatedBy = reservationData.CreatedBy,
                ApprovedBy = null,
                IsApproved = false,
                CreatedDateTime = DateTime.UtcNow
            };

            await _context.StandingReservation.AddAsync(standingReservation);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Reservation>> GetAllReservationsByUserType(Guid userId)
        {
            var user = await _context.User.FindAsync(userId);
            if (user == null)
            {
                throw new Exception("User does not exists");
            }
            List<Reservation> reservations;
            if (user.IsStaff)
            {
                reservations = await this._context.Reservation.Include(x => x.User).ThenInclude(y => y.Membership).ToListAsync();
            }
            else
            {
                reservations = await this._context.Reservation.Where(x => x.UserId == userId).Include(x => x.User).ThenInclude(y => y.Membership).ToListAsync();
            }


            foreach (var reservation in reservations)
            {
                reservation.StartDate = ConvertToMountainTime(reservation.StartDate);
                reservation.EndDate = ConvertToMountainTime(reservation.EndDate);
                reservation.CreatedDateTime = ConvertToMountainTime(reservation.CreatedDateTime);
            }

            return reservations;
        }
    }
}
