using System;
using System.Collections.Generic;

namespace VCare2.DatabaseLayer.Models
{
    public partial class Qualification
    {
        public Qualification()
        {
            StaffQualifications = new HashSet<StaffQualification>();
        }

        public int QualificationsId { get; set; }
        public string QualificationType { get; set; } = null!;

        public virtual ICollection<StaffQualification> StaffQualifications { get; set; }
    }
}
