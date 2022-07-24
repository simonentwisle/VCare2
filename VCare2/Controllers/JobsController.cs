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
    public class JobsController : Controller
    {
        private readonly CareHomeContext _context;
        private readonly JobTitleService _service;
        
        public JobsController(CareHomeContext context, JobTitleService staffService)
        {
            _context = context;
            _service = staffService;
        }

        // GET: Jobs
        public async Task<IActionResult> Index()
        {
            return _context.Jobs != null ?
                        View(await _service.Index()) :
                        Problem("Entity set 'CareHomeContext.Jobs'  is null.");
        }

        // GET: Jobs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Jobs == null)
            {
                return NotFound();
            }

            var job =  _service.Details(id);
            //var job = await _context.Jobs
            //    .FirstOrDefaultAsync(m => m.JobTitleId == id);
            if (job == null)
            {
                return NotFound();
            }

            return View(job);
        }

        // GET: Jobs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Jobs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("JobTitleId,JobTitle,DateModified,DateCreated")] Job job)
        {
            job.DateCreated = DateTime.Now;
            job.DateModified = DateTime.Now;

            if (ModelState.IsValid)
            {
                _service.Create(job);
                return RedirectToAction(nameof(Index));
            }
            return View(job);
        }

        // GET: Jobs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Jobs == null)
            {
                return NotFound();
            }

            var job = _service.Edit(id);
            //var job = await _context.Jobs.FindAsync(id);
            if (job == null)
            {
                return NotFound();
            }
            return View(job);
        }

        // POST: Jobs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("JobTitleId,JobTitle,DateModified,DateCreated")] Job job)
        {
            if (id != job.JobTitleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //_service.Update(job);
                    //_context.Update(job);
                    await _service.Update(job,id);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobExists(job.JobTitleId))
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
            return View(job);
        }

        // GET: Jobs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Jobs == null)
            {
                return NotFound();
            }

            var job = _service.Delete(id);
            //var job = await _context.Jobs
            //    .FirstOrDefaultAsync(m => m.JobTitleId == id);
            if (job == null)
            {
                return NotFound();
            }

            return View(job);
        }

        // POST: Jobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Jobs == null)
            {
                return Problem("Entity set 'CareHomeContext.Jobs'  is null.");
            }
            var job = _service.DeleteConfirmed(id);

            return RedirectToAction(nameof(Index));
        }

        private bool JobExists(int id)
        {
            return (_context.Jobs?.Any(e => e.JobTitleId == id)).GetValueOrDefault();
        }
    }
}
