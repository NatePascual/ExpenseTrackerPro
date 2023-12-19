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
                    { 1, "Cash", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7015), "System", "Images\\AccountType\\BankAccount.jpg", null, null, "Bank Account" },
                    { 2, "Cash", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7018), "System", "Images\\AccountType\\Cash.jpg", null, null, "Cash" },
                    { 3, "Cash", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7021), "System", "Images\\AccountType\\Wallet.jpg", null, null, "Wallet" },
                    { 4, "Cash", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7023), "System", "Images\\AccountType\\Checking.jpg", null, null, "Checking" },
                    { 5, "Cash", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7025), "System", "Images\\AccountType\\Saving.jpg", null, null, "Saving" },
                    { 6, "Credit", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7028), "System", "Images\\AccountType\\CreditCard.jpg", null, null, "Credit Card" },
                    { 7, "Credit", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7030), "System", "Images\\AccountType\\LineofCredit.jpg", null, null, "Line of Credit" },
                    { 8, "Investment", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7033), "System", "Images\\AccountType\\Retirement.jpg", null, null, "Retirement" },
                    { 9, "Investment", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7035), "System", "Images\\AccountType\\Brokerage.jpg", null, null, "Brokerage" },
                    { 10, "Investment", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7037), "System", "Images\\AccountType\\Investment.jpg", null, null, "Investment" },
                    { 11, "Investment", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7039), "System", "Images\\AccountType\\Insurance.jpg", null, null, "Insurance" },
                    { 12, "Investment", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7041), "System", "Images\\AccountType\\Crypto.jpg", null, null, "Crypto" },
                    { 13, "Loans", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7043), "System", "Images\\AccountType\\Loan.jpg", null, null, "Loan" },
                    { 14, "Loans", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7046), "System", "Images\\AccountType\\Mortgage.jpg", null, null, "Mortgage" },
                    { 15, "Assets", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7048), "System", "Images\\AccountType\\Property.jpg", null, null, "Property" },
                    { 16, "OtherAccount", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7050), "System", "Images\\AccountType\\OtherAccount.jpg", null, null, "Other Account" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Created", "CreatedBy", "ImageUrl", "LastModified", "LastModifiedBy", "Name", "ParentId" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4127), "System", "Images\\Category\\", null, null, "Bills & Utilities", null },
                    { 2, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4131), "System", "Images\\Category\\", null, null, "Drink & Dine", null },
                    { 3, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4136), "System", "Images\\Category\\", null, null, "Education", null },
                    { 4, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4139), "System", "Images\\Category\\", null, null, "Entertainment", null },
                    { 5, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4141), "System", "Images\\Category\\", null, null, "Events", null },
                    { 6, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4143), "System", "Images\\Category\\", null, null, "Family Care", null },
                    { 7, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4146), "System", "Images\\Category\\", null, null, "Fees & Charges", null },
                    { 8, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4148), "System", "Images\\Category\\", null, null, "Food & Grocery", null },
                    { 9, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4150), "System", "Images\\Category\\", null, null, "Gifts & Donations", null },
                    { 10, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4154), "System", "Images\\Category\\", null, null, "Health & Fitness", null },
                    { 11, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4156), "System", "Images\\Category\\", null, null, "House", null },
                    { 12, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4159), "System", "Images\\Category\\", null, null, "Insurance", null },
                    { 13, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4162), "System", "Images\\Category\\", null, null, "Investments", null },
                    { 14, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4164), "System", "Images\\Category\\", null, null, "Kids Care", null },
                    { 15, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4166), "System", "Images\\Category\\", null, null, "Loan & Debts", null },
                    { 16, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4168), "System", "Images\\Category\\", null, null, "Misc Expenses", null },
                    { 17, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4170), "System", "Images\\Category\\", null, null, "Office Expenses", null },
                    { 18, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4173), "System", "Images\\Category\\", null, null, "Personal Care", null },
                    { 19, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4175), "System", "Images\\Category\\", null, null, "Pet Care", null },
                    { 20, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4270), "System", "Images\\Category\\", null, null, "Shopping", null },
                    { 21, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4274), "System", "Images\\Category\\", null, null, "Taxes", null },
                    { 22, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4276), "System", "Images\\Category\\", null, null, "Transfer", null },
                    { 23, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4278), "System", "Images\\Category\\", null, null, "Transport", null },
                    { 24, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4280), "System", "Images\\Category\\", null, null, "Travel & Vacation", null },
                    { 25, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4283), "System", "Images\\Category\\", null, null, "Others", null }
                });

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Id", "Code", "CountryCurrency", "Created", "CreatedBy", "LastModified", "LastModifiedBy", "Symbol" },
                values: new object[,]
                {
                    { 1, "ALL", "Albania Lek", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4640), "System", null, null, "Lek" },
                    { 2, "AFN", "Afghanistan Afghani", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4643), "System", null, null, "؋" },
                    { 3, "ARS", "Argentina Peso", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4645), "System", null, null, "$" },
                    { 4, "AWG", "Aruba Guilder", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4646), "System", null, null, "ƒ" },
                    { 5, "AUD", "Australia Dollar", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4648), "System", null, null, "$" },
                    { 6, "AZN", "Azerbaijan Manat", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4650), "System", null, null, "₼" },
                    { 7, "BSD", "Bahamas Dollar", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4652), "System", null, null, "$" },
                    { 8, "BBD", "Barbados Dollar", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4766), "System", null, null, "$" },
                    { 9, "BYN", "Belarus Ruble", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(5106), "System", null, null, "Br" },
                    { 10, "BZD", "Belize Dollar", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(5115), "System", null, null, "BZ$" },
                    { 11, "BMD", "Bermuda Dollar", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(5117), "System", null, null, "$" },
                    { 12, "BOB", "Bolivia Bolíviano", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(5119), "System", null, null, "$b" },
                    { 13, "BAM", "Bosnia and Herzegovina Convertible Mark", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(5121), "System", null, null, "KM" },
                    { 14, "BWP", "Botswana Pula", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(5123), "System", null, null, "P" },
                    { 15, "BGN", "Bulgaria Lev", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(5125), "System", null, null, "лв" },
                    { 16, "BRL", "Brazil Real", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(5126), "System", null, null, "R$" },
                    { 17, "BND", "Brunei Darussalam Dollar", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(5128), "System", null, null, "$" },
                    { 18, "KHR", "Cambodia Riel", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(5129), "System", null, null, "៛" },
                    { 19, "CAD", "Canada Dollar", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(5131), "System", null, null, "$" },
                    { 20, "KYD", "Cayman Islands Dollar", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(5133), "System", null, null, "$" },
                    { 21, "CLP", "Chile Peso", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(5134), "System", null, null, "$" },
                    { 22, "CNY", "China Yuan Renminbi", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(5136), "System", null, null, "¥" },
                    { 23, "COP", "Colombia Peso", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(5138), "System", null, null, "$" },
                    { 24, "CRC", "Costa Rica Colon", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(5140), "System", null, null, "₡" },
                    { 25, "HRK", "Croatia Kuna", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(5141), "System", null, null, "kn" },
                    { 26, "CUP", "Cuba Peso", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(5143), "System", null, null, "₱" },
                    { 27, "CZK", "Czech Republic Koruna", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(5144), "System", null, null, "Kč" },
                    { 28, "DKK", "Denmark Krone", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(5146), "System", null, null, "kr" },
                    { 29, "DOP", "Dominican Republic Peso", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(5147), "System", null, null, "RD$" },
                    { 30, "XCD", "East Caribbean Dollar", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(5149), "System", null, null, "$" },
                    { 31, "EGP", "Egypt Pound", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(5151), "System", null, null, "£" },
                    { 32, "SVC", "El Salvador Colon", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(5152), "System", null, null, "$" },
                    { 33, "EUR", "Euro Member Countries", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(5154), "System", null, null, "€" },
                    { 34, "FKP", "Falkland Islands (Malvinas) Pound", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(5155), "System", null, null, "£" },
                    { 35, "FJD", "Fiji Dollar", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(5157), "System", null, null, "$" },
                    { 36, "GHS", "Ghana Cedi", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(5158), "System", null, null, "¢" },
                    { 37, "GIP", "Gibraltar Pound", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(5160), "System", null, null, "£" },
                    { 38, "GTQ", "Guatemala Quetzal", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(5161), "System", null, null, "Q" },
                    { 39, "GGP", "Guernsey Pound", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(5163), "System", null, null, "£" },
                    { 40, "GYD", "Guyana Dollar", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(5165), "System", null, null, "$" },
                    { 41, "HNL", "Honduras Lempira", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(5166), "System", null, null, "L" },
                    { 42, "HKD", "Hong Kong Dollar", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(5168), "System", null, null, "$" },
                    { 43, "HUF", "Hungary Forint", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(5170), "System", null, null, "Ft" },
                    { 44, "ISK", "Iceland Krona", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(5172), "System", null, null, "kr" },
                    { 45, "INR", "India Rupee", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(5173), "System", null, null, "₹" },
                    { 46, "IDR", "Indonesia Rupiah", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(5175), "System", null, null, "Rp" },
                    { 47, "IRR", "Iran Rial", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(5176), "System", null, null, "﷼" },
                    { 48, "IMP", "Isle of Man Pound", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(5178), "System", null, null, "£" },
                    { 49, "ILS", "Israel Shekel", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(5179), "System", null, null, "₪" },
                    { 50, "JMD", "Jamaica Dollar", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(5181), "System", null, null, "J$" },
                    { 51, "JPY", "Japan Yen", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(5183), "System", null, null, "¥" },
                    { 52, "JEP", "Jersey Pound", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(5184), "System", null, null, "£" },
                    { 53, "KZT", "Kazakhstan Tenge", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(5186), "System", null, null, "лв" },
                    { 54, "KPW", "Korea (North) Won", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(5188), "System", null, null, "₩" },
                    { 55, "KRW", "Korea (South) Won", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(5189), "System", null, null, "₩" },
                    { 56, "KGS", "Kyrgyzstan Som", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(5191), "System", null, null, "лв" },
                    { 57, "LAK", "Laos Kip", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(5192), "System", null, null, "₭" },
                    { 58, "LBP", "Lebanon Pound", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(5194), "System", null, null, "£" },
                    { 59, "LRD", "Liberia Dollar", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(5196), "System", null, null, "$" },
                    { 60, "MKD", "Macedonia Denar", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(5197), "System", null, null, "ден" },
                    { 61, "MYR", "Malaysia Ringgit", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(5199), "System", null, null, "RM" },
                    { 62, "MUR", "Mauritius Rupee", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(5200), "System", null, null, "₨" },
                    { 63, "MXN", "Mexico Peso", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(5202), "System", null, null, "$" },
                    { 64, "MNT", "Mongolia Tughrik", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(5204), "System", null, null, "₮" },
                    { 65, "MNT", "Moroccan-dirham", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(5205), "System", null, null, " د.إ" },
                    { 66, "MZN", "Mozambique Metical", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(5207), "System", null, null, "MT" },
                    { 67, "NAD", "Namibia Dollar", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(5208), "System", null, null, "$" },
                    { 68, "NPR", "Nepal Rupee", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(5210), "System", null, null, "₨" },
                    { 69, "ANG", "Netherlands Antilles Guilder", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(5212), "System", null, null, "ƒ" },
                    { 70, "NZD", "New Zealand Dollar", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(6510), "System", null, null, "$" },
                    { 71, "NIO", "Nicaragua Cordoba", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(6519), "System", null, null, "C$" },
                    { 72, "NGN", "Nigeria Naira", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(6521), "System", null, null, "₦" },
                    { 73, "NOK", "Norway Krone", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(6522), "System", null, null, "kr" },
                    { 74, "OMR", "Oman Rial", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(6524), "System", null, null, "﷼" },
                    { 75, "PKR", "Pakistan Rupee", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(6526), "System", null, null, "₨" },
                    { 76, "PAB", "Panama Balboa", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(6527), "System", null, null, "B/." },
                    { 77, "PYG", "Paraguay Guarani", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(6529), "System", null, null, "Gs" },
                    { 78, "PEN", "Peru Sol", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(6531), "System", null, null, "S/." },
                    { 79, "PHP", "Philippines Peso", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(6532), "System", null, null, "₱" },
                    { 80, "PLN", "Poland Zloty", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(6534), "System", null, null, "zł" },
                    { 81, "QAR", "Qatar Riyal", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(6536), "System", null, null, "﷼" },
                    { 82, "RON", "Romania Leu", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(6537), "System", null, null, "lei" },
                    { 83, "RUB", "Russia Ruble", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(6539), "System", null, null, "₽" },
                    { 84, "SHP", "Saint Helena Pound", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(6541), "System", null, null, "£" },
                    { 85, "SAR", "Saudi Arabia Riyal", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(6542), "System", null, null, "﷼" },
                    { 86, "RSD", "Serbia Dinar", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(6544), "System", null, null, "Дин." },
                    { 87, "SCR", "Seychelles Rupee", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(6546), "System", null, null, "₨" },
                    { 88, "SGD", "Singapore Dollar", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(6547), "System", null, null, "$" },
                    { 89, "SBD", "Solomon Islands Dollar", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(6549), "System", null, null, "$" },
                    { 90, "SOS", "Somalia Shilling", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(6551), "System", null, null, "S" },
                    { 91, "KRW", "South Korean Won", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(6553), "System", null, null, "₩" },
                    { 92, "ZAR", "South Africa Rand", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(6554), "System", null, null, "R" },
                    { 93, "LKR", "Sri Lanka Rupee", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(6556), "System", null, null, "₨" },
                    { 94, "SEK", "Sweden Krona", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(6558), "System", null, null, "kr" },
                    { 95, "CHF", "Switzerland Franc", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(6559), "System", null, null, "CHF" },
                    { 96, "SRD", "Suriname Dollar", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(6561), "System", null, null, "$" },
                    { 97, "SYP", "Syria Pound", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(6563), "System", null, null, "£" },
                    { 98, "TWD", "Taiwan New Dollar", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(6564), "System", null, null, "NT$" },
                    { 99, "THB", "Thailand Baht", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(6566), "System", null, null, "฿" },
                    { 100, "TTD", "Trinidad and Tobago Dollar", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(6568), "System", null, null, "TT$" },
                    { 101, "TRY", "Turkey Lira", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(6651), "System", null, null, "₺" },
                    { 102, "TVD", "Tuvalu Dollar", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(6653), "System", null, null, "$" },
                    { 103, "UAH", "Ukraine Hryvnia", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(6655), "System", null, null, "₴" },
                    { 104, "AED", "UAE-Dirham", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(6657), "System", null, null, " د.إ" },
                    { 105, "GBP", "United Kingdom Pound", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(6659), "System", null, null, "£" },
                    { 106, "USD", "United States Dollar", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(6661), "System", null, null, "$" },
                    { 107, "UYU", "Uruguay Peso", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(6663), "System", null, null, "$U" },
                    { 108, "UZS", "Uzbekistan Som", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(6665), "System", null, null, "лв" },
                    { 109, "VEF", "Venezuela Bolívar", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(6666), "System", null, null, "Bs" },
                    { 110, "VND", "Viet Nam Dong", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(6668), "System", null, null, "₫" },
                    { 111, "YER", "Yemen Rial", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(6669), "System", null, null, "﷼" },
                    { 112, "ZWD", "Zimbabwe Dollar", new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(6671), "System", null, null, "Z$" }
                });

            migrationBuilder.InsertData(
                table: "IncomeCategories",
                columns: new[] { "Id", "Created", "CreatedBy", "ImageUrl", "LastModified", "LastModifiedBy", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7130), "System", "Images\\IncomeCategory\\Bonus.jpg", null, null, "Bonus" },
                    { 2, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7132), "System", "Images\\IncomeCategory\\Brokerage.jpg", null, null, "Brokerage" },
                    { 3, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7134), "System", "Images\\IncomeCategory\\BusinessAndProfession.jpg", null, null, "Business & Profession" },
                    { 4, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7136), "System", "Images\\IncomeCategory\\Coupons.jpg", null, null, "Coupons" },
                    { 5, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7137), "System", "Images\\IncomeCategory\\Credit.jpg", null, null, "Credit" },
                    { 6, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7139), "System", "Images\\IncomeCategory\\Gifts.jpg", null, null, "Gifts" },
                    { 7, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7141), "System", "Images\\IncomeCategory\\Interest.jpg", null, null, "Interest" },
                    { 8, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7142), "System", "Images\\IncomeCategory\\Investments.jpg", null, null, "Investments" },
                    { 9, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7144), "System", "Images\\IncomeCategory\\Loan.jpg", null, null, "Loan" },
                    { 10, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7146), "System", "Images\\IncomeCategory\\LotteryGambling.jpg", null, null, "Lottery, Gambling" },
                    { 11, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7147), "System", "Images\\IncomeCategory\\MutualFunds.jpg", null, null, "Mutual Funds" },
                    { 12, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7149), "System", "Images\\IncomeCategory\\Refunds.jpg", null, null, "Refunds" },
                    { 13, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7151), "System", "Images\\IncomeCategory\\Reimbursement.jpg", null, null, "Reimbursement" },
                    { 14, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7152), "System", "Images\\IncomeCategory\\RentalIncome.jpg", null, null, "Rental Income" },
                    { 15, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7154), "System", "Images\\IncomeCategory\\SalaryAndPaycheck.jpg", null, null, "Salary & Paycheck" },
                    { 16, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7155), "System", "Images\\IncomeCategory\\Savings.jpg", null, null, "Savings" },
                    { 17, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7157), "System", "Images\\IncomeCategory\\SellingIncome.jpg", null, null, "Selling Income" },
                    { 18, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7159), "System", "Images\\IncomeCategory\\Transfer.jpg", null, null, "Transfer" },
                    { 19, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7160), "System", "Images\\IncomeCategory\\WagesAndTips.jpg", null, null, "Wages & Tips" },
                    { 20, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7162), "System", "Images\\IncomeCategory\\Others.jpg", null, null, "Others" }
                });

            migrationBuilder.InsertData(
                table: "Institutions",
                columns: new[] { "Id", "Created", "CreatedBy", "ImageUrl", "LastModified", "LastModifiedBy", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7362), "System", "Images\\Institution\\ABCapital.jpg", null, null, "AB Capital" },
                    { 2, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7366), "System", "Images\\Institution\\AUB.jpg", null, null, "AUB" },
                    { 3, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7368), "System", "Images\\Institution\\AmericanExpress.jpg", null, null, "American Express" },
                    { 4, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7370), "System", "Images\\Institution\\AppleCard.jpg", null, null, "Apple Card" },
                    { 5, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7371), "System", "Images\\Institution\\Atome.jpg", null, null, "Atome" },
                    { 6, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7373), "System", "Images\\Institution\\BDO.jpg", null, null, "BDO" },
                    { 7, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7375), "System", "Images\\Institution\\BPI.jpg", null, null, "BPI" },
                    { 8, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7376), "System", "Images\\Institution\\BankofCommerce.jpg", null, null, "Bank of Commerce" },
                    { 9, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7378), "System", "Images\\Institution\\BankofMakati.jpg", null, null, "Bank of Makati" },
                    { 10, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7380), "System", "Images\\Institution\\Barclays.jpg", null, null, "Barclays" },
                    { 11, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7381), "System", "Images\\Institution\\Bayad.jpg", null, null, "Bayad" },
                    { 12, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7383), "System", "Images\\Institution\\Billease.jpg", null, null, "Billease" },
                    { 13, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7384), "System", "Images\\Institution\\BinanceExchange.jpg", null, null, "Binance Exchange" },
                    { 14, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7386), "System", "Images\\Institution\\CARDBank.jpg", null, null, "CARD Bank" },
                    { 15, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7388), "System", "Images\\Institution\\CIMB.jpg", null, null, "CIMB" },
                    { 16, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7390), "System", "Images\\Institution\\COLFinancial.jpg", null, null, "COL Financial" },
                    { 17, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7391), "System", "Images\\Institution\\Cashalo.jpg", null, null, "Cashalo" },
                    { 18, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7393), "System", "Images\\Institution\\CebuanaLhullier.jpg", null, null, "Cebuana Lhullier" },
                    { 19, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7394), "System", "Images\\Institution\\ChinaBank.jpg", null, null, "China Bank" },
                    { 20, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7396), "System", "Images\\Institution\\Citibank.jpg", null, null, "Citibank" },
                    { 21, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7398), "System", "Images\\Institution\\CliQQ.jpg", null, null, "CliQQ" },
                    { 22, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7399), "System", "Images\\Institution\\Coinbase.jpg", null, null, "Coinbase" },
                    { 23, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7401), "System", "Images\\Institution\\Coins.ph.jpg", null, null, "Coins.ph" },
                    { 24, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7403), "System", "Images\\Institution\\Deutche.jpg", null, null, "Deutche" },
                    { 25, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7404), "System", "Images\\Institution\\DiskarTech.jpg", null, null, "DiskarTech" },
                    { 26, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7406), "System", "Images\\Institution\\DragonFi.jpg", null, null, "DragonFi" },
                    { 27, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7408), "System", "Images\\Institution\\EastWestBank.jpg", null, null, "EastWest Bank" },
                    { 28, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7409), "System", "Images\\Institution\\Ficco.jpg", null, null, "Ficco" },
                    { 29, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7411), "System", "Images\\Institution\\Gcash.jpg", null, null, "Gcash" },
                    { 30, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7412), "System", "Images\\Institution\\GoTrade.jpg", null, null, "GoTrade" },
                    { 31, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7415), "System", "Images\\Institution\\GoTymeBank.jpg", null, null, "GoTyme Bank" },
                    { 32, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7416), "System", "Images\\Institution\\GrabPay.jpg", null, null, "GrabPay" },
                    { 33, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7418), "System", "Images\\Institution\\HomeCredit.jpg", null, null, "Home Credit" },
                    { 34, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7419), "System", "Images\\Institution\\HSBC.jpg", null, null, "HSBC" },
                    { 35, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7421), "System", "Images\\Institution\\ING.jpg", null, null, "ING" },
                    { 36, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7423), "System", "Images\\Institution\\ING Bank.jpg", null, null, "ING Bank" },
                    { 37, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7424), "System", "Images\\Institution\\Komo.jpg", null, null, "Komo" },
                    { 38, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7426), "System", "Images\\Institution\\KuCoin.jpg", null, null, "KuCoin" },
                    { 39, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7428), "System", "Images\\Institution\\Landbank.jpg", null, null, "Landbank" },
                    { 40, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7429), "System", "Images\\Institution\\Lazada.jpg", null, null, "Lazada" },
                    { 41, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7431), "System", "Images\\Institution\\Mastercard.jpg", null, null, "Mastercard" },
                    { 42, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7433), "System", "Images\\Institution\\Maya.jpg", null, null, "Maya" },
                    { 43, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7434), "System", "Images\\Institution\\Maybank.jpg", null, null, "Maybank" },
                    { 44, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7436), "System", "Images\\Institution\\Metrobank.jpg", null, null, "Metrobank" },
                    { 45, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7438), "System", "Images\\Institution\\Netbank.jpg", null, null, "Netbank" },
                    { 46, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7439), "System", "Images\\Institution\\OwnBank.jpg", null, null, "OwnBank" },
                    { 47, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7441), "System", "Images\\Institution\\PBCOM.jpg", null, null, "PBCOM" },
                    { 48, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7442), "System", "Images\\Institution\\PNB.jpg", null, null, "PNB" },
                    { 49, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7444), "System", "Images\\Institution\\PSBank.jpg", null, null, "PSBank" },
                    { 50, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7446), "System", "Images\\Institution\\Pag-Ibig.jpg", null, null, "Pag-Ibig" },
                    { 51, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7447), "System", "Images\\Institution\\PayMaya.jpg", null, null, "PayMaya" },
                    { 52, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7449), "System", "Images\\Institution\\PayPal.jpg", null, null, "PayPal" },
                    { 53, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7451), "System", "Images\\Institution\\PNB.jpg", null, null, "PNB" },
                    { 54, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7452), "System", "Images\\Institution\\Pletina.jpg", null, null, "Pletina" },
                    { 55, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7454), "System", "Images\\Institution\\RCBC.jpg", null, null, "RCBC" },
                    { 56, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7456), "System", "Images\\Institution\\RobinsonsBank.jpg", null, null, "RobinsonsBank" },
                    { 57, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7457), "System", "Images\\Institution\\Seabank.jpg", null, null, "Seabank" },
                    { 58, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7459), "System", "Images\\Institution\\SecurityBank.jpg", null, null, "Security Bank" },
                    { 59, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7460), "System", "Images\\Institution\\ShopeePay.jpg", null, null, "ShopeePay" },
                    { 60, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7462), "System", "Images\\Institution\\StandardChartered.jpg", null, null, "Standard Chartered" },
                    { 61, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7463), "System", "Images\\Institution\\SterlingBank.jpg", null, null, "Sterling Bank" },
                    { 62, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7465), "System", "Images\\Institution\\Tala.jpg", null, null, "Tala" },
                    { 63, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7467), "System", "Images\\Institution\\Tonik.jpg", null, null, "Tonik" },
                    { 64, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7468), "System", "Images\\Institution\\UCPB.jpg", null, null, "UCPB" },
                    { 65, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7470), "System", "Images\\Institution\\UNODigitalBank.jpg", null, null, "UNO Digital Bank" },
                    { 66, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7472), "System", "Images\\Institution\\Unionbank.jpg", null, null, "Unionbank" },
                    { 67, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7473), "System", "Images\\Institution\\Visa.jpg", null, null, "Visa" },
                    { 68, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7475), "System", "Images\\Institution\\Wells Fargo.jpg", null, null, "Wells Fargo" },
                    { 69, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7477), "System", "Images\\Institution\\ztock.jpg", null, null, "ztock" },
                    { 70, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(7478), "System", "Images\\Institution\\Others.jpg", null, null, "Others" }
                });

            migrationBuilder.InsertData(
                table: "UserProfiles",
                columns: new[] { "Id", "Created", "CreatedBy", "Email", "FirstName", "ImageUrl", "IsActive", "LastModified", "LastModifiedBy", "LastName", "Mobile" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(3735), "System", "system@yahoo.com", "System", "", true, null, null, "", "+639267444551" },
                    { 2, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(3753), "System", "nathan.pascual20@yahoo.com", "Nathan", "", true, null, null, "Pascual", "+639267444551" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Created", "CreatedBy", "ImageUrl", "LastModified", "LastModifiedBy", "Name", "ParentId" },
                values: new object[,]
                {
                    { 26, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4285), "System", "Images\\Category\\", null, null, "Electricity", 1 },
                    { 27, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4288), "System", "Images\\Category\\", null, null, "Gas", 1 },
                    { 28, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4290), "System", "Images\\Category\\", null, null, "Internet", 1 },
                    { 29, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4292), "System", "Images\\Category\\", null, null, "Mobile", 1 },
                    { 30, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4295), "System", "Images\\Category\\", null, null, "Phone", 1 },
                    { 31, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4297), "System", "Images\\Category\\", null, null, "Water", 1 },
                    { 32, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4300), "System", "Images\\Category\\", null, null, "Alcohol & Bar", 2 },
                    { 33, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4302), "System", "Images\\Category\\", null, null, "Coffee shops", 2 },
                    { 34, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4305), "System", "Images\\Category\\", null, null, "Fast Food", 2 },
                    { 35, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4307), "System", "Images\\Category\\", null, null, "Restaurant", 2 },
                    { 36, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4310), "System", "Images\\Category\\", null, null, "Books & Stationery", 3 },
                    { 37, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4312), "System", "Images\\Category\\", null, null, "School Fee", 3 },
                    { 38, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4314), "System", "Images\\Category\\", null, null, "Tuition Fee", 3 },
                    { 39, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4316), "System", "Images\\Category\\", null, null, "Amusement", 4 },
                    { 40, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4319), "System", "Images\\Category\\", null, null, "Arts", 4 },
                    { 41, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4322), "System", "Images\\Category\\", null, null, "Cable or DTH", 4 },
                    { 42, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4324), "System", "Images\\Category\\", null, null, "Movies & Cinema", 4 },
                    { 43, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4327), "System", "Images\\Category\\", null, null, "Music", 4 },
                    { 44, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4329), "System", "Images\\Category\\", null, null, "Newspapers & Magazines", 4 },
                    { 45, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4331), "System", "Images\\Category\\", null, null, "Games", 4 },
                    { 46, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4335), "System", "Images\\Category\\", null, null, "Birthday", 5 },
                    { 47, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4338), "System", "Images\\Category\\", null, null, "Get Together", 5 },
                    { 48, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4340), "System", "Images\\Category\\", null, null, "Wedding", 5 },
                    { 49, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4343), "System", "Images\\Category\\", null, null, "Kids Activities", 6 },
                    { 50, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4345), "System", "Images\\Category\\", null, null, "Old age care", 6 },
                    { 51, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4347), "System", "Images\\Category\\", null, null, "ATM Fee", 7 },
                    { 52, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4349), "System", "Images\\Category\\", null, null, "Commission Fee", 7 },
                    { 53, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4352), "System", "Images\\Category\\", null, null, "Late Fee", 7 },
                    { 54, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4354), "System", "Images\\Category\\", null, null, "Service Fee", 7 },
                    { 55, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4357), "System", "Images\\Category\\", null, null, "Charity", 9 },
                    { 56, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4359), "System", "Images\\Category\\", null, null, "Gift", 9 },
                    { 57, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4362), "System", "Images\\Category\\", null, null, "Dentist", 10 },
                    { 58, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4364), "System", "Images\\Category\\", null, null, "Doctor", 10 },
                    { 59, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4366), "System", "Images\\Category\\", null, null, "Gym", 10 },
                    { 60, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4369), "System", "Images\\Category\\", null, null, "Pharmacy", 10 },
                    { 61, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4371), "System", "Images\\Category\\", null, null, "Spa & Massage", 10 },
                    { 62, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4373), "System", "Images\\Category\\", null, null, "House Maintenance", 11 },
                    { 63, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4376), "System", "Images\\Category\\", null, null, "House Rent", 11 },
                    { 64, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4378), "System", "Images\\Category\\", null, null, "Auto Insurance", 12 },
                    { 65, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4380), "System", "Images\\Category\\", null, null, "Health Insurance", 12 },
                    { 66, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4383), "System", "Images\\Category\\", null, null, "Property Insurance", 12 },
                    { 67, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4385), "System", "Images\\Category\\", null, null, "Car Loan", 15 },
                    { 68, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4387), "System", "Images\\Category\\", null, null, "Credit Card", 15 },
                    { 69, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4426), "System", "Images\\Category\\", null, null, "Home Loan", 15 },
                    { 70, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4430), "System", "Images\\Category\\", null, null, "Loan", 15 },
                    { 71, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4433), "System", "Images\\Category\\", null, null, "Hair & Salon", 18 },
                    { 72, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4435), "System", "Images\\Category\\", null, null, "Laundry", 18 },
                    { 73, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4438), "System", "Images\\Category\\", null, null, "Clothing", 20 },
                    { 74, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4440), "System", "Images\\Category\\", null, null, "Electronics & Accessories", 20 },
                    { 75, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4443), "System", "Images\\Category\\", null, null, "Gifts &  Toys", 20 },
                    { 76, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4445), "System", "Images\\Category\\", null, null, "Health & Beauty", 20 },
                    { 77, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4447), "System", "Images\\Category\\", null, null, "Home & furnishing", 20 },
                    { 78, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4450), "System", "Images\\Category\\", null, null, "Jewellery", 20 },
                    { 79, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4452), "System", "Images\\Category\\", null, null, "Lawn & Garden", 20 },
                    { 80, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4454), "System", "Images\\Category\\", null, null, "Pets & Animals", 20 },
                    { 81, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4457), "System", "Images\\Category\\", null, null, "Sports", 20 },
                    { 82, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4459), "System", "Images\\Category\\", null, null, "Withholding Tax", 21 },
                    { 83, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4461), "System", "Images\\Category\\", null, null, "Local Tax", 21 },
                    { 84, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4463), "System", "Images\\Category\\", null, null, "Property Tax", 21 },
                    { 85, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4466), "System", "Images\\Category\\", null, null, "Sales Tax", 21 },
                    { 86, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4468), "System", "Images\\Category\\", null, null, "Car Maintenance", 23 },
                    { 87, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4470), "System", "Images\\Category\\", null, null, "Fuel & Gas", 23 },
                    { 88, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4473), "System", "Images\\Category\\", null, null, "Public Transport", 23 },
                    { 89, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4475), "System", "Images\\Category\\", null, null, "Taxi", 23 },
                    { 90, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4477), "System", "Images\\Category\\", null, null, "TNVS", 23 },
                    { 91, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4479), "System", "Images\\Category\\", null, null, "Air Travel", 24 },
                    { 92, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4482), "System", "Images\\Category\\", null, null, "Hotel", 24 },
                    { 93, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4484), "System", "Images\\Category\\", null, null, "Sea Travel", 24 },
                    { 94, new DateTime(2023, 12, 20, 2, 19, 46, 198, DateTimeKind.Local).AddTicks(4486), "System", "Images\\Category\\", null, null, "Rental Car", 24 }
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
