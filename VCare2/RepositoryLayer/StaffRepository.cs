using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VCare2.DatabaseLayer;
using VCare2.DatabaseLayer.Models;
using VCare2.Models.ViewModels;

namespace VCare2.RepositoryLayer
{
    public class StaffRepository
    {
        private readonly CareHomeContext _context;

        public StaffRepository(CareHomeContext context)
        {
            _context = context;
        }

        // GET: StaffMembers/Details/5
        public async Task<staff> Details(int? id)
        {
            if (id == null || _context.staff == null)
            {
                return null;
            }

            var staffMember = await _context.staff
                .Include(s => s.CareHomeName)
                .Include(s => s.JobTitle)
                .FirstOrDefaultAsync(m => m.StaffId == id);

            if (staffMember == null)
            {
                return null;
            }

            return staffMember;

            if (id == null || _context.staff == null)
            {
                return null;
            }


            //var staff = await _context.staff.FirstOrDefaultAsync(m => m.StaffId == id);

            //var staffQualificationsViewData = new staffQualificationsViewData();

            //if (staffQualificationsViewData == null) return null;

            //staffQualificationsViewData.staffMember = staff;

            //staff.CareHomeName = _context.Locations.Where(t => t.CareHomeId == staff.CareHomeId).Select(t => t.Name).Single();

            //staff.JobName = _context.Jobs.Where(t => t.JobTitleId == staff.JobTitleId).Select(t => t.JobTitle).Single();

            ////staff.Qualifications = _context.StaffQualifications.Where(q => q.StaffId == staff.StaffId).Select(s => s);

            //var staffQualList = _context.StaffQualifications
            //    .Join(_context.Qualifications, sq => sq.QualificationTypeId, q => q.QualificationsId, (sq, q) => new { sq, q })
            //    .Where(sqq => sqq.sq.StaffId == id)
            //    .Select(c => c).ToList();

            //staffQualificationsViewData.StaffQualifications = staffQualList.ToList<StaffQualification>();

            //ViewData["StaffQualifications"] = _context.StaffQualifications
            //    .Join(_context.Qualifications, sq => sq.QualificationTypeId, q => q.QualificationsId, (sq, q) => new { sq, q })
            //    .Where(sqq => sqq.sq.StaffId == id)
            //    .Select(c => c);

            //if (staffQualificationsViewData == null) return null;



            ////var test = await _context.staff
            ////    .Join(_context.StaffQualifications, s => s.StaffId, sq => sq.StaffId, (s, sq) => new { s, sq })
            ////   .Join(_context.Qualifications, s => s.sq.QualificationTypeId, q => q.QualificationsId, (s, q) => new { s, q })
            ////   .Select(m => new {
            ////       QualificationType = m.q.QualificationType,
            ////       CatId = m.s.s.Forename
            ////       // other assignments
            ////   });

            //return staffQualificationsViewData;
        }

        public virtual async Task<staff> Create(staff newStaffMember)
        {
            try
            {
                using (_context)
                {
                    _context.staff.Add(newStaffMember);
                    await _context.SaveChangesAsync();
                }
            }
            catch
            {
                return null;
            }

            return newStaffMember;
        }

        // GET: StaffMembers/Edit/5
        public async Task<staff> Edit(int? id)
        {
            if (id == null || _context.staff == null)
            {
                return null;
            }

            var staffMember = await _context.staff.FindAsync(id);
            if (staffMember == null)
            {
                return null;
            }

            return staffMember;
        }

        // POST: StaffMembers/Edit/5
        public async Task<staff> Edit(int id, staff staffMember)
        {
            if (id != staffMember.StaffId)
            {
                return null;
            }


            try
            {
                _context.Update(staffMember);
                await _context.SaveChangesAsync();
                return staffMember;
            }
            catch
            {
                {
                    return null;
                }
            }

        }


        // GET: StaffMembers/Delete/5
        public async Task<staff> Delete(int? id)
        {
            if (id == null || _context.staff == null)
            {
                return null; //NOt found
            }

            var staffMember = await _context.staff
                .Include(s => s.CareHome)
                .Include(s => s.JobTitle)
                .FirstOrDefaultAsync(m => m.StaffId == id);

            if (staffMember == null)
            {
                return null;//NotFound();
            }

            return staffMember;
        }

        // POST: StaffMembers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<bool> DeleteConfirmed(int id)
        {
            if (_context.staff == null)
            {
                return false; // Problem("Entity set 'VoyageCareContext.StaffMembers'  is null.");
            }
            var staffMember = await _context.staff.FindAsync(id);
            if (staffMember != null)
            {
                _context.staff.Remove(staffMember);
            }

            await _context.SaveChangesAsync();
            return true;
        }

        private bool IsInvalidStaffMemberName(string name)
        {
            char[] forbiddenCharacters = { '!', '@', '#', '$', '%', '&', '*' };
            return string.IsNullOrEmpty(name) || name.Any(x => forbiddenCharacters.Contains(x));
        }

    }
}
