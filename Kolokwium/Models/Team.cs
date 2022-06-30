using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KolokwiumPoprawa.Models
{
    public record Team
    {
        public int TeamID { get; set; }

        [StringLength(50)]
        public string TeamName { get; set; }

        [StringLength(500)]
        public string TeamDescription { get; set; }
        
        public int OrganizationID { get; set; }
        
        public virtual Organization Organization { get; set; }

        public virtual ICollection<File> Files { get; set; }
        
        public virtual ICollection<Membership> Memberships { get; set; }
    }
}
