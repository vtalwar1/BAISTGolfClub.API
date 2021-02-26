using BAISTGolfClub.API.Interfaces;
using BAISTGolfClub.Data.DTO;
using BAISTGolfClub.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BAISTGolfClub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        public ReservationsController(IReservationService reservationService)
        {
            this._reservationService = reservationService;
        }
        // GET: api/<ValuesController>
        [HttpGet("{activeOnly}")]
        public async Task<ActionResult<List<Reservation>>> Get(bool activeOnly)
        {
            try
            {
                var reservations = await this._reservationService.GetAllReservations(activeOnly);
                if (reservations == null)
                {
                    return NotFound("User does not exists.");
                }
                return reservations;
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("GetAllReservationsByUserType/{userId}")]
        public async Task<ActionResult<List<Reservation>>> GetAllReservationsByUserType(Guid userId)
        {
            try
            {
                var reservations = await this._reservationService.GetAllReservationsByUserType(userId);
               
                return reservations;
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        // POST api/<ValuesController>
        [HttpPost]
        public async Task<ActionResult<bool>> Post([FromBody] ReservationDTO reservationData)
        {
            try
            {
                var reservations = await this._reservationService.CreateReservation(reservationData);

                return reservations;
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        
    }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
