using VCare2.DatabaseLayer.Models;
using VCare2.RepositoryLayer;

namespace VCare2.ServiceLayer
{
    public class StaffService
    {
        private readonly StaffRepository _stafRepository;

        public StaffService(StaffRepository staffRepository)
        {
            _stafRepository = staffRepository;
        }

        public async Task<List<staff>?> Index()
        {
            try
            {
                return await _stafRepository.Index();
            }
            catch (Exception exception)
            {
                return null;
            }
        }

        public async Task<staff?>  Details(int? id)
        {
            try
            {
                return await _stafRepository.Details(id);
            }
            catch (Exception exception)
            {
                return null;
            }
        }

        public async Task<staff?> Create(staff staffMember)
        {
            try
            {
                return await _stafRepository.Create(staffMember);
            }
            catch (Exception exception)
            {
                return null; 
            }
        }

        //Get
        public async Task<staff?> Edit(int? id)
        {
            try
            {
                return await _stafRepository.Edit(id);
            }

            catch (Exception exception)
            {
                return null; 
            }
        }

        //post
        public async Task<staff> Update(staff staffMember)
        {
            return await _stafRepository.Update(staffMember);
        }

        //get
        public async Task<staff> Delete(int? id)
        {
            return await _stafRepository.Delete(id);
        }

        //post
        public async Task<bool> DeleteConfirmed(int id)
        {
            return await _stafRepository.DeleteConfirmed(id);
        }
    }
}
