using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InsideAirbnbApp.ViewModels;
using Microsoft.EntityFrameworkCore;

// ReSharper disable InconsistentNaming

namespace InsideAirbnbApp.Json
{
    public class ChartJson
    {
        public List<string> labels;
        public List<DataSet> datasets;

        public static async Task<ChartJson> CreateAvailability(IQueryable<ListingsViewModel> listings)
        {
            var averageAvailabilityMonth = (int) await listings.AverageAsync(l => l.Availability30);
            var averageAvailabilitySeason = (int) await listings.AverageAsync(l => l.Availability90);
            var averageAvailabilityYear = (int) await listings.AverageAsync(l => l.Availability365);

            return new ChartJson
            {
                labels = new List<string> { "" },
                datasets = new List<DataSet> {
                    new DataSet
                    {
                        label = "Maand",
                        data = new List<int> { averageAvailabilityMonth },
                        borderWidth = 1,
                        backgroundColor = new List<string> {"rgba(0, 0, 128, 0.7)"},
                        borderColor = new List<string> {"rgba(0, 0, 128, 1.0)"}
                    },
                    new DataSet
                    {
                        label = "Kwartaal",
                        data = new List<int> { averageAvailabilitySeason },
                        borderWidth = 1,
                        backgroundColor = new List<string> {"rgba(0, 128, 0, 0.7)"},
                        borderColor = new List<string> {"rgba(0, 128, 0, 1.0)"}
                    },
                    new DataSet
                    {
                        label = "Jaar",
                        data = new List<int> { averageAvailabilityYear },
                        borderWidth = 1,
                        backgroundColor = new List<string> {"rgba(128, 0, 0, 0.7)"},
                        borderColor = new List<string> {"rgba(128, 0, 0, 1.0)"}
                    }
                }
            };
        }

        public static async Task<ChartJson> CreatePrices(IQueryable<ListingsViewModel> listings)
        {
            var averagePrice = (int)await listings.AverageAsync(l => l.Price);
            var averageWeeklyPrice = (int)await listings.AverageAsync(l => l.WeeklyPrice);
            var averageMonthlyPrice = (int)await listings.AverageAsync(l => l.MonthlyPrice);
            var averageCleaningFee = (int)await listings.AverageAsync(l => l.CleaningFee);
            var averageSecurityDeposit = (int)await listings.AverageAsync(l => l.SecurityDeposit);

            return new ChartJson
            {
                labels = new List<string> { "Huur", "Schoonmaakprijs", "Borg" },
                datasets = new List<DataSet> {
                    new DataSet
                    {
                        label = "Per dag",
                        data = new List<int> { averagePrice, averageCleaningFee, averageSecurityDeposit },
                        borderWidth = 1,
                        backgroundColor = new List<string> { "rgba(163, 9, 9, 0.7)", "rgba(215, 123, 18, 0.7)", "rgb(241, 199, 95, 0.7)"},
                        borderColor = new List<string> { "rgba(249, 120, 0, 0.2)", "rgba(249, 120, 0, 0.2)", "rgba(249, 120, 0, 0.2)" }
                    },
                    new DataSet
                    {
                        label = "Wekelijkse",
                        data = new List<int> { averageWeeklyPrice, averageCleaningFee, averageSecurityDeposit },
                        borderWidth = 1,
                        backgroundColor = new List<string> { "rgba(23, 100, 22, 0.7)", "rgba(16, 131, 102, 0.7)", "rgba(19, 190, 209, 0.7)"},
                        borderColor = new List<string> { "rgba(31, 208, 190, 0.2)", "rgba(31, 208, 190, 0.2)", "rgba(31, 208, 190, 0.2)" }
                    },
                    new DataSet
                    {
                        label = "Maandelijkse",
                        data = new List<int> { averageMonthlyPrice, averageCleaningFee, averageSecurityDeposit },
                        borderWidth = 1,
                        backgroundColor = new List<string> { "rgba(0, 19, 142, 0.71)", "rgba(49, 65, 235, 0.7)", "rgba(137, 150, 244, 0.7)"},
                        borderColor = new List<string> { "rgba(4, 0, 249, 0.2)", "rgba(4, 0, 249, 0.2)", "rgba(4, 0, 249, 0.2)" }
                    }
                }
            };
        }

        public static async Task<ChartJson> CreatePropertyType(IQueryable<ListingsViewModel> listings)
        {
            var amountPropertyTypeApartment = listings.Count(l => l.PropertyType.Equals("Apartment"));
            var amountPropertyTypeHouse = listings.Count(l => l.PropertyType.Equals("House"));
            var amountPropertyTypeBnb = listings.Count(l => l.PropertyType.Equals("Bed & Breakfast"));

            return new ChartJson
            {
                labels = new List<string> { "" },
                datasets = new List<DataSet> {
                    new DataSet
                    {
                        label = "Apartement",
                        data = new List<int> { amountPropertyTypeApartment },
                        borderWidth = 1,
                        backgroundColor = new List<string> {"rgba(0, 0, 128, 0.7)"},
                        borderColor = new List<string> {"rgba(0, 0, 128, 1.0)"}
                    },
                    new DataSet
                    {
                        label = "Huis",
                        data = new List<int> { amountPropertyTypeHouse },
                        borderWidth = 1,
                        backgroundColor = new List<string> {"rgba(0, 128, 0, 0.7)"},
                        borderColor = new List<string> {"rgba(0, 128, 0, 1.0)"}
                    },
                    new DataSet
                    {
                        label = "Bed & Breakfast",
                        data = new List<int> { amountPropertyTypeBnb },
                        borderWidth = 1,
                        backgroundColor = new List<string> {"rgba(128, 0, 0, 0.7)"},
                        borderColor = new List<string> {"rgba(128, 0, 0, 1.0)"}
                    }
                }
            };
        }
    }

    public class DataSet
    {
        public string label;
        public List<int> data;
        public List<string> backgroundColor;
        public List<string> borderColor;
        public int borderWidth;
    }
}
