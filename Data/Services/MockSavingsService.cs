using Financials.Data.Classes;

namespace Financials.Data.Services
{
    public class MockSavingsService
    {
        public List<Saving> GetSavings()
        {
            return new List<Saving>
            {
                new Saving 
                { 
                    Id = 1,
                    ContactId = 1,
                    Timeframe = "NOW", 
                    Owner = "JT", 
                    Value = 25000, 
                    Category = "Savings", 
                    ReportDesc = "High-Yield Savings Account", 
                    ValueDate = DateTime.Now.AddDays(-5), 
                    Notes = "Emergency fund savings account" 
                },
                new Saving 
                { 
                    Id = 2,
                    ContactId = 1,
                    Timeframe = "NOW", 
                    Owner = "C1", 
                    Value = 15000, 
                    Category = "CD", 
                    ReportDesc = "6-Month Certificate", 
                    ValueDate = DateTime.Now.AddDays(-10), 
                    Notes = "Short-term CD for upcoming expenses" 
                },
                new Saving 
                { 
                    Id = 3,
                    ContactId = 1,
                    Timeframe = "FUTR", 
                    Owner = "JT", 
                    Value = 50000, 
                    Category = "Money Market", 
                    ReportDesc = "Money Market Account", 
                    ValueDate = DateTime.Now.AddDays(-15), 
                    Notes = "Money market account for liquidity" 
                },
                new Saving 
                { 
                    Id = 4,
                    ContactId = 1,
                    Timeframe = "NOW", 
                    Owner = "C2", 
                    Value = 12000, 
                    Category = "Checking", 
                    ReportDesc = "High-Yield Checking Account", 
                    ValueDate = DateTime.Now.AddDays(-3), 
                    Notes = "Primary checking account with interest" 
                },
                new Saving 
                { 
                    Id = 5,
                    ContactId = 1,
                    Timeframe = "PAST", 
                    Owner = "TR", 
                    Value = 35000, 
                    Category = "Emergency Fund", 
                    ReportDesc = "Emergency Fund Savings", 
                    ValueDate = DateTime.Now.AddDays(-20), 
                    Notes = "Emergency fund for unexpected expenses" 
                }
            };
        }
    }
}
