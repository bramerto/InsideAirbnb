using System;
using System.Collections.Generic;

namespace InsideAirbnbApp.Models
{
    public partial class SummaryReviews
    {
        public int ListingId { get; set; }
        public DateTime Date { get; set; }

        public virtual Listings Listing { get; set; }
    }
}
