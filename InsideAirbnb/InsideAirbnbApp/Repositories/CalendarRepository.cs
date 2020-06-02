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
            return _context.Calendar.Join(
                    _context.SummaryListings,
                    calendar => calendar.ListingId,
                    listings => listings.Id,
                    (calendar, listings) => new { calendar, listings }
                )
                .Select(j => new CalendarViewModel
                {
                            ListingId = j.calendar.ListingId,
                            Date = j.calendar.Date,
                            Available = j.calendar.Available,
                            Price = j.listings.Price??0
                })
                .AsNoTracking();
        }

        public IQueryable<CalendarViewModel> Filter(Filter filter)
        {
            var query = _context.Calendar.Join(
                    _context.Listings,
                    calendar => calendar.ListingId,
                    listings => listings.Id,
                    (calendar, listings) => new { calendar, listings }
            ).Join(
                _context.SummaryListings,
                model => model.listings.Id,
                summary => summary.Id,
                (model, summary ) => new {model, summary}
            );

            if (filter.minPrice != 0)
            {
                query = query.Where(j => j.summary.Price >= filter.minPrice);
            }

            if (filter.maxPrice != 1000)
            {
                query = query.Where(j => j.summary.Price <= filter.maxPrice);
            }

            if (!filter.neighbourhood.Equals("Selecteer..."))
            {
                query = query.Where(j => j.summary.Neighbourhood.Equals(filter.neighbourhood));
            }

            if (filter.minReviewRate != 0)
            {
                query = query.Where(j => j.model.listings.ReviewScoresRating >= filter.minReviewRate);
            }

            return query.Select(j => new CalendarViewModel
            {
                ListingId = j.model.calendar.ListingId,
                Date = j.model.calendar.Date,
                Available = j.model.calendar.Available,
                Price = j.summary.Price ?? 0
            })
                .AsNoTracking();
        }
    }
}
