using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InsideAirbnbApp.Models;

namespace InsideAirbnbApp.Controllers
{
    public class ListingsController : Controller
    {
        private readonly AirbnbContext _context;

        public ListingsController(AirbnbContext context)
        {
            _context = context;
        }

        // GET: Listings
        public async Task<IActionResult> Index()
        {
            return View(await _context.Listings.Take(100).ToListAsync());
        }

        // GET: Listings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listings = await _context.Listings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (listings == null)
            {
                return NotFound();
            }

            return View(listings);
        }
    }
}
