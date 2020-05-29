using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InsideAirbnbApp.Models;

namespace InsideAirbnbApp.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly AirbnbContext _context;

        public ReviewsController(AirbnbContext context)
        {
            _context = context;
        }

        // GET: Reviews
        public async Task<IActionResult> Index()
        {
            var airbnbContext = _context.Reviews.Take(100).Include(r => r.Listing);
            return View(await airbnbContext.ToListAsync());
        }

        // GET: Reviews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reviews = await _context.Reviews
                .Include(r => r.Listing)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reviews == null)
            {
                return NotFound();
            }

            return View(reviews);
        }
    }
}
