using System;
using System.Linq;
using System.Threading.Tasks;
using InsideAirbnbApp.Models;
using InsideAirbnbApp.Util;
using InsideAirbnbApp.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace InsideAirbnbApp.Repositories
{
    public class CalendarRepository : IRepository<CalendarViewModel>
    {
        private readonly AirbnbContext _context;
        public CalendarRepository(AirbnbContext context)
        {
            _context = context;
        }

        public Task<CalendarViewModel> Get(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<CalendarViewModel> All()
        {
            return _context.Calendar.Select(c => new CalendarViewModel { Price = c.Price }).AsNoTracking();
        }

        public IQueryable<CalendarViewModel> Filter(Filter filter)
        {
            throw new NotImplementedException();
        }

        public IQueryable<CalendarViewModel> Join(IQueryable<ListingsViewModel> query)
        {
            return query.Join(
                    _context.Calendar, 
                    listing => listing.Id, 
                    calendar => calendar.ListingId, 
                    (listing, calendar) => new CalendarViewModel {Price = calendar.Price}
                )
                .AsNoTracking();
        }
    }
}
