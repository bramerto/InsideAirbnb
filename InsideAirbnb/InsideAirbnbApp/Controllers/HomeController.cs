using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InsideAirbnbApp.Json;
using Microsoft.AspNetCore.Mvc;
using InsideAirbnbApp.Models;
using InsideAirbnbApp.Repositories;
using InsideAirbnbApp.Util;
using InsideAirbnbApp.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace InsideAirbnbApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository<ListingsViewModel> _listingsRepo;
        private readonly IRepository<CalendarViewModel> _calendarRepo;
        private readonly IDistributedCache _cache;

        public HomeController(IRepository<ListingsViewModel> listingsRepo, IRepository<CalendarViewModel> calendarRepo, IDistributedCache cache)
        {
            _listingsRepo = listingsRepo;
            _calendarRepo = calendarRepo;
            _cache = cache;
        }

        public async Task<IActionResult> Index()
        {
            const string key = "UnfilteredMapResponse";
            var cacheItem = await _cache.GetAsync(key);
            if (cacheItem != null) return Ok(Encoding.ASCII.GetString(cacheItem));

            var listings = _listingsRepo.All();
            var calendar = _calendarRepo.All();
            var geoJson = await GeoJson.Create(listings);

            var totalLocations = listings.Count();
            var staysPerMonth = calendar.Count();
            var collectionPerMonth = calendar.Sum(c => c.Price);

            var response = Newtonsoft.Json.JsonConvert.SerializeObject(
                new { geoJson, staysPerMonth, collectionPerMonth, totalLocations }
            );

            _cache.Set(key, Encoding.ASCII.GetBytes(response));

            return Ok(response);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
