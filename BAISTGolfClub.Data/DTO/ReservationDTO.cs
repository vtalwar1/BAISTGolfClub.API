using BAISTGolfClub.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BAISTGolfClub.Data.DTO
{
    public class ReservationDTO
    {
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
        public bool IsApproved { get; set; }
        public Guid? ApprovedBy { get; set; }
        public long StandingReservationNumber { get; set; }
        public string ReservationType { get; set; }
        public virtual User User { get; set; }
    }
}
