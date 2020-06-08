namespace InsideAirbnbApp.ViewModels
{
    public class ListingsViewModel
    {
        public int Id { get; set; }
        public string ListingUrl { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Neighbourhood { get; set; }
        public string Zipcode { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public string SquareFeet { get; set; }
        public int? Price { get; set; }
        public int? ReviewScoresRating { get; set; }
        public int WeeklyPrice { get; set; }
        public int MonthlyPrice { get; set; }
        public int SecurityDeposit { get; set; }
        public int CleaningFee { get; set; }
        public int? MinimumNights { get; set; }
        public int? MaximumNights { get; set; }
    }
}
