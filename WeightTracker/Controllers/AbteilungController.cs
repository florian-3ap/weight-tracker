using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeightTracker.Data;
using WeightTracker.Models;

namespace WeightTracker.Controllers
{
    public class AbteilungController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AbteilungController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Abteilung/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Abteilung/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Abteilung abteilung)
        {
            if (ModelState.IsValid)
            {
                _context.Add(abteilung);
                await _context.SaveChangesAsync();
                return RedirectToAction("MasterData", "Home");
            }

            return View(abteilung);
        }

        // GET: Abteilung/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var abteilung = await _context.Abteilung.FindAsync(id);
            if (abteilung == null)
            {
                return NotFound();
            }

            return View(abteilung);
        }

        // POST: Abteilung/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Abteilung abteilung)
        {
            if (id != abteilung.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(abteilung);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AbteilungExists(abteilung.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction("MasterData", "Home");
            }

            return View(abteilung);
        }

        // GET: Abteilung/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var abteilung = await _context.Abteilung
                .FirstOrDefaultAsync(m => m.Id == id);
            if (abteilung == null)
            {
                return NotFound();
            }

            return View(abteilung);
        }

        // POST: Abteilung/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var abteilung = await _context.Abteilung.FindAsync(id);
            _context.Abteilung.Remove(abteilung);
            await _context.SaveChangesAsync();
            return RedirectToAction("MasterData", "Home");
        }

        private bool AbteilungExists(int id)
        {
            return _context.Abteilung.Any(e => e.Id == id);
        }

        //// GET: Abteilung
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Abteilung.ToListAsync());
        //}

        //// GET: Abteilung/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var abteilung = await _context.Abteilung
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (abteilung == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(abteilung);
        //}
    }
}