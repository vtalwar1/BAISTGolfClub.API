using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BAISTGolfClub.Data.Models
{
    public partial class Reservation
    {
        public Guid ReservationId { get; set; }
        public Guid UserId { get; set; }
        public Guid? StandingReservationId { get; set; }
        public long ResevationNumber { get; set; }
        public int NumberOfPlayers { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Notes { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDateTime { get; set; }

        public virtual StandingReservation StandingReservation { get; set; }
        public virtual User User { get; set; }
    }
}
