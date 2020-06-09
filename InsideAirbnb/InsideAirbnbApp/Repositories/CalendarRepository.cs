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
            return _context.Calendar.Where(c => 
                    c.Date >= new DateTime(2018, 11, 1) && 
                    c.Date <= new DateTime(2018, 11, 30)
                    )
                .Select(c => new CalendarViewModel { Price = c.Price??0 })
                .AsNoTracking();
        }

        public IQueryable<CalendarViewModel> AllStats(string type, int id = 0)
        {
            throw new NotImplementedException();
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
                    (listing, calendar) => calendar
                )
                .Where(c =>
                    c.Date >= new DateTime(2018, 11, 1) &&
                    c.Date <= new DateTime(2018, 11, 30)
                )
                .Select(calendar => new CalendarViewModel { Price = calendar.Price ?? 0 })
                .AsNoTracking();
        }
    }
}
