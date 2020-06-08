using System.Linq;
using System.Threading.Tasks;
using InsideAirbnbApp.Geo;
using Microsoft.AspNetCore.Mvc;
using InsideAirbnbApp.Repositories;
using InsideAirbnbApp.Util;
using InsideAirbnbApp.ViewModels;
using Microsoft.Extensions.Caching.Distributed;

namespace InsideAirbnbApp.Controllers.Api
{
    [Route("api/listings")]
    [ApiController]
    public class ListingsController : ControllerBase
    {
        private readonly IRepository<ListingsViewModel> _listingsRepo;
        private readonly IRepository<CalendarViewModel> _calendarRepo;
        private readonly IDistributedCache _cache;

        public ListingsController(IRepository<ListingsViewModel> listingsRepo, IRepository<CalendarViewModel> calendarRepo, IDistributedCache cache)
        {
            _listingsRepo = listingsRepo;
            _calendarRepo = calendarRepo;
            _cache = cache;
        }

        [HttpGet]
        public async Task<string> GetListings()
        {
            var listings = _listingsRepo.All();
            var calendar = _calendarRepo.All();
            var geoPoints = await GeoJson.Create(listings);

            var staysPerMonth = calendar.Count();
            var collectionPerMonth = calendar.Sum(c => c.Price);

            var response = new { geoJson = geoPoints, staysPerMonth, collectionPerMonth, totalLocations = listings.Count() };
            
            return Newtonsoft.Json.JsonConvert.SerializeObject(response);
        }

        [HttpPost]
        public async Task<string> GetFilteredListings()
        {
            var minPrice = int.Parse(Request.Form["minPrice"].ToString());
            var maxPrice = int.Parse(Request.Form["maxPrice"].ToString());
            var neighbourhood = Request.Form["neighbourhood"].ToString();
            var minReviewRate = int.Parse(Request.Form["minReviewRate"].ToString());

            //add validation

            var filter = new Filter
            {
                minPrice = minPrice,
                maxPrice = maxPrice,
                neighbourhood = neighbourhood,
                minReviewRate = minReviewRate
            };

            var listings = _listingsRepo.Filter(filter);
            var calendar = _calendarRepo.Join(listings);

            var staysPerMonth = calendar.Count();
            var collectionPerMonth = calendar.Sum(c => c.Price);
            var geoPoints = await GeoJson.Create(listings);

            var response = new { geoJson = geoPoints, staysPerMonth, collectionPerMonth, totalLocations = listings.Count() };

            return Newtonsoft.Json.JsonConvert.SerializeObject(response);
        }

        [HttpGet("{id}")]
        public async Task<string> ListingDetails(int id)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(await _listingsRepo.Get(id));
        }
    }
}
