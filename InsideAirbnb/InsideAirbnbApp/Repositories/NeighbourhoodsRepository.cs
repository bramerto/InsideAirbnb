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
            throw new NotImplementedException();
        }

        public Task<NeighbourhoodsViewModel> Get(string id)
        {
            return _context.Neighbourhoods.Select(n => new NeighbourhoodsViewModel
            {
                Neighbourhood = n.Neighbourhood,
                NeighbourhoodGroup = n.NeighbourhoodGroup
            })
                .AsNoTracking()
                .FirstOrDefaultAsync(n => n.Neighbourhood == id);
        }

        public IQueryable<NeighbourhoodsViewModel> All()
        {
            return _context.Neighbourhoods.Select(n => new NeighbourhoodsViewModel
            {
                Neighbourhood = n.Neighbourhood,
                NeighbourhoodGroup = n.NeighbourhoodGroup
            })
                .AsNoTracking();
        }
    }
}
