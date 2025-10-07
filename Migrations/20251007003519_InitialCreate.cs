using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Financials.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Assets",
                columns: table => new
                {
                    AssetId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ContactId = table.Column<int>(type: "INTEGER", nullable: false),
                    SpouseId = table.Column<int>(type: "INTEGER", nullable: false),
                    HouseholdId = table.Column<int>(type: "INTEGER", nullable: false),
                    Timeframe = table.Column<string>(type: "TEXT", nullable: true),
                    Owner = table.Column<string>(type: "TEXT", nullable: true),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Category = table.Column<string>(type: "TEXT", nullable: true),
                    ReportDesc = table.Column<string>(type: "TEXT", nullable: true),
                    ReportHoverNote = table.Column<string>(type: "TEXT", nullable: true),
                    ValueDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Notes = table.Column<string>(type: "TEXT", nullable: true),
                    PurchPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PurchPriceDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    OwnershipPct = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OwnershipPctDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    OwnershipDetail = table.Column<string>(type: "TEXT", nullable: true),
                    Taxes = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TaxesDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    InsCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InsCostDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    HOAFees = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HOAFeesDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    OtherCosts = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OtherCostsDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ExpenseNotes = table.Column<string>(type: "TEXT", nullable: true),
                    AssociatedDebts = table.Column<string>(type: "TEXT", nullable: false),
                    AssociatedInvestments = table.Column<string>(type: "TEXT", nullable: false),
                    AssociatedSavings = table.Column<string>(type: "TEXT", nullable: false),
                    AssociatedIncomes = table.Column<string>(type: "TEXT", nullable: false),
                    AssociatedInsurances = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assets", x => x.AssetId);
                });

            migrationBuilder.CreateTable(
                name: "Debts",
                columns: table => new
                {
                    DebtId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ContactId = table.Column<int>(type: "INTEGER", nullable: false),
                    SpouseId = table.Column<int>(type: "INTEGER", nullable: false),
                    HouseholdId = table.Column<int>(type: "INTEGER", nullable: false),
                    Timeframe = table.Column<string>(type: "TEXT", nullable: true),
                    Owner = table.Column<string>(type: "TEXT", nullable: true),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PaymentMade = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    InterestRate = table.Column<string>(type: "TEXT", nullable: true),
                    Term = table.Column<int>(type: "INTEGER", nullable: true),
                    ReportDesc = table.Column<string>(type: "TEXT", nullable: true),
                    BalanceDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Notes = table.Column<string>(type: "TEXT", nullable: true),
                    OriginalLoanDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    OriginalLoanAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    BorrowLimit = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MinimumPayment = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EscrowPayment = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EscrowPaymentDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    LoanNumber = table.Column<string>(type: "TEXT", nullable: true),
                    LoanRepresentative = table.Column<string>(type: "TEXT", nullable: true),
                    LenderPhone = table.Column<string>(type: "TEXT", nullable: true),
                    LenderPhoneExt = table.Column<string>(type: "TEXT", nullable: true),
                    LenderName = table.Column<string>(type: "TEXT", nullable: true),
                    FutureAdjustDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    RateTiedTo = table.Column<string>(type: "TEXT", nullable: true),
                    Spread = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    AdjustableRate = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IsInterestOnly = table.Column<bool>(type: "INTEGER", nullable: false),
                    FutureAdjustRate = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    FuturePaymentMade = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    HasBalloonPayment = table.Column<bool>(type: "INTEGER", nullable: false),
                    Category = table.Column<string>(type: "TEXT", nullable: true),
                    AssociatedAssets = table.Column<string>(type: "TEXT", nullable: false),
                    AssociatedInvestments = table.Column<string>(type: "TEXT", nullable: false),
                    AssociatedSavings = table.Column<string>(type: "TEXT", nullable: false),
                    AssociatedIncomes = table.Column<string>(type: "TEXT", nullable: false),
                    AssociatedInsurances = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Debts", x => x.DebtId);
                });

            migrationBuilder.CreateTable(
                name: "Investments",
                columns: table => new
                {
                    InvestmentId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ContactId = table.Column<int>(type: "INTEGER", nullable: false),
                    SpouseId = table.Column<int>(type: "INTEGER", nullable: false),
                    HouseholdId = table.Column<int>(type: "INTEGER", nullable: false),
                    Timeframe = table.Column<string>(type: "TEXT", nullable: true),
                    Owner = table.Column<string>(type: "TEXT", nullable: true),
                    CurrentValue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CostBasis = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    UnrealizedGainLoss = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Category = table.Column<string>(type: "TEXT", nullable: true),
                    ReportDesc = table.Column<string>(type: "TEXT", nullable: true),
                    ReportHoverNote = table.Column<string>(type: "TEXT", nullable: true),
                    ValueDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Notes = table.Column<string>(type: "TEXT", nullable: true),
                    PurchaseDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    PurchasePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Shares = table.Column<int>(type: "INTEGER", nullable: true),
                    SharePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Symbol = table.Column<string>(type: "TEXT", nullable: true),
                    AccountType = table.Column<string>(type: "TEXT", nullable: true),
                    AccountNumber = table.Column<string>(type: "TEXT", nullable: true),
                    Institution = table.Column<string>(type: "TEXT", nullable: true),
                    AnnualReturn = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DividendYield = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ExpenseRatio = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    RiskLevel = table.Column<string>(type: "TEXT", nullable: true),
                    MaturityDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    InterestRate = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    InvestmentType = table.Column<string>(type: "TEXT", nullable: true),
                    Firm = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    Outside = table.Column<bool>(type: "INTEGER", nullable: true),
                    PreTax = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Roth = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    AfterTax = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TaxPaid = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    AssociatedAssets = table.Column<string>(type: "TEXT", nullable: false),
                    AssociatedDebts = table.Column<string>(type: "TEXT", nullable: false),
                    AssociatedSavings = table.Column<string>(type: "TEXT", nullable: false),
                    AssociatedIncomes = table.Column<string>(type: "TEXT", nullable: false),
                    AssociatedInsurances = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Investments", x => x.InvestmentId);
                });

            migrationBuilder.CreateTable(
                name: "Savings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ContactId = table.Column<int>(type: "INTEGER", nullable: false),
                    Timeframe = table.Column<string>(type: "TEXT", nullable: false),
                    Owner = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Category = table.Column<string>(type: "TEXT", nullable: false),
                    ReportDesc = table.Column<string>(type: "TEXT", nullable: false),
                    ValueDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Notes = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    AssociatedAssets = table.Column<string>(type: "TEXT", nullable: false),
                    AssociatedDebts = table.Column<string>(type: "TEXT", nullable: false),
                    AssociatedInvestments = table.Column<string>(type: "TEXT", nullable: false),
                    AssociatedIncomes = table.Column<string>(type: "TEXT", nullable: false),
                    AssociatedInsurances = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Savings", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Assets");

            migrationBuilder.DropTable(
                name: "Debts");

            migrationBuilder.DropTable(
                name: "Investments");

            migrationBuilder.DropTable(
                name: "Savings");
        }
    }
}
