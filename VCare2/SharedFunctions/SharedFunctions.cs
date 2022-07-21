using VCare2.DatabaseLayer;
using VCare2.DatabaseLayer.Models;

namespace VCare2.SharedFunctions
{
    public class SharedFunctions
    {

        private readonly CareHomeContext _context;

        public SharedFunctions(CareHomeContext context)
        {
            _context = context;
        }

        internal string? GetJobTitle(int jobTitleId)
        {
            return "test job title";
        }
    }
}
