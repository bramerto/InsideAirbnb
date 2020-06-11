using System;

namespace InsideAirbnbApp.Models
{
    public partial class SummaryListings
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int HostId { get; set; }
        public string HostName { get; set; }
        public string RoomType { get; set; }
        public int? Price { get; set; }
        public int? MinimumNights { get; set; }
        public int? NumberOfReviews { get; set; }
        public DateTime? LastReview { get; set; }
        public double? ReviewsPerMonth { get; set; }
        public int? CalculatedHostListingsCount { get; set; }
        public int? Availability365 { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public int? NeighbourhoodId { get; set; }

        public virtual Neighbourhoods Neighbourhood { get; set; }
        public virtual Listings Listings { get; set; }
    }
}
