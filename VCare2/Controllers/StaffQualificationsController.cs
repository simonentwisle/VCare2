﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VCare2.DatabaseLayer;
using VCare2.DatabaseLayer.Models;

namespace VCare2.Controllers
{
    public class StaffQualificationsController : Controller
    {
        private readonly CareHomeContext _context;

        public StaffQualificationsController(CareHomeContext context)
        {
            _context = context;
        }

        // GET: StaffQualifications
        public async Task<IActionResult> Index()
        {
            var careHomeContext = _context.StaffQualifications.Include(s => s.QualificationType).Include(s => s.Staff);
            return View(await careHomeContext.ToListAsync());
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
                _context.Add(staffQualification);
                await _context.SaveChangesAsync();
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

            var staffQualification = await _context.StaffQualifications
               .Include(s => s.QualificationType)
               .Include(s => s.Staff)
               .FirstOrDefaultAsync(m => m.StaffQualificationId == id);

            if (staffQualification == null)
            {
                return NotFound();
            }

            PopulateQualificationDropDownList();

            ViewData["StaffId"] = new SelectList(_context.staff, "StaffId", "StaffId", staffQualification.StaffId);
            return View(staffQualification);
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
                    _context.Update(staffQualification);
                    await _context.SaveChangesAsync();
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

        // POST: StaffQualifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.StaffQualifications == null)
            {
                return Problem("Entity set 'CareHomeContext.StaffQualifications'  is null.");
            }
            var staffQualification = await _context.StaffQualifications.FindAsync(id);
            if (staffQualification != null)
            {
                _context.StaffQualifications.Remove(staffQualification);
            }
            
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

        //private async Task<staff> SetStaffMember(StaffQualification staffQualification)
        //{
        //    if (staffQualification == null)
        //    {
        //        return new staff();
        //    }

        //    var staff = await _context.staff.FirstOrDefaultAsync(m => m.StaffId == staffQualification.StaffId);
        //    ViewData["staffmember"] = staff;
        //    return staff;
        //}

    }
}
