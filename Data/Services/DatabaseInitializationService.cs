using Microsoft.EntityFrameworkCore;
using Financials.Data;
using Financials.Data.Classes;

namespace Financials.Data.Services
{
    public class DatabaseInitializationService
    {
        private readonly FinancialsContext _context;

        public DatabaseInitializationService(FinancialsContext context)
        {
            _context = context;
        }

        public async Task InitializeDatabaseAsync()
        {
            // Ensure database is created
            await _context.Database.EnsureCreatedAsync();

            // Check if data already exists
            if (await _context.Assets.AnyAsync())
            {
                return; // Data already seeded
            }

            // Seed Assets
            var assets = new List<Asset>
            {
                new Asset
                {
                    AssetId = 1,
                    ContactId = 1,
                    SpouseId = 2,
                    HouseholdId = 1,
                    Timeframe = "NOW",
                    Owner = "C1",
                    Value = 25000,
                    Category = "Timeshare",
                    ReportDesc = "Disney Vacation Club",
                    ReportHoverNote = "150 points annually, expires if not used within 3 years. Maintenance fees are $1,200/year.",
                    ValueDate = new DateTime(2024, 2, 28),
                    Notes = "Family vacation timeshare purchased in 2017"
                },
                new Asset
                {
                    AssetId = 2,
                    ContactId = 1,
                    SpouseId = 2,
                    HouseholdId = 1,
                    Timeframe = "NOW",
                    Owner = "JT",
                    Value = 450000,
                    Category = "Real Estate",
                    ReportDesc = "Primary Residence",
                    ValueDate = new DateTime(2022, 3, 15),
                    Notes = "Main family home purchased in 2017"
                },
                new Asset
                {
                    AssetId = 3,
                    ContactId = 1,
                    SpouseId = 2,
                    HouseholdId = 1,
                    Timeframe = "NOW",
                    Owner = "LLC",
                    Value = 387000,
                    Category = "Real Estate",
                    ReportDesc = "City Rental Property",
                    ReportHoverNote = "Currently rented to family members (cousin Bill) at below-market rate. Rent-to-own arrangement in progress.",
                    ValueDate = new DateTime(2025, 12, 1),
                    PurchPrice = 350000,
                    PurchPriceDate = new DateTime(2017, 3, 12),
                    OwnershipPct = 50,
                    OwnershipPctDate = new DateTime(2017, 3, 12),
                    OwnershipDetail = "Canal Rentals LLC",
                    Notes = "John bought the rental property from his Uncle. (House used to be his mom's house and has been in the family). They rent it for below market rates as his cousin Bill and his wife now live there. They are in the process of a rent-to-own with the hope of purchasing it from him once they can qualify for a mortgage on their own."
                },
                new Asset
                {
                    AssetId = 4,
                    ContactId = 1,
                    SpouseId = 2,
                    HouseholdId = 1,
                    Timeframe = "NOW",
                    Owner = "C2",
                    Value = 15000,
                    Category = "Auto",
                    ReportDesc = "2022 Hyundai Sante Fe",
                    ValueDate = new DateTime(2025, 2, 1),
                    Notes = "Jane's car, but they will likely let their daughter take it to college and she'll probably get a new one."
                }
            };

            // Seed Debts
            var debts = new List<Debt>
            {
                new Debt 
                {
                    DebtId = 1,
                    ContactId = 1,
                    SpouseId = 2,
                    HouseholdId = 1,
                    Timeframe = "NOW",
                    Owner = "JT",
                    Balance = 250000,
                    PaymentMade = 1500,
                    InterestRate = "6.375%",
                    Term = 360,
                    Category = "RE",
                    ReportDesc = "Primary Mortgage",
                    ReportHoverNote = "Escrow includes $350/month property tax and $100/month homeowner's insurance. Considering refinance if rates drop below 5.5%.",
                    BalanceDate = new DateTime(2024, 6, 15),
                    Notes = "30-year fixed rate mortgage. Property located at 123 Main St. Rate locked in 2022. Annual property taxes and insurance included in escrow payment.",
                    OriginalLoanAmount = 275000,
                    OriginalLoanDate = new DateTime(2022, 6, 15),
                    MinimumPayment = 1450,
                    EscrowPayment = 450,
                    EscrowPaymentDate = new DateTime(2024, 5, 15),
                    LoanNumber = "12345678",
                    LoanRepresentative = "John Smith",
                    LenderPhone = "(555) 123-4567",
                    LenderPhoneExt = "123",
                    LenderName = "Big Bank USA",
                    // Adjustable loan fields
                    FutureAdjustDate = new DateTime(2025, 6, 15),
                    RateTiedTo = "Prime Rate",
                    Spread = 2.5m,
                    AdjustableRate = 6.375m,
                    FutureAdjustRate = 7.25m,
                    FuturePaymentMade = 1600m,
                    IsInterestOnly = false,
                    HasBalloonPayment = false
                },
                new Debt 
                {
                    DebtId = 2,
                    ContactId = 1,
                    SpouseId = 2,
                    HouseholdId = 1,
                    Timeframe = "NOW",
                    Owner = "C1",
                    Balance = 15000,
                    PaymentMade = 350,
                    InterestRate = "18.99%",
                    Term = 0,
                    Category = "CC",
                    ReportDesc = "Credit Card",
                    ReportHoverNote = "⚠️ Priority payoff target. Balance transfer offer available at 0% APR for 15 months (3% fee).",
                    BalanceDate = new DateTime(2024, 6, 19),
                    Notes = "High interest credit card. Planning to transfer balance to lower rate card. Current promotional rate expires in 3 months.",
                    BorrowLimit = 20000,
                    MinimumPayment = 300,
                    LoanNumber = "4444555566667777",
                    LenderName = "Credit Bank",
                    LenderPhone = "555-999-8888",
                    IsInterestOnly = false,
                    HasBalloonPayment = false
                },
                new Debt 
                {
                    DebtId = 3,
                    ContactId = 1,
                    SpouseId = 2,
                    HouseholdId = 1,
                    Timeframe = "NOW",
                    Owner = "C2",
                    Balance = 35000,
                    PaymentMade = 650,
                    InterestRate = "4.5%",
                    Term = 60,
                    Category = "AL",
                    ReportDesc = "Auto Loan",
                    BalanceDate = new DateTime(2024, 6, 17),
                    Notes = "Car loan for 2023 Model X. Includes extended warranty. Gap insurance purchased at dealership.",
                    OriginalLoanAmount = 45000,
                    OriginalLoanDate = new DateTime(2023, 10, 15),
                    MinimumPayment = 650,
                    LoanNumber = "AL98765432",
                    LenderName = "Auto Finance Co",
                    LenderPhone = "555-777-6666",
                    LoanRepresentative = "Jane Doe",
                    IsInterestOnly = false,
                    HasBalloonPayment = false
                }
            };

            // Seed Investments
            var investments = new List<Investment>
            {
                new Investment 
                {
                    InvestmentId = 1,
                    ContactId = 1,
                    SpouseId = 2,
                    HouseholdId = 1,
                    Timeframe = "NOW",
                    Owner = "JT",
                    CurrentValue = 125000,
                    CostBasis = 95000,
                    UnrealizedGainLoss = 30000,
                    Category = "401K",
                    ReportDesc = "John's 401K",
                    ReportHoverNote = "Contributing $23,000 annually (2024 max). Company matches 100% up to 6% of salary. Currently 31.6% gain.",
                    ValueDate = DateTime.Now.AddDays(-2),
                    Notes = "Company 401K with 6% employer match. Invested in target date fund 2045. Maxing out contributions annually.",
                    PurchaseDate = DateTime.Now.AddYears(-5),
                    PurchasePrice = 95000,
                    Shares = 1250,
                    SharePrice = 100.00m,
                    Symbol = "TDF2045",
                    AccountType = "401K",
                    AccountNumber = "401K-123456",
                    Institution = "Fidelity",
                    AnnualReturn = 8.5m,
                    DividendYield = 2.1m,
                    ExpenseRatio = 0.12m,
                    RiskLevel = "Moderate",
                    InvestmentType = "Target Date Fund",
                    // New investment breakdown properties
                    Firm = "Fidelity",
                    Outside = false,
                    PreTax = 80000m,
                    Roth = 25000m,
                    AfterTax = 20000m,
                    TaxPaid = 15000m
                },
                new Investment 
                {
                    InvestmentId = 2,
                    ContactId = 1,
                    SpouseId = 2,
                    HouseholdId = 1,
                    Timeframe = "NOW",
                    Owner = "JT",
                    CurrentValue = 85000,
                    CostBasis = 70000,
                    UnrealizedGainLoss = 15000,
                    Category = "IRA",
                    ReportDesc = "John's Traditional IRA",
                    ValueDate = DateTime.Now.AddDays(-1),
                    Notes = "Rollover IRA from previous employer. Diversified portfolio of index funds and individual stocks.",
                    PurchaseDate = DateTime.Now.AddYears(-3),
                    PurchasePrice = 70000,
                    Shares = 850,
                    SharePrice = 100.00m,
                    Symbol = "MIXED",
                    AccountType = "IRA",
                    AccountNumber = "IRA-789012",
                    Institution = "Vanguard",
                    AnnualReturn = 7.2m,
                    DividendYield = 1.8m,
                    ExpenseRatio = 0.05m,
                    RiskLevel = "Moderate",
                    InvestmentType = "Mixed Portfolio",
                    // New investment breakdown properties
                    Firm = "Vanguard",
                    Outside = false,
                    PreTax = 60000m,
                    Roth = 0m,
                    AfterTax = 25000m,
                    TaxPaid = 20000m
                },
                new Investment 
                {
                    InvestmentId = 3,
                    ContactId = 1,
                    SpouseId = 2,
                    HouseholdId = 1,
                    Timeframe = "NOW",
                    Owner = "C2",
                    CurrentValue = 45000,
                    CostBasis = 40000,
                    UnrealizedGainLoss = 5000,
                    Category = "403B",
                    ReportDesc = "Jane's 403B",
                    ValueDate = DateTime.Now.AddDays(-3),
                    Notes = "Hospital 403B plan with 4% employer match. Conservative allocation due to approaching retirement.",
                    PurchaseDate = DateTime.Now.AddYears(-4),
                    PurchasePrice = 40000,
                    Shares = 450,
                    SharePrice = 100.00m,
                    Symbol = "CONSERV",
                    AccountType = "403B",
                    AccountNumber = "403B-345678",
                    Institution = "TIAA",
                    AnnualReturn = 5.8m,
                    DividendYield = 2.5m,
                    ExpenseRatio = 0.25m,
                    RiskLevel = "Conservative",
                    InvestmentType = "Conservative Fund",
                    // New investment breakdown properties
                    Firm = "TIAA",
                    Outside = false,
                    PreTax = 35000m,
                    Roth = 0m,
                    AfterTax = 10000m,
                    TaxPaid = 8000m
                }
            };

            // Seed Savings
            var savings = new List<Saving>
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
                    ReportHoverNote = "Emergency fund target: $30,000 (6 months expenses). Currently earning 4.5% APY at Marcus by Goldman Sachs.",
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
                    ReportHoverNote = "Matures June 2025. Rate: 5.25% APY. Earmarked for home renovations next summer.",
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
                }
            };

            // Add all entities to context
            await _context.Assets.AddRangeAsync(assets);
            await _context.Debts.AddRangeAsync(debts);
            await _context.Investments.AddRangeAsync(investments);
            await _context.Savings.AddRangeAsync(savings);

            // Save changes
            await _context.SaveChangesAsync();
        }
    }
}
