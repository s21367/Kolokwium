using System;

namespace KolokwiumPoprawa.Models.DTO
{
    public record NewMember
    {
        public string Name { get; set; }
        
        public string Surname { get; set; }

        public DateTime MembershipDate { get; set; }
    }
}
