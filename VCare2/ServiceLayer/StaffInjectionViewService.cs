using System.Collections;
using VCare2.DatabaseLayer;
using VCare2.DatabaseLayer.Models;

namespace VCare2.ServiceLayer
{
    public interface IStaffInjectionViewServicece
    {
        IEnumerable<StaffQualification> List();
        int id { get; set; }    
    }

    public class StaffInjectionViewService : IStaffInjectionViewServicece
    {
        private readonly CareHomeContext _context;

        public StaffInjectionViewService(CareHomeContext careHomeContext)
        {
            _context = careHomeContext;
        }

        public int id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public IEnumerable<StaffQualification> List(int id)
        {
            List<StaffQualification> list = new List<StaffQualification>();

            //READ UP IList

            IList<StaffQualification> staffQualificationsList = new List<StaffQualification>();
            //IList<Qualification> qualificationsList = new List<Qualification>();
            IEnumerable<StaffQualification?> staffQualifications = _context.StaffQualifications.Where(sq => sq.StaffId == id).ToList();
            IEnumerable<Qualification?> qualifications = _context.Qualifications.ToList();

            //IEnumerable<StaffQualification> staffQualifications = (IList<StaffQualification>)_context.StaffQualifications
            //    .Join(_context.Qualifications, sq => sq.QualificationTypeId, q => q.QualificationsId, (sq, q) => new { sq, q })
            //    .Where(sqq => sqq.sq.StaffId == 3)
            //    .Select(m => new {  QualificationType  =  m.q.QualificationType,
            //                        Grade = m.sq.Grade,
            //                        }).ToList();

            foreach (var item in qualifications)
            {
                if (item != null)
                {
                    var staffid = item.StaffQualifications.Select(x => x.StaffId).SingleOrDefault();

                    if (staffid == id)
                    {
                        staffQualificationsList.Add(item.StaffQualifications.SingleOrDefault());
                    }

                }
            }

            return (IEnumerable<StaffQualification>)staffQualificationsList;
        }

        public IEnumerable<StaffQualification> List()
        {
            throw new NotImplementedException();
        }
    }
}
    

