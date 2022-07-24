using Microsoft.EntityFrameworkCore;
using VCare2.DatabaseLayer;
using VCare2.DatabaseLayer.Models;

namespace VCare2.RepositoryLayer
{
    public class LocationRepository
    {
        private readonly CareHomeContext _context;

        public LocationRepository(CareHomeContext context)
        {
            _context = context;
        }

        public async Task<List<Location>> Index()
        {
            var locationsList = _context.Locations;
            return await locationsList.ToListAsync();
        }

        // GET: Locations/Details/5
        public Location Details(int? id)
        {
            if (id == null || _context.Locations == null)
            {
                return null;
            }

            var Location = _context.Locations
                .SingleOrDefault(m => m.CareHomeId == id);
            if (Location == null)
            {
                return null;
            }

            return Location;
        }

        public async Task<Location> Create(Location Location)
        {
            _context.Add(Location);
            await _context.SaveChangesAsync();

            return Location;
        }


        public async Task<Location> Edit(int? id)
        {
            if (id == null || _context.Locations == null)
            {
                return null;
            }

            var Location = await _context.Locations.FindAsync(id);
            if (Location == null)
            {
                return null;
            }
            return Location;
        }

        public async Task<Location> Edit(int id, Location Location)
        {
            if (id != Location.CareHomeId)
            {
                return null;
            }

            try
            {
                _context.Update(Location);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocationExists(Location.CareHomeId))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
            return Location;
        }

        public Location Delete(int? id)
        {
            if (id == null || _context.Locations == null)
            {
                return null;
            }

            var Location = _context.Locations.SingleOrDefault(j => j.CareHomeId == id);

            if (Location == null)
            {
                return null;
            }


            return Location;
        }

        public async Task<bool> DeleteConfirmed(int id)
        {
            if (_context.Locations == null)
            {
                return false;
            }
            var Location = await _context.Locations.FindAsync(id);
            if (Location != null)
            {
                _context.Locations.Remove(Location);
            }

            await _context.SaveChangesAsync();
            return true;
        }

        private bool LocationExists(int id)
        {
            return (_context.Locations?.Any(e => e.CareHomeId == id)).GetValueOrDefault();
        }
    }
}

