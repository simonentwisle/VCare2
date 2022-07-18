using System;
using System.Collections.Generic;

namespace VCare2.DatabaseLayer.Models
{
    public partial class Location
    {
        public Location()
        {
            staff = new HashSet<staff>();
        }

        public int CareHomeId { get; set; }
        public string? Name { get; set; }
        public DateTime DateModified { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual ICollection<staff> staff { get; set; }
    }
}
