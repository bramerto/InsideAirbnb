using System.Collections.Generic;

namespace InsideAirbnbApp.Geo
{
    public class GeoJson
    {
        public string type;
        public List<Feature> features;
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
