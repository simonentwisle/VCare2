using System;
using System.Collections.Generic;

namespace VCare2.DatabaseLayer.Models
{
    public partial class Job
    {
        public Job()
        {
            staff = new HashSet<staff>();
        }

        public int JobTitleId { get; set; }
        public string? JobTitle { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateCreated { get; set; }

        public virtual ICollection<staff> staff { get; set; }
    }
}
