using System;
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
            const int numberOfDays = 7;

            var persons = _context.Person.ToList().OrderBy(person => person.Id).ToList();
            if (persons.Count == 0)
                return View(new StatisticsViewModel());

            var weightTracking = _context.WeightTracking.ToList()
                .FindAll(tracking => tracking.Date >= DateTime.Now.AddDays(-numberOfDays))
                .OrderByDescending(tracking => tracking.Date);

            var statisticListHeader = new List<object> {"Datum"}
                .Concat(persons.Select(person => person.Name).ToList())
                .ToList();
            var statisticList = new List<List<object>> {statisticListHeader};

            var date = DateTime.Now;
            while (date > DateTime.Now.AddDays(-7))
            {
                var weightTrackings = persons.Select(person =>
                        weightTracking.FirstOrDefault(tracking =>
                            tracking.PersonId == person.Id && tracking.Date.Date == date.Date))
                    .Select(tracking => tracking == null ? (object) null : tracking.Weight)
                    .ToList();

                var enumerable =
                    new List<Object> {date.Date.ToString("dd.MM.yyyy")}
                        .Concat(weightTrackings)
                        .ToList();

                statisticList.Add(enumerable);
                date = date.AddDays(-1);
            }

            return View(new StatisticsViewModel {StatisticList = statisticList});
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}