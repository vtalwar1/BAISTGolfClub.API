using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BAISTGolfClub.Data.Models
{
    public partial class StandingReservation
    {
        public StandingReservation()
        {
            Reservation = new HashSet<Reservation>();
        }

        public Guid StandingReservationId { get; set; }
        public Guid UserId { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public bool IsApproved { get; set; }
        public Guid? ApprovedBy { get; set; }
        public DateTimeOffset? ApprovedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset CreatedDateTime { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTimeOffset? LastModifiedDateTime { get; set; }
        public long StandingReservationNumber { get; set; }

        public virtual User ApprovedByNavigation { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Reservation> Reservation { get; set; }
    }
}
