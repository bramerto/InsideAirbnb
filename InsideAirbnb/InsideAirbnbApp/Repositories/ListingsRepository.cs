using System.Linq;
using System.Threading.Tasks;
using InsideAirbnbApp.Models;
using InsideAirbnbApp.Util;
using InsideAirbnbApp.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace InsideAirbnbApp.Repositories
{
    public class ListingsRepository : IRepository<ListingsViewModel>
    {
        private readonly AirbnbContext _context;
        public ListingsRepository(AirbnbContext context)
        {
            _context = context;
        }

        public async Task<ListingsViewModel> Get(int id)
        {
            return await _context.Listings.AsQueryable()
                .Select(l => new ListingsViewModel
                {
                    Id = l.Id,
                    ListingUrl = l.ListingUrl,
                    Name = l.Name,
                    Description = l.Description,
                    Neighbourhood = l.Neighbourhood.Neighbourhood,
                    Zipcode = l.Zipcode,
                    Latitude = l.Latitude,
                    Longitude = l.Longitude,
                    SquareFeet = l.SquareFeet,
                    Price = l.Price,
                    ReviewScoresRating = l.ReviewScoresRating,
                    WeeklyPrice = l.WeeklyPrice ?? 0,
                    MonthlyPrice = l.MonthlyPrice ?? 0,
                    SecurityDeposit = l.SecurityDeposit ?? 0,
                    CleaningFee = l.CleaningFee ?? 0,
                    MinimumNights = l.MinimumNights,
                    MaximumNights = l.MaximumNights
                })
                .AsNoTracking()
                .FirstOrDefaultAsync(l => l.Id == id);
        }

        public IQueryable<ListingsViewModel> All()
        {
            return _context.Listings.Select(l => new ListingsViewModel
                {
                    Id = l.Id,
                    Latitude = l.Latitude,
                    Longitude = l.Longitude
                })
                .Take(100)
                .AsNoTracking();
        }

        public IQueryable<ListingsViewModel> AllStats(string type, int id = 0)
        {
            var query = _context.Listings.AsQueryable();

            if (type.Equals("availability"))
            {
                query = query
                    .Where(l => l.Availability30 != 0)
                    .Where(l => l.Availability60 != 0)
                    .Where(l => l.Availability90 != 0)
                    .Where(l => l.Availability365 != 0);
            }
            else if (type.Equals("prices"))
            {
                query = query
                    .Where(l => l.Price != 0)
                    .Where(l => l.WeeklyPrice != null)
                    .Where(l => l.MonthlyPrice != null)
                    .Where(l => l.CleaningFee != null)
                    .Where(l => l.SecurityDeposit != null);
            }
            else
            {
                query = query
                    .Where(l => l.RoomType != null)
                    .Where(l => l.PropertyType != null);
            }

            if (id != 0)
            {
                query = query.Where(l => l.NeighbourhoodId == id);
            }

            return query.Select(l => new ListingsViewModel
                {
                    Id = l.Id,
                    Availability30 = l.Availability30,
                    Availability90 = l.Availability90,
                    Availability365 = l.Availability365,
                    NumberOfReviews = l.NumberOfReviews,
                    ReviewScoresRating = l.ReviewScoresRating,
                    ReviewsPerMonth = l.ReviewsPerMonth,
                    Price = l.Price,
                    WeeklyPrice = l.WeeklyPrice??0,
                    MonthlyPrice = l.MonthlyPrice??0,
                    CleaningFee = l.CleaningFee??0,
                    SecurityDeposit = l.SecurityDeposit??0,
                    ExtraPeoplePrice = l.ExtraPeoplePrice,
                    PropertyType = l.PropertyType,
                    RoomType = l.RoomType
            })
                .AsNoTracking();
        }

        public IQueryable<ListingsViewModel> Filter(Filter filter)
        {
            var query = _context.Listings.AsQueryable();

            if (filter.minPrice != 0)
            {
                query = query.Where(l => l.Price >= filter.minPrice);
            }

            if (filter.maxPrice != 1000)
            {
                query = query.Where(l => l.Price <= filter.maxPrice);
            }

            if (filter.neighbourhood != 0)
            {
                query = query.Where(l => l.Neighbourhood.Id == filter.neighbourhood);
            }

            if (filter.minReviewRate != 0)
            {
                query = query.Where(l => l.ReviewScoresRating >= filter.minReviewRate);
            }

            return query.Select(l => new ListingsViewModel
            {
                Id = l.Id,
                Latitude = l.Latitude,
                Longitude = l.Longitude
            })
                .AsNoTracking();
        }

        public IQueryable<ListingsViewModel> Join(IQueryable<ListingsViewModel> query)
        {
            throw new System.NotImplementedException();
        }
    }
}
