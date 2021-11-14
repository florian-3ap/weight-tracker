using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WeightTracker.Data;
using WeightTracker.Models;

namespace WeightTracker.Controllers
{
    public class WeightTrackingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WeightTrackingController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Create()
        {
            ViewBag.ParsonNameList =
                new SelectList(_context.Person, "Id", "Name", _context.Person.FirstOrDefault().Name);
            return View(new WeightTracking
            {
                Date = DateTime.Today
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(WeightTracking tracking)
        {
            tracking.Person = _context.Person.FirstOrDefault(x => x.Id == tracking.Person.Id);
            if (tracking.Person != null)
            {
                tracking.PersonId = tracking.Person.Id;
            }

            if (ModelState.IsValid)
            {
                _context.Add(tracking);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }

            return View(tracking);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();

            var tracking = Queryable.SingleOrDefault(_context.WeightTracking, x => x.Id == id);
            if (tracking == null) return NotFound();

            return View(tracking);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, WeightTracking tracking)
        {
            if (id != tracking.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tracking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WeightTrackingExists(tracking.Id))
                        return NotFound();

                    throw;
                }

                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) 
                return NotFound();

            var tracking = await _context.WeightTracking.SingleOrDefaultAsync(x => x.Id == id);
            if (tracking == null) 
                return NotFound();

            return View(tracking);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tracking = await _context.WeightTracking.FindAsync(id);
            _context.WeightTracking.Remove(tracking);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }

        private bool WeightTrackingExists(int id)
        {
            return _context.WeightTracking.Any(e => e.Id == id);
        }
    }
}