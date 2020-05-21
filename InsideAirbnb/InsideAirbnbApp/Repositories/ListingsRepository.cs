using System;
using System.Collections.Generic;
using System.Linq;
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

        public ListingsViewModel Get(int id)
        {
            return _context.Listings.Select(l => new ListingsViewModel
            {
                Id = l.Id,
                ListingUrl = l.ListingUrl,
                Name = l.Name,
                Summary = l.Summary,
                Description = l.Description,
                Street = l.Street,
                Neighbourhood = l.Neighbourhood,
                City = l.City,
                State = l.State,
                Zipcode = l.Zipcode,
                Latitude = l.Latitude,
                Longitude = l.Longitude,
                SquareFeet = l.SquareFeet,
                Price = l.Price,
                WeeklyPrice = l.WeeklyPrice,
                MonthlyPrice = l.MonthlyPrice,
                SecurityDeposit = l.SecurityDeposit,
                CleaningFee = l.CleaningFee,
                GuestsIncluded = l.GuestsIncluded,
                ExtraPeople = l.ExtraPeople,
                MinimumNights = l.MinimumNights,
                MaximumNights = l.MaximumNights
            })
                .AsNoTracking()
                .First(l => l.Id == id);
        }

        public ListingsViewModel Get(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ListingsViewModel> All()
        {
            return _context.Listings.Select(l => new ListingsViewModel
            {
                Id = l.Id,
                // ListingUrl = l.ListingUrl,
                // Name = l.Name,
                // Summary = l.Summary,
                // Description = l.Description,
                // Street = l.Street,
                // Neighbourhood = l.Neighbourhood,
                // City = l.City,
                // State = l.State,
                // Zipcode = l.Zipcode,
                Latitude = l.Latitude,
                Longitude = l.Longitude,
                // SquareFeet = l.SquareFeet,
                // Price = l.Price,
                // WeeklyPrice = l.WeeklyPrice,
                // MonthlyPrice = l.MonthlyPrice,
                // SecurityDeposit = l.SecurityDeposit,
                // CleaningFee = l.CleaningFee,
                // GuestsIncluded = l.GuestsIncluded,
                // ExtraPeople = l.ExtraPeople,
                // MinimumNights = l.MinimumNights,
                // MaximumNights = l.MaximumNights
            })
                .Take(100)
                .AsNoTracking();
        }
    }
}
