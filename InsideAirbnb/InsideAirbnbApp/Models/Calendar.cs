using System;

namespace InsideAirbnbApp.Models
{
    public partial class Calendar
    {
        public int ListingId { get; set; }
        public DateTime Date { get; set; }
        public string Available { get; set; }
        public string Price { get; set; }
        public string AdjustedPrice { get; set; }
        public int MinimumNights { get; set; }
        public int MaximumNights { get; set; }

        public virtual Listings Listing { get; set; }
    }
}
