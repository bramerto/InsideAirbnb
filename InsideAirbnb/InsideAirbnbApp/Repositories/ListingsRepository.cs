using System.Linq;
using System.Threading.Tasks;
using InsideAirbnbApp.Models;
using InsideAirbnbApp.Util;
using InsideAirbnbApp.ViewModels;
using InsideAirbnbApp.ViewModels.Joined;
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
            return await All().FirstOrDefaultAsync(l => l.Id == id);
        }

        public IQueryable<ListingsViewModel> All()
        {
            return _context.Listings.Join(
                    _context.SummaryListings,
                    listing => listing.Id,
                    summary => summary.Id,
                    (listing, summary) => new ListingSummaryListing { Listings = listing, Summary = summary }
                )
                .Select(j => new ListingsViewModel
                {
                    Id = j.Listings.Id,
                    ListingUrl = j.Listings.ListingUrl,
                    Name = j.Listings.Name,
                    Description = j.Listings.Description,
                    Neighbourhood = j.Summary.Neighbourhood,
                    Zipcode = j.Listings.Zipcode,
                    Latitude = j.Listings.Latitude,
                    Longitude = j.Listings.Longitude,
                    SquareFeet = j.Listings.SquareFeet,
                    Price = j.Summary.Price ?? 0,
                    ReviewScoresRating = j.Listings.ReviewScoresRating,
                    WeeklyPrice = j.Listings.WeeklyPrice,
                    MonthlyPrice = j.Listings.MonthlyPrice,
                    SecurityDeposit = j.Listings.SecurityDeposit,
                    CleaningFee = j.Listings.CleaningFee,
                    MinimumNights = j.Listings.MinimumNights,
                    MaximumNights = j.Listings.MaximumNights
                })
                .AsNoTracking();
        }

        public IQueryable<ListingsViewModel> Filter(Filter filter)
        {
            var query = All();

            if (filter.minPrice != 0)
            {
                query = query.Where(l => l.Price >= filter.minPrice);
            }

            if (filter.maxPrice != 1000)
            {
                query = query.Where(l => l.Price <= filter.maxPrice);
            }

            if (!filter.neighbourhood.Equals("Selecteer..."))
            {
                query = query.Where(l => l.Neighbourhood.Equals(filter.neighbourhood));
            }

            if (filter.minReviewRate != 0)
            {
                query = query.Where(l => l.ReviewScoresRating >= filter.minReviewRate);
            }

            return query.AsNoTracking();
        }
    }
}
