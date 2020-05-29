using System;
using System.Linq;
using System.Threading.Tasks;
using InsideAirbnbApp.Models;
using InsideAirbnbApp.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace InsideAirbnbApp.Repositories
{
    public class NeighbourhoodsRepository : IRepository<NeighbourhoodsViewModel>
    {
        private readonly AirbnbContext _context;
        public NeighbourhoodsRepository(AirbnbContext context)
        {
            _context = context;
        }

        public Task<NeighbourhoodsViewModel> Get(int id)
        {
            return _context.Neighbourhoods.Select(n => new NeighbourhoodsViewModel
                {
                    Neighbourhood = n.Neighbourhood,
                    Id = n.Id
                })
                .AsNoTracking()
                .FirstOrDefaultAsync(n => n.Id == id);
        }

        public IQueryable<NeighbourhoodsViewModel> All()
        {
            return _context.Neighbourhoods.Select(n => new NeighbourhoodsViewModel
            {
                Neighbourhood = n.Neighbourhood,
                Id = n.Id
            })
                .AsNoTracking();
        }
    }
}
