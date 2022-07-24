using Microsoft.EntityFrameworkCore;
using VCare2.DatabaseLayer;
using VCare2.DatabaseLayer.Models;

namespace VCare2.RepositoryLayer
{
    public class JobTitleRepository
    {
        private readonly CareHomeContext _context;

        public JobTitleRepository(CareHomeContext context)
        {
            _context = context;
        }

        public async Task<List<Job>> Index()
        {
            var jobsList = _context.Jobs;
            return await jobsList.ToListAsync();
        }

        // GET: Jobs/Details/5
        public async Task<Job?> Details(int? id)
        {
            if (id == null || _context.Jobs == null)
            {
                return null;
            }

            var job = await _context.Jobs.SingleOrDefaultAsync(m => m.JobTitleId == id);
            if (job == null)
            {
                return null;
            }

            return job;
        }

        public async Task<Job> Create(Job job)
        {
           _context.Add(job);
           await _context.SaveChangesAsync();

           return job;
        }


        public async Task<Job?> Edit(int? id)
        {
            if (id == null || _context.Jobs == null)
            {
                return null;
            }

            var job = await _context.Jobs.FindAsync(id);
            if (job == null)
            {
                return null;
            }
            return job;
        }

        public async Task<Job?> Update(int id, Job job)
        {
            if (id != job.JobTitleId)
            {
                return null;
            }

            try
            {
                _context.Update(job);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobExists(job.JobTitleId))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
            return job;
        }

        public Job? Delete(int? id)
        {
            if (id == null || _context.Jobs == null)
            {
                return null;
            }

            var job = _context.Jobs.SingleOrDefault(j => j.JobTitleId == id);

            if (job == null)
            {
                return null;
            }

            return job;
        }

        public async Task<bool> DeleteConfirmed(int id)
        {
            if (_context.Jobs == null)
            {
                return false;
            }
            var job = await _context.Jobs.FindAsync(id);
            if (job != null)
            {
                _context.Jobs.Remove(job);
            }

            await _context.SaveChangesAsync();
            return true;
        }

        private bool JobExists(int id)
        {
            return (_context.Jobs?.Any(e => e.JobTitleId == id)).GetValueOrDefault();
        }
    }
}
