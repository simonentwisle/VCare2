using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VCare2.DatabaseLayer;
using VCare2.DatabaseLayer.Models;
using VCare2.ServiceLayer;

namespace VCare2.Controllers
{
    public class StaffQualificationsController : Controller
    {
        private readonly StaffQualificationService _service;
        private readonly CareHomeContext _context;

        public StaffQualificationsController(CareHomeContext context,StaffQualificationService staffQualificationService)
        {
            _context = context;
            _service = staffQualificationService;
        }

        // GET: StaffQualifications
        public async Task<IActionResult> Index()
        {
            return View(await _service.Index());
        }

        // GET: StaffQualifications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.StaffQualifications == null)
            {
                return NotFound();
            }

            var staffQualification = await _context.StaffQualifications
                .Include(s => s.QualificationType)
                .Include(s => s.Staff)
                .FirstOrDefaultAsync(m => m.StaffQualificationId == id);

            if (staffQualification == null)
            {
                return NotFound();
            }

            return View(staffQualification);
        }

        // GET: StaffQualifications/Create
        public IActionResult Create()
        {
            PopulateQualificationDropDownList();
            PopulateStaffDropDownList();
            return View();
        }

        // POST: StaffQualifications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StaffQualificationId,StaffId,QualificationTypeId,Grade,AttainmentDate,DateModified,DateCreated")] StaffQualification staffQualification)
        {
            staffQualification.DateCreated = DateTime.Now;
            staffQualification.DateModified = DateTime.Now;

            ModelState.Remove("Staff");
            ModelState.Clear();

            if (ModelState.IsValid)
            {
                _service.Create(staffQualification);

                return RedirectToAction(nameof(Index));
            }

            PopulateQualificationDropDownList();
            PopulateStaffDropDownList();
            return View(staffQualification);
        }

        // GET: StaffQualifications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.StaffQualifications == null)
            {
                return NotFound();
            }

            StaffQualification staffQualification = GetStaffQualification(id);
            staffQualification.FullName = GetFullName(staffQualification);
         
            if (staffQualification == null)
            {
                return NotFound();
            }

            PopulateQualificationDropDownList();

            ViewData["StaffId"] = new SelectList(_context.staff, "StaffId", "StaffId", staffQualification.StaffId);
            return View(staffQualification);
        }

        private StaffQualification GetStaffQualification(int? id)
        {
            return _service.Edit(id);
        }

        private string? GetFullName(StaffQualification staffQualification)
        {
            return _context.staff.Where(stf => stf.StaffId == staffQualification.StaffId).Select(s => s.FullName).SingleOrDefault().ToString();
        }


        // POST: StaffQualifications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StaffQualificationId,StaffId,QualificationTypeId,Grade,AttainmentDate,DateModified,DateCreated,Staff,QualificationType")] StaffQualification staffQualification)
        {
            if (id != staffQualification.StaffQualificationId)
            {
                return NotFound();
            }

            ModelState.Remove("Staff");
            ModelState.Clear();

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.Edit(staffQualification);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StaffQualificationExists(staffQualification.StaffQualificationId))
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
            ViewData["QualificationTypeId"] = new SelectList(_context.Qualifications, "QualificationsId", "QualificationsId", staffQualification.QualificationTypeId);
            ViewData["StaffId"] = new SelectList(_context.staff, "StaffId", "StaffId", staffQualification.StaffId);
            return View(staffQualification);
        }

        // GET: StaffQualifications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.StaffQualifications == null)
            {
                return NotFound();
            }

            StaffQualification staffQualification = _service.Delete(id);

            if (staffQualification == null)
            {
                return NotFound();
            }

            return View(staffQualification);
        }

        // POST: StaffQualifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.StaffQualifications == null)
            {
                return Problem("Entity set 'CareHomeContext.StaffQualifications'  is null.");
            }

            bool success = await _service.DeleteConfirmed(id);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StaffQualificationExists(int id)
        {
          return (_context.StaffQualifications?.Any(e => e.StaffQualificationId == id)).GetValueOrDefault();
        }

        private void PopulateQualificationDropDownList(object? selectedItemId = null)
        {
            var qualifications = _context.Qualifications;
            ViewData["QualificationsId"] = new SelectList(qualifications, "QualificationsId", "QualificationType", selectedItemId);
        }

        private void PopulateStaffDropDownList(object? selectedItemId = null)
        {
            var staff = _context.staff;
            if (selectedItemId == null)
            {
                ViewData["StaffId"] = new SelectList(_context.staff, "StaffId", "FullName");
            }
            else
            {
                ViewData["StaffId"] = new SelectList(_context.staff, "StaffId", "Fullname", selectedItemId);
            }
            
        }


    }
}
