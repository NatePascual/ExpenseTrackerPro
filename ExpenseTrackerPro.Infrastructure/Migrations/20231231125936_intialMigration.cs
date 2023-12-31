using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ExpenseTrackerPro.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class intialMigration : Migration
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
                    AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
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
                    SenderId = table.Column<int>(type: "int", nullable: false),
                    ReceiverId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<float>(type: "real", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
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
                    { 1, "Cash", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9093), "System", "bank.png", null, null, "Bank Account" },
                    { 2, "Cash", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9095), "System", "cash.png", null, null, "Cash" },
                    { 3, "Cash", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9097), "System", "wallet.png", null, null, "Wallet" },
                    { 4, "Cash", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9098), "System", "checking.png", null, null, "Checking" },
                    { 5, "Cash", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9100), "System", "savings.png", null, null, "Saving" },
                    { 6, "Investment", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9101), "System", "retirement.png", null, null, "Retirement" },
                    { 7, "Investment", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9103), "System", "brokerage.png", null, null, "Brokerage" },
                    { 8, "Investment", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9104), "System", "investment.png", null, null, "Investment" },
                    { 9, "Investment", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9106), "System", "insurance.png", null, null, "Insurance" },
                    { 10, "Investment", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9107), "System", "crypto.png", null, null, "Crypto" },
                    { 11, "Assets", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9109), "System", "property.png", null, null, "Property" },
                    { 12, "OtherAccount", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9111), "System", "bank.png", null, null, "Other Account" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Created", "CreatedBy", "ImageUrl", "LastModified", "LastModifiedBy", "Name", "ParentId" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8481), "System", "bills.png", null, null, "Bills & Utilities", null },
                    { 2, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8483), "System", "drinkanddine.png", null, null, "Drink & Dine", null },
                    { 3, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8485), "System", "education.png", null, null, "Education", null },
                    { 4, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8486), "System", "entertainment.png", null, null, "Entertainment", null },
                    { 5, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8487), "System", "events.png", null, null, "Events", null },
                    { 6, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8489), "System", "familycare.png", null, null, "Family Care", null },
                    { 7, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8490), "System", "fees.png", null, null, "Fees & Charges", null },
                    { 8, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8492), "System", "foodandgrocery.png", null, null, "Food & Grocery", null },
                    { 9, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8493), "System", "giftanddonation.png", null, null, "Gifts & Donations", null },
                    { 10, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8495), "System", "healthandfitness.png", null, null, "Health & Fitness", null },
                    { 11, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8496), "System", "house.png", null, null, "House", null },
                    { 12, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8498), "System", "insurance.png", null, null, "Insurance", null },
                    { 13, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8499), "System", "investment.png", null, null, "Investments", null },
                    { 14, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8500), "System", "kidscare.png", null, null, "Kids Care", null },
                    { 15, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8502), "System", "loan.png", null, null, "Loan & Debts", null },
                    { 16, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8549), "System", "misc.png", null, null, "Misc Expenses", null },
                    { 17, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8550), "System", "office.png", null, null, "Office Expenses", null },
                    { 18, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8552), "System", "personalcare.png", null, null, "Personal Care", null },
                    { 19, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8553), "System", "petcare.png", null, null, "Pet Care", null },
                    { 20, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8555), "System", "shopping.png", null, null, "Shopping", null },
                    { 21, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8556), "System", "taxes.png", null, null, "Taxes", null },
                    { 22, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8558), "System", "transfer.png", null, null, "Transfer", null },
                    { 23, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8559), "System", "transport.png", null, null, "Transport", null },
                    { 24, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8561), "System", "travel.png", null, null, "Travel & Vacation", null },
                    { 25, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8562), "System", "others.png", null, null, "Others", null }
                });

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Id", "Code", "CountryCurrency", "Created", "CreatedBy", "LastModified", "LastModifiedBy", "Symbol" },
                values: new object[,]
                {
                    { 1, "ALL", "Albania Lek", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8824), "System", null, null, "Lek" },
                    { 2, "AFN", "Afghanistan Afghani", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8825), "System", null, null, "؋" },
                    { 3, "ARS", "Argentina Peso", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8826), "System", null, null, "$" },
                    { 4, "AWG", "Aruba Guilder", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8828), "System", null, null, "ƒ" },
                    { 5, "AUD", "Australia Dollar", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8829), "System", null, null, "$" },
                    { 6, "AZN", "Azerbaijan Manat", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8830), "System", null, null, "₼" },
                    { 7, "BSD", "Bahamas Dollar", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8831), "System", null, null, "$" },
                    { 8, "BBD", "Barbados Dollar", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8832), "System", null, null, "$" },
                    { 9, "BYN", "Belarus Ruble", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8834), "System", null, null, "Br" },
                    { 10, "BZD", "Belize Dollar", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8835), "System", null, null, "BZ$" },
                    { 11, "BMD", "Bermuda Dollar", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8836), "System", null, null, "$" },
                    { 12, "BOB", "Bolivia Bolíviano", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8837), "System", null, null, "$b" },
                    { 13, "BAM", "Bosnia and Herzegovina Convertible Mark", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8838), "System", null, null, "KM" },
                    { 14, "BWP", "Botswana Pula", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8839), "System", null, null, "P" },
                    { 15, "BGN", "Bulgaria Lev", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8841), "System", null, null, "лв" },
                    { 16, "BRL", "Brazil Real", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8842), "System", null, null, "R$" },
                    { 17, "BND", "Brunei Darussalam Dollar", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8843), "System", null, null, "$" },
                    { 18, "KHR", "Cambodia Riel", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8844), "System", null, null, "៛" },
                    { 19, "CAD", "Canada Dollar", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8845), "System", null, null, "$" },
                    { 20, "KYD", "Cayman Islands Dollar", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8846), "System", null, null, "$" },
                    { 21, "CLP", "Chile Peso", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8848), "System", null, null, "$" },
                    { 22, "CNY", "China Yuan Renminbi", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8849), "System", null, null, "¥" },
                    { 23, "COP", "Colombia Peso", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8850), "System", null, null, "$" },
                    { 24, "CRC", "Costa Rica Colon", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8851), "System", null, null, "₡" },
                    { 25, "HRK", "Croatia Kuna", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8852), "System", null, null, "kn" },
                    { 26, "CUP", "Cuba Peso", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8853), "System", null, null, "₱" },
                    { 27, "CZK", "Czech Republic Koruna", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8854), "System", null, null, "Kč" },
                    { 28, "DKK", "Denmark Krone", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8856), "System", null, null, "kr" },
                    { 29, "DOP", "Dominican Republic Peso", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8857), "System", null, null, "RD$" },
                    { 30, "XCD", "East Caribbean Dollar", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8858), "System", null, null, "$" },
                    { 31, "EGP", "Egypt Pound", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8859), "System", null, null, "£" },
                    { 32, "SVC", "El Salvador Colon", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8860), "System", null, null, "$" },
                    { 33, "EUR", "Euro Member Countries", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8861), "System", null, null, "€" },
                    { 34, "FKP", "Falkland Islands (Malvinas) Pound", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8863), "System", null, null, "£" },
                    { 35, "FJD", "Fiji Dollar", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8864), "System", null, null, "$" },
                    { 36, "GHS", "Ghana Cedi", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8865), "System", null, null, "¢" },
                    { 37, "GIP", "Gibraltar Pound", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8866), "System", null, null, "£" },
                    { 38, "GTQ", "Guatemala Quetzal", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8867), "System", null, null, "Q" },
                    { 39, "GGP", "Guernsey Pound", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8868), "System", null, null, "£" },
                    { 40, "GYD", "Guyana Dollar", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8869), "System", null, null, "$" },
                    { 41, "HNL", "Honduras Lempira", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8871), "System", null, null, "L" },
                    { 42, "HKD", "Hong Kong Dollar", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8872), "System", null, null, "$" },
                    { 43, "HUF", "Hungary Forint", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8873), "System", null, null, "Ft" },
                    { 44, "ISK", "Iceland Krona", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8874), "System", null, null, "kr" },
                    { 45, "INR", "India Rupee", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8875), "System", null, null, "₹" },
                    { 46, "IDR", "Indonesia Rupiah", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8876), "System", null, null, "Rp" },
                    { 47, "IRR", "Iran Rial", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8878), "System", null, null, "﷼" },
                    { 48, "IMP", "Isle of Man Pound", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8879), "System", null, null, "£" },
                    { 49, "ILS", "Israel Shekel", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8880), "System", null, null, "₪" },
                    { 50, "JMD", "Jamaica Dollar", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8881), "System", null, null, "J$" },
                    { 51, "JPY", "Japan Yen", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8882), "System", null, null, "¥" },
                    { 52, "JEP", "Jersey Pound", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8883), "System", null, null, "£" },
                    { 53, "KZT", "Kazakhstan Tenge", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8885), "System", null, null, "лв" },
                    { 54, "KPW", "Korea (North) Won", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8886), "System", null, null, "₩" },
                    { 55, "KRW", "Korea (South) Won", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8887), "System", null, null, "₩" },
                    { 56, "KGS", "Kyrgyzstan Som", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8888), "System", null, null, "лв" },
                    { 57, "LAK", "Laos Kip", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8889), "System", null, null, "₭" },
                    { 58, "LBP", "Lebanon Pound", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8890), "System", null, null, "£" },
                    { 59, "LRD", "Liberia Dollar", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8892), "System", null, null, "$" },
                    { 60, "MKD", "Macedonia Denar", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8893), "System", null, null, "ден" },
                    { 61, "MYR", "Malaysia Ringgit", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8894), "System", null, null, "RM" },
                    { 62, "MUR", "Mauritius Rupee", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8895), "System", null, null, "₨" },
                    { 63, "MXN", "Mexico Peso", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8897), "System", null, null, "$" },
                    { 64, "MNT", "Mongolia Tughrik", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8898), "System", null, null, "₮" },
                    { 65, "MNT", "Moroccan-dirham", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8899), "System", null, null, " د.إ" },
                    { 66, "MZN", "Mozambique Metical", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8900), "System", null, null, "MT" },
                    { 67, "NAD", "Namibia Dollar", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8901), "System", null, null, "$" },
                    { 68, "NPR", "Nepal Rupee", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8902), "System", null, null, "₨" },
                    { 69, "ANG", "Netherlands Antilles Guilder", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8903), "System", null, null, "ƒ" },
                    { 70, "NZD", "New Zealand Dollar", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8905), "System", null, null, "$" },
                    { 71, "NIO", "Nicaragua Cordoba", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8906), "System", null, null, "C$" },
                    { 72, "NGN", "Nigeria Naira", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8907), "System", null, null, "₦" },
                    { 73, "NOK", "Norway Krone", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8908), "System", null, null, "kr" },
                    { 74, "OMR", "Oman Rial", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8909), "System", null, null, "﷼" },
                    { 75, "PKR", "Pakistan Rupee", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8911), "System", null, null, "₨" },
                    { 76, "PAB", "Panama Balboa", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8912), "System", null, null, "B/." },
                    { 77, "PYG", "Paraguay Guarani", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8913), "System", null, null, "Gs" },
                    { 78, "PEN", "Peru Sol", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8914), "System", null, null, "S/." },
                    { 79, "PHP", "Philippines Peso", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8915), "System", null, null, "₱" },
                    { 80, "PLN", "Poland Zloty", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8916), "System", null, null, "zł" },
                    { 81, "QAR", "Qatar Riyal", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8917), "System", null, null, "﷼" },
                    { 82, "RON", "Romania Leu", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8919), "System", null, null, "lei" },
                    { 83, "RUB", "Russia Ruble", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8920), "System", null, null, "₽" },
                    { 84, "SHP", "Saint Helena Pound", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8921), "System", null, null, "£" },
                    { 85, "SAR", "Saudi Arabia Riyal", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8922), "System", null, null, "﷼" },
                    { 86, "RSD", "Serbia Dinar", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8923), "System", null, null, "Дин." },
                    { 87, "SCR", "Seychelles Rupee", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8925), "System", null, null, "₨" },
                    { 88, "SGD", "Singapore Dollar", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8926), "System", null, null, "$" },
                    { 89, "SBD", "Solomon Islands Dollar", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8927), "System", null, null, "$" },
                    { 90, "SOS", "Somalia Shilling", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8928), "System", null, null, "S" },
                    { 91, "KRW", "South Korean Won", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8929), "System", null, null, "₩" },
                    { 92, "ZAR", "South Africa Rand", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8959), "System", null, null, "R" },
                    { 93, "LKR", "Sri Lanka Rupee", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8961), "System", null, null, "₨" },
                    { 94, "SEK", "Sweden Krona", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8962), "System", null, null, "kr" },
                    { 95, "CHF", "Switzerland Franc", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8963), "System", null, null, "CHF" },
                    { 96, "SRD", "Suriname Dollar", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8964), "System", null, null, "$" },
                    { 97, "SYP", "Syria Pound", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8965), "System", null, null, "£" },
                    { 98, "TWD", "Taiwan New Dollar", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8967), "System", null, null, "NT$" },
                    { 99, "THB", "Thailand Baht", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8968), "System", null, null, "฿" },
                    { 100, "TTD", "Trinidad and Tobago Dollar", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8969), "System", null, null, "TT$" },
                    { 101, "TRY", "Turkey Lira", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8970), "System", null, null, "₺" },
                    { 102, "TVD", "Tuvalu Dollar", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8971), "System", null, null, "$" },
                    { 103, "UAH", "Ukraine Hryvnia", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8972), "System", null, null, "₴" },
                    { 104, "AED", "UAE-Dirham", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8974), "System", null, null, " د.إ" },
                    { 105, "GBP", "United Kingdom Pound", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8975), "System", null, null, "£" },
                    { 106, "USD", "United States Dollar", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8976), "System", null, null, "$" },
                    { 107, "UYU", "Uruguay Peso", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8977), "System", null, null, "$U" },
                    { 108, "UZS", "Uzbekistan Som", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8978), "System", null, null, "лв" },
                    { 109, "VEF", "Venezuela Bolívar", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8979), "System", null, null, "Bs" },
                    { 110, "VND", "Viet Nam Dong", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8981), "System", null, null, "₫" },
                    { 111, "YER", "Yemen Rial", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8982), "System", null, null, "﷼" },
                    { 112, "ZWD", "Zimbabwe Dollar", new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8983), "System", null, null, "Z$" }
                });

            migrationBuilder.InsertData(
                table: "IncomeCategories",
                columns: new[] { "Id", "Created", "CreatedBy", "ImageUrl", "LastModified", "LastModifiedBy", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9157), "System", "bonus.png", null, null, "Bonus" },
                    { 2, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9158), "System", "brokerage.png", null, null, "Brokerage" },
                    { 3, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9159), "System", "business.png", null, null, "Business & Profession" },
                    { 4, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9160), "System", "coupon.png", null, null, "Coupons" },
                    { 5, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9161), "System", "credit.png", null, null, "Credit" },
                    { 6, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9162), "System", "gift.png", null, null, "Gifts" },
                    { 7, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9164), "System", "interest.png", null, null, "Interest" },
                    { 8, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9165), "System", "investment.png", null, null, "Investments" },
                    { 9, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9166), "System", "loan.png", null, null, "Loan" },
                    { 10, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9167), "System", "gambling.png", null, null, "Lottery, Gambling" },
                    { 11, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9168), "System", "mutualfunds.png", null, null, "Mutual Funds" },
                    { 12, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9169), "System", "refund.png", null, null, "Refunds" },
                    { 13, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9170), "System", "reimbursement.png", null, null, "Reimbursement" },
                    { 14, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9172), "System", "rental.png", null, null, "Rental Income" },
                    { 15, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9173), "System", "salary.png", null, null, "Salary & Paycheck" },
                    { 16, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9174), "System", "savings.png", null, null, "Savings" },
                    { 17, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9175), "System", "selling.png", null, null, "Selling Income" },
                    { 18, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9176), "System", "transfer.png", null, null, "Transfer" },
                    { 19, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9177), "System", "wage.png", null, null, "Wages & Tips" },
                    { 20, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9178), "System", "others.png", null, null, "Others" }
                });

            migrationBuilder.InsertData(
                table: "Institutions",
                columns: new[] { "Id", "Created", "CreatedBy", "ImageUrl", "LastModified", "LastModifiedBy", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9274), "System", "abcapital.png", null, null, "AB Capital" },
                    { 2, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9276), "System", "aub.png", null, null, "AUB" },
                    { 3, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9277), "System", "amex.png", null, null, "American Express" },
                    { 4, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9278), "System", "applecard.png", null, null, "Apple Card" },
                    { 5, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9279), "System", "atome.jfif", null, null, "Atome" },
                    { 6, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9280), "System", "bdo.png", null, null, "BDO" },
                    { 7, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9282), "System", "bpi.png", null, null, "BPI" },
                    { 8, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9283), "System", "bankofcommerce.jfif", null, null, "Bank of Commerce" },
                    { 9, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9284), "System", "bankofmakati.png", null, null, "Bank of Makati" },
                    { 10, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9285), "System", "barclays.jfif", null, null, "Barclays" },
                    { 11, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9286), "System", "bayad.png", null, null, "Bayad" },
                    { 12, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9287), "System", "billease.png", null, null, "Billease" },
                    { 13, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9288), "System", "binance.png", null, null, "Binance Exchange" },
                    { 14, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9290), "System", "others.jfif", null, null, "CARD Bank" },
                    { 15, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9291), "System", "cimb.png", null, null, "CIMB" },
                    { 16, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9292), "System", "colfinancial.png", null, null, "COL Financial" },
                    { 17, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9293), "System", "cashalo.jfif", null, null, "Cashalo" },
                    { 18, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9294), "System", "cebuana.png", null, null, "Cebuana Lhullier" },
                    { 19, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9295), "System", "chinabank.jfif", null, null, "China Bank" },
                    { 20, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9296), "System", "citibank.jfif", null, null, "Citibank" },
                    { 21, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9297), "System", "cliqq.jfif", null, null, "CliQQ" },
                    { 22, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9298), "System", "coinbase.png", null, null, "Coinbase" },
                    { 23, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9299), "System", "coinph.jfif", null, null, "Coins.ph" },
                    { 24, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9301), "System", "deutsche.png", null, null, "Deutche" },
                    { 25, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9302), "System", "diskarTech.jfif", null, null, "DiskarTech" },
                    { 26, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9303), "System", "others.jfif", null, null, "DragonFi" },
                    { 27, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9304), "System", "eastwest.jfif", null, null, "EastWest Bank" },
                    { 28, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9305), "System", "ficco.png", null, null, "Ficco" },
                    { 29, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9306), "System", "gcash.png", null, null, "Gcash" },
                    { 30, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9307), "System", "gotrade.png", null, null, "GoTrade" },
                    { 31, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9308), "System", "gotyme.png", null, null, "GoTyme Bank" },
                    { 32, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9310), "System", "grab.jfif", null, null, "GrabPay" },
                    { 33, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9311), "System", "homecredit.jfif", null, null, "Home Credit" },
                    { 34, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9312), "System", "hsbc.png", null, null, "HSBC" },
                    { 35, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9313), "System", "ing.jfif", null, null, "ING" },
                    { 36, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9314), "System", "ing.jfif", null, null, "ING Bank" },
                    { 37, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9315), "System", "komo.jfif", null, null, "Komo" },
                    { 38, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9316), "System", "kucoin.png", null, null, "KuCoin" },
                    { 39, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9317), "System", "landbank.jfif", null, null, "Landbank" },
                    { 40, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9319), "System", "lazada.jfif", null, null, "Lazada" },
                    { 41, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9320), "System", "mastercard.png", null, null, "Mastercard" },
                    { 42, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9321), "System", "maya.png", null, null, "Maya" },
                    { 43, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9322), "System", "maybank.png", null, null, "Maybank" },
                    { 44, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9323), "System", "metrobank.png", null, null, "Metrobank" },
                    { 45, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9324), "System", "netbank.png", null, null, "Netbank" },
                    { 46, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9325), "System", "ownbank.jfif", null, null, "OwnBank" },
                    { 47, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9326), "System", "pbcom.jfif", null, null, "PBCOM" },
                    { 48, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9328), "System", "pnb.png", null, null, "PNB" },
                    { 49, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9329), "System", "psbank.jfif", null, null, "PSBank" },
                    { 50, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9330), "System", "pagibig.jfif", null, null, "Pag-Ibig" },
                    { 51, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9331), "System", "paymaya.png", null, null, "PayMaya" },
                    { 52, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9332), "System", "paypal.png", null, null, "PayPal" },
                    { 53, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9333), "System", "plentina.png", null, null, "Pletina" },
                    { 54, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9334), "System", "rcbc.jfif", null, null, "RCBC" },
                    { 55, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9335), "System", "robinsonsbank.png", null, null, "RobinsonsBank" },
                    { 56, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9336), "System", "seabank.png", null, null, "Seabank" },
                    { 57, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9338), "System", "securitybank.jfif", null, null, "Security Bank" },
                    { 58, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9339), "System", "shopeepay.jfif", null, null, "ShopeePay" },
                    { 59, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9340), "System", "standardchartered.png", null, null, "Standard Chartered" },
                    { 60, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9341), "System", "sterlingbank.jfif", null, null, "Sterling Bank" },
                    { 61, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9342), "System", "tala.png", null, null, "Tala" },
                    { 62, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9343), "System", "tonik.png", null, null, "Tonik" },
                    { 63, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9344), "System", "ucpb.png", null, null, "UCPB" },
                    { 64, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9345), "System", "uno.jfif", null, null, "UNO Digital Bank" },
                    { 65, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9346), "System", "unionbank.jfif", null, null, "Unionbank" },
                    { 66, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9347), "System", "visa.jfif", null, null, "Visa" },
                    { 67, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9349), "System", "wellsfargo.png", null, null, "Wells Fargo" },
                    { 68, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9350), "System", "others.jfif", null, null, "ztock" },
                    { 69, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(9351), "System", "others.jfif", null, null, "Others" }
                });

            migrationBuilder.InsertData(
                table: "UserProfiles",
                columns: new[] { "Id", "Created", "CreatedBy", "Email", "FirstName", "ImageUrl", "IsActive", "LastModified", "LastModifiedBy", "LastName", "Mobile" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8314), "System", "system@yahoo.com", "System", "", true, null, null, "", "+639267444551" },
                    { 2, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8326), "System", "nathan.pascual20@yahoo.com", "Nathan", "", true, null, null, "Pascual", "+639267444551" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Created", "CreatedBy", "ImageUrl", "LastModified", "LastModifiedBy", "Name", "ParentId" },
                values: new object[,]
                {
                    { 26, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8564), "System", "electric.png", null, null, "Electricity", 1 },
                    { 27, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8565), "System", "gas.png", null, null, "Gas", 1 },
                    { 28, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8566), "System", "internet.png", null, null, "Internet", 1 },
                    { 29, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8568), "System", "mobile.png", null, null, "Mobile", 1 },
                    { 30, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8569), "System", "telephone.png", null, null, "Phone", 1 },
                    { 31, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8571), "System", "water.png", null, null, "Water", 1 },
                    { 32, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8572), "System", "alcoholic-drink.png", null, null, "Alcohol & Bar", 2 },
                    { 33, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8574), "System", "coffee.png", null, null, "Coffee shops", 2 },
                    { 34, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8575), "System", "fastfood.png", null, null, "Fast Food", 2 },
                    { 35, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8577), "System", "restaurant.png", null, null, "Restaurant", 2 },
                    { 36, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8578), "System", "books.png", null, null, "Books & Stationery", 3 },
                    { 37, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8580), "System", "schoolfee.png", null, null, "School Fee", 3 },
                    { 38, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8581), "System", "tuition.png", null, null, "Tuition Fee", 3 },
                    { 39, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8583), "System", "amusement.png", null, null, "Amusement", 4 },
                    { 40, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8584), "System", "arts.png", null, null, "Arts", 4 },
                    { 41, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8586), "System", "cable.png", null, null, "Cable or DTH", 4 },
                    { 42, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8587), "System", "movies.png", null, null, "Movies & Cinema", 4 },
                    { 43, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8589), "System", "music.png", null, null, "Music", 4 },
                    { 44, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8590), "System", "newspaper.png", null, null, "Newspapers & Magazines", 4 },
                    { 45, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8592), "System", "games.png", null, null, "Games", 4 },
                    { 46, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8593), "System", "happybirthday.png", null, null, "Birthday", 5 },
                    { 47, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8595), "System", "gettogether.png", null, null, "Get Together", 5 },
                    { 48, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8596), "System", "wedding.png", null, null, "Wedding", 5 },
                    { 49, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8598), "System", "kidsactivities.png", null, null, "Kids Activities", 6 },
                    { 50, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8599), "System", "oldagecare.png", null, null, "Old age care", 6 },
                    { 51, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8601), "System", "atm.png", null, null, "ATM Fee", 7 },
                    { 52, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8602), "System", "commission.png", null, null, "Commission Fee", 7 },
                    { 53, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8605), "System", "latefee.png", null, null, "Late Fee", 7 },
                    { 54, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8607), "System", "servicefee.png", null, null, "Service Fee", 7 },
                    { 55, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8608), "System", "charity.png", null, null, "Charity", 9 },
                    { 56, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8610), "System", "gift.png", null, null, "Gift", 9 },
                    { 57, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8611), "System", "dentist.png", null, null, "Dentist", 10 },
                    { 58, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8613), "System", "doctor.png", null, null, "Doctor", 10 },
                    { 59, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8614), "System", "gym.png", null, null, "Gym", 10 },
                    { 60, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8616), "System", "pharmacy.png", null, null, "Pharmacy", 10 },
                    { 61, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8617), "System", "spamassage.png", null, null, "Spa & Massage", 10 },
                    { 62, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8619), "System", "housemaintenance.png", null, null, "House Maintenance", 11 },
                    { 63, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8620), "System", "rent.png", null, null, "House Rent", 11 },
                    { 64, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8622), "System", "autoinsurance.png", null, null, "Auto Insurance", 12 },
                    { 65, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8652), "System", "healthinsurance.png", null, null, "Health Insurance", 12 },
                    { 66, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8654), "System", "propertyinsurance.png", null, null, "Property Insurance", 12 },
                    { 67, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8656), "System", "carloan.png", null, null, "Car Loan", 15 },
                    { 68, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8658), "System", "credit.png", null, null, "Credit Card", 15 },
                    { 69, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8659), "System", "homeloan.png", null, null, "Home Loan", 15 },
                    { 70, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8661), "System", "loan.png", null, null, "Loan", 15 },
                    { 71, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8663), "System", "hairsalon.png", null, null, "Hair & Salon", 18 },
                    { 72, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8665), "System", "laundry.png", null, null, "Laundry", 18 },
                    { 73, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8666), "System", "clothing.png", null, null, "Clothing", 20 },
                    { 74, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8668), "System", "electronics.png", null, null, "Electronics & Accessories", 20 },
                    { 75, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8669), "System", "giftstoys.png", null, null, "Gifts &  Toys", 20 },
                    { 76, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8671), "System", "healthandbeauty.png", null, null, "Health & Beauty", 20 },
                    { 77, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8672), "System", "homeandfurnishing.png", null, null, "Home & furnishing", 20 },
                    { 78, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8674), "System", "jewelry.png", null, null, "Jewellery", 20 },
                    { 79, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8675), "System", "lawnandgarden.png", null, null, "Lawn & Garden", 20 },
                    { 80, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8677), "System", "pets.png", null, null, "Pets & Animals", 20 },
                    { 81, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8678), "System", "sports.png", null, null, "Sports", 20 },
                    { 82, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8680), "System", "withholdingtaxes.png", null, null, "Withholding Tax", 21 },
                    { 83, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8681), "System", "localtaxes.png", null, null, "Local Tax", 21 },
                    { 84, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8683), "System", "propertytax.png", null, null, "Property Tax", 21 },
                    { 85, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8684), "System", "salestax.png", null, null, "Sales Tax", 21 },
                    { 86, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8686), "System", "carmaintenance.png", null, null, "Car Maintenance", 23 },
                    { 87, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8687), "System", "fuel.png", null, null, "Fuel & Gas", 23 },
                    { 88, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8688), "System", "publictransport.png", null, null, "Public Transport", 23 },
                    { 89, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8690), "System", "taxi.png", null, null, "Taxi", 23 },
                    { 90, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8691), "System", "tnvs.png", null, null, "TNVS", 23 },
                    { 91, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8693), "System", "airtravel.png", null, null, "Air Travel", 24 },
                    { 92, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8694), "System", "hotel.png", null, null, "Hotel", 24 },
                    { 93, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8696), "System", "seatravel.png", null, null, "Sea Travel", 24 },
                    { 94, new DateTime(2023, 12, 31, 20, 59, 35, 564, DateTimeKind.Local).AddTicks(8697), "System", "tnvs.png", null, null, "Rental Car", 24 }
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
