using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InsideAirbnbApp.Models;

namespace InsideAirbnbApp.Controllers
{
    public class CalendarController : Controller
    {
        private readonly AirbnbContext _context;

        public CalendarController(AirbnbContext context)
        {
            _context = context;
        }

        // GET: Calendar
        public async Task<IActionResult> Index()
        {
            var airbnbContext = _context.Calendar.Include(c => c.Listing);
            return View(await airbnbContext.ToListAsync());
        }

        // GET: Calendar/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calendar = await _context.Calendar
                .Include(c => c.Listing)
                .FirstOrDefaultAsync(m => m.ListingId == id);
            if (calendar == null)
            {
                return NotFound();
            }

            return View(calendar);
        }

        // GET: Calendar/Create
        public IActionResult Create()
        {
            ViewData["ListingId"] = new SelectList(_context.Listings, "Id", "Name");
            return View();
        }

        // POST: Calendar/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ListingId,Date,Available,Price,AdjustedPrice,MinimumNights,MaximumNights")] Calendar calendar)
        {
            if (ModelState.IsValid)
            {
                _context.Add(calendar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ListingId"] = new SelectList(_context.Listings, "Id", "Name", calendar.ListingId);
            return View(calendar);
        }

        // GET: Calendar/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calendar = await _context.Calendar.FindAsync(id);
            if (calendar == null)
            {
                return NotFound();
            }
            ViewData["ListingId"] = new SelectList(_context.Listings, "Id", "Name", calendar.ListingId);
            return View(calendar);
        }

        // POST: Calendar/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ListingId,Date,Available,Price,AdjustedPrice,MinimumNights,MaximumNights")] Calendar calendar)
        {
            if (id != calendar.ListingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(calendar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CalendarExists(calendar.ListingId))
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
            ViewData["ListingId"] = new SelectList(_context.Listings, "Id", "Name", calendar.ListingId);
            return View(calendar);
        }

        // GET: Calendar/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calendar = await _context.Calendar
                .Include(c => c.Listing)
                .FirstOrDefaultAsync(m => m.ListingId == id);
            if (calendar == null)
            {
                return NotFound();
            }

            return View(calendar);
        }

        // POST: Calendar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var calendar = await _context.Calendar.FindAsync(id);
            _context.Calendar.Remove(calendar);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CalendarExists(int id)
        {
            return _context.Calendar.Any(e => e.ListingId == id);
        }
    }
}
