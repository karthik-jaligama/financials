using Microsoft.EntityFrameworkCore;
using Financials.Data;
using Financials.Data.Classes;

namespace Financials.Data
{
    public class FinancialsContext : DbContext
    {
        public FinancialsContext(DbContextOptions<FinancialsContext> options) : base(options) { }

        public DbSet<Asset> Assets { get; set; }
        public DbSet<Debt> Debts { get; set; }
        public DbSet<Investment> Investments { get; set; }
        public DbSet<Saving> Savings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Asset entity
            modelBuilder.Entity<Asset>(entity =>
            {
                entity.HasKey(e => e.AssetId);
                entity.Property(e => e.Value).HasColumnType("decimal(18,2)");
                entity.Property(e => e.PurchPrice).HasColumnType("decimal(18,2)");
                entity.Property(e => e.OwnershipPct).HasColumnType("decimal(18,2)");
                entity.Property(e => e.Taxes).HasColumnType("decimal(18,2)");
                entity.Property(e => e.InsCost).HasColumnType("decimal(18,2)");
                entity.Property(e => e.HOAFees).HasColumnType("decimal(18,2)");
                entity.Property(e => e.OtherCosts).HasColumnType("decimal(18,2)");
            });

            // Configure Debt entity
            modelBuilder.Entity<Debt>(entity =>
            {
                entity.HasKey(e => e.DebtId);
                entity.Property(e => e.Balance).HasColumnType("decimal(18,2)");
                entity.Property(e => e.PaymentMade).HasColumnType("decimal(18,2)");
                entity.Property(e => e.OriginalLoanAmount).HasColumnType("decimal(18,2)");
                entity.Property(e => e.BorrowLimit).HasColumnType("decimal(18,2)");
                entity.Property(e => e.MinimumPayment).HasColumnType("decimal(18,2)");
                entity.Property(e => e.EscrowPayment).HasColumnType("decimal(18,2)");
                entity.Property(e => e.Spread).HasColumnType("decimal(18,2)");
                entity.Property(e => e.AdjustableRate).HasColumnType("decimal(18,2)");
                entity.Property(e => e.FutureAdjustRate).HasColumnType("decimal(18,2)");
                entity.Property(e => e.FuturePaymentMade).HasColumnType("decimal(18,2)");
            });

            // Configure Investment entity
            modelBuilder.Entity<Investment>(entity =>
            {
                entity.HasKey(e => e.InvestmentId);
                entity.Property(e => e.CurrentValue).HasColumnType("decimal(18,2)");
                entity.Property(e => e.CostBasis).HasColumnType("decimal(18,2)");
                entity.Property(e => e.UnrealizedGainLoss).HasColumnType("decimal(18,2)");
                entity.Property(e => e.PurchasePrice).HasColumnType("decimal(18,2)");
                entity.Property(e => e.SharePrice).HasColumnType("decimal(18,2)");
                entity.Property(e => e.AnnualReturn).HasColumnType("decimal(18,2)");
                entity.Property(e => e.DividendYield).HasColumnType("decimal(18,2)");
                entity.Property(e => e.ExpenseRatio).HasColumnType("decimal(18,2)");
                entity.Property(e => e.InterestRate).HasColumnType("decimal(18,2)");
            });

            // Configure Saving entity
            modelBuilder.Entity<Saving>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Value).HasColumnType("decimal(18,2)");
            });
        }
    }
}
