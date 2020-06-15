using System.Linq;
using System.Threading.Tasks;
using InsideAirbnbApp.Json;
using Microsoft.AspNetCore.Mvc;
using InsideAirbnbApp.Repositories;
using InsideAirbnbApp.Util;
using InsideAirbnbApp.Validators;
using InsideAirbnbApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
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
            var filterRequest = new FilterRequest
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

            var filter = filterRequest.GetFilter();
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
        public async Task<IActionResult> ListingDetails(int id)
        {
            var key = $"ListingDetail-{id}";
            var cacheItem = await _cache.Get(key);
            if (cacheItem != null) return Ok(cacheItem);

            var response = Newtonsoft.Json.JsonConvert.SerializeObject(await _listingsRepo.Get(id));
            _cache.Set(key, response);

            return Ok(response);
        }

        [Authorize(Policy = "Admin")]
        [HttpGet("chart/average/availability")]
        public async Task<IActionResult> GetListingsAverageAvailability()
        {
            const string key = "AverageAvailability";
            var cacheItem = await _cache.Get(key);
            if (cacheItem != null) return Ok(cacheItem);

            var response = Newtonsoft.Json.JsonConvert.SerializeObject(
                await ChartJson.CreateAvailability(_listingsRepo.AllStats("availability"))
            );
            _cache.Set(key, response);

            return Ok(response);
        }

        [Authorize(Policy = "Admin")]
        [HttpGet("chart/average/availability/{neighbourhoodId}")]
        public async Task<IActionResult> GetListingsAverageAvailabilityFiltered(int neighbourhoodId)
        {
            var key = $"AverageAvailabilityFiltered-{neighbourhoodId}";
            var cacheItem = await _cache.Get(key);
            if (cacheItem != null) return Ok(cacheItem);

            var response = Newtonsoft.Json.JsonConvert.SerializeObject(
                await ChartJson.CreateAvailability(_listingsRepo.AllStats("availability", neighbourhoodId))
            );
            _cache.Set(key, response);

            return Ok(response);
        }

        [Authorize(Policy = "Admin")]
        [HttpGet("chart/average/prices")]
        public async Task<IActionResult> GetListingsAveragePrice()
        {
            const string key = "AveragePrice";
            var cacheItem = await _cache.Get(key);
            if (cacheItem != null) return Ok(cacheItem);

            var response = Newtonsoft.Json.JsonConvert.SerializeObject(
                await ChartJson.CreatePrices(_listingsRepo.AllStats("prices"))
            );
            _cache.Set(key, response);

            return Ok(response);
        }

        [Authorize(Policy = "Admin")]
        [HttpGet("chart/average/prices/{neighbourhoodId}")]
        public async Task<IActionResult> GetListingsAveragePriceFiltered(int neighbourhoodId)
        {
            var key = $"AveragePriceFiltered-{neighbourhoodId}";
            var cacheItem = await _cache.Get(key);
            if (cacheItem != null) return Ok(cacheItem);

            var response = Newtonsoft.Json.JsonConvert.SerializeObject(
                await ChartJson.CreatePrices(_listingsRepo.AllStats("prices", neighbourhoodId))
            );
            _cache.Set(key, response);

            return Ok(response);
        }

        [Authorize(Policy = "Admin")]
        [HttpGet("chart/property/type")]
        public async Task<IActionResult> GetListingsPropertiesTypes()
        {
            const string key = "PropertyTypes";
            var cacheItem = await _cache.Get(key);
            if (cacheItem != null) return Ok(cacheItem);

            var response = Newtonsoft.Json.JsonConvert.SerializeObject(
                await ChartJson.CreatePropertyType(_listingsRepo.AllStats("propertyType"))
            );
            _cache.Set(key, response);

            return Ok(response);
        }

        [Authorize(Policy = "Admin")]
        [HttpGet("chart/property/type/{neighbourhoodId}")]
        public async Task<IActionResult> GetListingsAverageReviewsFiltered(int neighbourhoodId)
        {
            var key = $"PropertyTypes-{neighbourhoodId}";
            var cacheItem = await _cache.Get(key);
            if (cacheItem != null) return Ok(cacheItem);

            var response = Newtonsoft.Json.JsonConvert.SerializeObject(
                await ChartJson.CreatePropertyType(_listingsRepo.AllStats("propertyType", neighbourhoodId))
            );
            _cache.Set(key, response);

            return Ok(response);
        }
    }
}
