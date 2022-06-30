using System;

namespace KolokwiumPoprawa.Models
{
    public record Membership
    {
        public int MemberID { get; set; }
        
        public int TeamID { get; set; }
        
        public virtual Team Team { get; set; }

        public virtual Member Member { get; set; }

        public DateTime MembershipDate { get; set; }
    }
}
