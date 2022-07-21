using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VCare2.SharedFunctions;

namespace VCare2.DatabaseLayer.Models
{
    public partial class Job
    {
        public Job()
        {
            staff = new HashSet<staff>();
        }

        [Column("JobTitleId")]
        public int JobTitleId { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$", ErrorMessage = "Job Title can only contain letters")]
        [Column("JobTitle")]
        [Display(Name = "Job Title")]
        public string? JobTitle { get; set; }




        [NotMapped]
        [Column("DateModified")]
        public DateTime? DateModified { get; set; } 
        [NotMapped]
        [Column("DateCreated")]
        public DateTime? DateCreated { get; set; } 

        public virtual ICollection<staff?> staff { get; set; }
    }
}
