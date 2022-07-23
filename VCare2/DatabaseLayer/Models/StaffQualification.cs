using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VCare2.DatabaseLayer.Models
{
    public partial class StaffQualification
    {
        
        public int StaffQualificationId { get; set; }

        [Required]
        [Column("StaffId")]
        [Display(Name = "StaffId")]
        public int StaffId { get; set; }

        [Required]
        [Column("QualificationTypeId")]
        [Display(Name = "QualificationTypeId")]
        public int QualificationTypeId { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$", ErrorMessage = "Grade can only contain letters")]
        [Column("Grade")]
        [Display(Name = "Grade")]
        public string Grade { get; set; } = null!;

        [Required]
        [Column("AttainmentDate")]
        [Display(Name = "AttainmentDate")]
        public DateTime AttainmentDate { get; set; }

        [Column("DateModified")]
        [Display(Name = "DateModified")]
        public DateTime DateModified { get; set; }

        [Column("DateCreated")]
        [Display(Name = "DateCreated")]
        [NotMapped]
        public DateTime DateCreated { get; set; }

        public virtual Qualification QualificationType { get; set; } = null!;
        public virtual staff Staff { get; set; } = null!;
    }
}
