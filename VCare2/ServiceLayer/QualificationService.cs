using VCare2.DatabaseLayer.Models;
using VCare2.RepositoryLayer;

namespace VCare2.ServiceLayer
{
    public class QualificationService
    {
        private readonly QualificationRepository _QualificationRepository;

        public QualificationService(QualificationRepository QualificationRepository)
        {
            _QualificationRepository = QualificationRepository;
        }

        public async Task<List<Qualification>> Index()
        {
            try
            {
                return await _QualificationRepository.Index();
            }
            catch (Exception exception)
            {
                return null;
            }
        }

        public async Task<Qualification> Details(int? id)
        {
            try
            {
                return await _QualificationRepository.Details(id);
            }
            catch (Exception exception)
            {
                return null;
            }
        }

        public async Task<Qualification> Create(Qualification QualificationMember)
        {
            try
            {
                return await _QualificationRepository.Create(QualificationMember);
            }
            catch (Exception exception)
            {
                return null; //false;   ;
            }
        }

        //Get
        public async Task<Qualification> Edit(int? id)
        {
            try
            {
                return await _QualificationRepository.Edit(id);
            }

            catch (Exception exception)
            {
                return null; //false;   ;
            }
        }

        //post
        public async Task<Qualification> Update(Qualification Qualification, int id)
        {
            return await _QualificationRepository.Edit(id, Qualification);
        }

        //get
        public Qualification Delete(int? id)
        {
            return _QualificationRepository.Delete(id);
        }

        //post
        public async Task<bool> DeleteConfirmed(int id)
        {
            return await _QualificationRepository.DeleteConfirmed(id);
        }
    }


}
