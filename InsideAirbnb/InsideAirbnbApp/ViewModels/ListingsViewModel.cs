namespace InsideAirbnbApp.ViewModels
{
    public class ListingsViewModel
    {
        public int Id { get; set; }
        public string ListingUrl { get; set; }
        public string Name { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string Neighbourhood { get; set; }
        public string Zipcode { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public string SquareFeet { get; set; }
        public string Price { get; set; }
        public string WeeklyPrice { get; set; }
        public string MonthlyPrice { get; set; }
        public string SecurityDeposit { get; set; }
        public string CleaningFee { get; set; }
        public int? MinimumNights { get; set; }
        public int? MaximumNights { get; set; }
    }
}
