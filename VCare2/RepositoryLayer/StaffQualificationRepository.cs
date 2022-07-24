using Microsoft.EntityFrameworkCore;
using System.Web.Mvc;
using VCare2.DatabaseLayer;
using VCare2.DatabaseLayer.Models;

namespace VCare2.RepositoryLayer
{
    public class StaffQualificationRepository
    {
        private readonly CareHomeContext _context;

        public StaffQualificationRepository(CareHomeContext context)
        {
            _context = context;
        }

        public async Task<List<StaffQualification>> Index()
        {
            var careHomeContext = _context.StaffQualifications.Include(s => s.QualificationType).Include(s => s.Staff);
            return await careHomeContext.ToListAsync();
        }

        public StaffQualification? Details(int? id)
        {
            if (id == null || _context.StaffQualifications == null)
            {
                return null;
            }

            StaffQualification? staffQualification = _context.StaffQualifications
                .Include(s => s.QualificationType)
                .Include(s => s.Staff)
                .SingleOrDefault(m => m.StaffQualificationId == id);


            if (staffQualification == null)
            {
                return null;
            }

            return staffQualification;
        }

        public virtual async Task<StaffQualification?> Create(StaffQualification newStaffQualification)
        {
            try
            {
                using (_context)
                {
                    _context.StaffQualifications.Add(newStaffQualification);
                    await _context.SaveChangesAsync();
                }
            }
            catch
            {
                return null;
            }

            return newStaffQualification;
        }

        public async Task<StaffQualification?> Edit(int? id)
        {
            if (id == null || _context.staff == null)
            {
                return null;
            }

            StaffQualification? staffQualification = await _context.StaffQualifications.FindAsync(id);

            if (staffQualification == null)
            {
                return null;
            }

            return staffQualification;
        }

        public async Task<StaffQualification?> Edit(StaffQualification staffQualification )
        {
            try
            {
                _context.Update(staffQualification);
                await _context.SaveChangesAsync();
                return staffQualification;
            }
            catch
            {
                {
                    return null;
                }
            }

        }

        public async Task<StaffQualification?>  Delete(int? id)
        {
            if (id == null || _context.StaffQualifications == null)
            {
                return null;
            }

            var staffQualification = await _context.StaffQualifications
                .Include(s => s.QualificationType)
                .Include(s => s.Staff)
                .SingleOrDefaultAsync(m => m.StaffQualificationId == id);

            if (staffQualification == null)
            {
                return null;
            }
                return staffQualification;
        }

        public async Task<bool> DeleteConfirmed(int id)
        {
            if (_context.StaffQualifications == null)
            {
                return false; 
            }

            var staffQualification = await _context.StaffQualifications.FindAsync(id);
            if (staffQualification != null)
            {
                _context.StaffQualifications.Remove(staffQualification);
            }

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
