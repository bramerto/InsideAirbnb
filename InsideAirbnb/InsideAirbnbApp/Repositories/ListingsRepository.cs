using System;
using System.Collections.Generic;
using System.Linq;
using InsideAirbnbApp.Models;

namespace InsideAirbnbApp.Repositories
{
    public class ListingsRepository : IRepository<Listings>
    {
        private readonly AirbnbContext _context;
        public ListingsRepository(AirbnbContext context)
        {
            _context = context;
        }

        public Listings Get(int id)
        {
            return _context.Listings.First(l => l.Id == id);
        }

        public Listings Get(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Listings> All()
        {
            return _context.Listings.Take(100);
        }
    }
}
