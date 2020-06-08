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
            return await _context.Listings.Join(
                    _context.Neighbourhoods,
                    listing => listing.NeighbourhoodId,
                    neighbourhood => neighbourhood.Id,
                    (listing, neighbourhood) => new { Listings = listing, Neighbourhood = neighbourhood }
                )
                .Select(j => new ListingsViewModel
                {
                    Id = j.Listings.Id,
                    ListingUrl = j.Listings.ListingUrl,
                    Name = j.Listings.Name,
                    Description = j.Listings.Description,
                    Neighbourhood = j.Neighbourhood.Neighbourhood,
                    Zipcode = j.Listings.Zipcode,
                    Latitude = j.Listings.Latitude,
                    Longitude = j.Listings.Longitude,
                    SquareFeet = j.Listings.SquareFeet,
                    Price = j.Listings.Price,
                    ReviewScoresRating = j.Listings.ReviewScoresRating,
                    WeeklyPrice = j.Listings.WeeklyPrice ?? 0,
                    MonthlyPrice = j.Listings.MonthlyPrice ?? 0,
                    SecurityDeposit = j.Listings.SecurityDeposit ?? 0,
                    CleaningFee = j.Listings.CleaningFee ?? 0,
                    MinimumNights = j.Listings.MinimumNights,
                    MaximumNights = j.Listings.MaximumNights
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
                .AsNoTracking();
        }

        public IQueryable<ListingsViewModel> Filter(Filter filter)
        {
            var query = _context.Listings.Join(
                _context.Neighbourhoods,
                listing => listing.NeighbourhoodId,
                neighbourhood => neighbourhood.Id,
                (listing, neighbourhood) => new {Listings = listing, Neighbourhood = neighbourhood}
            );

            if (filter.minPrice != 0)
            {
                query = query.Where(j => j.Listings.Price >= filter.minPrice);
            }

            if (filter.maxPrice != 1000)
            {
                query = query.Where(j => j.Listings.Price <= filter.maxPrice);
            }

            if (filter.neighbourhood != 0)
            {
                query = query.Where(j => j.Neighbourhood.Id == filter.neighbourhood);
            }

            if (filter.minReviewRate != 0)
            {
                query = query.Where(j => j.Listings.ReviewScoresRating >= filter.minReviewRate);
            }

            return query.Select(j => new ListingsViewModel
            {
                Id = j.Listings.Id,
                Latitude = j.Listings.Latitude,
                Longitude = j.Listings.Longitude
            }).AsNoTracking();
        }

        public IQueryable<ListingsViewModel> Join(IQueryable<ListingsViewModel> query)
        {
            throw new System.NotImplementedException();
        }
    }
}
