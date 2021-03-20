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
    public class ScoreService : IScoreService
    {
        private readonly BAISTGolfClubContext _context;

        public ScoreService(BAISTGolfClubContext context)
        {
            this._context = context;
        }
        public async Task<bool> SubmitScore(Score scoreData)
        {
            try
            {
                Reservation reservation = null;
                if (scoreData.Reservation.ResevationNumber != 0)
                {
                    reservation = await _context.Reservation.Where(x => x.ResevationNumber == long.Parse(scoreData.Reservation.ResevationNumber.ToString())).FirstOrDefaultAsync();
                }

                Score score = new Score()
                {
                    ScoreId = Guid.NewGuid(),
                    ReservationId = reservation?.ReservationId,
                    Date = scoreData.Date,
                    UserId = scoreData.User.UserId,
                    TotalScore = scoreData.TotalScore
                };

                await _context.Score.AddAsync(score);
                await _context.SaveChangesAsync();
                return false;
            } catch (Exception ex)
            {
                return false;
            }
        }
    }
}
