using ExpenseTrackerPro.Application.Common.Interfaces;
using ExpenseTrackerPro.Domain.Contracts;
using ExpenseTrackerPro.Domain.Entities;
using ExpenseTrackerPro.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace ExpenseTrackerPro.Infrastructure.Contexts;

public class ApplicationDbContext : DbContext
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTime _dateTime;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
                                ICurrentUserService currentUserService,
                                IDateTime dateTime) : base(options)
    {
        _currentUserService = currentUserService;
        _dateTime = dateTime;
    }


    public DbSet<AccountType> AccountTypes => Set<AccountType>();
    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<Expense> Expenses => Set<Expense>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Income> Incomes => Set<Income>();
    public DbSet<IncomeCategory> IncomeCategories => Set<IncomeCategory>();
    public DbSet<Transfer> Transfers => Set<Transfer>();
    public DbSet<UserProfile> UserProfiles => Set<UserProfile>();
    public DbSet<Institution> Institutions => Set<Institution>();
    public DbSet<Currency> Currencies => Set<Currency>();

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<BaseAuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = _currentUserService.UserId;
                    entry.Entity.Created = _dateTime.Now;
                    break;
                case EntityState.Modified:
                    entry.Entity.LastModifiedBy = _currentUserService.UserId;
                    entry.Entity.LastModified = _dateTime.Now;
                    break;
                case EntityState.Detached:
                    break;
                case EntityState.Unchanged:
                    break;
                case EntityState.Deleted:
                    break;
                default:
                    break;
            }
        }
        return await base.SaveChangesAsync(cancellationToken);
    }

    public override int SaveChanges()
    {
        foreach (var entry in ChangeTracker.Entries<BaseAuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = _currentUserService.UserId;
                    entry.Entity.Created = _dateTime.Now;
                    break;
                case EntityState.Modified:
                    entry.Entity.LastModifiedBy = _currentUserService.UserId;
                    entry.Entity.LastModified = _dateTime.Now;
                    break;
                case EntityState.Detached:
                    break;
                case EntityState.Unchanged:
                    break;
                case EntityState.Deleted:
                    break;
                default:
                    break;
            }
        }
        return base.SaveChangesAsync().GetAwaiter().GetResult();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        AddUser(modelBuilder);
        AddCategory(modelBuilder: modelBuilder);
        AddCurrencies(modelBuilder: modelBuilder);
        AddAccountTypes(modelBuilder: modelBuilder);    
        AddIncomeCategory(modelBuilder: modelBuilder);
        AddInstitutions(modelBuilder: modelBuilder);

        modelBuilder.Entity<Transfer>()
            .HasOne(t => t.FromAccount)
            .WithMany(a => a.TransfersFrom)
            .HasForeignKey(t => t.FromAccountId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Transfer>()
            .HasOne(t => t.ToAccount)
            .WithMany(a => a.TransfersTo)
            .HasForeignKey(t => t.ToAccountId)
            .OnDelete(DeleteBehavior.Restrict);

        base.OnModelCreating(modelBuilder);
    }

    private void AddCurrencies(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Currency>().HasData(
            new Currency("Albania Lek", "ALL", "Lek") { Id = 1, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Afghanistan Afghani", "AFN", "؋") { Id = 2, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Argentina Peso", "ARS", "$") { Id = 3, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Aruba Guilder", "AWG", "ƒ") { Id = 4, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Australia Dollar", "AUD", "$") { Id = 5, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Azerbaijan Manat", "AZN", "₼") { Id = 6, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Bahamas Dollar", "BSD", "$") { Id = 7, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Barbados Dollar", "BBD", "$") { Id = 8, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Belarus Ruble", "BYN", "Br") { Id = 9, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Belize Dollar", "BZD", "BZ$") { Id = 10, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Bermuda Dollar", "BMD", "$") { Id = 11, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Bolivia Bolíviano", "BOB", "$b") { Id = 12, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Bosnia and Herzegovina Convertible Mark", "BAM", "KM") { Id = 13, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Botswana Pula", "BWP", "P") { Id = 14, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Bulgaria Lev", "BGN", "лв") { Id = 15, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Brazil Real", "BRL", "R$") { Id = 16, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Brunei Darussalam Dollar", "BND", "$") { Id = 17, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Cambodia Riel", "KHR", "៛") { Id = 18, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Canada Dollar", "CAD", "$") { Id = 19, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Cayman Islands Dollar", "KYD", "$") { Id = 20, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Chile Peso", "CLP", "$") { Id = 21, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("China Yuan Renminbi", "CNY", "¥") { Id = 22, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Colombia Peso", "COP", "$") { Id = 23, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Costa Rica Colon", "CRC", "₡") { Id = 24, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Croatia Kuna", "HRK", "kn") { Id = 25, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Cuba Peso", "CUP", "₱") { Id = 26, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Czech Republic Koruna", "CZK", "Kč") { Id = 27, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Denmark Krone", "DKK", "kr") { Id = 28, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Dominican Republic Peso", "DOP", "RD$") { Id = 29, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("East Caribbean Dollar", "XCD", "$") { Id = 30, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Egypt Pound", "EGP", "£") { Id = 31, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("El Salvador Colon", "SVC", "$") { Id = 32, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Euro Member Countries", "EUR", "€") { Id = 33, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Falkland Islands (Malvinas) Pound", "FKP", "£") { Id = 34, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Fiji Dollar", "FJD", "$") { Id = 35, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Ghana Cedi", "GHS", "¢") { Id = 36, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Gibraltar Pound", "GIP", "£") { Id = 37, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Guatemala Quetzal", "GTQ", "Q") { Id = 38, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Guernsey Pound", "GGP", "£") { Id = 39, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Guyana Dollar", "GYD", "$") { Id = 40, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Honduras Lempira", "HNL", "L") { Id = 41, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Hong Kong Dollar", "HKD", "$") { Id = 42, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Hungary Forint", "HUF", "Ft") { Id = 43, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Iceland Krona", "ISK", "kr") { Id = 44, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("India Rupee", "INR", "₹") { Id = 45, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Indonesia Rupiah", "IDR", "Rp") { Id = 46, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Iran Rial", "IRR", "﷼") { Id = 47, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Isle of Man Pound", "IMP", "£") { Id = 48, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Israel Shekel", "ILS", "₪") { Id = 49, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Jamaica Dollar", "JMD", "J$") { Id = 50, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Japan Yen", "JPY", "¥") { Id = 51, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Jersey Pound", "JEP", "£") { Id = 52, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Kazakhstan Tenge", "KZT", "лв") { Id = 53, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Korea (North) Won", "KPW", "₩") { Id = 54, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Korea (South) Won", "KRW", "₩") { Id = 55, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Kyrgyzstan Som", "KGS", "лв") { Id = 56, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Laos Kip", "LAK", "₭") { Id = 57, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Lebanon Pound", "LBP", "£") { Id = 58, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Liberia Dollar", "LRD", "$") { Id = 59, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Macedonia Denar", "MKD", "ден") { Id = 60, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Malaysia Ringgit", "MYR", "RM") { Id = 61, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Mauritius Rupee", "MUR", "₨") { Id = 62, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Mexico Peso", "MXN", "$") { Id = 63, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Mongolia Tughrik", "MNT", "₮") { Id = 64, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Moroccan-dirham", "MNT", " د.إ") { Id = 65, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Mozambique Metical", "MZN", "MT") { Id = 66, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Namibia Dollar", "NAD", "$") { Id = 67, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Nepal Rupee", "NPR", "₨") { Id = 68, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Netherlands Antilles Guilder", "ANG", "ƒ") { Id = 69, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("New Zealand Dollar", "NZD", "$") { Id = 70, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Nicaragua Cordoba", "NIO", "C$") { Id = 71, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Nigeria Naira", "NGN", "₦") { Id = 72, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Norway Krone", "NOK", "kr") { Id = 73, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Oman Rial", "OMR", "﷼") { Id = 74, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Pakistan Rupee", "PKR", "₨") { Id = 75, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Panama Balboa", "PAB", "B/.") { Id = 76, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Paraguay Guarani", "PYG", "Gs") { Id = 77, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Peru Sol", "PEN", "S/.") { Id = 78, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Philippines Peso", "PHP", "₱") { Id = 79, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Poland Zloty", "PLN", "zł") { Id = 80, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Qatar Riyal", "QAR", "﷼") { Id = 81, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Romania Leu", "RON", "lei") { Id = 82, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Russia Ruble", "RUB", "₽") { Id = 83, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Saint Helena Pound", "SHP", "£") { Id = 84, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Saudi Arabia Riyal", "SAR", "﷼") { Id = 85, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Serbia Dinar", "RSD", "Дин.") { Id = 86, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Seychelles Rupee", "SCR", "₨") { Id = 87, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Singapore Dollar", "SGD", "$") { Id = 88, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Solomon Islands Dollar", "SBD", "$") { Id = 89, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Somalia Shilling", "SOS", "S") { Id = 90, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("South Korean Won", "KRW", "₩") { Id = 91, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("South Africa Rand", "ZAR", "R") { Id = 92, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Sri Lanka Rupee", "LKR", "₨") { Id = 93, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Sweden Krona", "SEK", "kr") { Id = 94, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Switzerland Franc", "CHF", "CHF") { Id = 95, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Suriname Dollar", "SRD", "$") { Id = 96, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Syria Pound", "SYP", "£") { Id = 97, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Taiwan New Dollar", "TWD", "NT$") { Id = 98, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Thailand Baht", "THB", "฿") { Id = 99, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Trinidad and Tobago Dollar", "TTD", "TT$") { Id = 100, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Turkey Lira", "TRY", "₺") { Id = 101, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Tuvalu Dollar", "TVD", "$") { Id = 102, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Ukraine Hryvnia", "UAH", "₴") { Id = 103, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("UAE-Dirham", "AED", " د.إ") { Id = 104, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("United Kingdom Pound", "GBP", "£") { Id = 105, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("United States Dollar", "USD", "$") { Id = 106, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Uruguay Peso", "UYU", "$U") { Id = 107, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Uzbekistan Som", "UZS", "лв") { Id = 108, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Venezuela Bolívar", "VEF", "Bs") { Id = 109, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Viet Nam Dong", "VND", "₫") { Id = 110, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Yemen Rial", "YER", "﷼") { Id = 111, Created = DateTime.Now, CreatedBy = "System" },
            new Currency("Zimbabwe Dollar", "ZWD", "Z$") { Id = 112, Created = DateTime.Now, CreatedBy = "System" }
        );
    }

    private void AddAccountTypes(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AccountType>().HasData(
             new AccountType("Bank Account", Classification.Cash.ToString(), "Images\\AccountType\\BankAccount.jpg") { Id = 1, Created = DateTime.Now, CreatedBy = "System" },
             new AccountType("Cash", Classification.Cash.ToString(), "Images\\AccountType\\Cash.jpg") { Id = 2, Created = DateTime.Now, CreatedBy = "System" },
             new AccountType("Wallet", Classification.Cash.ToString(), "Images\\AccountType\\Wallet.jpg") { Id = 3, Created = DateTime.Now, CreatedBy = "System" },
             new AccountType("Checking", Classification.Cash.ToString(), "Images\\AccountType\\Checking.jpg") { Id = 4, Created = DateTime.Now, CreatedBy = "System" },
             new AccountType("Saving", Classification.Cash.ToString(), "Images\\AccountType\\Saving.jpg") { Id = 5, Created = DateTime.Now, CreatedBy = "System" },
             new AccountType("Credit Card", Classification.Credit.ToString(), "Images\\AccountType\\CreditCard.jpg") { Id = 6, Created = DateTime.Now, CreatedBy = "System" },
             new AccountType("Line of Credit", Classification.Credit.ToString(), "Images\\AccountType\\LineofCredit.jpg") { Id = 7, Created = DateTime.Now, CreatedBy = "System" },
             new AccountType("Retirement", Classification.Investment.ToString(), "Images\\AccountType\\Retirement.jpg") { Id = 8, Created = DateTime.Now, CreatedBy = "System" },
             new AccountType("Brokerage", Classification.Investment.ToString(), "Images\\AccountType\\Brokerage.jpg") { Id = 9, Created = DateTime.Now, CreatedBy = "System" },
             new AccountType("Investment", Classification.Investment.ToString(), "Images\\AccountType\\Investment.jpg") { Id = 10, Created = DateTime.Now, CreatedBy = "System" },
             new AccountType("Insurance", Classification.Investment.ToString(), "Images\\AccountType\\Insurance.jpg") { Id = 11, Created = DateTime.Now, CreatedBy = "System" },
             new AccountType("Crypto", Classification.Investment.ToString(), "Images\\AccountType\\Crypto.jpg") { Id = 12, Created = DateTime.Now, CreatedBy = "System" },
             new AccountType("Loan", Classification.Loans.ToString(), "Images\\AccountType\\Loan.jpg") { Id = 13, Created = DateTime.Now, CreatedBy = "System" },
             new AccountType("Mortgage", Classification.Loans.ToString(), "Images\\AccountType\\Mortgage.jpg") { Id = 14, Created = DateTime.Now, CreatedBy = "System" },
             new AccountType("Property", Classification.Assets.ToString(), "Images\\AccountType\\Property.jpg") { Id = 15, Created = DateTime.Now, CreatedBy = "System" },
             new AccountType("Other Account", Classification.OtherAccount.ToString(), "Images\\AccountType\\OtherAccount.jpg") { Id = 16, Created = DateTime.Now, CreatedBy = "System" }
        );
    }

    private void AddInstitutions(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Institution>().HasData(
            new Institution("AB Capital", "Images\\Institution\\ABCapital.jpg") { Id = 1, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("AUB", "Images\\Institution\\AUB.jpg") { Id = 2, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("American Express", "Images\\Institution\\AmericanExpress.jpg") { Id = 3, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("Apple Card", "Images\\Institution\\AppleCard.jpg") { Id = 4, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("Atome", "Images\\Institution\\Atome.jpg") { Id = 5, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("BDO", "Images\\Institution\\BDO.jpg") { Id = 6, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("BPI", "Images\\Institution\\BPI.jpg") { Id = 7, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("Bank of Commerce", "Images\\Institution\\BankofCommerce.jpg") { Id = 8, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("Bank of Makati", "Images\\Institution\\BankofMakati.jpg") { Id = 9, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("Barclays", "Images\\Institution\\Barclays.jpg") { Id = 10, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("Bayad", "Images\\Institution\\Bayad.jpg") { Id = 11, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("Billease", "Images\\Institution\\Billease.jpg") { Id = 12, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("Binance Exchange", "Images\\Institution\\BinanceExchange.jpg") { Id = 13, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("CARD Bank", "Images\\Institution\\CARDBank.jpg") { Id = 14, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("CIMB", "Images\\Institution\\CIMB.jpg") { Id = 15, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("COL Financial", "Images\\Institution\\COLFinancial.jpg") { Id = 16, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("Cashalo", "Images\\Institution\\Cashalo.jpg") { Id = 17, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("Cebuana Lhullier", "Images\\Institution\\CebuanaLhullier.jpg") { Id = 18, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("China Bank", "Images\\Institution\\ChinaBank.jpg") { Id = 19, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("Citibank", "Images\\Institution\\Citibank.jpg") { Id = 20, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("CliQQ", "Images\\Institution\\CliQQ.jpg") { Id = 21, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("Coinbase", "Images\\Institution\\Coinbase.jpg") { Id = 22, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("Coins.ph", "Images\\Institution\\Coins.ph.jpg") { Id = 23, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("Deutche", "Images\\Institution\\Deutche.jpg") { Id = 24, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("DiskarTech", "Images\\Institution\\DiskarTech.jpg") { Id = 25, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("DragonFi", "Images\\Institution\\DragonFi.jpg") { Id = 26, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("EastWest Bank", "Images\\Institution\\EastWestBank.jpg") { Id = 27, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("Ficco", "Images\\Institution\\Ficco.jpg") { Id = 28, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("Gcash", "Images\\Institution\\Gcash.jpg") { Id = 29, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("GoTrade", "Images\\Institution\\GoTrade.jpg") { Id = 30, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("GoTyme Bank", "Images\\Institution\\GoTymeBank.jpg") { Id = 31, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("GrabPay", "Images\\Institution\\GrabPay.jpg") { Id = 32, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("Home Credit", "Images\\Institution\\HomeCredit.jpg") { Id = 33, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("HSBC", "Images\\Institution\\HSBC.jpg") { Id = 34, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("ING", "Images\\Institution\\ING.jpg") { Id = 35, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("ING Bank", "Images\\Institution\\ING Bank.jpg") { Id = 36, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("Komo", "Images\\Institution\\Komo.jpg") { Id = 37, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("KuCoin", "Images\\Institution\\KuCoin.jpg") { Id = 38, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("Landbank", "Images\\Institution\\Landbank.jpg") { Id = 39, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("Lazada", "Images\\Institution\\Lazada.jpg") { Id = 40, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("Mastercard", "Images\\Institution\\Mastercard.jpg") { Id = 41, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("Maya", "Images\\Institution\\Maya.jpg") { Id = 42, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("Maybank", "Images\\Institution\\Maybank.jpg") { Id = 43, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("Metrobank", "Images\\Institution\\Metrobank.jpg") { Id = 44, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("Netbank", "Images\\Institution\\Netbank.jpg") { Id = 45, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("OwnBank", "Images\\Institution\\OwnBank.jpg") { Id = 46, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("PBCOM", "Images\\Institution\\PBCOM.jpg") { Id = 47, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("PNB", "Images\\Institution\\PNB.jpg") { Id = 48, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("PSBank", "Images\\Institution\\PSBank.jpg") { Id = 49, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("Pag-Ibig", "Images\\Institution\\Pag-Ibig.jpg") { Id = 50, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("PayMaya", "Images\\Institution\\PayMaya.jpg") { Id = 51, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("PayPal", "Images\\Institution\\PayPal.jpg") { Id = 52, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("PNB", "Images\\Institution\\PNB.jpg") { Id = 53, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("Pletina", "Images\\Institution\\Pletina.jpg") { Id = 54, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("RCBC", "Images\\Institution\\RCBC.jpg") { Id = 55, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("RobinsonsBank", "Images\\Institution\\RobinsonsBank.jpg") { Id = 56, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("Seabank", "Images\\Institution\\Seabank.jpg") { Id = 57, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("Security Bank", "Images\\Institution\\SecurityBank.jpg") { Id = 58, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("ShopeePay", "Images\\Institution\\ShopeePay.jpg") { Id = 59, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("Standard Chartered", "Images\\Institution\\StandardChartered.jpg") { Id = 60, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("Sterling Bank", "Images\\Institution\\SterlingBank.jpg") { Id = 61, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("Tala", "Images\\Institution\\Tala.jpg") { Id = 62, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("Tonik", "Images\\Institution\\Tonik.jpg") { Id = 63, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("UCPB", "Images\\Institution\\UCPB.jpg") { Id = 64, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("UNO Digital Bank", "Images\\Institution\\UNODigitalBank.jpg") { Id = 65, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("Unionbank", "Images\\Institution\\Unionbank.jpg") { Id = 66, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("Visa", "Images\\Institution\\Visa.jpg") { Id = 67, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("Wells Fargo", "Images\\Institution\\Wells Fargo.jpg") { Id = 68, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("ztock", "Images\\Institution\\ztock.jpg") { Id = 69, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("Others", "Images\\Institution\\Others.jpg") { Id = 70, Created = DateTime.Now, CreatedBy = "System" }
        );
    }

    private void AddIncomeCategory(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<IncomeCategory>().HasData(
            new IncomeCategory("Bonus", "Images\\IncomeCategory\\Bonus.jpg") { Id = 1, Created = DateTime.Now, CreatedBy = "System" },
            new IncomeCategory("Brokerage", "Images\\IncomeCategory\\Brokerage.jpg") { Id = 2, Created = DateTime.Now, CreatedBy = "System" },
            new IncomeCategory("Business & Profession", "Images\\IncomeCategory\\BusinessAndProfession.jpg") { Id = 3, Created = DateTime.Now, CreatedBy = "System" },
            new IncomeCategory("Coupons", "Images\\IncomeCategory\\Coupons.jpg") { Id = 4, Created = DateTime.Now, CreatedBy = "System" },
            new IncomeCategory("Credit", "Images\\IncomeCategory\\Credit.jpg") { Id = 5, Created = DateTime.Now, CreatedBy = "System" },
            new IncomeCategory("Gifts", "Images\\IncomeCategory\\Gifts.jpg") { Id = 6, Created = DateTime.Now, CreatedBy = "System" },
            new IncomeCategory("Interest", "Images\\IncomeCategory\\Interest.jpg") { Id = 7, Created = DateTime.Now, CreatedBy = "System" },
            new IncomeCategory("Investments", "Images\\IncomeCategory\\Investments.jpg") { Id = 8, Created = DateTime.Now, CreatedBy = "System" },
            new IncomeCategory("Loan", "Images\\IncomeCategory\\Loan.jpg") { Id = 9, Created = DateTime.Now, CreatedBy = "System" },
            new IncomeCategory("Lottery, Gambling", "Images\\IncomeCategory\\LotteryGambling.jpg") { Id = 10, Created = DateTime.Now, CreatedBy = "System" },
            new IncomeCategory("Mutual Funds", "Images\\IncomeCategory\\MutualFunds.jpg") { Id = 11, Created = DateTime.Now, CreatedBy = "System" },
            new IncomeCategory("Refunds", "Images\\IncomeCategory\\Refunds.jpg") { Id = 12, Created = DateTime.Now, CreatedBy = "System" },
            new IncomeCategory("Reimbursement", "Images\\IncomeCategory\\Reimbursement.jpg") { Id = 13, Created = DateTime.Now, CreatedBy = "System" },
            new IncomeCategory("Rental Income", "Images\\IncomeCategory\\RentalIncome.jpg") { Id = 14, Created = DateTime.Now, CreatedBy = "System" },
            new IncomeCategory("Salary & Paycheck", "Images\\IncomeCategory\\SalaryAndPaycheck.jpg") { Id = 15, Created = DateTime.Now, CreatedBy = "System" },
            new IncomeCategory("Savings", "Images\\IncomeCategory\\Savings.jpg") { Id = 16, Created = DateTime.Now, CreatedBy = "System" },
            new IncomeCategory("Selling Income", "Images\\IncomeCategory\\SellingIncome.jpg") { Id = 17, Created = DateTime.Now, CreatedBy = "System" },
            new IncomeCategory("Transfer", "Images\\IncomeCategory\\Transfer.jpg") { Id = 18, Created = DateTime.Now, CreatedBy = "System" },
            new IncomeCategory("Wages & Tips", "Images\\IncomeCategory\\WagesAndTips.jpg") { Id = 19, Created = DateTime.Now, CreatedBy = "System" },
            new IncomeCategory("Others", "Images\\IncomeCategory\\Others.jpg") { Id = 20, Created = DateTime.Now, CreatedBy = "System" }
        );
    }

    private void AddCategory(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasData(
           new Category(null, "Bills & Utilities", "Images\\Category\\") { Id = 1, Created = DateTime.Now, CreatedBy = "System" },
            new Category(null, "Drink & Dine", "Images\\Category\\") { Id = 2, Created = DateTime.Now, CreatedBy = "System" },
            new Category(null, "Education", "Images\\Category\\") { Id = 3, Created = DateTime.Now, CreatedBy = "System" },
            new Category(null, "Entertainment", "Images\\Category\\") { Id = 4, Created = DateTime.Now, CreatedBy = "System" },
            new Category(null, "Events", "Images\\Category\\") { Id = 5, Created = DateTime.Now, CreatedBy = "System" },
            new Category(null, "Family Care", "Images\\Category\\") { Id = 6, Created = DateTime.Now, CreatedBy = "System" },
            new Category(null, "Fees & Charges", "Images\\Category\\") { Id = 7, Created = DateTime.Now, CreatedBy = "System" },
            new Category(null, "Food & Grocery", "Images\\Category\\") { Id = 8, Created = DateTime.Now, CreatedBy = "System" },
            new Category(null, "Gifts & Donations", "Images\\Category\\") { Id = 9, Created = DateTime.Now, CreatedBy = "System" },
            new Category(null, "Health & Fitness", "Images\\Category\\") { Id = 10, Created = DateTime.Now, CreatedBy = "System" },
            new Category(null, "House", "Images\\Category\\") { Id = 11, Created = DateTime.Now, CreatedBy = "System" },
            new Category(null, "Insurance", "Images\\Category\\") { Id = 12, Created = DateTime.Now, CreatedBy = "System" },
            new Category(null, "Investments", "Images\\Category\\") { Id = 13, Created = DateTime.Now, CreatedBy = "System" },
            new Category(null, "Kids Care", "Images\\Category\\") { Id = 14, Created = DateTime.Now, CreatedBy = "System" },
            new Category(null, "Loan & Debts", "Images\\Category\\") { Id = 15, Created = DateTime.Now, CreatedBy = "System" },
            new Category(null, "Misc Expenses", "Images\\Category\\") { Id = 16, Created = DateTime.Now, CreatedBy = "System" },
            new Category(null, "Office Expenses", "Images\\Category\\") { Id = 17, Created = DateTime.Now, CreatedBy = "System" },
            new Category(null, "Personal Care", "Images\\Category\\") { Id = 18, Created = DateTime.Now, CreatedBy = "System" },
            new Category(null, "Pet Care", "Images\\Category\\") { Id = 19, Created = DateTime.Now, CreatedBy = "System" },
            new Category(null, "Shopping", "Images\\Category\\") { Id = 20, Created = DateTime.Now, CreatedBy = "System" },
            new Category(null, "Taxes", "Images\\Category\\") { Id = 21, Created = DateTime.Now, CreatedBy = "System" },
            new Category(null, "Transfer", "Images\\Category\\") { Id = 22, Created = DateTime.Now, CreatedBy = "System" },
            new Category(null, "Transport", "Images\\Category\\") { Id = 23, Created = DateTime.Now, CreatedBy = "System" },
            new Category(null, "Travel & Vacation", "Images\\Category\\") { Id = 24, Created = DateTime.Now, CreatedBy = "System" },
            new Category(null, "Others", "Images\\Category\\") { Id = 25, Created = DateTime.Now, CreatedBy = "System" },
            new Category(1, "Electricity", "Images\\Category\\") { Id = 26, Created = DateTime.Now, CreatedBy = "System" },
            new Category(1, "Gas", "Images\\Category\\") { Id = 27, Created = DateTime.Now, CreatedBy = "System" },
            new Category(1, "Internet", "Images\\Category\\") { Id = 28, Created = DateTime.Now, CreatedBy = "System" },
            new Category(1, "Mobile", "Images\\Category\\") { Id = 29, Created = DateTime.Now, CreatedBy = "System" },
            new Category(1, "Phone", "Images\\Category\\") { Id = 30, Created = DateTime.Now, CreatedBy = "System" },
            new Category(1, "Water", "Images\\Category\\") { Id = 31, Created = DateTime.Now, CreatedBy = "System" },
            new Category(2, "Alcohol & Bar", "Images\\Category\\") { Id = 32, Created = DateTime.Now, CreatedBy = "System" },
            new Category(2, "Coffee shops", "Images\\Category\\") { Id = 33, Created = DateTime.Now, CreatedBy = "System" },
            new Category(2, "Fast Food", "Images\\Category\\") { Id = 34, Created = DateTime.Now, CreatedBy = "System" },
            new Category(2, "Restaurant", "Images\\Category\\") { Id = 35, Created = DateTime.Now, CreatedBy = "System" },
            new Category(3, "Books & Stationery", "Images\\Category\\") { Id = 36, Created = DateTime.Now, CreatedBy = "System" },
            new Category(3, "School Fee", "Images\\Category\\") { Id = 37, Created = DateTime.Now, CreatedBy = "System" },
            new Category(3, "Tuition Fee", "Images\\Category\\") { Id = 38, Created = DateTime.Now, CreatedBy = "System" },
            new Category(4, "Amusement", "Images\\Category\\") { Id = 39, Created = DateTime.Now, CreatedBy = "System" },
            new Category(4, "Arts", "Images\\Category\\") { Id = 40, Created = DateTime.Now, CreatedBy = "System" },
            new Category(4, "Cable or DTH", "Images\\Category\\") { Id = 41, Created = DateTime.Now, CreatedBy = "System" },
            new Category(4, "Movies & Cinema", "Images\\Category\\") { Id = 42, Created = DateTime.Now, CreatedBy = "System" },
            new Category(4, "Music", "Images\\Category\\") { Id = 43, Created = DateTime.Now, CreatedBy = "System" },
            new Category(4, "Newspapers & Magazines", "Images\\Category\\") { Id = 44, Created = DateTime.Now, CreatedBy = "System" },
            new Category(4, "Games", "Images\\Category\\") { Id = 45, Created = DateTime.Now, CreatedBy = "System" },
            new Category(5, "Birthday", "Images\\Category\\") { Id = 46, Created = DateTime.Now, CreatedBy = "System" },
            new Category(5, "Get Together", "Images\\Category\\") { Id = 47, Created = DateTime.Now, CreatedBy = "System" },
            new Category(5, "Wedding", "Images\\Category\\") { Id = 48, Created = DateTime.Now, CreatedBy = "System" },
            new Category(6, "Kids Activities", "Images\\Category\\") { Id = 49, Created = DateTime.Now, CreatedBy = "System" },
            new Category(6, "Old age care", "Images\\Category\\") { Id = 50, Created = DateTime.Now, CreatedBy = "System" },
            new Category(7, "ATM Fee", "Images\\Category\\") { Id = 51, Created = DateTime.Now, CreatedBy = "System" },
            new Category(7, "Commission Fee", "Images\\Category\\") { Id = 52, Created = DateTime.Now, CreatedBy = "System" },
            new Category(7, "Late Fee", "Images\\Category\\") { Id = 53, Created = DateTime.Now, CreatedBy = "System" },
            new Category(7, "Service Fee", "Images\\Category\\") { Id = 54, Created = DateTime.Now, CreatedBy = "System" },
            new Category(9, "Charity", "Images\\Category\\") { Id = 55, Created = DateTime.Now, CreatedBy = "System" },
            new Category(9, "Gift", "Images\\Category\\") { Id = 56, Created = DateTime.Now, CreatedBy = "System" },
            new Category(10, "Dentist", "Images\\Category\\") { Id = 57, Created = DateTime.Now, CreatedBy = "System" },
            new Category(10, "Doctor", "Images\\Category\\") { Id = 58, Created = DateTime.Now, CreatedBy = "System" },
            new Category(10, "Gym", "Images\\Category\\") { Id = 59, Created = DateTime.Now, CreatedBy = "System" },
            new Category(10, "Pharmacy", "Images\\Category\\") { Id = 60, Created = DateTime.Now, CreatedBy = "System" },
            new Category(10, "Spa & Massage", "Images\\Category\\") { Id = 61, Created = DateTime.Now, CreatedBy = "System" },
            new Category(11, "House Maintenance", "Images\\Category\\") { Id = 62, Created = DateTime.Now, CreatedBy = "System" },
            new Category(11, "House Rent", "Images\\Category\\") { Id = 63, Created = DateTime.Now, CreatedBy = "System" },
            new Category(12, "Auto Insurance", "Images\\Category\\") { Id = 64, Created = DateTime.Now, CreatedBy = "System" },
            new Category(12, "Health Insurance", "Images\\Category\\") { Id = 65, Created = DateTime.Now, CreatedBy = "System" },
            new Category(12, "Property Insurance", "Images\\Category\\") { Id = 66, Created = DateTime.Now, CreatedBy = "System" },
            new Category(15, "Car Loan", "Images\\Category\\") { Id = 67, Created = DateTime.Now, CreatedBy = "System" },
            new Category(15, "Credit Card", "Images\\Category\\") { Id = 68, Created = DateTime.Now, CreatedBy = "System" },
            new Category(15, "Home Loan", "Images\\Category\\") { Id = 69, Created = DateTime.Now, CreatedBy = "System" },
            new Category(15, "Loan", "Images\\Category\\") { Id = 70, Created = DateTime.Now, CreatedBy = "System" },
            new Category(18, "Hair & Salon", "Images\\Category\\") { Id = 71, Created = DateTime.Now, CreatedBy = "System" },
            new Category(18, "Laundry", "Images\\Category\\") { Id = 72, Created = DateTime.Now, CreatedBy = "System" },
            new Category(20, "Clothing", "Images\\Category\\") { Id = 73, Created = DateTime.Now, CreatedBy = "System" },
            new Category(20, "Electronics & Accessories", "Images\\Category\\") { Id = 74, Created = DateTime.Now, CreatedBy = "System" },
            new Category(20, "Gifts &  Toys", "Images\\Category\\") { Id = 75, Created = DateTime.Now, CreatedBy = "System" },
            new Category(20, "Health & Beauty", "Images\\Category\\") { Id = 76, Created = DateTime.Now, CreatedBy = "System" },
            new Category(20, "Home & furnishing", "Images\\Category\\") { Id = 77, Created = DateTime.Now, CreatedBy = "System" },
            new Category(20, "Jewellery", "Images\\Category\\") { Id = 78, Created = DateTime.Now, CreatedBy = "System" },
            new Category(20, "Lawn & Garden", "Images\\Category\\") { Id = 79, Created = DateTime.Now, CreatedBy = "System" },
            new Category(20, "Pets & Animals", "Images\\Category\\") { Id = 80, Created = DateTime.Now, CreatedBy = "System" },
            new Category(20, "Sports", "Images\\Category\\") { Id = 81, Created = DateTime.Now, CreatedBy = "System" },
            new Category(21, "Withholding Tax", "Images\\Category\\") { Id = 82, Created = DateTime.Now, CreatedBy = "System" },
            new Category(21, "Local Tax", "Images\\Category\\") { Id = 83, Created = DateTime.Now, CreatedBy = "System" },
            new Category(21, "Property Tax", "Images\\Category\\") { Id = 84, Created = DateTime.Now, CreatedBy = "System" },
            new Category(21, "Sales Tax", "Images\\Category\\") { Id = 85, Created = DateTime.Now, CreatedBy = "System" },
            new Category(23, "Car Maintenance", "Images\\Category\\") { Id = 86, Created = DateTime.Now, CreatedBy = "System" },
            new Category(23, "Fuel & Gas", "Images\\Category\\") { Id = 87, Created = DateTime.Now, CreatedBy = "System" },
            new Category(23, "Public Transport", "Images\\Category\\") { Id = 88, Created = DateTime.Now, CreatedBy = "System" },
            new Category(23, "Taxi", "Images\\Category\\") { Id = 89, Created = DateTime.Now, CreatedBy = "System" },
            new Category(23, "TNVS", "Images\\Category\\") { Id = 90, Created = DateTime.Now, CreatedBy = "System" },
            new Category(24, "Air Travel", "Images\\Category\\") { Id = 91, Created = DateTime.Now, CreatedBy = "System" },
            new Category(24, "Hotel", "Images\\Category\\") { Id = 92, Created = DateTime.Now, CreatedBy = "System" },
            new Category(24, "Sea Travel", "Images\\Category\\") { Id = 93, Created = DateTime.Now, CreatedBy = "System" },
            new Category(24, "Rental Car", "Images\\Category\\") { Id = 94, Created = DateTime.Now, CreatedBy = "System" }
        );
    }

    private void AddUser(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserProfile>().HasData(
            new UserProfile("System", "", "system@yahoo.com", "+639267444551", "", true) { Id = 1, Created = DateTime.Now, CreatedBy = "System" },
            new UserProfile("Nathan", "Pascual", "nathan.pascual20@yahoo.com", "+639267444551", "", true) { Id= 2,Created=DateTime.Now, CreatedBy="System" }
        ); 
    }
}