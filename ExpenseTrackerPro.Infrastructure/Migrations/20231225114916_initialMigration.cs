using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ExpenseTrackerPro.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Classification = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ImageUrl = table.Column<string>(type: "text", maxLength: 200, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", maxLength: 200, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Categories_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryCurrency = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IncomeCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", maxLength: 200, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncomeCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Institutions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", maxLength: 200, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Institutions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountTypeId = table.Column<int>(type: "int", nullable: false),
                    InstitutionId = table.Column<int>(type: "int", nullable: false),
                    CurrencyId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    Balance = table.Column<float>(type: "real", nullable: false),
                    IsIncludedBalance = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accounts_AccountTypes_AccountTypeId",
                        column: x => x.AccountTypeId,
                        principalTable: "AccountTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Accounts_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Accounts_Institutions_InstitutionId",
                        column: x => x.InstitutionId,
                        principalTable: "Institutions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    Provider = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TransactionDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Amount = table.Column<float>(type: "real", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Photo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Expenses_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Expenses_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Incomes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IncomeCategoryId = table.Column<int>(type: "int", nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<float>(type: "real", nullable: false),
                    TransactionDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Photo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incomes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Incomes_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Incomes_IncomeCategories_IncomeCategoryId",
                        column: x => x.IncomeCategoryId,
                        principalTable: "IncomeCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transfers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FromAccountId = table.Column<int>(type: "int", nullable: false),
                    ToAccountId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<float>(type: "real", nullable: false),
                    TransactionDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsTransferAsExpense = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transfers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transfers_Accounts_FromAccountId",
                        column: x => x.FromAccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transfers_Accounts_ToAccountId",
                        column: x => x.ToAccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AccountTypes",
                columns: new[] { "Id", "Classification", "Created", "CreatedBy", "ImageUrl", "LastModified", "LastModifiedBy", "Name" },
                values: new object[,]
                {
                    { 1, "Cash", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3078), "System", "bank.png", null, null, "Bank Account" },
                    { 2, "Cash", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3080), "System", "cash.png", null, null, "Cash" },
                    { 3, "Cash", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3082), "System", "wallet.png", null, null, "Wallet" },
                    { 4, "Cash", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3084), "System", "checking.png", null, null, "Checking" },
                    { 5, "Cash", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3085), "System", "savings.png", null, null, "Saving" },
                    { 6, "Credit", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3087), "System", "credit.png", null, null, "Credit Card" },
                    { 7, "Credit", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3088), "System", "line-of-credit.png", null, null, "Line of Credit" },
                    { 8, "Investment", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3090), "System", "retirement.png", null, null, "Retirement" },
                    { 9, "Investment", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3091), "System", "brokerage.png", null, null, "Brokerage" },
                    { 10, "Investment", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3093), "System", "investment.png", null, null, "Investment" },
                    { 11, "Investment", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3094), "System", "insurance.png", null, null, "Insurance" },
                    { 12, "Investment", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3096), "System", "crypto.png", null, null, "Crypto" },
                    { 13, "Loans", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3097), "System", "loan.png", null, null, "Loan" },
                    { 14, "Loans", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3099), "System", "mortgage.png", null, null, "Mortgage" },
                    { 15, "Assets", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3100), "System", "property.png", null, null, "Property" },
                    { 16, "OtherAccount", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3102), "System", "bank.png", null, null, "Other Account" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Created", "CreatedBy", "ImageUrl", "LastModified", "LastModifiedBy", "Name", "ParentId" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2469), "System", "bills.png", null, null, "Bills & Utilities", null },
                    { 2, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2470), "System", "drinkanddine.png", null, null, "Drink & Dine", null },
                    { 3, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2472), "System", "education.png", null, null, "Education", null },
                    { 4, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2474), "System", "entertainment.png", null, null, "Entertainment", null },
                    { 5, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2475), "System", "events.png", null, null, "Events", null },
                    { 6, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2477), "System", "familycare.png", null, null, "Family Care", null },
                    { 7, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2479), "System", "fees.png", null, null, "Fees & Charges", null },
                    { 8, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2480), "System", "foodandgrocery.png", null, null, "Food & Grocery", null },
                    { 9, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2482), "System", "giftanddonation.png", null, null, "Gifts & Donations", null },
                    { 10, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2483), "System", "healthandfitness.png", null, null, "Health & Fitness", null },
                    { 11, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2484), "System", "house.png", null, null, "House", null },
                    { 12, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2486), "System", "insurance.png", null, null, "Insurance", null },
                    { 13, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2487), "System", "investment.png", null, null, "Investments", null },
                    { 14, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2489), "System", "kidscare.png", null, null, "Kids Care", null },
                    { 15, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2490), "System", "loan.png", null, null, "Loan & Debts", null },
                    { 16, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2491), "System", "misc.png", null, null, "Misc Expenses", null },
                    { 17, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2493), "System", "office.png", null, null, "Office Expenses", null },
                    { 18, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2494), "System", "personalcare.png", null, null, "Personal Care", null },
                    { 19, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2495), "System", "petcare.png", null, null, "Pet Care", null },
                    { 20, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2497), "System", "shopping.png", null, null, "Shopping", null },
                    { 21, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2498), "System", "taxes.png", null, null, "Taxes", null },
                    { 22, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2500), "System", "transfer.png", null, null, "Transfer", null },
                    { 23, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2501), "System", "transport.png", null, null, "Transport", null },
                    { 24, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2502), "System", "travel.png", null, null, "Travel & Vacation", null },
                    { 25, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2504), "System", "others.png", null, null, "Others", null }
                });

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Id", "Code", "CountryCurrency", "Created", "CreatedBy", "LastModified", "LastModifiedBy", "Symbol" },
                values: new object[,]
                {
                    { 1, "ALL", "Albania Lek", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2785), "System", null, null, "Lek" },
                    { 2, "AFN", "Afghanistan Afghani", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2787), "System", null, null, "؋" },
                    { 3, "ARS", "Argentina Peso", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2788), "System", null, null, "$" },
                    { 4, "AWG", "Aruba Guilder", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2789), "System", null, null, "ƒ" },
                    { 5, "AUD", "Australia Dollar", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2790), "System", null, null, "$" },
                    { 6, "AZN", "Azerbaijan Manat", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2791), "System", null, null, "₼" },
                    { 7, "BSD", "Bahamas Dollar", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2792), "System", null, null, "$" },
                    { 8, "BBD", "Barbados Dollar", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2794), "System", null, null, "$" },
                    { 9, "BYN", "Belarus Ruble", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2795), "System", null, null, "Br" },
                    { 10, "BZD", "Belize Dollar", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2796), "System", null, null, "BZ$" },
                    { 11, "BMD", "Bermuda Dollar", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2797), "System", null, null, "$" },
                    { 12, "BOB", "Bolivia Bolíviano", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2798), "System", null, null, "$b" },
                    { 13, "BAM", "Bosnia and Herzegovina Convertible Mark", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2800), "System", null, null, "KM" },
                    { 14, "BWP", "Botswana Pula", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2801), "System", null, null, "P" },
                    { 15, "BGN", "Bulgaria Lev", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2802), "System", null, null, "лв" },
                    { 16, "BRL", "Brazil Real", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2803), "System", null, null, "R$" },
                    { 17, "BND", "Brunei Darussalam Dollar", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2804), "System", null, null, "$" },
                    { 18, "KHR", "Cambodia Riel", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2806), "System", null, null, "៛" },
                    { 19, "CAD", "Canada Dollar", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2807), "System", null, null, "$" },
                    { 20, "KYD", "Cayman Islands Dollar", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2808), "System", null, null, "$" },
                    { 21, "CLP", "Chile Peso", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2809), "System", null, null, "$" },
                    { 22, "CNY", "China Yuan Renminbi", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2810), "System", null, null, "¥" },
                    { 23, "COP", "Colombia Peso", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2812), "System", null, null, "$" },
                    { 24, "CRC", "Costa Rica Colon", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2813), "System", null, null, "₡" },
                    { 25, "HRK", "Croatia Kuna", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2814), "System", null, null, "kn" },
                    { 26, "CUP", "Cuba Peso", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2815), "System", null, null, "₱" },
                    { 27, "CZK", "Czech Republic Koruna", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2816), "System", null, null, "Kč" },
                    { 28, "DKK", "Denmark Krone", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2840), "System", null, null, "kr" },
                    { 29, "DOP", "Dominican Republic Peso", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2842), "System", null, null, "RD$" },
                    { 30, "XCD", "East Caribbean Dollar", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2843), "System", null, null, "$" },
                    { 31, "EGP", "Egypt Pound", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2844), "System", null, null, "£" },
                    { 32, "SVC", "El Salvador Colon", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2845), "System", null, null, "$" },
                    { 33, "EUR", "Euro Member Countries", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2846), "System", null, null, "€" },
                    { 34, "FKP", "Falkland Islands (Malvinas) Pound", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2848), "System", null, null, "£" },
                    { 35, "FJD", "Fiji Dollar", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2849), "System", null, null, "$" },
                    { 36, "GHS", "Ghana Cedi", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2850), "System", null, null, "¢" },
                    { 37, "GIP", "Gibraltar Pound", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2851), "System", null, null, "£" },
                    { 38, "GTQ", "Guatemala Quetzal", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2852), "System", null, null, "Q" },
                    { 39, "GGP", "Guernsey Pound", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2853), "System", null, null, "£" },
                    { 40, "GYD", "Guyana Dollar", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2855), "System", null, null, "$" },
                    { 41, "HNL", "Honduras Lempira", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2856), "System", null, null, "L" },
                    { 42, "HKD", "Hong Kong Dollar", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2858), "System", null, null, "$" },
                    { 43, "HUF", "Hungary Forint", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2859), "System", null, null, "Ft" },
                    { 44, "ISK", "Iceland Krona", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2860), "System", null, null, "kr" },
                    { 45, "INR", "India Rupee", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2861), "System", null, null, "₹" },
                    { 46, "IDR", "Indonesia Rupiah", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2863), "System", null, null, "Rp" },
                    { 47, "IRR", "Iran Rial", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2864), "System", null, null, "﷼" },
                    { 48, "IMP", "Isle of Man Pound", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2865), "System", null, null, "£" },
                    { 49, "ILS", "Israel Shekel", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2866), "System", null, null, "₪" },
                    { 50, "JMD", "Jamaica Dollar", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2867), "System", null, null, "J$" },
                    { 51, "JPY", "Japan Yen", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2869), "System", null, null, "¥" },
                    { 52, "JEP", "Jersey Pound", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2870), "System", null, null, "£" },
                    { 53, "KZT", "Kazakhstan Tenge", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2871), "System", null, null, "лв" },
                    { 54, "KPW", "Korea (North) Won", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2872), "System", null, null, "₩" },
                    { 55, "KRW", "Korea (South) Won", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2873), "System", null, null, "₩" },
                    { 56, "KGS", "Kyrgyzstan Som", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2874), "System", null, null, "лв" },
                    { 57, "LAK", "Laos Kip", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2876), "System", null, null, "₭" },
                    { 58, "LBP", "Lebanon Pound", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2877), "System", null, null, "£" },
                    { 59, "LRD", "Liberia Dollar", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2878), "System", null, null, "$" },
                    { 60, "MKD", "Macedonia Denar", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2879), "System", null, null, "ден" },
                    { 61, "MYR", "Malaysia Ringgit", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2880), "System", null, null, "RM" },
                    { 62, "MUR", "Mauritius Rupee", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2882), "System", null, null, "₨" },
                    { 63, "MXN", "Mexico Peso", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2883), "System", null, null, "$" },
                    { 64, "MNT", "Mongolia Tughrik", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2884), "System", null, null, "₮" },
                    { 65, "MNT", "Moroccan-dirham", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2885), "System", null, null, " د.إ" },
                    { 66, "MZN", "Mozambique Metical", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2886), "System", null, null, "MT" },
                    { 67, "NAD", "Namibia Dollar", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2887), "System", null, null, "$" },
                    { 68, "NPR", "Nepal Rupee", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2889), "System", null, null, "₨" },
                    { 69, "ANG", "Netherlands Antilles Guilder", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2890), "System", null, null, "ƒ" },
                    { 70, "NZD", "New Zealand Dollar", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2891), "System", null, null, "$" },
                    { 71, "NIO", "Nicaragua Cordoba", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2892), "System", null, null, "C$" },
                    { 72, "NGN", "Nigeria Naira", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2893), "System", null, null, "₦" },
                    { 73, "NOK", "Norway Krone", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2894), "System", null, null, "kr" },
                    { 74, "OMR", "Oman Rial", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2896), "System", null, null, "﷼" },
                    { 75, "PKR", "Pakistan Rupee", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2897), "System", null, null, "₨" },
                    { 76, "PAB", "Panama Balboa", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2898), "System", null, null, "B/." },
                    { 77, "PYG", "Paraguay Guarani", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2899), "System", null, null, "Gs" },
                    { 78, "PEN", "Peru Sol", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2900), "System", null, null, "S/." },
                    { 79, "PHP", "Philippines Peso", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2901), "System", null, null, "₱" },
                    { 80, "PLN", "Poland Zloty", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2903), "System", null, null, "zł" },
                    { 81, "QAR", "Qatar Riyal", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2904), "System", null, null, "﷼" },
                    { 82, "RON", "Romania Leu", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2905), "System", null, null, "lei" },
                    { 83, "RUB", "Russia Ruble", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2906), "System", null, null, "₽" },
                    { 84, "SHP", "Saint Helena Pound", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2907), "System", null, null, "£" },
                    { 85, "SAR", "Saudi Arabia Riyal", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2908), "System", null, null, "﷼" },
                    { 86, "RSD", "Serbia Dinar", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2910), "System", null, null, "Дин." },
                    { 87, "SCR", "Seychelles Rupee", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2911), "System", null, null, "₨" },
                    { 88, "SGD", "Singapore Dollar", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2912), "System", null, null, "$" },
                    { 89, "SBD", "Solomon Islands Dollar", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2913), "System", null, null, "$" },
                    { 90, "SOS", "Somalia Shilling", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2914), "System", null, null, "S" },
                    { 91, "KRW", "South Korean Won", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2915), "System", null, null, "₩" },
                    { 92, "ZAR", "South Africa Rand", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2917), "System", null, null, "R" },
                    { 93, "LKR", "Sri Lanka Rupee", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2918), "System", null, null, "₨" },
                    { 94, "SEK", "Sweden Krona", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2919), "System", null, null, "kr" },
                    { 95, "CHF", "Switzerland Franc", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2920), "System", null, null, "CHF" },
                    { 96, "SRD", "Suriname Dollar", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2921), "System", null, null, "$" },
                    { 97, "SYP", "Syria Pound", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2922), "System", null, null, "£" },
                    { 98, "TWD", "Taiwan New Dollar", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2923), "System", null, null, "NT$" },
                    { 99, "THB", "Thailand Baht", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2925), "System", null, null, "฿" },
                    { 100, "TTD", "Trinidad and Tobago Dollar", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2926), "System", null, null, "TT$" },
                    { 101, "TRY", "Turkey Lira", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2927), "System", null, null, "₺" },
                    { 102, "TVD", "Tuvalu Dollar", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2928), "System", null, null, "$" },
                    { 103, "UAH", "Ukraine Hryvnia", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2929), "System", null, null, "₴" },
                    { 104, "AED", "UAE-Dirham", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2930), "System", null, null, " د.إ" },
                    { 105, "GBP", "United Kingdom Pound", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2932), "System", null, null, "£" },
                    { 106, "USD", "United States Dollar", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2933), "System", null, null, "$" },
                    { 107, "UYU", "Uruguay Peso", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2934), "System", null, null, "$U" },
                    { 108, "UZS", "Uzbekistan Som", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2935), "System", null, null, "лв" },
                    { 109, "VEF", "Venezuela Bolívar", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2936), "System", null, null, "Bs" },
                    { 110, "VND", "Viet Nam Dong", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2938), "System", null, null, "₫" },
                    { 111, "YER", "Yemen Rial", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2939), "System", null, null, "﷼" },
                    { 112, "ZWD", "Zimbabwe Dollar", new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2940), "System", null, null, "Z$" }
                });

            migrationBuilder.InsertData(
                table: "IncomeCategories",
                columns: new[] { "Id", "Created", "CreatedBy", "ImageUrl", "LastModified", "LastModifiedBy", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3144), "System", "bonus.png", null, null, "Bonus" },
                    { 2, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3145), "System", "brokerage.png", null, null, "Brokerage" },
                    { 3, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3146), "System", "business.png", null, null, "Business & Profession" },
                    { 4, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3148), "System", "coupon.png", null, null, "Coupons" },
                    { 5, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3149), "System", "credit.png", null, null, "Credit" },
                    { 6, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3150), "System", "gift.png", null, null, "Gifts" },
                    { 7, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3151), "System", "interest.png", null, null, "Interest" },
                    { 8, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3152), "System", "investment.png", null, null, "Investments" },
                    { 9, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3153), "System", "loan.png", null, null, "Loan" },
                    { 10, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3154), "System", "gambling.png", null, null, "Lottery, Gambling" },
                    { 11, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3155), "System", "mutualfunds.png", null, null, "Mutual Funds" },
                    { 12, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3157), "System", "refund.png", null, null, "Refunds" },
                    { 13, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3158), "System", "reimbursement.png", null, null, "Reimbursement" },
                    { 14, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3159), "System", "rental.png", null, null, "Rental Income" },
                    { 15, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3160), "System", "salary.png", null, null, "Salary &Paycheck" },
                    { 16, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3161), "System", "savings.png", null, null, "Savings" },
                    { 17, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3162), "System", "selling.png", null, null, "Selling Income" },
                    { 18, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3163), "System", "transfer.png", null, null, "Transfer" },
                    { 19, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3165), "System", "wage.png", null, null, "Wages & Tips" },
                    { 20, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3166), "System", "others.png", null, null, "Others" }
                });

            migrationBuilder.InsertData(
                table: "Institutions",
                columns: new[] { "Id", "Created", "CreatedBy", "ImageUrl", "LastModified", "LastModifiedBy", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3209), "System", "abcapital.png", null, null, "AB Capital" },
                    { 2, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3210), "System", "aub.png", null, null, "AUB" },
                    { 3, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3211), "System", "amex.png", null, null, "American Express" },
                    { 4, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3212), "System", "applecard.png", null, null, "Apple Card" },
                    { 5, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3214), "System", "atome.jfif", null, null, "Atome" },
                    { 6, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3215), "System", "bdo.png", null, null, "BDO" },
                    { 7, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3216), "System", "bpi.png", null, null, "BPI" },
                    { 8, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3217), "System", "bankofcommerce.jfif", null, null, "Bank of Commerce" },
                    { 9, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3218), "System", "bankofmakati.png", null, null, "Bank of Makati" },
                    { 10, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3219), "System", "barclays.jfif", null, null, "Barclays" },
                    { 11, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3221), "System", "bayad.png", null, null, "Bayad" },
                    { 12, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3222), "System", "billease.png", null, null, "Billease" },
                    { 13, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3248), "System", "binance.png", null, null, "Binance Exchange" },
                    { 14, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3249), "System", "others.jfif", null, null, "CARD Bank" },
                    { 15, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3250), "System", "cimb.png", null, null, "CIMB" },
                    { 16, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3251), "System", "colfinancial.png", null, null, "COL Financial" },
                    { 17, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3252), "System", "cashalo.jfif", null, null, "Cashalo" },
                    { 18, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3253), "System", "cebuana.png", null, null, "Cebuana Lhullier" },
                    { 19, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3255), "System", "chinabank.jfif", null, null, "China Bank" },
                    { 20, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3257), "System", "citibank.jfif", null, null, "Citibank" },
                    { 21, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3258), "System", "cliqq.jfif", null, null, "CliQQ" },
                    { 22, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3259), "System", "coinbase.png", null, null, "Coinbase" },
                    { 23, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3260), "System", "coinph.jfif", null, null, "Coins.ph" },
                    { 24, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3261), "System", "deutsche.png", null, null, "Deutche" },
                    { 25, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3262), "System", "diskarTech.jfif", null, null, "DiskarTech" },
                    { 26, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3263), "System", "others.jfif", null, null, "DragonFi" },
                    { 27, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3265), "System", "eastwest.jfif", null, null, "EastWest Bank" },
                    { 28, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3266), "System", "ficco.png", null, null, "Ficco" },
                    { 29, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3267), "System", "gcash.png", null, null, "Gcash" },
                    { 30, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3268), "System", "gotrade.png", null, null, "GoTrade" },
                    { 31, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3269), "System", "gotyme.png", null, null, "GoTyme Bank" },
                    { 32, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3270), "System", "grab.jfif", null, null, "GrabPay" },
                    { 33, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3272), "System", "homecredit.jfif", null, null, "Home Credit" },
                    { 34, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3273), "System", "hsbc.png", null, null, "HSBC" },
                    { 35, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3274), "System", "ing.jfif", null, null, "ING" },
                    { 36, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3275), "System", "ing.jfif", null, null, "ING Bank" },
                    { 37, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3276), "System", "komo.jfif", null, null, "Komo" },
                    { 38, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3277), "System", "kucoin.png", null, null, "KuCoin" },
                    { 39, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3278), "System", "landbank.jfif", null, null, "Landbank" },
                    { 40, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3279), "System", "lazada.jfif", null, null, "Lazada" },
                    { 41, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3281), "System", "mastercard.png", null, null, "Mastercard" },
                    { 42, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3282), "System", "maya.png", null, null, "Maya" },
                    { 43, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3283), "System", "maybank.png", null, null, "Maybank" },
                    { 44, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3284), "System", "metrobank.png", null, null, "Metrobank" },
                    { 45, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3285), "System", "netbank.png", null, null, "Netbank" },
                    { 46, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3286), "System", "ownbank.jfif", null, null, "OwnBank" },
                    { 47, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3287), "System", "pbcom.jfif", null, null, "PBCOM" },
                    { 48, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3288), "System", "pnb.png", null, null, "PNB" },
                    { 49, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3290), "System", "psbank.jfif", null, null, "PSBank" },
                    { 50, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3291), "System", "pagibig.jfif", null, null, "Pag-Ibig" },
                    { 51, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3292), "System", "paymaya.png", null, null, "PayMaya" },
                    { 52, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3293), "System", "paypal.png", null, null, "PayPal" },
                    { 53, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3294), "System", "plentina.png", null, null, "Pletina" },
                    { 54, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3295), "System", "rcbc.jfif", null, null, "RCBC" },
                    { 55, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3296), "System", "robinsonsbank.png", null, null, "RobinsonsBank" },
                    { 56, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3298), "System", "seabank.png", null, null, "Seabank" },
                    { 57, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3299), "System", "securitybank.jfif", null, null, "Security Bank" },
                    { 58, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3300), "System", "shopeepay.jfif", null, null, "ShopeePay" },
                    { 59, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3301), "System", "standardchartered.png", null, null, "Standard Chartered" },
                    { 60, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3302), "System", "sterlingbank.jfif", null, null, "Sterling Bank" },
                    { 61, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3303), "System", "tala.png", null, null, "Tala" },
                    { 62, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3304), "System", "tonik.png", null, null, "Tonik" },
                    { 63, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3305), "System", "ucpb.png", null, null, "UCPB" },
                    { 64, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3307), "System", "uno.jfif", null, null, "UNO Digital Bank" },
                    { 65, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3308), "System", "unionbank.jfif", null, null, "Unionbank" },
                    { 66, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3309), "System", "visa.jfif", null, null, "Visa" },
                    { 67, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3310), "System", "wellsfargo.png", null, null, "Wells Fargo" },
                    { 68, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3312), "System", "others.jfif", null, null, "ztock" },
                    { 69, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(3313), "System", "others.jfif", null, null, "Others" }
                });

            migrationBuilder.InsertData(
                table: "UserProfiles",
                columns: new[] { "Id", "Created", "CreatedBy", "Email", "FirstName", "ImageUrl", "IsActive", "LastModified", "LastModifiedBy", "LastName", "Mobile" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2296), "System", "system@yahoo.com", "System", "", true, null, null, "", "+639267444551" },
                    { 2, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2307), "System", "nathan.pascual20@yahoo.com", "Nathan", "", true, null, null, "Pascual", "+639267444551" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Created", "CreatedBy", "ImageUrl", "LastModified", "LastModifiedBy", "Name", "ParentId" },
                values: new object[,]
                {
                    { 26, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2505), "System", "electric.png", null, null, "Electricity", 1 },
                    { 27, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2507), "System", "gas.png", null, null, "Gas", 1 },
                    { 28, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2508), "System", "internet.png", null, null, "Internet", 1 },
                    { 29, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2510), "System", "mobile.png", null, null, "Mobile", 1 },
                    { 30, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2511), "System", "telephone.png", null, null, "Phone", 1 },
                    { 31, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2569), "System", "water.png", null, null, "Water", 1 },
                    { 32, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2570), "System", "alcoholic-drink.png", null, null, "Alcohol & Bar", 2 },
                    { 33, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2572), "System", "coffee.png", null, null, "Coffee shops", 2 },
                    { 34, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2573), "System", "fastfood.png", null, null, "Fast Food", 2 },
                    { 35, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2575), "System", "restaurant.png", null, null, "Restaurant", 2 },
                    { 36, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2577), "System", "books.png", null, null, "Books & Stationery", 3 },
                    { 37, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2578), "System", "schoolfee.png", null, null, "School Fee", 3 },
                    { 38, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2579), "System", "tuition.png", null, null, "Tuition Fee", 3 },
                    { 39, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2581), "System", "amusement.png", null, null, "Amusement", 4 },
                    { 40, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2582), "System", "arts.png", null, null, "Arts", 4 },
                    { 41, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2584), "System", "cable.png", null, null, "Cable or DTH", 4 },
                    { 42, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2585), "System", "movies.png", null, null, "Movies & Cinema", 4 },
                    { 43, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2587), "System", "music.png", null, null, "Music", 4 },
                    { 44, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2588), "System", "newspaper.png", null, null, "Newspapers & Magazines", 4 },
                    { 45, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2590), "System", "games.png", null, null, "Games", 4 },
                    { 46, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2591), "System", "happybirthday.png", null, null, "Birthday", 5 },
                    { 47, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2593), "System", "gettogether.png", null, null, "Get Together", 5 },
                    { 48, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2594), "System", "wedding.png", null, null, "Wedding", 5 },
                    { 49, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2596), "System", "kidsactivities.png", null, null, "Kids Activities", 6 },
                    { 50, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2597), "System", "oldagecare.png", null, null, "Old age care", 6 },
                    { 51, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2599), "System", "atm.png", null, null, "ATM Fee", 7 },
                    { 52, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2600), "System", "commission.png", null, null, "Commission Fee", 7 },
                    { 53, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2602), "System", "latefee.png", null, null, "Late Fee", 7 },
                    { 54, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2603), "System", "servicefee.png", null, null, "Service Fee", 7 },
                    { 55, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2604), "System", "charity.png", null, null, "Charity", 9 },
                    { 56, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2606), "System", "gift.png", null, null, "Gift", 9 },
                    { 57, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2607), "System", "dentist.png", null, null, "Dentist", 10 },
                    { 58, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2609), "System", "doctor.png", null, null, "Doctor", 10 },
                    { 59, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2610), "System", "gym.png", null, null, "Gym", 10 },
                    { 60, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2612), "System", "pharmacy.png", null, null, "Pharmacy", 10 },
                    { 61, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2613), "System", "spamassage.png", null, null, "Spa & Massage", 10 },
                    { 62, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2615), "System", "housemaintenance.png", null, null, "House Maintenance", 11 },
                    { 63, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2616), "System", "rent.png", null, null, "House Rent", 11 },
                    { 64, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2618), "System", "autoinsurance.png", null, null, "Auto Insurance", 12 },
                    { 65, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2619), "System", "healthinsurance.png", null, null, "Health Insurance", 12 },
                    { 66, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2621), "System", "propertyinsurance.png", null, null, "Property Insurance", 12 },
                    { 67, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2622), "System", "carloan.png", null, null, "Car Loan", 15 },
                    { 68, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2624), "System", "credit.png", null, null, "Credit Card", 15 },
                    { 69, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2625), "System", "homeloan.png", null, null, "Home Loan", 15 },
                    { 70, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2626), "System", "loan.png", null, null, "Loan", 15 },
                    { 71, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2628), "System", "hairsalon.png", null, null, "Hair & Salon", 18 },
                    { 72, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2629), "System", "laundry.png", null, null, "Laundry", 18 },
                    { 73, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2631), "System", "clothing.png", null, null, "Clothing", 20 },
                    { 74, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2632), "System", "electronics.png", null, null, "Electronics & Accessories", 20 },
                    { 75, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2634), "System", "giftstoys.png", null, null, "Gifts &  Toys", 20 },
                    { 76, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2635), "System", "healthandbeauty.png", null, null, "Health & Beauty", 20 },
                    { 77, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2637), "System", "homeandfurnishing.png", null, null, "Home & furnishing", 20 },
                    { 78, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2638), "System", "jewelry.png", null, null, "Jewellery", 20 },
                    { 79, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2640), "System", "lawnandgarden.png", null, null, "Lawn & Garden", 20 },
                    { 80, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2672), "System", "pets.png", null, null, "Pets & Animals", 20 },
                    { 81, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2673), "System", "sports.png", null, null, "Sports", 20 },
                    { 82, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2675), "System", "withholdingtaxes.png", null, null, "Withholding Tax", 21 },
                    { 83, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2676), "System", "localtaxes.png", null, null, "Local Tax", 21 },
                    { 84, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2678), "System", "propertytax.png", null, null, "Property Tax", 21 },
                    { 85, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2679), "System", "salestax.png", null, null, "Sales Tax", 21 },
                    { 86, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2681), "System", "carmaintenance.png", null, null, "Car Maintenance", 23 },
                    { 87, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2682), "System", "fuel.png", null, null, "Fuel & Gas", 23 },
                    { 88, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2684), "System", "publictransport.png", null, null, "Public Transport", 23 },
                    { 89, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2685), "System", "taxi.png", null, null, "Taxi", 23 },
                    { 90, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2687), "System", "tnvs.png", null, null, "TNVS", 23 },
                    { 91, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2688), "System", "airtravel.png", null, null, "Air Travel", 24 },
                    { 92, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2689), "System", "hotel.png", null, null, "Hotel", 24 },
                    { 93, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2691), "System", "seatravel.png", null, null, "Sea Travel", 24 },
                    { 94, new DateTime(2023, 12, 25, 19, 49, 16, 208, DateTimeKind.Local).AddTicks(2692), "System", "tnvs.png", null, null, "Rental Car", 24 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_AccountTypeId",
                table: "Accounts",
                column: "AccountTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_CurrencyId",
                table: "Accounts",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_InstitutionId",
                table: "Accounts",
                column: "InstitutionId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentId",
                table: "Categories",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_AccountId",
                table: "Expenses",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_CategoryId",
                table: "Expenses",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Incomes_AccountId",
                table: "Incomes",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Incomes_IncomeCategoryId",
                table: "Incomes",
                column: "IncomeCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_FromAccountId",
                table: "Transfers",
                column: "FromAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_ToAccountId",
                table: "Transfers",
                column: "ToAccountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropTable(
                name: "Incomes");

            migrationBuilder.DropTable(
                name: "Transfers");

            migrationBuilder.DropTable(
                name: "UserProfiles");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "IncomeCategories");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "AccountTypes");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropTable(
                name: "Institutions");
        }
    }
}
