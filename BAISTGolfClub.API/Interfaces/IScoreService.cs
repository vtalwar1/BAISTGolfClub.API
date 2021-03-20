using BAISTGolfClub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BAISTGolfClub.API.Interfaces
{
    public interface IScoreService
    {
        Task<bool> SubmitScore(Score scoreData);
    }
}
