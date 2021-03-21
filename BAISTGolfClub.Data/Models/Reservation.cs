using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BAISTGolfClub.Data.Models
{
    public partial class Reservation
    {
        public Reservation()
        {
            Score = new HashSet<Score>();
        }

        public Guid ReservationId { get; set; }
        public Guid UserId { get; set; }
        public Guid? StandingReservationId { get; set; }
        public long ResevationNumber { get; set; }
        public int NumberOfPlayers { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public string Notes { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset CreatedDateTime { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTimeOffset? LastModifiedDateTime { get; set; }
        public bool CartRequired { get; set; }

        public virtual StandingReservation StandingReservation { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Score> Score { get; set; }
    }
}
