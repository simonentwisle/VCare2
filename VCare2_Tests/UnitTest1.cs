using Microsoft.EntityFrameworkCore;
using VCare2.DatabaseLayer;
using VCare2.DatabaseLayer.Models;
using VCare2.RepositoryLayer;

namespace VCare2_Tests
{
    [TestClass]
    public class StaffMemberRepositoryTests
    {
        private CareHomeContext? _context;
        private StaffRepository? _repository;

        [TestInitialize]
        public async Task TestInitialize()
        {
            DbContextOptions<CareHomeContext> dbContextOptions = new DbContextOptionsBuilder<CareHomeContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            _context = new CareHomeContext(dbContextOptions);

            _context.Database.EnsureCreated();

            _repository = new StaffRepository(_context);
            Assert.IsNotNull(_repository);

            var staffMembers = new[]
            {
                 new staff
                {   StaffId = 1,
                   Forename = "Clarence",
                    Surname = "Beaks",
                    Dob =DateTime.Parse("1984-05-08 00:00:00"),
                    Salary = 32000,
                    JobTitleId = 1,
                    JobTitle = new Job() { JobTitleId = 1, JobTitle = "Carer" },
                    CareHomeId = 1,
                    CareHome = new Location { Name = "Birmingham" }
                },
                 new staff
                {
                    StaffId = 2,
                    Forename = "Jenny",
                    Surname = "Rik",
                    Dob =DateTime.Parse("2001-07-25 00:00:00"),
                    Salary = 25000,
                    JobTitleId = 2,
                    JobTitle = new Job() { JobTitleId = 2, JobTitle = "Nurse" },
                    CareHomeId = 2,
                    CareHome = new Location { Name = "Manchester" }

                }
            };

            _context.AddRange(staffMembers);
            await _context.SaveChangesAsync();
        }

        [TestMethod]
        public async Task? GetStaffMember_Failure()
        {
            var result = await _repository.Details(-1);
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task GetStaffMember_Success()
        {
            var result = await _repository.Details(2);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task CreateStaffMember_Success()
        {
            //var staffMember = _context.StaffMembers.Where(x => x.StaffMemberId == 2).FirstOrDefault();
            var newStaffMember = new staff
            {
                StaffId = 3,
                Forename = "Dennis",
                Surname = "Reynolds",
                Dob = DateTime.Parse("1977-05-08 00:00:00"),
                Salary = 4000,
                JobTitleId = 1,
                JobTitle = new Job() { JobTitleId =3, JobTitle = "Chef" },
                CareHomeId = 3,
                CareHome = new Location { Name = "Liverpool" }
            };

             var result = await _repository.Create(newStaffMember);
            Assert.AreEqual("Reynolds", newStaffMember.Surname);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task CreateStaffMember_Failure()
        {
            var newStaffMember = await _repository.Create(null);
            Assert.IsNull(newStaffMember);
        }
    }
}