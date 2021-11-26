using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Create(int? id)
        {
            if (id == null) return NotFound();

            var person = _context.Person.FirstOrDefault(x => x.Id == id);
            if (person == null) return NotFound();

            return View(new WeightTracking
            {
                Date = DateTime.Today,
                PersonId = person.Id,
                Person = person
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, WeightTracking tracking)
        {
            tracking.Person = _context.Person.FirstOrDefault(x => x.Id == id);
            if (tracking.Person == null) return NotFound();

            tracking.Id = 0;
            tracking.PersonId = tracking.Person.Id;

            if (!ModelState.IsValid) return View(tracking);

            var sameDateTracking =
                _context.WeightTracking.FirstOrDefault(x => (x.PersonId == id && x.Date == tracking.Date));
            if (sameDateTracking != null)
            {
                ModelState.AddModelError("Date", "Datum wurde schon verwendet.");
                return View(tracking);
            }


            _context.Add(tracking);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();

            var tracking = _context.WeightTracking.SingleOrDefault(x => x.Id == id);
            if (tracking == null) return NotFound();

            tracking.Person = _context.Person.SingleOrDefault(person => person.Id == tracking.PersonId);

            return View(tracking);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, WeightTracking tracking)
        {
            if (id != tracking.Id) return NotFound();

            if (!ModelState.IsValid) return View(tracking);

            try
            {
                _context.Entry(tracking).State = EntityState.Detached;
                var tempTracking = _context.WeightTracking.FirstOrDefault(x => x.Id == id);
                if (tempTracking == null)
                    return RedirectToAction("Index", "Home");

                tracking.PersonId = tempTracking.PersonId;
                tracking.Person = tempTracking.Person;

                _context.Entry(tempTracking).CurrentValues.SetValues(tracking);
                _context.Update(tempTracking);
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

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var tracking = await _context.WeightTracking.SingleOrDefaultAsync(x => x.Id == id);
            if (tracking == null) return NotFound();

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