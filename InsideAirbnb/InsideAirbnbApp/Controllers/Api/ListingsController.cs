using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InsideAirbnbApp.Repositories;
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
        public async Task<List<ListingsViewModel>> Index()
        {
            return await _repo.All().ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ListingsViewModel> Details(int id)
        {
            var listings = await _repo.Get(id);
            return listings ?? null;
        }
    }
}
