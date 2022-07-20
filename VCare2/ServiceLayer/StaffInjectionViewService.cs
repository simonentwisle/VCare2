using System.Collections;
using VCare2.DatabaseLayer;
using VCare2.DatabaseLayer.Models;

namespace VCare2.ServiceLayer
{
    public interface IStaffInjectionViewServicece
    {
        IEnumerable<StaffQualification> List();
    }

    public class StaffInjectionViewService : IStaffInjectionViewServicece
    {
        private readonly CareHomeContext _context;

        public StaffInjectionViewService(CareHomeContext careHomeContext)
        {
            _context = careHomeContext;
        } 
        public IEnumerable<StaffQualification> List()
        {
            List<StaffQualification> list = new List<StaffQualification>();

            //get staff qualifications list and qualifications list separetely the merge the two
            //so the return type is List<StaffQualification> 

            IList resultList = new List<StaffQualification>();
            IEnumerable<StaffQualification> staffQualifications = (IList<StaffQualification>)_context.StaffQualifications.Where(sq => sq.StaffId == 3).ToList();

            //IEnumerable<StaffQualification> staffQualifications = (IList<StaffQualification>)_context.StaffQualifications
            //    .Join(_context.Qualifications, sq => sq.QualificationTypeId, q => q.QualificationsId, (sq, q) => new { sq, q })
            //    .Where(sqq => sqq.sq.StaffId == 3)
            //    .Select(m => new {  QualificationType  =  m.q.QualificationType,
            //                        Grade = m.sq.Grade,
            //                        }).ToList();

            //.Select(m => new
            // {
            //     ProdId = m.ppc.p.Id, // or m.ppc.pc.ProdId
            //     CatId = m.c.CatId
            //      other assignments
            // });
            foreach (var item in staffQualifications)
            {
                resultList.Add(new StaffQualification { Grade = item.Grade, QualificationType = item.QualificationType ,AttainmentDate = item.AttainmentDate});
            }


            //var test = _context.StaffQualifications
            //    .Join(_context.Qualifications, sq => sq.QualificationTypeId, q => q.QualificationsId, (sq, q) => new { sq, q })
            //    .Where(sqq => sqq.sq.StaffId == 3)
            //    .Select(c => c)
            //    .ToList();

            //foreach (var item in test)
            //{
            //    list.Add(item);
            //}

        //    public IEnumerable<State> List()
        //    {
        //        return new List<State>
        //{
        //    new State { Abbreviation = "AK", Name = "Alaska" },
        //    new State { Abbreviation = "AL", Name = "Alabama" }
        //};
        //    }

            return (IEnumerable<StaffQualification>)resultList;
        }

    }
}
    

