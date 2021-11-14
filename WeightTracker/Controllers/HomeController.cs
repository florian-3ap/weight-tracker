using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WeightTracker.Data;
using WeightTracker.Models;

namespace WeightTracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var persons = _context.Person.ToList();
            var weightTracking = _context.WeightTracking.ToList();

            var personTracking = new Dictionary<Person, List<WeightTracking>>();
            persons.ForEach(person =>
            {
                personTracking.Add(person, weightTracking.FindAll(tracking => tracking.PersonId == person.Id));
            });

            return View(new WeightTrackingViewModel
            {
                PersonTracking = personTracking
            });
        }

        public IActionResult Persons()
        {
            return View(_context.Person.ToList());
        }

        public IActionResult Statistics()
        {
            return View(new StatisticsViewModel());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}