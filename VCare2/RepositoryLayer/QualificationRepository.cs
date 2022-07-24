using Microsoft.EntityFrameworkCore;
using VCare2.DatabaseLayer;
using VCare2.DatabaseLayer.Models;

namespace VCare2.RepositoryLayer
{
    public class QualificationRepository
    {
        private readonly CareHomeContext _context;

        public QualificationRepository(CareHomeContext context)
        {
            _context = context;
        }

        public async Task<List<Qualification>> Index()
        {
            var QualificationsList = _context.Qualifications;
            return await QualificationsList.ToListAsync();
        }

        // GET: Qualifications/Details/5
        public Qualification Details(int? id)
        {
            if (id == null || _context.Qualifications == null)
            {
                return null;
            }

            var Qualification = _context.Qualifications
                .SingleOrDefault(m => m.QualificationsId == id);
            if (Qualification == null)
            {
                return null;
            }

            return Qualification;
        }

        public async Task<Qualification> Create(Qualification Qualification)
        {
            _context.Add(Qualification);
            await _context.SaveChangesAsync();

            return Qualification;
        }


        public async Task<Qualification> Edit(int? id)
        {
            if (id == null || _context.Qualifications == null)
            {
                return null;
            }

            var Qualification = await _context.Qualifications.FindAsync(id);
            if (Qualification == null)
            {
                return null;
            }
            return Qualification;
        }

        public async Task<Qualification> Edit(int id, Qualification Qualification)
        {
            if (id != Qualification.QualificationsId)
            {
                return null;
            }

            try
            {
                _context.Update(Qualification);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QualificationExists(Qualification.QualificationsId))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
            return Qualification;
        }

        public Qualification Delete(int? id)
        {
            if (id == null || _context.Qualifications == null)
            {
                return null;
            }

            var Qualification = _context.Qualifications.SingleOrDefault(j => j.QualificationsId == id);

            if (Qualification == null)
            {
                return null;
            }


            return Qualification;
        }

        public async Task<bool> DeleteConfirmed(int id)
        {
            if (_context.Qualifications == null)
            {
                return false;
            }
            var Qualification = await _context.Qualifications.FindAsync(id);
            if (Qualification != null)
            {
                _context.Qualifications.Remove(Qualification);
            }

            await _context.SaveChangesAsync();
            return true;
        }

        private bool QualificationExists(int id)
        {
            return (_context.Qualifications?.Any(e => e.QualificationsId == id)).GetValueOrDefault();
        }
    }
}
