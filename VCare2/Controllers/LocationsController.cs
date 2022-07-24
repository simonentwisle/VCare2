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
    public class LocationsController : Controller
    {
        private readonly CareHomeContext _context;
        private readonly LocationService _service;

        public LocationsController(CareHomeContext context, LocationService locationService)
        {
            _context = context;
            _service = locationService;
        }

        // GET: Locations
        public async Task<IActionResult> Index()
        {
              return _context.Locations != null ? 
                          View(await _service.Index()) :
                          Problem("Entity set 'CareHomeContext.Locations'  is null.");
        }

        // GET: Locations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Locations == null)
            {
                return NotFound();
            }

            var location = _service.Details(id);
            if (location == null)
            {
                return NotFound();
            }

            return View(location);
        }

        // GET: Locations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Locations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CareHomeId,Name,DateModified,DateCreated")] Location location)
        {
            location.DateCreated = DateTime.Now;
            location.DateModified = DateTime.Now;

            if (ModelState.IsValid)
            {
                await _service.Create(location);
                return RedirectToAction(nameof(Index));
            }
            return View(location);
        }

        // GET: Locations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Locations == null)
            {
                return NotFound();
            }

            var location = await _service.Edit(id);
            if (location == null)
            {
                return NotFound();
            }
            return View(location);
        }

        // POST: Locations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CareHomeId,Name,DateModified,DateCreated")] Location location)
        {
            location.DateCreated = DateTime.Now;
            location.DateModified = DateTime.Now;

            if (id != location.CareHomeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.Update(location,id);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LocationExists(location.CareHomeId))
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
            return View(location);
        }

        // GET: Locations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Locations == null)
            {
                return NotFound();
            }

            var location = await _service.Delete(id);
            if (location == null)
            {
                return NotFound();
            }

            return View(location);
        }

        // POST: Locations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Locations == null)
            {
                return Problem("Entity set 'CareHomeContext.Locations'  is null.");
            }

            await _service.DeleteConfirmed(id);
            
            return RedirectToAction(nameof(Index));
        }

        private bool LocationExists(int id)
        {
          return (_context.Locations?.Any(e => e.CareHomeId == id)).GetValueOrDefault();
        }
    }
}
