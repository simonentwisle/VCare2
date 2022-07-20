using System.Collections;
using VCare2.DatabaseLayer.Models;
using VCare2.Models.ViewModels;

namespace VCare2.Models.ViewModels
{
    public class staffQualificationsViewData: staff
    {
        public IEnumerable<StaffQualification>? staffQualifications { get; set; }
        public staff? staffMember { get; set; }
    }
}
