using System;

namespace InsideAirbnbApp.ViewModels
{
    public class CalendarViewModel
    {
        public int ListingId { get; set; }
        public DateTime Date { get; set; }
        public string Available { get; set; }
        public int Price { get; set; }
    }
}
