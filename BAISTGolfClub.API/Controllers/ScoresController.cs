using BAISTGolfClub.API.Interfaces;
using BAISTGolfClub.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BAISTGolfClub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoresController : ControllerBase
    {
        private readonly IScoreService _scoreService;
        public ScoresController(IScoreService scoreService)
        {
            this._scoreService = scoreService;
        }

        [HttpPost]
        public async Task<ActionResult<bool>> Post([FromBody] Score scoreDate)
        {
            try
            {
                var reservations = await this._scoreService.SubmitScore(scoreDate);

                return reservations;
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }


    }
}
