using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InsideAirbnbApp.Repositories;
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
        private readonly IDistributedCache _cache;

        public NeighbourhoodsController(IRepository<NeighbourhoodsViewModel> repo, IDistributedCache cache)
        {
            _repo = repo;
            _cache = cache;
        }

        [HttpGet]
        public async Task<string> GetNeigbourhoods()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(await _repo.All().ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<string> NeighbourhoodDetails(int id)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(await _repo.Get(id));
        }
    }
}
