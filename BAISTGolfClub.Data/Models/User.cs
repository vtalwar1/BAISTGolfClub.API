using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BAISTGolfClub.Data.Models
{
    public partial class User
    {
        public User()
        {
            Reservation = new HashSet<Reservation>();
            StandingReservationApprovedByNavigation = new HashSet<StandingReservation>();
            StandingReservationUser = new HashSet<StandingReservation>();
        }

        public Guid UserId { get; set; }
        public long MembershipNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }

        public virtual Membership Membership { get; set; }
        public virtual ICollection<Reservation> Reservation { get; set; }
        public virtual ICollection<StandingReservation> StandingReservationApprovedByNavigation { get; set; }
        public virtual ICollection<StandingReservation> StandingReservationUser { get; set; }
    }
}
