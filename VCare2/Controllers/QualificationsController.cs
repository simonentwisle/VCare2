using System;
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
    public class QualificationsController : Controller
    {
        private readonly CareHomeContext _context;

        public QualificationsController(CareHomeContext context)
        {
            _context = context;
        }

        // GET: Qualifications
        public async Task<IActionResult> Index()
        {
              return _context.Qualifications != null ? 
                          View(await _context.Qualifications.ToListAsync()) :
                          Problem("Entity set 'CareHomeContext.Qualifications'  is null.");
        }

        // GET: Qualifications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Qualifications == null)
            {
                return NotFound();
            }

            var qualification = await _context.Qualifications
                .FirstOrDefaultAsync(m => m.QualificationsId == id);
            if (qualification == null)
            {
                return NotFound();
            }

            return View(qualification);
        }

        // GET: Qualifications/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Qualifications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("QualificationsId,QualificationType")] Qualification qualification)
        {
            if (ModelState.IsValid)
            {
                _context.Add(qualification);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(qualification);
        }

        // GET: Qualifications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Qualifications == null)
            {
                return NotFound();
            }

            var qualification = await _context.Qualifications.FindAsync(id);
            if (qualification == null)
            {
                return NotFound();
            }
            return View(qualification);
        }

        // POST: Qualifications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("QualificationsId,QualificationType")] Qualification qualification)
        {
            if (id != qualification.QualificationsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(qualification);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QualificationExists(qualification.QualificationsId))
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
            return View(qualification);
        }

        // GET: Qualifications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Qualifications == null)
            {
                return NotFound();
            }

            var qualification = await _context.Qualifications
                .FirstOrDefaultAsync(m => m.QualificationsId == id);
            if (qualification == null)
            {
                return NotFound();
            }

            return View(qualification);
        }

        // POST: Qualifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Qualifications == null)
            {
                return Problem("Entity set 'CareHomeContext.Qualifications'  is null.");
            }
            var qualification = await _context.Qualifications.FindAsync(id);
            if (qualification != null)
            {
                _context.Qualifications.Remove(qualification);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QualificationExists(int id)
        {
          return (_context.Qualifications?.Any(e => e.QualificationsId == id)).GetValueOrDefault();
        }
    }
}
