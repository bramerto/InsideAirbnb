﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InsideAirbnbApp.ViewModels;
using Microsoft.EntityFrameworkCore;
// ReSharper disable InconsistentNaming

namespace InsideAirbnbApp.Json
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
                        properties = new Properties { id = l.Id },
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
    }

    public class Geometry
    {
        public string type;
        public decimal[] coordinates;
    }
}
