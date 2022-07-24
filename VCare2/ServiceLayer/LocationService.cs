using VCare2.DatabaseLayer.Models;
using VCare2.RepositoryLayer;

namespace VCare2.ServiceLayer
{
    public class LocationService
    {
        private readonly LocationRepository _locationRepository;

        public LocationService(LocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public async Task<List<Location>?> Index()
        {
            try
            {
                return await _locationRepository.Index();
            }
            catch (Exception exception)
            {
                return null;
            }
        }

        public async Task<Location?> Details(int? id)
        {
            try
            {
                return await _locationRepository.Details(id);
            }
            catch (Exception exception)
            {
                return null;
            }
        }

        public async Task<Location?> Create(Location LocationMember)
        {
            try
            {
                return await _locationRepository.Create(LocationMember);
            }
            catch (Exception exception)
            {
                return null; 
            }
        }

        //Get
        public async Task<Location?> Edit(int? id)
        {
            try
            {
                return await _locationRepository.Edit(id);
            }

            catch (Exception exception)
            {
                return null; 
            }
        }

        //post
        public async Task<Location?> Update(Location Location, int id)
        {
            return await _locationRepository.Edit(id, Location);
        }

        //get
        public async Task<Location?> Delete(int? id)
        {
            return await _locationRepository.Delete(id);
        }

        //post
        public async Task<bool> DeleteConfirmed(int id)
        {
            return await _locationRepository.DeleteConfirmed(id);
        }
    }
}

