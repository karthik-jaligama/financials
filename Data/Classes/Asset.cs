namespace Financials.Data
{
    public class Asset
    {
        public int ContactId { get; set; }
        public int SpouseId { get; set; }
        public int HouseholdId { get; set; }
        public int AssetId { get; set; }
        public string? Timeframe { get; set; }
        public string? Owner { get; set; }
        public decimal Value { get; set; }
        public string? Category { get; set; }
        public string? ReportDesc { get; set; }
        public string? ReportHoverNote { get; set; }
        public DateTime? ValueDate { get; set; }
        public string? Notes { get; set; }
        public decimal PurchPrice { get; set; }
        public DateTime? PurchPriceDate { get; set; }
        public decimal OwnershipPct { get; set; }
        public DateTime? OwnershipPctDate { get; set; }
        public string? OwnershipDetail { get; set; }
        public decimal Taxes { get; set; }
        public DateTime? TaxesDate { get; set; }
        public decimal InsCost { get; set; }
        public DateTime? InsCostDate { get; set; }
        public decimal HOAFees { get; set; }
        public DateTime? HOAFeesDate { get; set; }
        public decimal OtherCosts { get; set; }
        public DateTime? OtherCostsDate { get; set; }
        public string? ExpenseNotes { get; set; }
        
        // Associated entities
        public List<int> AssociatedDebts { get; set; } = new();
        public List<int> AssociatedInvestments { get; set; } = new();
        public List<int> AssociatedSavings { get; set; } = new();
        public List<int> AssociatedIncomes { get; set; } = new();
        public List<int> AssociatedInsurances { get; set; } = new();

        public Asset()
        {
            ContactId = -1;
            SpouseId = -1;
            HouseholdId = -1;
            AssetId = -1;
            Value = 0;
            PurchPrice = 0;
            OwnershipPct = 0;
            Taxes = 0;
            InsCost = 0;
            HOAFees = 0;
            OtherCosts = 0;
        }

        public void Copy(Asset source)
        {
            ContactId = source.ContactId;
            SpouseId = source.SpouseId;
            HouseholdId = source.HouseholdId;
            AssetId = source.AssetId;
            Timeframe = source.Timeframe;
            Owner = source.Owner;
            Value = source.Value;
            Category = source.Category;
            ReportDesc = source.ReportDesc;
            ReportHoverNote = source.ReportHoverNote;
            ValueDate = source.ValueDate;
            Notes = source.Notes;
            PurchPrice = source.PurchPrice;
            PurchPriceDate = source.PurchPriceDate;
            OwnershipPct = source.OwnershipPct;
            OwnershipPctDate = source.OwnershipPctDate;
            OwnershipDetail = source.OwnershipDetail;
            Taxes = source.Taxes;
            TaxesDate = source.TaxesDate;
            InsCost = source.InsCost;
            InsCostDate = source.InsCostDate;
            HOAFees = source.HOAFees;
            HOAFeesDate = source.HOAFeesDate;
            OtherCosts = source.OtherCosts;
            OtherCostsDate = source.OtherCostsDate;
            ExpenseNotes = source.ExpenseNotes;
        }
    }
} 