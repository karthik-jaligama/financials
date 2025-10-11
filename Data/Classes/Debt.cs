namespace Financials.Data
{
    public class Debt
    {
        public int ContactId { get; set; }
        public int SpouseId { get; set; }
        public int HouseholdId { get; set; }
        public int DebtId { get; set; }
        public string? Timeframe { get; set; }
        public string? Owner { get; set; }
        public decimal? Balance { get; set; }
        public decimal? PaymentMade { get; set; }
        public string? InterestRate { get; set; }
        public int? Term { get; set; }
        public string? ReportDesc { get; set; }
        public string? ReportHoverNote { get; set; }
        public DateTime? BalanceDate { get; set; }
        public string? Notes { get; set; }
        public DateTime? OriginalLoanDate { get; set; }
        public decimal? OriginalLoanAmount { get; set; }
        public decimal? BorrowLimit { get; set; }
        public decimal? MinimumPayment { get; set; }
        public decimal? EscrowPayment { get; set; }
        public DateTime? EscrowPaymentDate { get; set; }
        public string? LoanNumber { get; set; }
        public string? LoanRepresentative { get; set; }
        public string? LenderPhone { get; set; }
        public string? LenderPhoneExt { get; set; }
        public string? LenderName { get; set; }
        public DateTime? FutureAdjustDate { get; set; }
        public string? RateTiedTo { get; set; }
        public decimal? Spread { get; set; }
        public decimal? AdjustableRate { get; set; }
        public bool IsInterestOnly { get; set; }
        public decimal? FutureAdjustRate { get; set; }
        public decimal? FuturePaymentMade { get; set; }
        public bool HasBalloonPayment { get; set; }
        public string? Category { get; set; }
        
        // Associated entities
        public List<int> AssociatedAssets { get; set; } = new();
        public List<int> AssociatedInvestments { get; set; } = new();
        public List<int> AssociatedSavings { get; set; } = new();
        public List<int> AssociatedIncomes { get; set; } = new();
        public List<int> AssociatedInsurances { get; set; } = new();

        public Debt()
        {
            ContactId = -1;
            SpouseId = -1;
            HouseholdId = -1;
            DebtId = -1;
            Balance = 0;
            PaymentMade = 0;
            OriginalLoanAmount = 0;
            BorrowLimit = 0;
            MinimumPayment = 0;
            EscrowPayment = 0;
            Spread = 0;
            AdjustableRate = 0;
            FutureAdjustRate = 0;
            FuturePaymentMade = 0;
            IsInterestOnly = false;
            HasBalloonPayment = false;
        }

        public void Copy(Debt source)
        {
            ContactId = source.ContactId;
            SpouseId = source.SpouseId;
            HouseholdId = source.HouseholdId;
            DebtId = source.DebtId;
            Timeframe = source.Timeframe;
            Owner = source.Owner;
            Balance = source.Balance;
            PaymentMade = source.PaymentMade;
            InterestRate = source.InterestRate;
            Term = source.Term;
            ReportDesc = source.ReportDesc;
            ReportHoverNote = source.ReportHoverNote;
            BalanceDate = source.BalanceDate;
            Notes = source.Notes;
            OriginalLoanDate = source.OriginalLoanDate;
            OriginalLoanAmount = source.OriginalLoanAmount;
            BorrowLimit = source.BorrowLimit;
            MinimumPayment = source.MinimumPayment;
            EscrowPayment = source.EscrowPayment;
            EscrowPaymentDate = source.EscrowPaymentDate;
            LoanNumber = source.LoanNumber;
            LoanRepresentative = source.LoanRepresentative;
            LenderPhone = source.LenderPhone;
            LenderPhoneExt = source.LenderPhoneExt;
            LenderName = source.LenderName;
            FutureAdjustDate = source.FutureAdjustDate;
            RateTiedTo = source.RateTiedTo;
            Spread = source.Spread;
            AdjustableRate = source.AdjustableRate;
            IsInterestOnly = source.IsInterestOnly;
            FutureAdjustRate = source.FutureAdjustRate;
            FuturePaymentMade = source.FuturePaymentMade;
            HasBalloonPayment = source.HasBalloonPayment;
            Category = source.Category;
        }
    }
}
