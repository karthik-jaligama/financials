using Microsoft.EntityFrameworkCore;
using Financials.Data;

namespace Financials.Data.Services
{
    public class MockFinancialsContext : DbContext
    {
        public MockFinancialsContext(DbContextOptions<MockFinancialsContext> options) : base(options) { }

        public DbSet<Asset> Assets { get; set; }
        public DbSet<Debt> Debts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed mock data for Assets
            modelBuilder.Entity<Asset>().HasData(
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
            );

            // Seed mock data for Debts
            modelBuilder.Entity<Debt>().HasData(
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
            );
        }
    }
} 