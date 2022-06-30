using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KolokwiumPoprawa.Models
{
    public record Member
    {
        public int MemberID { get; set; }

        [StringLength(20)]
        public string MemberName { get; set; }

        [StringLength(50)]
        public string MemberSurname { get; set; }

        [StringLength(20)]
        public string MemberNickName { get; set; } = null!;

        public int OrganizationID { get; set; }

        public virtual Organization Organization { get; set; }

        public virtual ICollection<Membership> Memberships { get; set; }
    }
}
