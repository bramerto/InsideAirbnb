using System;
using System.Collections.Generic;

namespace InsideAirbnbApp.Models
{
    public partial class Neighbourhoods
    {
        public Neighbourhoods()
        {
            Listings = new HashSet<Listings>();
            SummaryListings = new HashSet<SummaryListings>();
        }

        public int Id { get; set; }
        public string Neighbourhood { get; set; }

        public virtual ICollection<Listings> Listings { get; set; }
        public virtual ICollection<SummaryListings> SummaryListings { get; set; }
    }
}
