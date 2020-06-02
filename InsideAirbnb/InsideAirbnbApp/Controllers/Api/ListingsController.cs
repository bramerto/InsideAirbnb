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
        private readonly IRepository<ListingsViewModel> _repo;
        private readonly IDistributedCache _cache;

        public ListingsController(IRepository<ListingsViewModel> repo, IDistributedCache cache)
        {
            _repo = repo;
            _cache = cache;
        }

        [HttpGet]
        public async Task<string> GetListings()
        {
            var geoPoints = await GeoJson.Create(_repo.All());
            
            return Newtonsoft.Json.JsonConvert.SerializeObject(geoPoints);
        }

        [HttpPost]
        public async Task<string> GetFilteredListings()
        {
            var minPrice = int.Parse(Request.Form["minPrice"].ToString());
            var maxPrice = int.Parse(Request.Form["maxPrice"].ToString());
            var neighbourhood = Request.Form["neighbourhood"].ToString();
            var minReviewRate = int.Parse(Request.Form["minReviewRate"].ToString());

            //add validation

            var listings = _repo.Filter(new Filter
            {
                minPrice = minPrice,
                maxPrice = maxPrice,
                neighbourhood = neighbourhood,
                minReviewRate = minReviewRate
            });

            var geoPoints = await GeoJson.Create(listings);

            return Newtonsoft.Json.JsonConvert.SerializeObject(geoPoints);
        }

        [HttpGet("{id}")]
        public async Task<string> Details(int id)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(await _repo.Get(id));
        }
    }
}
