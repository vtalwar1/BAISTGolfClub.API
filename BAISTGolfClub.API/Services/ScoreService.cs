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
                if (scoreData.Reservation!=null && scoreData.Reservation?.ResevationNumber != 0)
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
                return true;
            } catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<Score>> GetAllScoresByUserType(Guid userId)
        {
            var user = await _context.User.FindAsync(userId);
            if (user == null)
            {
                throw new Exception("User does not exists");
            }
            List<Score> scores;
            if (user.IsStaff)
            {
                scores = await this._context.Score.Include(x => x.Reservation).Include(x => x.User).ThenInclude(y => y.Membership).ToListAsync();
            }
            else
            {
                scores = await this._context.Score.Where(x => x.UserId == userId).Include(x => x.Reservation).Include(x => x.User).ThenInclude(y => y.Membership).ToListAsync();
            }


            foreach (var score in scores)
            {
                score.Date = ConvertToMountainTime(score.Date);
            }

            return scores;
        }

        private DateTimeOffset ConvertToMountainTime(DateTimeOffset utc)
        {
            return TimeZoneInfo.ConvertTimeBySystemTimeZoneId
                (utc, "Mountain Standard Time");
        }
    }
}
