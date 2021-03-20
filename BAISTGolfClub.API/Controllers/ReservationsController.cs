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
                    return NotFound("Reservations does not exists.");
                }
                return reservations;
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("GetAllStandingReservationsForApproval/{activeOnly}")]
        public async Task<ActionResult<List<StandingReservation>>> GetAllStandingReservationsForApproval(bool activeOnly)
        {
            try
            {
                var reservations = await this._reservationService.GetAllStandingReservationsForApproval(activeOnly);
                if (reservations == null)
                {
                    return NotFound("Reservations does not exists.");
                }
                return reservations;
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("GetReservationByReservationNumber/{reservationNumber}/{membershipNumberNumber}")]
        public async Task<ActionResult<Reservation>> GetReservationByReservationNumber(long reservationNumber, long membershipNumberNumber)
        {
            try
            {
                Reservation reservation = await this._reservationService.GetReservationByReservationNumber(reservationNumber, membershipNumberNumber);
       
                return reservation;
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

        [HttpGet("GetReservationById/{reservationId}")]
        public async Task<ActionResult<ReservationDTO>> GetReservationById(Guid reservationId)
        {
            try
            {
                var reservations = await this._reservationService.GetReservationById(reservationId);

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

        [HttpPost("ApproveStandingReservation")]
        public async Task<ActionResult<bool>> ApproveStandingReservation([FromBody] StandingReservation reservationData)
        {
            try
            {
                var isApproved = await this._reservationService.ApproveStandingReservation(reservationData);

                return isApproved;
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> Put(Guid id, [FromBody] ReservationDTO reservationDTO)
        {
            try
            {
                return await this._reservationService.UpdateReservation(id, reservationDTO);


            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
