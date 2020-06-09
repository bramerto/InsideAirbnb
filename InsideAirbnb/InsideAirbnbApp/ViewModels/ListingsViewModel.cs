namespace InsideAirbnbApp.ViewModels
{
    public class ListingsViewModel
    {
        /* MAP information */
        public int Id { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }

        /* Listing information for display */
        public string ListingUrl { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Neighbourhood { get; set; }
        public string Zipcode { get; set; }
        public string SquareFeet { get; set; }
        public int? Price { get; set; }
        public int? ReviewScoresRating { get; set; } //also a statistic
        public int WeeklyPrice { get; set; } //also a statistic
        public int MonthlyPrice { get; set; } //also a statistic
        public int SecurityDeposit { get; set; } //also a statistic
        public int CleaningFee { get; set; } //also a statistic
        public int? MinimumNights { get; set; }
        public int? MaximumNights { get; set; }

        /* Listing Statistics */
        public int? Availability30 { get; set; }
        public int? Availability90 { get; set; }
        public int? Availability365 { get; set; }
        public int? NumberOfReviews { get; set; }
        public double? ReviewsPerMonth { get; set; }
        public int? ExtraPeoplePrice { get; set; }
        public string PropertyType { get; set; }
        public string RoomType { get; set; }
    }
}
