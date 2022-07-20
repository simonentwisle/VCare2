using VCare2.DatabaseLayer;

namespace VCare2.ServiceLayer
{

    public class StatisticsService
    {
        private readonly CareHomeContext _context;

        public StatisticsService(CareHomeContext careHomeContext)
        {
            _context = careHomeContext;
        }

        public int GetCount()
        {
            return 1;
        }

        public int GetCompletedCount()
        {
            return 10;
        }

        public double GetAveragePriority()
        {
            return 2000;
        }
    }
}
