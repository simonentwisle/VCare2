using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VCare2.DatabaseLayer.Models
{
    public partial class 
        staff
    {
        public staff()
        {
            StaffQualifications = new HashSet<StaffQualification>();
        }

        public int StaffId { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Forename cannot be longer than 50 characters.")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$", ErrorMessage ="Forname can only contain letters")]
        [Column("Forename")]
        [Display(Name = "Forename")]
        public string? Forename { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Surname cannot be longer than 50 characters.")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$", ErrorMessage = "Surname can only contain letters")]
        [Column("Surname")]
        [Display(Name = "Surname")]
        public string? Surname { get; set; }

        [Required]
        [Column("Dob")]
        [Display(Name = "DOB")]
        public DateTime? Dob { get; set; }

        [Required]
        [Column("Salary")]
        [Display(Name = "Salary")]
        public decimal? Salary { get; set; }

        [Required]
        [Column("JobTitleId")]
        [Display(Name = "JobTitleId")]
        public int? JobTitleId { get; set; }

        [Required]
        [Column("CareHomeId")]
        [Display(Name = "CareHomeId")]
        public int? CareHomeId { get; set; }


        [Column("CareHome")]
        [Display(Name = "Carehome")]
        public virtual Location? CareHome { get; set; }


        [Column("JobTitle")]
        [Display(Name = "Job Title")]
        public virtual Job? JobTitle { get; set; }

        public virtual ICollection<StaffQualification> StaffQualifications { get; set; }

        [NotMapped]
        public string? CareHomeName { get; set; } = string.Empty;
        [NotMapped]
        public string? JobName { get; set; } = string.Empty;

        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                return Forename + " " + Surname;
            }
        }
    }
}
