using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using InsideAirbnbApp.ViewModels;
using Microsoft.EntityFrameworkCore;
// ReSharper disable InconsistentNaming

namespace InsideAirbnbApp.Geo
{
    public class GeoJson
    {
        public string type;
        public List<Feature> features;

        public static async Task<GeoJson> Create(IQueryable<ListingsViewModel> listings)
        {
            return new GeoJson
            {
                type = "FeatureCollection",
                features = await listings.Select(l => new Feature
                    {
                        type = "Feature",
                        properties = new Properties
                        {
                            id = l.Id,
                            neighbourhood = l.Neighbourhood,
                            price = decimal.Parse(Regex.Replace(l.Price, @"[^\d.]", "")),
                            reviewScore = l.ReviewScoresRating?? 0
                        },
                        geometry = new Geometry
                        {
                            type = "Point",
                            coordinates = new[] { l.Longitude ?? 0, l.Latitude ?? 0, 0 }
                        }
                    }
                ).ToListAsync()
            };
        }
    }

    public class Feature
    {
        public string type;
        public Properties properties;
        public Geometry geometry;
    }

    public class Properties
    {
        public int id;
        public string neighbourhood;
        public decimal price;
        public int reviewScore;
    }

    public class Geometry
    {
        public string type;
        public decimal[] coordinates;
    }
}
