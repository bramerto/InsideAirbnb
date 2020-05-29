using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InsideAirbnbApp.Models;

namespace InsideAirbnbApp.Controllers
{
    public class NeighbourhoodsController : Controller
    {
        private readonly AirbnbContext _context;

        public NeighbourhoodsController(AirbnbContext context)
        {
            _context = context;
        }

        // GET: Neighbourhoods
        public async Task<IActionResult> Index()
        {
            return View(await _context.Neighbourhoods.Take(100).ToListAsync());
        }

        // GET: Neighbourhoods/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var neighbourhoods = await _context.Neighbourhoods.FirstOrDefaultAsync(m => m.Neighbourhood == id);
            if (neighbourhoods == null)
            {
                return NotFound();
            }

            return View(neighbourhoods);
        }
    }
}
