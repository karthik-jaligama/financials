using Financials.Data;

namespace Financials.Data.Services
{
    public class MockDebtService
    {
        public List<Debt> GetDebts()
        {
            return new List<Debt>
            {
                new Debt 
                {
                    ContactId = 1,
                    SpouseId = 2,
                    HouseholdId = 1,
                    DebtId = 1,
                    Timeframe = "NOW",
                    Owner = "JT",
                    Balance = 250000,
                    PaymentMade = 1500,
                    InterestRate = "6.375%",
                    Term = 360,
                    Category = "RE",
                    ReportDesc = "Primary Mortgage",
                    BalanceDate = DateTime.Now.AddDays(-5),
                    Notes = "30-year fixed rate mortgage. Property located at 123 Main St. Rate locked in 2022. Annual property taxes and insurance included in escrow payment. 30-year fixed rate mortgage. Property located at 123 Main St. Rate locked in 2022. Annual property taxes and insurance included in escrow payment. 30-year fixed rate mortgage. Property located at 123 Main St. Rate locked in 2022. Annual property taxes and insurance included in escrow payment. 30-year fixed rate mortgage. Property located at 123 Main St. Rate locked in 2022. Annual property taxes and insurance included in escrow payment.",
                    OriginalLoanAmount = 275000,
                    OriginalLoanDate = DateTime.Now.AddYears(-2),
                    MinimumPayment = 1450,
                    EscrowPayment = 450,
                    EscrowPaymentDate = DateTime.Now.AddMonths(-1),
                    LoanNumber = "12345678",
                    LoanRepresentative = "John Smith",
                    LenderPhone = "(555) 123-4567",
                    LenderPhoneExt = "123",
                    LenderName = "Big Bank USA",
                    FutureAdjustDate = null,
                    RateTiedTo = null,
                    Spread = null,
                    AdjustableRate = null,
                    IsInterestOnly = false,
                    FutureAdjustRate = null,
                    FuturePaymentMade = null,
                    HasBalloonPayment = false
                },
                new Debt 
                {
                    ContactId = 1,
                    SpouseId = 2,
                    HouseholdId = 1,
                    DebtId = 2,
                    Timeframe = "NOW",
                    Owner = "C1",
                    Balance = 15000,
                    PaymentMade = 350,
                    InterestRate = "18.99%",
                    Term = 0,
                    Category = "CC",
                    ReportDesc = "Credit Card",
                    BalanceDate = DateTime.Now.AddDays(-1),
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
                    ContactId = 1,
                    SpouseId = 2,
                    HouseholdId = 1,
                    DebtId = 3,
                    Timeframe = "NOW",
                    Owner = "C2",
                    Balance = 35000,
                    PaymentMade = 650,
                    InterestRate = "4.5%",
                    Term = 60,
                    Category = "AL",
                    ReportDesc = "Auto Loan",
                    BalanceDate = DateTime.Now.AddDays(-3),
                    Notes = "Car loan for 2023 Model X. Includes extended warranty. Gap insurance purchased at dealership.",
                    OriginalLoanAmount = 45000,
                    OriginalLoanDate = DateTime.Now.AddMonths(-8),
                    MinimumPayment = 650,
                    LoanNumber = "AL98765432",
                    LenderName = "Auto Finance Co",
                    LenderPhone = "555-777-6666",
                    LoanRepresentative = "Jane Doe",
                    IsInterestOnly = false,
                    HasBalloonPayment = false
                }
            };
        }
    }
} 