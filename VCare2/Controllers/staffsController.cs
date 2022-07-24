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
using VCare2.ServiceLayer;

namespace VCare2.Controllers
{
    public class staffsController : Controller
    {
        private readonly StaffService _service;
        private readonly CareHomeContext _context;

        public staffsController(CareHomeContext context, StaffService staffService )
        {
            _context = context;
            _service = staffService;
        }

        // GET: staffs
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _service.Index());
        }

        // GET: staffs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.staff == null)
            {
                return NotFound();
            }

            var staff = _service.Details(id);
            ViewData["StaffID"] = id;

            UpdateCareHomeName(staff);
            UpdateJobTitle(staff);

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
                await _service.Create(staff);
                return RedirectToAction(nameof(Index));
            }

            PopulateJobsDropDownList(staff.JobTitleId);
            PopulateCareHomesDropDownList(staff.CareHomeId);
            return View(staff);
        }

        // GET: staffs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.staff == null)
            {
                return NotFound();
            }

            staff staff = _service.Edit(id).Result;

            if (staff == null)
            {
                return NotFound();
            }

            PopulateJobsDropDownList(staff.JobTitleId);
            PopulateCareHomesDropDownList(staff.CareHomeId);

            return View(staff);
        }

        // POST: staffs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StaffId,Forename,Surname,Dob,Salary,JobTitleId,CareHomeId,CareHome,CareHomeName,JobTitle,JobName")] staff staff)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _service.Update(staff);
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
            PopulateJobsDropDownList(staff.JobTitleId);
            PopulateCareHomesDropDownList(staff.CareHomeId);
            return View(staff);
        }

        // GET: staffs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.staff == null)
            {
                return NotFound();
            }

            var staff = _service.Delete(id).Result;

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

            bool success = await _service.DeleteConfirmed(id);
 
            return RedirectToAction(nameof(Index));
        }

        private bool staffExists(int id)
        {
          return (_context.staff?.Any(e => e.StaffId == id)).GetValueOrDefault();
        }

        internal staff UpdateCareHomeName(staff staff)
        {
            staff.CareHomeName =  _context.Locations.Where(t => t.CareHomeId == staff.CareHomeId).Select(t => t.Name).Single();
            return staff;
        }

        internal staff UpdateJobTitle(staff? staff)
        {
            staff.JobTitle = _context.Jobs.Where(t => t.JobTitleId == staff.JobTitleId).FirstOrDefault();
            staff.JobName = staff.JobTitle.JobTitle;
            return staff;
        }

        private void PopulateJobsDropDownList(object? selectedItemId = null)
        {
            var jobs = _context.Jobs;
            ViewBag.JobTitleId = new SelectList(jobs, "JobTitleId", "JobTitle", selectedItemId);  
        }

        private void PopulateCareHomesDropDownList(object? selectedItemId = null)
        {
            var locations = _context.Locations;
            ViewBag.CareHomeId = new SelectList(locations, "CareHomeId", "Name", selectedItemId);
        }
    }
}
