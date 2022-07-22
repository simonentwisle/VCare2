using System.Collections;
using VCare2.DatabaseLayer.Models;
using VCare2.Models.ViewModels;

namespace VCare2.Models.ViewModels
{
    public class StaffQualificationsViewData
    {
        public int StaffQualificationId { get; set; }
        public int QualificationTypeId { get; set; }
        public string Grade { get; set; } = null!;
        public DateTime AttainmentDate { get; set; }
    }
}
