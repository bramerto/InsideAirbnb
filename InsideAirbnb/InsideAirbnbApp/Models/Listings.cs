using System;
using System.Collections.Generic;

namespace InsideAirbnbApp.Models
{
    public partial class Listings
    {
        public Listings()
        {
            Reviews = new HashSet<Reviews>();
        }

        public int Id { get; set; }
        public string ListingUrl { get; set; }
        public string Name { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string ThumbnailUrl { get; set; }
        public string MediumUrl { get; set; }
        public string PictureUrl { get; set; }
        public string XlPictureUrl { get; set; }
        public int HostId { get; set; }
        public string City { get; set; }
        public string Zipcode { get; set; }
        public string CountryCode { get; set; }
        public string PropertyType { get; set; }
        public string RoomType { get; set; }
        public int? Accommodates { get; set; }
        public double? Bathrooms { get; set; }
        public int? Bedrooms { get; set; }
        public int? Beds { get; set; }
        public string SquareFeet { get; set; }
        public int? GuestsIncluded { get; set; }
        public int? MinimumNights { get; set; }
        public int? MaximumNights { get; set; }
        public string CalendarUpdated { get; set; }
        public string HasAvailability { get; set; }
        public int? Availability30 { get; set; }
        public int? Availability60 { get; set; }
        public int? Availability90 { get; set; }
        public int? Availability365 { get; set; }
        public int? NumberOfReviews { get; set; }
        public DateTime? FirstReview { get; set; }
        public DateTime? LastReview { get; set; }
        public int? ReviewScoresRating { get; set; }
        public int? ReviewScoresAccuracy { get; set; }
        public int? ReviewScoresCleanliness { get; set; }
        public int? ReviewScoresCheckin { get; set; }
        public int? ReviewScoresCommunication { get; set; }
        public int? ReviewScoresLocation { get; set; }
        public int? ReviewScoresValue { get; set; }
        public double? ReviewsPerMonth { get; set; }
        public decimal? Longitude { get; set; }
        public decimal? Latitude { get; set; }
        public int? NeighbourhoodId { get; set; }
        public int? Price { get; set; }
        public int? WeeklyPrice { get; set; }
        public int? MonthlyPrice { get; set; }
        public int? CleaningFee { get; set; }
        public int? SecurityDeposit { get; set; }
        public int? ExtraPeoplePrice { get; set; }

        public virtual Hosts Host { get; set; }
        public virtual SummaryListings IdNavigation { get; set; }
        public virtual Neighbourhoods Neighbourhood { get; set; }
        public virtual ICollection<Reviews> Reviews { get; set; }
    }
}
