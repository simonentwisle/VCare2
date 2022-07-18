using System;
using System.Collections.Generic;

namespace VCare2.DatabaseLayer.Models
{
    public partial class StaffQualification
    {
        public int StaffQualificationId { get; set; }
        public int StaffId { get; set; }
        public int QualificationTypeId { get; set; }
        public string Grade { get; set; } = null!;
        public DateTime AttainmentDate { get; set; }
        public DateTime DateModified { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual Qualification QualificationType { get; set; } = null!;
        public virtual staff Staff { get; set; } = null!;
    }
}
