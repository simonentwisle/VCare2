using VCare2.DatabaseLayer.Models;
using VCare2.RepositoryLayer;

namespace VCare2.ServiceLayer
{
    public class StaffQualificationService
    {
        private readonly StaffQualificationRepository _staffQualificationRepository;

        public StaffQualificationService(StaffQualificationRepository staffQualificationRepository)
        {
            _staffQualificationRepository = staffQualificationRepository;
        }

        public async Task<List<StaffQualification>> Index()
        {
            try
            {
                return await _staffQualificationRepository.Index();
            }
            catch (Exception exception)
            {
                return null;
            }
        }

        public StaffQualification Details(int? id)
        {
            try
            {
                return _staffQualificationRepository.Details(id);
            }
            catch (Exception exception)
            {
                return null;
            }
        }

        public async Task<StaffQualification> Create(StaffQualification staffQualification)
        {
            try
            {
                return await _staffQualificationRepository.Create(staffQualification);
            }
            catch (Exception exception)
            {
                return null; //false;   ;
            }
        }

        //Get
        public StaffQualification Edit(int? id)
        {
            try
            {
                return _staffQualificationRepository.Edit(id);
            }

            catch (Exception exception)
            {
                return null; //false;   ;
            }
        }

        //post
        public async Task<StaffQualification> Edit(StaffQualification staffQualification)
        {
            return await _staffQualificationRepository.Edit(staffQualification);
        }

        //get
        public StaffQualification Delete(int? id)
        {
            return _staffQualificationRepository.Delete(id);
        }

        //post
        public async Task<bool> DeleteConfirmed(int id)
        {
            return await _staffQualificationRepository.DeleteConfirmed(id);
        }
    }
}
