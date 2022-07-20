using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace VCare2.DatabaseLayer.Models
{
    public partial class staff
    {
        public staff()
        {
            StaffQualifications = new HashSet<StaffQualification>();
        }

        public int StaffId { get; set; }
        public string? Forename { get; set; }
        public string? Surname { get; set; }
        public DateTime? Dob { get; set; }
        public decimal? Salary { get; set; }
        public int? JobTitleId { get; set; }
        public int? CareHomeId { get; set; }

        public virtual Location? CareHome { get; set; }
        public virtual Job? JobTitle { get; set; }
        public virtual ICollection<StaffQualification> StaffQualifications { get; set; }

        [NotMapped]
        public string? CareHomeName { get; set; }
        [NotMapped]
        public string? JobName { get; set; }
        [NotMapped]
        public class State
        {
            public string Abbreviation { get; set; }
            public string Name { get; set; }
        }
    }
}
