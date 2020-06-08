using System;
using System.Collections.Generic;

namespace InsideAirbnbApp.Models
{
    public partial class Hosts
    {
        public Hosts()
        {
            Listings = new HashSet<Listings>();
        }

        public int Id { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public DateTime? Since { get; set; }
        public string Location { get; set; }
        public string About { get; set; }
        public string ResponseTime { get; set; }
        public string ResponseRate { get; set; }
        public string AcceptanceRate { get; set; }
        public string IsSuperhost { get; set; }
        public string ThumbnailUrl { get; set; }
        public string PictureUrl { get; set; }
        public string Neighbourhood { get; set; }
        public int? ListingsCount { get; set; }
        public int? TotalListingsCount { get; set; }
        public string Verifications { get; set; }

        public virtual ICollection<Listings> Listings { get; set; }
    }
}
