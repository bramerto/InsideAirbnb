using System;

namespace InsideAirbnbApp.Models
{
    public partial class Reviews
    {
        public int ListingId { get; set; }
        public DateTime Date { get; set; }
        public int Id { get; set; }

        public virtual Listings Listing { get; set; }
    }
}
