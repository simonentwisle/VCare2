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

        // GET: staffs
        public async Task<List<staff>> Index()
        {
            var staffList = _context.staff.Include(s => s.CareHome).Include(s => s.JobTitle);
            return await staffList.ToListAsync();
        }

        // GET: staffs/Details/5
        public async Task<staff?> Details(int? id)
        {
            if (id == null || _context.staff == null)
            {
                return null;
            }

            staff? staff = await _context.staff.FirstOrDefaultAsync(m => m.StaffId == id);

            UpdateCareHomeName(staff);
            UpdateJobTitle(staff);

            return staff;
        }

        public virtual async Task<staff?> Create(staff newStaffMember)
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
        public async Task<staff?> Edit(int? id)
        {
            if (id == null || _context.staff == null)
            {
                return null;
            }

            staff? staffMember = await _context.staff.FindAsync(id);
            if (staffMember == null)
            {
                return null;
            }

            return staffMember;
        }

        // POST: StaffMembers/Edit/5
        public async Task<staff?> Update(staff staffMember)
        {
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
        public async Task<staff?> Delete(int? id)
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

        internal staff? UpdateCareHomeName(staff staff)
        {
            staff.CareHomeName = _context.Locations.Where(t => t.CareHomeId == staff.CareHomeId).Select(t => t.Name).Single();
            return staff;
        }

        internal staff UpdateJobTitle(staff staff)
        {
            staff.JobTitle = _context.Jobs.Where(t => t.JobTitleId == staff.JobTitleId).FirstOrDefault();
            staff.JobName = staff.JobTitle.JobTitle;
            return staff;
        }

        private void PopulateJobsDropDownList(object? selectedItemId = null)
        {
            var jobs = _context.Jobs;
            //ViewBag.JobTitleId = new SelectList(jobs, "JobTitleId", "JobTitle", selectedItemId);
        }

        private void PopulateCareHomesDropDownList(object? selectedItemId = null)
        {
            var locations = _context.Locations;
            //ViewBag.CareHomeId = new SelectList(locations, "CareHomeId", "Name", selectedItemId);
        }
    }
}
