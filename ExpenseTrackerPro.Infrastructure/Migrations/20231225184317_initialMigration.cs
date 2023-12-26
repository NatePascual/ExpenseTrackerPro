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
                name: "Transaction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    TransactionType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AsSender = table.Column<bool>(type: "bit", nullable: false),
                    AsReceiver = table.Column<bool>(type: "bit", nullable: false),
                    TransactionDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Amount = table.Column<float>(type: "real", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transaction_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transfers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderId = table.Column<int>(type: "int", nullable: false),
                    ReceiverId = table.Column<int>(type: "int", nullable: false),
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
                        name: "FK_Transfers_Accounts_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transfers_Accounts_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AccountTypes",
                columns: new[] { "Id", "Classification", "Created", "CreatedBy", "ImageUrl", "LastModified", "LastModifiedBy", "Name" },
                values: new object[,]
                {
                    { 1, "Cash", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7960), "System", "bank.png", null, null, "Bank Account" },
                    { 2, "Cash", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7962), "System", "cash.png", null, null, "Cash" },
                    { 3, "Cash", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7963), "System", "wallet.png", null, null, "Wallet" },
                    { 4, "Cash", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7965), "System", "checking.png", null, null, "Checking" },
                    { 5, "Cash", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7967), "System", "savings.png", null, null, "Saving" },
                    { 6, "Credit", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7968), "System", "credit.png", null, null, "Credit Card" },
                    { 7, "Credit", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7970), "System", "line-of-credit.png", null, null, "Line of Credit" },
                    { 8, "Investment", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7971), "System", "retirement.png", null, null, "Retirement" },
                    { 9, "Investment", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7973), "System", "brokerage.png", null, null, "Brokerage" },
                    { 10, "Investment", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7974), "System", "investment.png", null, null, "Investment" },
                    { 11, "Investment", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7976), "System", "insurance.png", null, null, "Insurance" },
                    { 12, "Investment", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7977), "System", "crypto.png", null, null, "Crypto" },
                    { 13, "Loans", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7979), "System", "loan.png", null, null, "Loan" },
                    { 14, "Loans", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7980), "System", "mortgage.png", null, null, "Mortgage" },
                    { 15, "Assets", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7982), "System", "property.png", null, null, "Property" },
                    { 16, "OtherAccount", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7983), "System", "bank.png", null, null, "Other Account" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Created", "CreatedBy", "ImageUrl", "LastModified", "LastModifiedBy", "Name", "ParentId" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7340), "System", "bills.png", null, null, "Bills & Utilities", null },
                    { 2, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7342), "System", "drinkanddine.png", null, null, "Drink & Dine", null },
                    { 3, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7343), "System", "education.png", null, null, "Education", null },
                    { 4, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7345), "System", "entertainment.png", null, null, "Entertainment", null },
                    { 5, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7346), "System", "events.png", null, null, "Events", null },
                    { 6, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7347), "System", "familycare.png", null, null, "Family Care", null },
                    { 7, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7349), "System", "fees.png", null, null, "Fees & Charges", null },
                    { 8, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7350), "System", "foodandgrocery.png", null, null, "Food & Grocery", null },
                    { 9, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7352), "System", "giftanddonation.png", null, null, "Gifts & Donations", null },
                    { 10, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7353), "System", "healthandfitness.png", null, null, "Health & Fitness", null },
                    { 11, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7356), "System", "house.png", null, null, "House", null },
                    { 12, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7357), "System", "insurance.png", null, null, "Insurance", null },
                    { 13, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7359), "System", "investment.png", null, null, "Investments", null },
                    { 14, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7360), "System", "kidscare.png", null, null, "Kids Care", null },
                    { 15, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7362), "System", "loan.png", null, null, "Loan & Debts", null },
                    { 16, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7363), "System", "misc.png", null, null, "Misc Expenses", null },
                    { 17, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7364), "System", "office.png", null, null, "Office Expenses", null },
                    { 18, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7366), "System", "personalcare.png", null, null, "Personal Care", null },
                    { 19, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7367), "System", "petcare.png", null, null, "Pet Care", null },
                    { 20, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7368), "System", "shopping.png", null, null, "Shopping", null },
                    { 21, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7370), "System", "taxes.png", null, null, "Taxes", null },
                    { 22, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7371), "System", "transfer.png", null, null, "Transfer", null },
                    { 23, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7373), "System", "transport.png", null, null, "Transport", null },
                    { 24, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7374), "System", "travel.png", null, null, "Travel & Vacation", null },
                    { 25, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7375), "System", "others.png", null, null, "Others", null }
                });

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Id", "Code", "CountryCurrency", "Created", "CreatedBy", "LastModified", "LastModifiedBy", "Symbol" },
                values: new object[,]
                {
                    { 1, "ALL", "Albania Lek", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7635), "System", null, null, "Lek" },
                    { 2, "AFN", "Afghanistan Afghani", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7636), "System", null, null, "؋" },
                    { 3, "ARS", "Argentina Peso", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7638), "System", null, null, "$" },
                    { 4, "AWG", "Aruba Guilder", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7639), "System", null, null, "ƒ" },
                    { 5, "AUD", "Australia Dollar", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7640), "System", null, null, "$" },
                    { 6, "AZN", "Azerbaijan Manat", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7641), "System", null, null, "₼" },
                    { 7, "BSD", "Bahamas Dollar", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7643), "System", null, null, "$" },
                    { 8, "BBD", "Barbados Dollar", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7644), "System", null, null, "$" },
                    { 9, "BYN", "Belarus Ruble", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7645), "System", null, null, "Br" },
                    { 10, "BZD", "Belize Dollar", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7646), "System", null, null, "BZ$" },
                    { 11, "BMD", "Bermuda Dollar", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7647), "System", null, null, "$" },
                    { 12, "BOB", "Bolivia Bolíviano", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7649), "System", null, null, "$b" },
                    { 13, "BAM", "Bosnia and Herzegovina Convertible Mark", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7650), "System", null, null, "KM" },
                    { 14, "BWP", "Botswana Pula", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7651), "System", null, null, "P" },
                    { 15, "BGN", "Bulgaria Lev", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7652), "System", null, null, "лв" },
                    { 16, "BRL", "Brazil Real", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7653), "System", null, null, "R$" },
                    { 17, "BND", "Brunei Darussalam Dollar", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7655), "System", null, null, "$" },
                    { 18, "KHR", "Cambodia Riel", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7656), "System", null, null, "៛" },
                    { 19, "CAD", "Canada Dollar", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7657), "System", null, null, "$" },
                    { 20, "KYD", "Cayman Islands Dollar", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7658), "System", null, null, "$" },
                    { 21, "CLP", "Chile Peso", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7659), "System", null, null, "$" },
                    { 22, "CNY", "China Yuan Renminbi", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7661), "System", null, null, "¥" },
                    { 23, "COP", "Colombia Peso", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7662), "System", null, null, "$" },
                    { 24, "CRC", "Costa Rica Colon", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7663), "System", null, null, "₡" },
                    { 25, "HRK", "Croatia Kuna", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7664), "System", null, null, "kn" },
                    { 26, "CUP", "Cuba Peso", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7665), "System", null, null, "₱" },
                    { 27, "CZK", "Czech Republic Koruna", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7667), "System", null, null, "Kč" },
                    { 28, "DKK", "Denmark Krone", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7668), "System", null, null, "kr" },
                    { 29, "DOP", "Dominican Republic Peso", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7669), "System", null, null, "RD$" },
                    { 30, "XCD", "East Caribbean Dollar", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7670), "System", null, null, "$" },
                    { 31, "EGP", "Egypt Pound", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7671), "System", null, null, "£" },
                    { 32, "SVC", "El Salvador Colon", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7673), "System", null, null, "$" },
                    { 33, "EUR", "Euro Member Countries", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7674), "System", null, null, "€" },
                    { 34, "FKP", "Falkland Islands (Malvinas) Pound", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7675), "System", null, null, "£" },
                    { 35, "FJD", "Fiji Dollar", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7676), "System", null, null, "$" },
                    { 36, "GHS", "Ghana Cedi", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7677), "System", null, null, "¢" },
                    { 37, "GIP", "Gibraltar Pound", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7678), "System", null, null, "£" },
                    { 38, "GTQ", "Guatemala Quetzal", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7680), "System", null, null, "Q" },
                    { 39, "GGP", "Guernsey Pound", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7681), "System", null, null, "£" },
                    { 40, "GYD", "Guyana Dollar", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7682), "System", null, null, "$" },
                    { 41, "HNL", "Honduras Lempira", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7703), "System", null, null, "L" },
                    { 42, "HKD", "Hong Kong Dollar", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7704), "System", null, null, "$" },
                    { 43, "HUF", "Hungary Forint", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7706), "System", null, null, "Ft" },
                    { 44, "ISK", "Iceland Krona", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7707), "System", null, null, "kr" },
                    { 45, "INR", "India Rupee", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7708), "System", null, null, "₹" },
                    { 46, "IDR", "Indonesia Rupiah", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7709), "System", null, null, "Rp" },
                    { 47, "IRR", "Iran Rial", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7711), "System", null, null, "﷼" },
                    { 48, "IMP", "Isle of Man Pound", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7712), "System", null, null, "£" },
                    { 49, "ILS", "Israel Shekel", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7713), "System", null, null, "₪" },
                    { 50, "JMD", "Jamaica Dollar", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7714), "System", null, null, "J$" },
                    { 51, "JPY", "Japan Yen", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7715), "System", null, null, "¥" },
                    { 52, "JEP", "Jersey Pound", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7717), "System", null, null, "£" },
                    { 53, "KZT", "Kazakhstan Tenge", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7718), "System", null, null, "лв" },
                    { 54, "KPW", "Korea (North) Won", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7719), "System", null, null, "₩" },
                    { 55, "KRW", "Korea (South) Won", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7720), "System", null, null, "₩" },
                    { 56, "KGS", "Kyrgyzstan Som", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7721), "System", null, null, "лв" },
                    { 57, "LAK", "Laos Kip", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7722), "System", null, null, "₭" },
                    { 58, "LBP", "Lebanon Pound", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7724), "System", null, null, "£" },
                    { 59, "LRD", "Liberia Dollar", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7725), "System", null, null, "$" },
                    { 60, "MKD", "Macedonia Denar", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7726), "System", null, null, "ден" },
                    { 61, "MYR", "Malaysia Ringgit", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7727), "System", null, null, "RM" },
                    { 62, "MUR", "Mauritius Rupee", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7728), "System", null, null, "₨" },
                    { 63, "MXN", "Mexico Peso", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7730), "System", null, null, "$" },
                    { 64, "MNT", "Mongolia Tughrik", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7731), "System", null, null, "₮" },
                    { 65, "MNT", "Moroccan-dirham", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7732), "System", null, null, " د.إ" },
                    { 66, "MZN", "Mozambique Metical", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7733), "System", null, null, "MT" },
                    { 67, "NAD", "Namibia Dollar", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7734), "System", null, null, "$" },
                    { 68, "NPR", "Nepal Rupee", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7736), "System", null, null, "₨" },
                    { 69, "ANG", "Netherlands Antilles Guilder", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7737), "System", null, null, "ƒ" },
                    { 70, "NZD", "New Zealand Dollar", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7738), "System", null, null, "$" },
                    { 71, "NIO", "Nicaragua Cordoba", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7739), "System", null, null, "C$" },
                    { 72, "NGN", "Nigeria Naira", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7740), "System", null, null, "₦" },
                    { 73, "NOK", "Norway Krone", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7742), "System", null, null, "kr" },
                    { 74, "OMR", "Oman Rial", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7743), "System", null, null, "﷼" },
                    { 75, "PKR", "Pakistan Rupee", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7744), "System", null, null, "₨" },
                    { 76, "PAB", "Panama Balboa", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7745), "System", null, null, "B/." },
                    { 77, "PYG", "Paraguay Guarani", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7746), "System", null, null, "Gs" },
                    { 78, "PEN", "Peru Sol", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7748), "System", null, null, "S/." },
                    { 79, "PHP", "Philippines Peso", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7749), "System", null, null, "₱" },
                    { 80, "PLN", "Poland Zloty", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7750), "System", null, null, "zł" },
                    { 81, "QAR", "Qatar Riyal", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7751), "System", null, null, "﷼" },
                    { 82, "RON", "Romania Leu", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7752), "System", null, null, "lei" },
                    { 83, "RUB", "Russia Ruble", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7754), "System", null, null, "₽" },
                    { 84, "SHP", "Saint Helena Pound", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7755), "System", null, null, "£" },
                    { 85, "SAR", "Saudi Arabia Riyal", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7756), "System", null, null, "﷼" },
                    { 86, "RSD", "Serbia Dinar", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7757), "System", null, null, "Дин." },
                    { 87, "SCR", "Seychelles Rupee", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7758), "System", null, null, "₨" },
                    { 88, "SGD", "Singapore Dollar", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7760), "System", null, null, "$" },
                    { 89, "SBD", "Solomon Islands Dollar", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7761), "System", null, null, "$" },
                    { 90, "SOS", "Somalia Shilling", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7762), "System", null, null, "S" },
                    { 91, "KRW", "South Korean Won", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7763), "System", null, null, "₩" },
                    { 92, "ZAR", "South Africa Rand", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7764), "System", null, null, "R" },
                    { 93, "LKR", "Sri Lanka Rupee", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7765), "System", null, null, "₨" },
                    { 94, "SEK", "Sweden Krona", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7767), "System", null, null, "kr" },
                    { 95, "CHF", "Switzerland Franc", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7768), "System", null, null, "CHF" },
                    { 96, "SRD", "Suriname Dollar", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7769), "System", null, null, "$" },
                    { 97, "SYP", "Syria Pound", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7770), "System", null, null, "£" },
                    { 98, "TWD", "Taiwan New Dollar", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7771), "System", null, null, "NT$" },
                    { 99, "THB", "Thailand Baht", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7772), "System", null, null, "฿" },
                    { 100, "TTD", "Trinidad and Tobago Dollar", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7774), "System", null, null, "TT$" },
                    { 101, "TRY", "Turkey Lira", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7775), "System", null, null, "₺" },
                    { 102, "TVD", "Tuvalu Dollar", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7776), "System", null, null, "$" },
                    { 103, "UAH", "Ukraine Hryvnia", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7777), "System", null, null, "₴" },
                    { 104, "AED", "UAE-Dirham", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7778), "System", null, null, " د.إ" },
                    { 105, "GBP", "United Kingdom Pound", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7780), "System", null, null, "£" },
                    { 106, "USD", "United States Dollar", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7781), "System", null, null, "$" },
                    { 107, "UYU", "Uruguay Peso", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7782), "System", null, null, "$U" },
                    { 108, "UZS", "Uzbekistan Som", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7783), "System", null, null, "лв" },
                    { 109, "VEF", "Venezuela Bolívar", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7784), "System", null, null, "Bs" },
                    { 110, "VND", "Viet Nam Dong", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7785), "System", null, null, "₫" },
                    { 111, "YER", "Yemen Rial", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7786), "System", null, null, "﷼" },
                    { 112, "ZWD", "Zimbabwe Dollar", new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7788), "System", null, null, "Z$" }
                });

            migrationBuilder.InsertData(
                table: "IncomeCategories",
                columns: new[] { "Id", "Created", "CreatedBy", "ImageUrl", "LastModified", "LastModifiedBy", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(8025), "System", "bonus.png", null, null, "Bonus" },
                    { 2, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(8027), "System", "brokerage.png", null, null, "Brokerage" },
                    { 3, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(8028), "System", "business.png", null, null, "Business & Profession" },
                    { 4, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(8030), "System", "coupon.png", null, null, "Coupons" },
                    { 5, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(8031), "System", "credit.png", null, null, "Credit" },
                    { 6, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(8032), "System", "gift.png", null, null, "Gifts" },
                    { 7, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(8033), "System", "interest.png", null, null, "Interest" },
                    { 8, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(8034), "System", "investment.png", null, null, "Investments" },
                    { 9, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(8036), "System", "loan.png", null, null, "Loan" },
                    { 10, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(8037), "System", "gambling.png", null, null, "Lottery, Gambling" },
                    { 11, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(8038), "System", "mutualfunds.png", null, null, "Mutual Funds" },
                    { 12, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(8039), "System", "refund.png", null, null, "Refunds" },
                    { 13, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(8040), "System", "reimbursement.png", null, null, "Reimbursement" },
                    { 14, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(8042), "System", "rental.png", null, null, "Rental Income" },
                    { 15, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(8043), "System", "salary.png", null, null, "Salary &Paycheck" },
                    { 16, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(8044), "System", "savings.png", null, null, "Savings" },
                    { 17, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(8045), "System", "selling.png", null, null, "Selling Income" },
                    { 18, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(8046), "System", "transfer.png", null, null, "Transfer" },
                    { 19, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(8047), "System", "wage.png", null, null, "Wages & Tips" },
                    { 20, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(8049), "System", "others.png", null, null, "Others" }
                });

            migrationBuilder.InsertData(
                table: "Institutions",
                columns: new[] { "Id", "Created", "CreatedBy", "ImageUrl", "LastModified", "LastModifiedBy", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(8091), "System", "abcapital.png", null, null, "AB Capital" },
                    { 2, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(8092), "System", "aub.png", null, null, "AUB" },
                    { 3, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(8093), "System", "amex.png", null, null, "American Express" },
                    { 4, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(8095), "System", "applecard.png", null, null, "Apple Card" },
                    { 5, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(8096), "System", "atome.jfif", null, null, "Atome" },
                    { 6, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(8097), "System", "bdo.png", null, null, "BDO" },
                    { 7, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(8098), "System", "bpi.png", null, null, "BPI" },
                    { 8, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(8099), "System", "bankofcommerce.jfif", null, null, "Bank of Commerce" },
                    { 9, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(8100), "System", "bankofmakati.png", null, null, "Bank of Makati" },
                    { 10, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(8102), "System", "barclays.jfif", null, null, "Barclays" },
                    { 11, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(8103), "System", "bayad.png", null, null, "Bayad" },
                    { 12, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(8104), "System", "billease.png", null, null, "Billease" },
                    { 13, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(8105), "System", "binance.png", null, null, "Binance Exchange" },
                    { 14, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(8106), "System", "others.jfif", null, null, "CARD Bank" },
                    { 15, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(8107), "System", "cimb.png", null, null, "CIMB" },
                    { 16, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(8108), "System", "colfinancial.png", null, null, "COL Financial" },
                    { 17, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(8109), "System", "cashalo.jfif", null, null, "Cashalo" },
                    { 18, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(8111), "System", "cebuana.png", null, null, "Cebuana Lhullier" },
                    { 19, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(8112), "System", "chinabank.jfif", null, null, "China Bank" },
                    { 20, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(8113), "System", "citibank.jfif", null, null, "Citibank" },
                    { 21, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(8114), "System", "cliqq.jfif", null, null, "CliQQ" },
                    { 22, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(8115), "System", "coinbase.png", null, null, "Coinbase" },
                    { 23, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(8152), "System", "coinph.jfif", null, null, "Coins.ph" },
                    { 24, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(8153), "System", "deutsche.png", null, null, "Deutche" },
                    { 25, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(8154), "System", "diskarTech.jfif", null, null, "DiskarTech" },
                    { 26, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(8155), "System", "others.jfif", null, null, "DragonFi" },
                    { 27, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(8157), "System", "eastwest.jfif", null, null, "EastWest Bank" },
                    { 28, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(8158), "System", "ficco.png", null, null, "Ficco" },
                    { 29, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(8159), "System", "gcash.png", null, null, "Gcash" },
                    { 30, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(8160), "System", "gotrade.png", null, null, "GoTrade" },
                    { 31, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(8161), "System", "gotyme.png", null, null, "GoTyme Bank" },
                    { 32, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(8162), "System", "grab.jfif", null, null, "GrabPay" },
                    { 33, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(8163), "System", "homecredit.jfif", null, null, "Home Credit" },
                    { 34, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(8165), "System", "hsbc.png", null, null, "HSBC" },
                    { 35, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(8166), "System", "ing.jfif", null, null, "ING" },
                    { 36, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(8167), "System", "ing.jfif", null, null, "ING Bank" },
                    { 37, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(8168), "System", "komo.jfif", null, null, "Komo" },
                    { 38, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(8169), "System", "kucoin.png", null, null, "KuCoin" },
                    { 39, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(8170), "System", "landbank.jfif", null, null, "Landbank" },
                    { 40, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(8172), "System", "lazada.jfif", null, null, "Lazada" },
                    { 41, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(8173), "System", "mastercard.png", null, null, "Mastercard" },
                    { 42, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(8174), "System", "maya.png", null, null, "Maya" },
                    { 43, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(8175), "System", "maybank.png", null, null, "Maybank" },
                    { 44, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(8176), "System", "metrobank.png", null, null, "Metrobank" },
                    { 45, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(8177), "System", "netbank.png", null, null, "Netbank" },
                    { 46, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(8178), "System", "ownbank.jfif", null, null, "OwnBank" },
                    { 47, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(8180), "System", "pbcom.jfif", null, null, "PBCOM" },
                    { 48, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(8181), "System", "pnb.png", null, null, "PNB" },
                    { 49, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(8182), "System", "psbank.jfif", null, null, "PSBank" },
                    { 50, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(8183), "System", "pagibig.jfif", null, null, "Pag-Ibig" },
                    { 51, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(8184), "System", "paymaya.png", null, null, "PayMaya" },
                    { 52, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(8188), "System", "paypal.png", null, null, "PayPal" },
                    { 53, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(8189), "System", "plentina.png", null, null, "Pletina" },
                    { 54, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(8190), "System", "rcbc.jfif", null, null, "RCBC" },
                    { 55, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(8191), "System", "robinsonsbank.png", null, null, "RobinsonsBank" },
                    { 56, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(8193), "System", "seabank.png", null, null, "Seabank" },
                    { 57, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(8194), "System", "securitybank.jfif", null, null, "Security Bank" },
                    { 58, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(8195), "System", "shopeepay.jfif", null, null, "ShopeePay" },
                    { 59, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(8196), "System", "standardchartered.png", null, null, "Standard Chartered" },
                    { 60, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(8197), "System", "sterlingbank.jfif", null, null, "Sterling Bank" },
                    { 61, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(8198), "System", "tala.png", null, null, "Tala" },
                    { 62, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(8199), "System", "tonik.png", null, null, "Tonik" },
                    { 63, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(8201), "System", "ucpb.png", null, null, "UCPB" },
                    { 64, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(8202), "System", "uno.jfif", null, null, "UNO Digital Bank" },
                    { 65, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(8203), "System", "unionbank.jfif", null, null, "Unionbank" },
                    { 66, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(8204), "System", "visa.jfif", null, null, "Visa" },
                    { 67, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(8205), "System", "wellsfargo.png", null, null, "Wells Fargo" },
                    { 68, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(8206), "System", "others.jfif", null, null, "ztock" },
                    { 69, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(8207), "System", "others.jfif", null, null, "Others" }
                });

            migrationBuilder.InsertData(
                table: "UserProfiles",
                columns: new[] { "Id", "Created", "CreatedBy", "Email", "FirstName", "ImageUrl", "IsActive", "LastModified", "LastModifiedBy", "LastName", "Mobile" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7119), "System", "system@yahoo.com", "System", "", true, null, null, "", "+639267444551" },
                    { 2, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7131), "System", "nathan.pascual20@yahoo.com", "Nathan", "", true, null, null, "Pascual", "+639267444551" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Created", "CreatedBy", "ImageUrl", "LastModified", "LastModifiedBy", "Name", "ParentId" },
                values: new object[,]
                {
                    { 26, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7377), "System", "electric.png", null, null, "Electricity", 1 },
                    { 27, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7378), "System", "gas.png", null, null, "Gas", 1 },
                    { 28, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7380), "System", "internet.png", null, null, "Internet", 1 },
                    { 29, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7381), "System", "mobile.png", null, null, "Mobile", 1 },
                    { 30, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7382), "System", "telephone.png", null, null, "Phone", 1 },
                    { 31, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7384), "System", "water.png", null, null, "Water", 1 },
                    { 32, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7385), "System", "alcoholic-drink.png", null, null, "Alcohol & Bar", 2 },
                    { 33, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7387), "System", "coffee.png", null, null, "Coffee shops", 2 },
                    { 34, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7388), "System", "fastfood.png", null, null, "Fast Food", 2 },
                    { 35, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7390), "System", "restaurant.png", null, null, "Restaurant", 2 },
                    { 36, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7391), "System", "books.png", null, null, "Books & Stationery", 3 },
                    { 37, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7393), "System", "schoolfee.png", null, null, "School Fee", 3 },
                    { 38, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7430), "System", "tuition.png", null, null, "Tuition Fee", 3 },
                    { 39, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7432), "System", "amusement.png", null, null, "Amusement", 4 },
                    { 40, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7433), "System", "arts.png", null, null, "Arts", 4 },
                    { 41, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7435), "System", "cable.png", null, null, "Cable or DTH", 4 },
                    { 42, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7437), "System", "movies.png", null, null, "Movies & Cinema", 4 },
                    { 43, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7438), "System", "music.png", null, null, "Music", 4 },
                    { 44, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7440), "System", "newspaper.png", null, null, "Newspapers & Magazines", 4 },
                    { 45, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7441), "System", "games.png", null, null, "Games", 4 },
                    { 46, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7443), "System", "happybirthday.png", null, null, "Birthday", 5 },
                    { 47, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7444), "System", "gettogether.png", null, null, "Get Together", 5 },
                    { 48, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7446), "System", "wedding.png", null, null, "Wedding", 5 },
                    { 49, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7447), "System", "kidsactivities.png", null, null, "Kids Activities", 6 },
                    { 50, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7449), "System", "oldagecare.png", null, null, "Old age care", 6 },
                    { 51, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7450), "System", "atm.png", null, null, "ATM Fee", 7 },
                    { 52, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7452), "System", "commission.png", null, null, "Commission Fee", 7 },
                    { 53, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7453), "System", "latefee.png", null, null, "Late Fee", 7 },
                    { 54, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7455), "System", "servicefee.png", null, null, "Service Fee", 7 },
                    { 55, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7456), "System", "charity.png", null, null, "Charity", 9 },
                    { 56, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7457), "System", "gift.png", null, null, "Gift", 9 },
                    { 57, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7459), "System", "dentist.png", null, null, "Dentist", 10 },
                    { 58, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7460), "System", "doctor.png", null, null, "Doctor", 10 },
                    { 59, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7462), "System", "gym.png", null, null, "Gym", 10 },
                    { 60, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7463), "System", "pharmacy.png", null, null, "Pharmacy", 10 },
                    { 61, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7465), "System", "spamassage.png", null, null, "Spa & Massage", 10 },
                    { 62, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7466), "System", "housemaintenance.png", null, null, "House Maintenance", 11 },
                    { 63, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7468), "System", "rent.png", null, null, "House Rent", 11 },
                    { 64, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7469), "System", "autoinsurance.png", null, null, "Auto Insurance", 12 },
                    { 65, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7471), "System", "healthinsurance.png", null, null, "Health Insurance", 12 },
                    { 66, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7472), "System", "propertyinsurance.png", null, null, "Property Insurance", 12 },
                    { 67, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7474), "System", "carloan.png", null, null, "Car Loan", 15 },
                    { 68, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7475), "System", "credit.png", null, null, "Credit Card", 15 },
                    { 69, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7477), "System", "homeloan.png", null, null, "Home Loan", 15 },
                    { 70, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7478), "System", "loan.png", null, null, "Loan", 15 },
                    { 71, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7479), "System", "hairsalon.png", null, null, "Hair & Salon", 18 },
                    { 72, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7481), "System", "laundry.png", null, null, "Laundry", 18 },
                    { 73, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7482), "System", "clothing.png", null, null, "Clothing", 20 },
                    { 74, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7484), "System", "electronics.png", null, null, "Electronics & Accessories", 20 },
                    { 75, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7485), "System", "giftstoys.png", null, null, "Gifts &  Toys", 20 },
                    { 76, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7487), "System", "healthandbeauty.png", null, null, "Health & Beauty", 20 },
                    { 77, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7488), "System", "homeandfurnishing.png", null, null, "Home & furnishing", 20 },
                    { 78, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7490), "System", "jewelry.png", null, null, "Jewellery", 20 },
                    { 79, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7491), "System", "lawnandgarden.png", null, null, "Lawn & Garden", 20 },
                    { 80, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7492), "System", "pets.png", null, null, "Pets & Animals", 20 },
                    { 81, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7494), "System", "sports.png", null, null, "Sports", 20 },
                    { 82, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7495), "System", "withholdingtaxes.png", null, null, "Withholding Tax", 21 },
                    { 83, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7497), "System", "localtaxes.png", null, null, "Local Tax", 21 },
                    { 84, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7498), "System", "propertytax.png", null, null, "Property Tax", 21 },
                    { 85, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7500), "System", "salestax.png", null, null, "Sales Tax", 21 },
                    { 86, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7530), "System", "carmaintenance.png", null, null, "Car Maintenance", 23 },
                    { 87, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7531), "System", "fuel.png", null, null, "Fuel & Gas", 23 },
                    { 88, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7533), "System", "publictransport.png", null, null, "Public Transport", 23 },
                    { 89, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7535), "System", "taxi.png", null, null, "Taxi", 23 },
                    { 90, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7536), "System", "tnvs.png", null, null, "TNVS", 23 },
                    { 91, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7538), "System", "airtravel.png", null, null, "Air Travel", 24 },
                    { 92, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7539), "System", "hotel.png", null, null, "Hotel", 24 },
                    { 93, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7541), "System", "seatravel.png", null, null, "Sea Travel", 24 },
                    { 94, new DateTime(2023, 12, 26, 2, 43, 16, 858, DateTimeKind.Local).AddTicks(7543), "System", "tnvs.png", null, null, "Rental Car", 24 }
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
                name: "IX_Transaction_AccountId",
                table: "Transaction",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_ReceiverId",
                table: "Transfers",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_SenderId",
                table: "Transfers",
                column: "SenderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropTable(
                name: "Incomes");

            migrationBuilder.DropTable(
                name: "Transaction");

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
