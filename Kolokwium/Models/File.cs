using System.ComponentModel.DataAnnotations;

namespace KolokwiumPoprawa.Models
{
    public record File
    {
        public int FileID { get; set; }
        
        public int TeamID { get; set; }

        [StringLength(100)]
        public string FileName { get; set; }

        [StringLength(4)]
        public string FileExtension { get; set; }

        public int FileSize { get; set; }

        public virtual Team Team { get; set; }
    }
}
