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
            return await _context.Listings.Select(l => new ListingsViewModel
            {
                Id = l.Id,
                ListingUrl = l.ListingUrl,
                Name = l.Name,
                Description = l.Description,
                Neighbourhood = l.Neighbourhood,
                Zipcode = l.Zipcode,
                Latitude = l.Latitude,
                Longitude = l.Longitude,
                SquareFeet = l.SquareFeet,
                Price = l.Price,
                ReviewScoresRating = l.ReviewScoresRating,
                WeeklyPrice = l.WeeklyPrice,
                MonthlyPrice = l.MonthlyPrice,
                SecurityDeposit = l.SecurityDeposit,
                CleaningFee = l.CleaningFee,
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
                // ListingUrl = l.ListingUrl,
                // Name = l.Name,
                // Description = l.Description,
                Neighbourhood = l.NeighbourhoodCleansed,
                // Zipcode = l.Zipcode,
                Latitude = l.Latitude,
                Longitude = l.Longitude,
                // SquareFeet = l.SquareFeet,
                Price = l.Price,
                ReviewScoresRating = l.ReviewScoresRating
                // WeeklyPrice = l.WeeklyPrice,
                // MonthlyPrice = l.MonthlyPrice,
                // SecurityDeposit = l.SecurityDeposit,
                // CleaningFee = l.CleaningFee,
                // MinimumNights = l.MinimumNights,
                // MaximumNights = l.MaximumNights
            })
                .AsNoTracking();
        }

        public IQueryable<ListingsViewModel> Filter(Filter filter)
        {
            var query = _context.Listings.Join(
                    _context.SummaryListings,
                    listing => listing.Id,
                    summary => summary.Id,
                    (listing, summary) => new  { listing, summary  }
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
                query = query.Where(j => j.listing.Neighbourhood.Equals(filter.neighbourhood));
            }

            if (filter.minReviewRate != 0)
            {
                query = query.Where(j => j.listing.ReviewScoresRating >= filter.minReviewRate);
            }

            return query.Select(j => new ListingsViewModel
            {
                Id = j.listing.Id,
                // ListingUrl = j.listing.ListingUrl,
                // Name = j.listing.Name,
                // Description = j.listing.Description,
                Neighbourhood = j.listing.NeighbourhoodCleansed,
                // Zipcode = j.listing.Zipcode,
                Latitude = j.listing.Latitude,
                Longitude = j.listing.Longitude,
                // SquareFeet = j.listing.SquareFeet,
                Price = j.listing.Price,
                ReviewScoresRating = j.listing.ReviewScoresRating
                // WeeklyPrice = j.listing.WeeklyPrice,
                // MonthlyPrice = j.listing.MonthlyPrice,
                // SecurityDeposit = j.listing.SecurityDeposit,
                // CleaningFee = j.listing.CleaningFee,
                // MinimumNights = j.listing.MinimumNights,
                // MaximumNights = j.listing.MaximumNights
            })
                .AsNoTracking();
        }
    }
}
