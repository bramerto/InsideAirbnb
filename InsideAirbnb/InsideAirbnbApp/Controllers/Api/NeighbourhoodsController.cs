using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InsideAirbnbApp.Repositories;
using InsideAirbnbApp.Util;
using InsideAirbnbApp.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace InsideAirbnbApp.Controllers.Api
{
    [Route("api/neighbourhoods")]
    [ApiController]
    public class NeighbourhoodsController : ControllerBase
    {
        private readonly IRepository<NeighbourhoodsViewModel> _repo;
        private readonly CacheHelper _cache;

        public NeighbourhoodsController(IRepository<NeighbourhoodsViewModel> repo, IDistributedCache cache)
        {
            _repo = repo;
            _cache = new CacheHelper(cache);
        }

        [HttpGet]
        public async Task<string> GetNeigbourhoods()
        {
            var cacheItem = await _cache.Get("NeighbourhoodList");
            if (cacheItem != null) return cacheItem;
            var response = Newtonsoft.Json.JsonConvert.SerializeObject(await _repo.All().ToListAsync());

            _cache.Set("NeighbourhoodList", response);
            return response;
        }

        [HttpGet("{id}")]
        public async Task<string> NeighbourhoodDetails(int id)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(await _repo.Get(id));
        }
    }
}
