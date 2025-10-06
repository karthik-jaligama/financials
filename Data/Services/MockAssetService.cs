namespace Financials.Data.Services
{
    public class MockAssetService
    {
        public List<Asset> GetAssets()
        {
            return new List<Asset>
            {
                new Asset
                {
                    Timeframe = "NOW",
                    Owner = "C1",
                    Value = 25000,
                    Category = "Boat",
                    ReportDesc = "24' Runabout",
                    ValueDate = DateTime.Parse("2024-02-28"),
                    Notes = "Family vacation timeshare purchased in 2017"
                },
                new Asset
                {
                    Timeframe = "NOW",
                    Owner = "JT",
                    Value = 450000,
                    Category = "Real Estate",
                    ReportDesc = "Primary Residence",
                    ValueDate = DateTime.Parse("2022-03-15"),
                    Notes = "Main family home purchased in 2017"
                },
                new Asset
                {
                    Timeframe = "NOW",
                    Owner = "LLC",
                    Value = 387000,
                    Category = "Real Estate",
                    ReportDesc = "City Rental Property",
                    ValueDate = DateTime.Parse("2025-12-01"),
                    PurchPrice = 350000,
                    PurchPriceDate = DateTime.Parse("2017-03-12"),
                    OwnershipPct = 50,
                    OwnershipPctDate = DateTime.Parse("2017-03-12"),
                    OwnershipDetail = "Canal Rentals LLC",
                    Notes = "John bought the rental property from his Uncle. (House used to be his mom's house and has been in the family). They rent it for below market rates as his cousin Bill and his wife now live there. They are in the process of a rent-to-own with the hope of purchasing it from him once they can qualify for a mortgage on their own."
                },
                new Asset
                {
                    Timeframe = "NOW",
                    Owner = "C2",
                    Value = 15000,
                    Category = "Collectibles",
                    ReportDesc = "1972 Corvette",
                    ValueDate = DateTime.Parse("2025-02-01"),
                    Notes = "Jane's car, but they will likely let their daughter take it to college and she'll probably get a new one."
                }
            };
        }
    }
} 