using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VCare2.DatabaseLayer.Models
{
    public partial class Location
    {
        public Location()
        {
            staff = new HashSet<staff>();
        }

        public int CareHomeId { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters.")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$", ErrorMessage = "Name can only contain letters")]
        [Column("Name")]
        [Display(Name = "Name")]
        public string? Name { get; set; }

        [Required]
        [Column("DateModified")]
        [Display(Name = "DateModified")]
        public DateTime DateModified { get; set; }

        [Required]
        [Column("DateCreated")]
        [Display(Name = "DateCreated")]
        public DateTime DateCreated { get; set; }

        public virtual ICollection<staff> staff { get; set; }
    }
}
