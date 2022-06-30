using System.Collections.Generic;

namespace KolokwiumPoprawa.Models.DTO
{
    public record NewTeam
    {
        public string Name { get; set; }
        
        public string Description { get; set; }

        public string OrganizationName { get; set; }

        public ICollection<NewMember> Members { get; set; }
    }
}
