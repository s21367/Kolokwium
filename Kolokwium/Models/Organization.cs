using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KolokwiumPoprawa.Models
{
    public record Organization
    {
        public int OrganizationID { get; set; }

        [StringLength(100)]
        public string OrganizationName { get; set; }

        [StringLength(50)]
        public string OrganizationDomain { get; set; }
        
        public virtual ICollection<Team> Teams { get; set; }

        public virtual ICollection<Member> Members { get; set; }
    }
}
