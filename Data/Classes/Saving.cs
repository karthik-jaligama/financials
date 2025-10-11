namespace Financials.Data.Classes
{
    public class Saving
    {
        public int Id { get; set; }
        public int ContactId { get; set; }
        public string Timeframe { get; set; } = "";
        public string Owner { get; set; } = "";
        public decimal Value { get; set; }
        public string Category { get; set; } = "";
        public string ReportDesc { get; set; } = "";
        public string? ReportHoverNote { get; set; }
        public DateTime? ValueDate { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }
        
        // Associated entities
        public List<int> AssociatedAssets { get; set; } = new();
        public List<int> AssociatedDebts { get; set; } = new();
        public List<int> AssociatedInvestments { get; set; } = new();
        public List<int> AssociatedIncomes { get; set; } = new();
        public List<int> AssociatedInsurances { get; set; } = new();
    }
}
