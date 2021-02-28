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

        public async Task<ReservationDTO> GetReservationById(Guid reservationId)
        {
            var reservation = await _context.Reservation.Where(x=> x.ReservationId == reservationId)
                                                        .Include(x=> x.StandingReservation)
                                                        .Include(x => x.User)
                                                        .ThenInclude(y => y.Membership).FirstOrDefaultAsync();
            if(reservation != null)
            {
                ReservationDTO reservationDTO = new ReservationDTO()
                {
                    ReservationId = reservation.ReservationId,
                    UserId = reservation.UserId,
                    StartDate = ConvertToMountainTime(reservation.StartDate),
                    EndDate = ConvertToMountainTime(reservation.EndDate),
                    CreatedBy = reservation.CreatedBy,
                    CreatedDateTime = ConvertToMountainTime(reservation.CreatedDateTime),
                    LastModifiedBy = reservation.LastModifiedBy,
                    LastModifiedDateTime = reservation.LastModifiedDateTime,
                    Notes = reservation.Notes,
                    NumberOfPlayers = reservation.NumberOfPlayers,
                    StandingReservationId = reservation.StandingReservationId,
                    ResevationNumber = reservation.ResevationNumber,
                    ReservationType = reservation.StandingReservation == null ? "O" : "S",
                    IsApproved = reservation.StandingReservation == null ? false : reservation.StandingReservation.IsApproved,
                    User = reservation.User 
                };

                return reservationDTO;
            } else
            {
                throw new Exception("Reservation not found");
            }
        }

        public async Task<bool> UpdateReservation(Guid reservationId, ReservationDTO reservationDTO)
        {
            if(reservationId != reservationDTO.ReservationId)
                throw new Exception("Invaild Reservation Id");

            var existingReservation = await _context.Reservation.FindAsync(reservationId);

            if(existingReservation == null)
                throw new Exception("Reservation does not exists.");
            else
            {
                existingReservation.StartDate = reservationDTO.StartDate;
                existingReservation.EndDate = reservationDTO.EndDate;
                existingReservation.NumberOfPlayers = reservationDTO.NumberOfPlayers;
                existingReservation.Notes = reservationDTO.Notes;
                existingReservation.LastModifiedBy = reservationDTO.LastModifiedBy;
                existingReservation.LastModifiedDateTime = DateTime.UtcNow;

                _context.Entry(existingReservation).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return true;
            }
        }

        public async Task<List<StandingReservation>> GetAllStandingReservationsForApproval(bool activeOnly)
        {
            List<StandingReservation> standingReservations;
            if (activeOnly)
            {
                standingReservations = await this._context.StandingReservation.Where(x => x.IsApproved == false && x.StartDate >= DateTime.UtcNow).Include(x => x.User).ThenInclude(y => y.Membership).ToListAsync();
                
            }
            else
            {
                standingReservations = await this._context.StandingReservation.ToListAsync();
            }


            foreach (var standingReservation in standingReservations)
            {
                standingReservation.StartDate = ConvertToMountainTime(standingReservation.StartDate);
                standingReservation.EndDate = ConvertToMountainTime(standingReservation.EndDate);
                standingReservation.CreatedDateTime = ConvertToMountainTime(standingReservation.CreatedDateTime);
            }

            return standingReservations;
        }

        public async Task<bool> ApproveStandingReservation(StandingReservation reservationData)
        {
            
            var standingReservation = await _context.StandingReservation.Where(x => x.StandingReservationId == reservationData.StandingReservationId)
                                                                        .Include(x => x.User)
                                                                        .FirstOrDefaultAsync();
            if (standingReservation == null)
                throw new Exception("Standing reservation not found");

            var approvedByUser = await _context.User.FindAsync(reservationData.ApprovedBy);
            
            if (approvedByUser == null)
                throw new Exception("User not found");
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {


                    List<DateTimeOffset> reservationStartDates = new List<DateTimeOffset>();

                    reservationStartDates.Add(standingReservation.StartDate);
                    reservationStartDates.Add(standingReservation.StartDate.AddDays(7));
                    reservationStartDates.Add(standingReservation.StartDate.AddDays(14));
                    reservationStartDates.Add(standingReservation.StartDate.AddDays(21));
                    reservationStartDates.Add(standingReservation.EndDate);

                    foreach (var reservationStartDate in reservationStartDates)
                    {
                        Reservation newReservation = new Reservation()
                        {
                            NumberOfPlayers = 1,
                            Notes = null,
                            CreatedBy = approvedByUser.Email,
                            CreatedDateTime = DateTime.UtcNow,
                            StartDate = reservationStartDate,
                            EndDate = reservationStartDate.AddHours(2),
                            UserId = standingReservation.UserId,
                            StandingReservationId = standingReservation.StandingReservationId,
                            ReservationId = Guid.NewGuid(),
                        };

                        await _context.Reservation.AddAsync(newReservation);
                    }

                    standingReservation.IsApproved = true;
                    standingReservation.ApprovedBy = approvedByUser.UserId;
                    _context.Entry(standingReservation).State = EntityState.Modified;
                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();
                    return true;
                } 
                catch(Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw ex;
                }
            }
        }
    }
}
