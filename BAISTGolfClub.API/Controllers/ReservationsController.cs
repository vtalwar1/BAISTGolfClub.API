using BAISTGolfClub.API.Interfaces;
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
        [HttpGet]
        public async Task<ActionResult<List<Reservation>>> Get()
        {
            try
            {
                var reservations = await this._reservationService.GetAllReservations();
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

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
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
