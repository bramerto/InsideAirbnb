using System.Linq;
using System.Threading.Tasks;
using InsideAirbnbApp.Geo;
using Microsoft.AspNetCore.Mvc;
using InsideAirbnbApp.Repositories;
using InsideAirbnbApp.Util;
using InsideAirbnbApp.Validators;
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
        private readonly CacheHelper _cache;
        private readonly FilterValidator _validator;

        public ListingsController(IRepository<ListingsViewModel> listingsRepo, IRepository<CalendarViewModel> calendarRepo, IDistributedCache cache)
        {
            _listingsRepo = listingsRepo;
            _calendarRepo = calendarRepo;
            _cache = new CacheHelper(cache);
            _validator = new FilterValidator();
        }

        [HttpGet]
        public async Task<IActionResult> GetListings()
        {
            const string key = "UnfilteredMapResponse";
            var cacheItem = await _cache.Get(key);
            if (cacheItem != null) return Ok(cacheItem);

            var listings = _listingsRepo.All();
            var calendar = _calendarRepo.All();
            var geoJson = await GeoJson.Create(listings);

            var totalLocations = listings.Count();
            var staysPerMonth = calendar.Count();
            var collectionPerMonth = calendar.Sum(c => c.Price);

            var response = Newtonsoft.Json.JsonConvert.SerializeObject(
                new { geoJson, staysPerMonth, collectionPerMonth, totalLocations }
            );

            _cache.Set(key, response);
            
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> GetFilteredListings()
        {
            var filterRequest = new FilterRequest()
            {
                minPrice = Request.Form["minPrice"].ToString(),
                maxPrice = Request.Form["maxPrice"].ToString(),
                neighbourhood = Request.Form["neighbourhood"].ToString(),
                minReviewRate = Request.Form["minReviewRate"].ToString(),
            };

            var result = _validator.Validate(filterRequest);

            if (result.IsValid == false)
            {
                return BadRequest(Newtonsoft.Json.JsonConvert.SerializeObject(
                    new { errors = result.Errors.Select(e => e.ErrorMessage).ToList() })
                );
            }

            var filter = new Filter
            {
                minPrice = int.Parse(filterRequest.minPrice),
                maxPrice = int.Parse(filterRequest.maxPrice),
                neighbourhood = int.Parse(filterRequest.neighbourhood),
                minReviewRate = int.Parse(filterRequest.minReviewRate)
            };

            var key = $"FilteredMapResponse-{filter.minPrice}-{filter.maxPrice}-{filter.neighbourhood}-{filter.minReviewRate}";

            var cacheItem = await _cache.Get(key);
            if (cacheItem != null) return Ok(cacheItem);

            var listings = _listingsRepo.Filter(filter);
            var calendar = _calendarRepo.Join(listings);
            var geoJson = await GeoJson.Create(listings);

            var totalLocations = listings.Count();
            var staysPerMonth = calendar.Count();
            var collectionPerMonth = calendar.Sum(c => c.Price);

            var response = Newtonsoft.Json.JsonConvert.SerializeObject(
                new { geoJson, staysPerMonth, collectionPerMonth, totalLocations }
            );

            _cache.Set(key, response);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<string> ListingDetails(int id)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(await _listingsRepo.Get(id));
        }
    }
}
