using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BAISTGolfClub.Data.Models
{
    public partial class Score
    {
        public Guid ScoreId { get; set; }
        public Guid UserId { get; set; }
        public Guid? ReservationId { get; set; }
        public DateTimeOffset Date { get; set; }
        public decimal TotalScore { get; set; }
        public decimal? Handicap { get; set; }

        public virtual Reservation Reservation { get; set; }
        public virtual User User { get; set; }
    }
}
