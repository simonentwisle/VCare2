using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VCare2.DatabaseLayer;
using VCare2.DatabaseLayer.Models;
using VCare2.Models.ViewModels;

namespace VCare2.Controllers
{
    public class staffsController : Controller
    {
        private readonly CareHomeContext _context;

        public staffsController(CareHomeContext context)
        {
            _context = context;
        }

        // GET: staffs
        public async Task<IActionResult> Index()
        {
            var careHomeContext = _context.staff.Include(s => s.CareHome).Include(s => s.JobTitle);
            return View(await careHomeContext.ToListAsync());
        }

        // GET: staffs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.staff == null)
            {
                return NotFound();
            }

            var staff = await _context.staff.FirstOrDefaultAsync(m => m.StaffId == id);

            ViewData["StaffID"] = id;

            //var staffQualificationsViewData = new staffQualificationsViewData();

            //if (staffQualificationsViewData == null) return NotFound();

            //staffQualificationsViewData.staffMember = staff;

            //staff.CareHomeName = _context.Locations.Where(t => t.CareHomeId == staff.CareHomeId).Select(t => t.Name).Single();

            //staff.JobName = _context.Jobs.Where(t => t.JobTitleId == staff.JobTitleId).Select(t => t.JobTitle).Single();

            ////staff.Qualifications = _context.StaffQualifications.Where(q => q.StaffId == staff.StaffId).Select(s => s);

            //var staffQualList = _context.StaffQualifications
            //    .Join(_context.Qualifications, sq => sq.QualificationTypeId, q => q.QualificationsId, (sq, q) => new { sq, q })
            //    .Where(sqq => sqq.sq.StaffId == id)
            //    .Select(c => c).ToList();

            ////staffQualificationsViewData.StaffQualifications = staffQualList.ToList<StaffQualification>();

            //ViewData["StaffQualifications"] = _context.StaffQualifications
            //    .Join(_context.Qualifications, sq => sq.QualificationTypeId, q => q.QualificationsId, (sq, q) => new { sq, q })
            //    .Where(sqq => sqq.sq.StaffId == id)
            //    .Select(c => c);

            //if (staffQualificationsViewData == null) return NotFound();



            //var test = await _context.staff
            //    .Join(_context.StaffQualifications, s => s.StaffId, sq => sq.StaffId, (s, sq) => new { s, sq })
            //   .Join(_context.Qualifications, s => s.sq.QualificationTypeId, q => q.QualificationsId, (s, q) => new { s, q })
            //   .Select(m => new {
            //       QualificationType = m.q.QualificationType,
            //       CatId = m.s.s.Forename
            //       // other assignments
            //   });

            return View(staff);
        }


        // GET: staffs/Create
        public IActionResult Create()
        {
            PopulateJobsDropDownList();
            PopulateCareHomesDropDownList();

            return View();
        }

        // POST: staffs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StaffId,Forename,Surname,Dob,Salary,JobTitleId,CareHomeId")] staff staff)
        {
            if (ModelState.IsValid)
            {
                _context.Add(staff);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            PopulateJobsDropDownList();
            PopulateCareHomesDropDownList();
            return View(staff);
        }

        // GET: staffs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.staff == null)
            {
                return NotFound();
            }

            var staff = await _context.staff.FindAsync(id);
            if (staff == null)
            {
                return NotFound();
            }
            PopulateJobsDropDownList();
            PopulateCareHomesDropDownList();

            return View(staff);
        }

        // POST: staffs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StaffId,Forename,Surname,Dob,Salary,JobTitleId,CareHomeId")] staff staff)
        {
            if (id != staff.StaffId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(staff);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!staffExists(staff.StaffId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            PopulateJobsDropDownList();
            PopulateCareHomesDropDownList();

            return View(staff);
        }

        // GET: staffs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.staff == null)
            {
                return NotFound();
            }

            var staff = await _context.staff
                .Include(s => s.CareHome)
                .Include(s => s.JobTitle)
                .FirstOrDefaultAsync(m => m.StaffId == id);
            if (staff == null)
            {
                return NotFound();
            }

            return View(staff);
        }

        // POST: staffs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.staff == null)
            {
                return Problem("Entity set 'CareHomeContext.staff'  is null.");
            }
            var staff = await _context.staff.FindAsync(id);
            if (staff != null)
            {
                _context.staff.Remove(staff);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool staffExists(int id)
        {
          return (_context.staff?.Any(e => e.StaffId == id)).GetValueOrDefault();
        }

        public IEnumerable<SelectListItem> GetJobs()
        {
            List<SelectListItem> jobTitles = _context.Jobs.AsNoTracking()
                .OrderBy(n => n.JobTitle)
                    .Select(n =>
                    new SelectListItem
                    {
                        Value = n.JobTitleId.ToString(),
                        Text = n.JobTitle
                    }).ToList();

            return new SelectList(jobTitles, "Value", "Text", 1);
        }

        public IEnumerable<SelectListItem> GetHomes()
        {
            List<SelectListItem> careHomes = _context.Locations.AsNoTracking()
                .OrderBy(n => n.CareHomeId)
                    .Select(n =>
                    new SelectListItem
                    {
                        Value = n.CareHomeId.ToString(),
                        Text = n.Name
                    }).ToList();

            return new SelectList(careHomes, "Value", "Text", 1);
        }

        private void PopulateJobsDropDownList(object selectedDepartment = null)
        {
            var jobs = _context.Jobs;
            ViewBag.JobTitleId = new SelectList(jobs, "JobTitleId", "JobTitle", selectedDepartment);  
        }

        private void PopulateCareHomesDropDownList(object selectedDepartment = null)
        {
            var locations = _context.Locations;
            ViewBag.CareHomeId = new SelectList(locations, "CareHomeId", "Name", selectedDepartment);
        }
    }
}
