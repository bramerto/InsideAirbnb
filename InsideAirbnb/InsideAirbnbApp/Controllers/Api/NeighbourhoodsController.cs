﻿using System.Collections.Generic;
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
        public async Task<List<NeighbourhoodsViewModel>> Index()
        {
            return await _repo.All().ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<NeighbourhoodsViewModel> Details(string id)
        {
            var neighbourhoods = await _repo.Get(id);
            return neighbourhoods ?? null;
        }
    }
}