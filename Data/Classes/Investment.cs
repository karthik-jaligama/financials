namespace Financials.Data
{
    public class Investment
    {
        public int ContactId { get; set; }
        public int SpouseId { get; set; }
        public int HouseholdId { get; set; }
        public int InvestmentId { get; set; }
        public string? Timeframe { get; set; }
        public string? Owner { get; set; }
        public decimal? CurrentValue { get; set; }
        public decimal? CostBasis { get; set; }
        public decimal? UnrealizedGainLoss { get; set; }
        public string? Category { get; set; }
        public string? ReportDesc { get; set; }
        public string? ReportHoverNote { get; set; }
        public DateTime? ValueDate { get; set; }
        public string? Notes { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public decimal? PurchasePrice { get; set; }
        public int? Shares { get; set; }
        public decimal? SharePrice { get; set; }
        public string? Symbol { get; set; }
        public string? AccountType { get; set; }
        public string? AccountNumber { get; set; }
        public string? Institution { get; set; }
        public decimal? AnnualReturn { get; set; }
        public decimal? DividendYield { get; set; }
        public decimal? ExpenseRatio { get; set; }
        public string? RiskLevel { get; set; }
        public DateTime? MaturityDate { get; set; }
        public decimal? InterestRate { get; set; }
        public string? InvestmentType { get; set; }
        public string? Firm { get; set; }
        public bool? Outside { get; set; }
        public decimal? PreTax { get; set; }
        public decimal? Roth { get; set; }
        public decimal? AfterTax { get; set; }
        public decimal? TaxPaid { get; set; }
        
        // Associated entities
        public List<int> AssociatedAssets { get; set; } = new();
        public List<int> AssociatedDebts { get; set; } = new();
        public List<int> AssociatedSavings { get; set; } = new();
        public List<int> AssociatedIncomes { get; set; } = new();
        public List<int> AssociatedInsurances { get; set; } = new();

        public Investment()
        {
            ContactId = -1;
            SpouseId = -1;
            HouseholdId = -1;
            InvestmentId = -1;
            CurrentValue = 0;
            CostBasis = 0;
            UnrealizedGainLoss = 0;
            PurchasePrice = 0;
            Shares = 0;
            SharePrice = 0;
            AnnualReturn = 0;
            DividendYield = 0;
            ExpenseRatio = 0;
            InterestRate = 0;
            PreTax = 0;
            Roth = 0;
            AfterTax = 0;
            TaxPaid = 0;
        }

        public void Copy(Investment source)
        {
            ContactId = source.ContactId;
            SpouseId = source.SpouseId;
            HouseholdId = source.HouseholdId;
            InvestmentId = source.InvestmentId;
            Timeframe = source.Timeframe;
            Owner = source.Owner;
            CurrentValue = source.CurrentValue;
            CostBasis = source.CostBasis;
            UnrealizedGainLoss = source.UnrealizedGainLoss;
            Category = source.Category;
            ReportDesc = source.ReportDesc;
            ReportHoverNote = source.ReportHoverNote;
            ValueDate = source.ValueDate;
            Notes = source.Notes;
            PurchaseDate = source.PurchaseDate;
            PurchasePrice = source.PurchasePrice;
            Shares = source.Shares;
            SharePrice = source.SharePrice;
            Symbol = source.Symbol;
            AccountType = source.AccountType;
            AccountNumber = source.AccountNumber;
            Institution = source.Institution;
            AnnualReturn = source.AnnualReturn;
            DividendYield = source.DividendYield;
            ExpenseRatio = source.ExpenseRatio;
            RiskLevel = source.RiskLevel;
            MaturityDate = source.MaturityDate;
            InterestRate = source.InterestRate;
            InvestmentType = source.InvestmentType;
            Firm = source.Firm;
            Outside = source.Outside;
            PreTax = source.PreTax;
            Roth = source.Roth;
            AfterTax = source.AfterTax;
            TaxPaid = source.TaxPaid;
        }
    }
}
