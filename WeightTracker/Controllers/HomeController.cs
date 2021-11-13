using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IActionResult> Index()
        {
            return View(await _context.Person.Include(x => x.Abteilung).Include(x => x.Project).ToListAsync());
        }

        public IActionResult MasterData()
        {
            var model = new MasterDataViewModel()
            {
                AbteilungList = _context.Abteilung.ToList(),
                ProjectList = _context.Project.ToList()
            };
            return View(model);
        }

        public IActionResult Statistics()
        {
            var personList = _context.Person.Include(x => x.Abteilung).Include(x => x.Project).ToList();
            var statisticList = new StatisticsViewModel();

            var abteilungsList = from pers in personList
                group pers.AbteilungId by pers.Abteilung.Name
                into newGroup
                select new
                {
                    Field = newGroup.Key,
                    Ncount = newGroup.Count()
                };

            foreach (var item in abteilungsList)
            {
                var statistic = new StatisticsViewModel()
                {
                    Table = "Abteilung",
                    Theme = item.Field,
                    Count = item.Ncount
                };
                statisticList.StatisticList.Add(statistic);
            }

            var projectLst = from proj in personList
                group proj.ProjectId by proj.Project.Name
                into newGroup
                select new
                {
                    Field = newGroup.Key,
                    Ncount = newGroup.Count()
                };

            foreach (var item in projectLst)
            {
                var statistic = new StatisticsViewModel()
                {
                    Table = "Projekt",
                    Theme = item.Field,
                    Count = item.Ncount
                };
                statisticList.StatisticList.Add(statistic);
            }

            return View(statisticList);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}