using System.Linq;
using System.Threading.Tasks;
using InsideAirbnbApp.Geo;
using Microsoft.AspNetCore.Mvc;
using InsideAirbnbApp.Repositories;
using InsideAirbnbApp.ViewModels;
using Microsoft.EntityFrameworkCore;
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
            var listings = _repo.All();

            var geoPoints = new GeoJson
            {
                type = "FeatureCollection",
                features = await listings.Select(l => new Feature
                    {
                        type = "Feature",
                        properties = new Properties
                        {
                            id = l.Id
                        },
                        geometry = new Geometry
                        {
                            type = "Point",
                            coordinates = new[] { l.Longitude??0, l.Latitude ?? 0, 0 }
                        }
                    }
                ).ToListAsync()
            };
            
            return Newtonsoft.Json.JsonConvert.SerializeObject(geoPoints);
        }

        [HttpPost]
        public string GetFilteredListings()
        {
            var minPrice = Request.Form["minPrice"];
            var maxPrice = Request.Form["maxPrice"];
            var neighbourhoodId = Request.Form["neighbourhoodId"];
            var minReviews = Request.Form["minReviews"];
            var maxReviews = Request.Form["maxReviews"];

            return Newtonsoft.Json.JsonConvert.SerializeObject(new { minPrice, maxPrice, neighbourhoodId, minReviews, maxReviews });
        }

        [HttpGet("{id}")]
        public async Task<string> Details(int id)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(await _repo.Get(id));
        }
    }
}
