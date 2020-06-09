namespace InsideAirbnbApp.Util
{
    public class FilterRequest
    {
        public string minPrice;
        public string maxPrice;
        public string neighbourhood;
        public string minReviewRate;

        public Filter GetFilter()
        {
            return new Filter
            {
                minPrice = int.Parse(minPrice),
                maxPrice = int.Parse(maxPrice),
                neighbourhood = int.Parse(neighbourhood),
                minReviewRate = int.Parse(minReviewRate)
            };
        }
    }
}
