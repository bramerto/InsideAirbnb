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
        public async Task<string> Index()
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

        [HttpGet("{id}")]
        public async Task<ListingsViewModel> Details(int id)
        {
            var listings = await _repo.Get(id);
            return listings ?? null;
        }
    }
}
