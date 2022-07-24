using VCare2.DatabaseLayer.Models;
using VCare2.RepositoryLayer;

namespace VCare2.ServiceLayer
{
    public class JobTitleService
    {
        private readonly JobTitleRepository _jobTitleRepository;

        public JobTitleService(JobTitleRepository jobTitleRepository)
        {
            _jobTitleRepository = jobTitleRepository;
        }

        public async Task<List<Job>> Index()
        {
            try
            {
                return await _jobTitleRepository.Index();
            }
            catch (Exception exception)
            {
                return null;
            }
        }

        public Job Details(int? id)
        {
            try
            {
                return _jobTitleRepository.Details(id);
            }
            catch (Exception exception)
            {
                return null;
            }
        }

        public async Task<Job> Create(Job JobMember)
        {
            try
            {
                return await _jobTitleRepository.Create(JobMember);
            }
            catch (Exception exception)
            {
                return null; //false;   ;
            }
        }

        //Get
        public async Task<Job> Edit(int? id)
        {
            try
            {
                return await _jobTitleRepository.Edit(id);
            }

            catch (Exception exception)
            {
                return null; //false;   ;
            }
        }

        //post
        public async Task<Job> Update(Job job, int id)
        {
            return await _jobTitleRepository.Update(id,job);
        }

        //get
        public Job Delete(int? id)
        {
            return  _jobTitleRepository.Delete(id);
        }

        //post
        public async Task<bool> DeleteConfirmed(int id)
        {
            return await _jobTitleRepository.DeleteConfirmed(id);
        }
    }
}
