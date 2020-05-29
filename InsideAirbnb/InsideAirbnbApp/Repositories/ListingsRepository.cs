using System;
using System.Linq;
using System.Threading.Tasks;
using InsideAirbnbApp.Models;
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
                Summary = l.Summary,
                Description = l.Description,
                Neighbourhood = l.Neighbourhood,
                Zipcode = l.Zipcode,
                Latitude = l.Latitude,
                Longitude = l.Longitude,
                SquareFeet = l.SquareFeet,
                Price = l.Price,
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
                // Summary = l.Summary,
                // Description = l.Description,
                // Neighbourhood = l.Neighbourhood,
                // Zipcode = l.Zipcode,
                Latitude = l.Latitude,
                Longitude = l.Longitude,
                // SquareFeet = l.SquareFeet,
                // Price = l.Price,
                // WeeklyPrice = l.WeeklyPrice,
                // MonthlyPrice = l.MonthlyPrice,
                // SecurityDeposit = l.SecurityDeposit,
                // CleaningFee = l.CleaningFee,
                // MinimumNights = l.MinimumNights,
                // MaximumNights = l.MaximumNights
            })
                .AsNoTracking();
        }
    }
}
