using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WeightTracker.Data;
using WeightTracker.Models;

namespace WeightTracker.Controllers
{
    public class PersonController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PersonController(ApplicationDbContext context)
        {
            _context = context;
        }


        // GET: Person/Create
        public IActionResult Create()
        {
            var person = new Person();
            ViewBag.AbteilungNameList =
                new SelectList(_context.Abteilung, "Id", "Name", _context.Abteilung.FirstOrDefault().Name);
            ViewBag.ProjectNameList =
                new SelectList(_context.Project, "Id", "Name", _context.Project.FirstOrDefault().Name);
            return View(person);
        }

        // POST: Person/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Person person)
        {
            person.Project = _context.Project.FirstOrDefault(x => x.Id == person.Project.Id);
            person.Abteilung = _context.Abteilung.FirstOrDefault(x => x.Id == person.Abteilung.Id);
            if (person.Project != null || person.Abteilung != null)
            {
                person.ProjectId = person.Project.Id;
                person.AbteilungId = person.Abteilung.Id;
            }

            if (ModelState.IsValid)
            {
                _context.Add(person);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }

            return View(person);
        }

        // GET: Person/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var person = _context.Person.Include(x => x.Abteilung).Include(x => x.Project)
                .SingleOrDefault(x => x.Id == id);
            if (person == null)
                return NotFound();
            ViewBag.AbteilungNameList = new SelectList(_context.Abteilung, "Id", "Name", person.Abteilung.Name);
            ViewBag.ProjectNameList = new SelectList(_context.Project, "Id", "Name", person.Project.Name);

            return View(person);
        }

        // POST: Person/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Person person)
        {
            if (id != person.Id)
            {
                return NotFound();
            }

            person.Project = _context.Project.FirstOrDefault(x => x.Id == person.Project.Id);
            person.Abteilung = _context.Abteilung.FirstOrDefault(x => x.Id == person.Abteilung.Id);
            if (person.Project != null || person.Abteilung != null)
            {
                person.ProjectId = person.Project.Id;
                person.AbteilungId = person.Abteilung.Id;
            }
            else
            {
                return View();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //_context.Entry(person).State = EntityState.Detached;
                    //var tmpPerson = _context.Person.Include(x => x.Abteilung).Include(x => x.Project).FirstOrDefault(g => g.Id == id);
                    //if(tmpPerson == null)
                    //    return RedirectToAction("Index", "Home");
                    //_context.Entry(tmpPerson).CurrentValues.SetValues(person);
                    _context.Update(person);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(person.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        // GET: Person/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Person.Include(x => x.Abteilung).Include(x => x.Project)
                .SingleOrDefaultAsync(x => x.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: Person/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var person = await _context.Person.FindAsync(id);
            _context.Person.Remove(person);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }

        private bool PersonExists(int id)
        {
            return _context.Person.Any(e => e.Id == id);
        }

        //// GET: Person
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Person.ToListAsync());
        //}

        //// GET: Person/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var person = await _context.Person
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (person == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(person);
        //}
    }
}