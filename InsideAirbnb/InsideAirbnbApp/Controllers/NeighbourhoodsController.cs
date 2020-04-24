using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            var airbnbContext = _context.Neighbourhoods.Include(n => n.Listing);
            return View(await airbnbContext.ToListAsync());
        }

        // GET: Neighbourhoods/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var neighbourhoods = await _context.Neighbourhoods
                .Include(n => n.Listing)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (neighbourhoods == null)
            {
                return NotFound();
            }

            return View(neighbourhoods);
        }

        // GET: Neighbourhoods/Create
        public IActionResult Create()
        {
            ViewData["ListingId"] = new SelectList(_context.Listings, "Id", "Name");
            return View();
        }

        // POST: Neighbourhoods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ListingId,Date,Id")] Neighbourhoods neighbourhoods)
        {
            if (ModelState.IsValid)
            {
                _context.Add(neighbourhoods);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ListingId"] = new SelectList(_context.Listings, "Id", "Name", neighbourhoods.ListingId);
            return View(neighbourhoods);
        }

        // GET: Neighbourhoods/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var neighbourhoods = await _context.Neighbourhoods.FindAsync(id);
            if (neighbourhoods == null)
            {
                return NotFound();
            }
            ViewData["ListingId"] = new SelectList(_context.Listings, "Id", "Name", neighbourhoods.ListingId);
            return View(neighbourhoods);
        }

        // POST: Neighbourhoods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ListingId,Date,Id")] Neighbourhoods neighbourhoods)
        {
            if (id != neighbourhoods.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(neighbourhoods);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NeighbourhoodsExists(neighbourhoods.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ListingId"] = new SelectList(_context.Listings, "Id", "Name", neighbourhoods.ListingId);
            return View(neighbourhoods);
        }

        // GET: Neighbourhoods/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var neighbourhoods = await _context.Neighbourhoods
                .Include(n => n.Listing)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (neighbourhoods == null)
            {
                return NotFound();
            }

            return View(neighbourhoods);
        }

        // POST: Neighbourhoods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var neighbourhoods = await _context.Neighbourhoods.FindAsync(id);
            _context.Neighbourhoods.Remove(neighbourhoods);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NeighbourhoodsExists(int id)
        {
            return _context.Neighbourhoods.Any(e => e.Id == id);
        }
    }
}
