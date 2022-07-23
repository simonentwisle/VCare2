using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VCare2.DatabaseLayer.Models
{
    public partial class Qualification
    {
        public Qualification()
        {
            StaffQualifications = new HashSet<StaffQualification>();
        }

        [Required]
        [Column("QualificationsId")]
        [Display(Name = "QualificationsId")]
        public int QualificationsId { get; set; }

        [Required]
        [Column("QualificationType")]
        [Display(Name = "QualificationType")]
        public string QualificationType { get; set; } = null!;

        public virtual ICollection<StaffQualification> StaffQualifications { get; set; }
    }
}
