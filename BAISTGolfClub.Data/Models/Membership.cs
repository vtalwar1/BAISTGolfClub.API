using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BAISTGolfClub.Data.Models
{
    public partial class Membership
    {
        public Membership()
        {
            User = new HashSet<User>();
        }

        public long MembershipNumber { get; set; }
        public string MembershipType { get; set; }
        public bool Active { get; set; }

        public virtual ICollection<User> User { get; set; }
    }
}
