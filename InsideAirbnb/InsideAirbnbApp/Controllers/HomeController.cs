using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using InsideAirbnbApp.Models;

namespace InsideAirbnbApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly AirbnbContext _context;

        public HomeController(AirbnbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Listings.Take(100).ToList());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
