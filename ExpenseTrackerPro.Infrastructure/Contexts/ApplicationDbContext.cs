using ExpenseTrackerPro.Application.Common.Interfaces;
using ExpenseTrackerPro.Domain.Contracts;
using ExpenseTrackerPro.Domain.Entities;
using ExpenseTrackerPro.Shared.Enums;
using Microsoft.EntityFrameworkCore;

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
    public DbSet<Transaction> Transactions => Set<Transaction>();

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
            .HasOne(t => t.Sender)
            .WithMany(a => a.Senders)
            .HasForeignKey(t => t.SenderId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Transfer>()
            .HasOne(t => t.Receiver)
            .WithMany(a => a.Receivers)
            .HasForeignKey(t => t.ReceiverId)
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
             new AccountType("Bank Account", Classification.Cash.ToString(), "bank.png") { Id = 1, Created = DateTime.Now, CreatedBy = "System" },
             new AccountType("Cash", Classification.Cash.ToString(), "cash.png") { Id = 2, Created = DateTime.Now, CreatedBy = "System" },
             new AccountType("Wallet", Classification.Cash.ToString(), "wallet.png") { Id = 3, Created = DateTime.Now, CreatedBy = "System" },
             new AccountType("Checking", Classification.Cash.ToString(), "checking.png") { Id = 4, Created = DateTime.Now, CreatedBy = "System" },
             new AccountType("Saving", Classification.Cash.ToString(), "savings.png") { Id = 5, Created = DateTime.Now, CreatedBy = "System" },
             new AccountType("Retirement", Classification.Investment.ToString(), "retirement.png") { Id = 6, Created = DateTime.Now, CreatedBy = "System" },
             new AccountType("Brokerage", Classification.Investment.ToString(), "brokerage.png") { Id = 7, Created = DateTime.Now, CreatedBy = "System" },
             new AccountType("Investment", Classification.Investment.ToString(), "investment.png") { Id = 8, Created = DateTime.Now, CreatedBy = "System" },
             new AccountType("Insurance", Classification.Investment.ToString(), "insurance.png") { Id = 9, Created = DateTime.Now, CreatedBy = "System" },
             new AccountType("Crypto", Classification.Investment.ToString(), "crypto.png") { Id = 10, Created = DateTime.Now, CreatedBy = "System" },
             new AccountType("Property", Classification.Assets.ToString(), "property.png") { Id = 11, Created = DateTime.Now, CreatedBy = "System" },
             new AccountType("Other Account", Classification.OtherAccount.ToString(), "bank.png") { Id = 12, Created = DateTime.Now, CreatedBy = "System" }
        );
    }

    private void AddInstitutions(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Institution>().HasData(
             new Institution("AB Capital", "abcapital.png") { Id = 1, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("AUB", "aub.png") { Id = 2, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("American Express", "amex.png") { Id = 3, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("Apple Card", "applecard.png") { Id = 4, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("Atome", "atome.jfif") { Id = 5, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("BDO", "bdo.png") { Id = 6, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("BPI", "bpi.png") { Id = 7, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("Bank of Commerce", "bankofcommerce.jfif") { Id = 8, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("Bank of Makati", "bankofmakati.png") { Id = 9, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("Barclays", "barclays.jfif") { Id = 10, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("Bayad", "bayad.png") { Id = 11, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("Billease", "billease.png") { Id = 12, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("Binance Exchange", "binance.png") { Id = 13, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("CARD Bank", "others.jfif") { Id = 14, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("CIMB", "cimb.png") { Id = 15, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("COL Financial", "colfinancial.png") { Id = 16, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("Cashalo", "cashalo.jfif") { Id = 17, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("Cebuana Lhullier", "cebuana.png") { Id = 18, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("China Bank", "chinabank.jfif") { Id = 19, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("Citibank", "citibank.jfif") { Id = 20, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("CliQQ", "cliqq.jfif") { Id = 21, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("Coinbase", "coinbase.png") { Id = 22, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("Coins.ph", "coinph.jfif") { Id = 23, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("Deutche", "deutsche.png") { Id = 24, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("DiskarTech", "diskarTech.jfif") { Id = 25, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("DragonFi", "others.jfif") { Id = 26, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("EastWest Bank", "eastwest.jfif") { Id = 27, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("Ficco", "ficco.png") { Id = 28, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("Gcash", "gcash.png") { Id = 29, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("GoTrade", "gotrade.png") { Id = 30, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("GoTyme Bank", "gotyme.png") { Id = 31, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("GrabPay", "grab.jfif") { Id = 32, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("Home Credit", "homecredit.jfif") { Id = 33, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("HSBC", "hsbc.png") { Id = 34, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("ING", "ing.jfif") { Id = 35, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("ING Bank", "ing.jfif") { Id = 36, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("Komo", "komo.jfif") { Id = 37, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("KuCoin", "kucoin.png") { Id = 38, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("Landbank", "landbank.jfif") { Id = 39, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("Lazada", "lazada.jfif") { Id = 40, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("Mastercard", "mastercard.png") { Id = 41, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("Maya", "maya.png") { Id = 42, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("Maybank", "maybank.png") { Id = 43, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("Metrobank", "metrobank.png") { Id = 44, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("Netbank", "netbank.png") { Id = 45, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("OwnBank", "ownbank.jfif") { Id = 46, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("PBCOM", "pbcom.jfif") { Id = 47, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("PNB", "pnb.png") { Id = 48, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("PSBank", "psbank.jfif") { Id = 49, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("Pag-Ibig", "pagibig.jfif") { Id = 50, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("PayMaya", "paymaya.png") { Id = 51, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("PayPal", "paypal.png") { Id = 52, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("Pletina", "plentina.png") { Id = 53, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("RCBC", "rcbc.jfif") { Id = 54, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("RobinsonsBank", "robinsonsbank.png") { Id = 55, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("Seabank", "seabank.png") { Id = 56, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("Security Bank", "securitybank.jfif") { Id = 57, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("ShopeePay", "shopeepay.jfif") { Id = 58, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("Standard Chartered", "standardchartered.png") { Id = 59, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("Sterling Bank", "sterlingbank.jfif") { Id = 60, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("Tala", "tala.png") { Id = 61, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("Tonik", "tonik.png") { Id = 62, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("UCPB", "ucpb.png") { Id = 63, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("UNO Digital Bank", "uno.jfif") { Id = 64, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("Unionbank", "unionbank.jfif") { Id = 65, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("Visa", "visa.jfif") { Id = 66, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("Wells Fargo", "wellsfargo.png") { Id = 67, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("ztock", "others.jfif") { Id = 68, Created = DateTime.Now, CreatedBy = "System" },
            new Institution("Others", "others.jfif") { Id = 69, Created = DateTime.Now, CreatedBy = "System" }

        );
    }

    private void AddIncomeCategory(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<IncomeCategory>().HasData(
            new IncomeCategory("Bonus", "bonus.png") { Id = 1, Created = DateTime.Now, CreatedBy = "System" },
            new IncomeCategory("Brokerage", "brokerage.png") { Id = 2, Created = DateTime.Now, CreatedBy = "System" },
            new IncomeCategory("Business & Profession", "business.png") { Id = 3, Created = DateTime.Now, CreatedBy = "System" },
            new IncomeCategory("Coupons", "coupon.png") { Id = 4, Created = DateTime.Now, CreatedBy = "System" },
            new IncomeCategory("Credit", "credit.png") { Id = 5, Created = DateTime.Now, CreatedBy = "System" },
            new IncomeCategory("Gifts", "gift.png") { Id = 6, Created = DateTime.Now, CreatedBy = "System" },
            new IncomeCategory("Interest", "interest.png") { Id = 7, Created = DateTime.Now, CreatedBy = "System" },
            new IncomeCategory("Investments", "investment.png") { Id = 8, Created = DateTime.Now, CreatedBy = "System" },
            new IncomeCategory("Loan", "loan.png") { Id = 9, Created = DateTime.Now, CreatedBy = "System" },
            new IncomeCategory("Lottery, Gambling", "gambling.png") { Id = 10, Created = DateTime.Now, CreatedBy = "System" },
            new IncomeCategory("Mutual Funds", "mutualfunds.png") { Id = 11, Created = DateTime.Now, CreatedBy = "System" },
            new IncomeCategory("Refunds", "refund.png") { Id = 12, Created = DateTime.Now, CreatedBy = "System" },
            new IncomeCategory("Reimbursement", "reimbursement.png") { Id = 13, Created = DateTime.Now, CreatedBy = "System" },
            new IncomeCategory("Rental Income", "rental.png") { Id = 14, Created = DateTime.Now, CreatedBy = "System" },
            new IncomeCategory("Salary & Paycheck", "salary.png") { Id = 15, Created = DateTime.Now, CreatedBy = "System" },
            new IncomeCategory("Savings", "savings.png") { Id = 16, Created = DateTime.Now, CreatedBy = "System" },
            new IncomeCategory("Selling Income", "selling.png") { Id = 17, Created = DateTime.Now, CreatedBy = "System" },
            new IncomeCategory("Transfer", "transfer.png") { Id = 18, Created = DateTime.Now, CreatedBy = "System" },
            new IncomeCategory("Wages & Tips", "wage.png") { Id = 19, Created = DateTime.Now, CreatedBy = "System" },
            new IncomeCategory("Others", "others.png") { Id = 20, Created = DateTime.Now, CreatedBy = "System" }
        );
    }

    private void AddCategory(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasData(
           new Category(null, "Bills & Utilities", "bills.png") { Id = 1, Created = DateTime.Now, CreatedBy = "System" },
            new Category(null, "Drink & Dine", "drinkanddine.png") { Id = 2, Created = DateTime.Now, CreatedBy = "System" },
            new Category(null, "Education", "education.png") { Id = 3, Created = DateTime.Now, CreatedBy = "System" },
            new Category(null, "Entertainment", "entertainment.png") { Id = 4, Created = DateTime.Now, CreatedBy = "System" },
            new Category(null, "Events", "events.png") { Id = 5, Created = DateTime.Now, CreatedBy = "System" },
            new Category(null, "Family Care", "familycare.png") { Id = 6, Created = DateTime.Now, CreatedBy = "System" },
            new Category(null, "Fees & Charges", "fees.png") { Id = 7, Created = DateTime.Now, CreatedBy = "System" },
            new Category(null, "Food & Grocery", "foodandgrocery.png") { Id = 8, Created = DateTime.Now, CreatedBy = "System" },
            new Category(null, "Gifts & Donations", "giftanddonation.png") { Id = 9, Created = DateTime.Now, CreatedBy = "System" },
            new Category(null, "Health & Fitness", "healthandfitness.png") { Id = 10, Created = DateTime.Now, CreatedBy = "System" },
            new Category(null, "House", "house.png") { Id = 11, Created = DateTime.Now, CreatedBy = "System" },
            new Category(null, "Insurance", "insurance.png") { Id = 12, Created = DateTime.Now, CreatedBy = "System" },
            new Category(null, "Investments", "investment.png") { Id = 13, Created = DateTime.Now, CreatedBy = "System" },
            new Category(null, "Kids Care", "kidscare.png") { Id = 14, Created = DateTime.Now, CreatedBy = "System" },
            new Category(null, "Loan & Debts", "loan.png") { Id = 15, Created = DateTime.Now, CreatedBy = "System" },
            new Category(null, "Misc Expenses", "misc.png") { Id = 16, Created = DateTime.Now, CreatedBy = "System" },
            new Category(null, "Office Expenses", "office.png") { Id = 17, Created = DateTime.Now, CreatedBy = "System" },
            new Category(null, "Personal Care", "personalcare.png") { Id = 18, Created = DateTime.Now, CreatedBy = "System" },
            new Category(null, "Pet Care", "petcare.png") { Id = 19, Created = DateTime.Now, CreatedBy = "System" },
            new Category(null, "Shopping", "shopping.png") { Id = 20, Created = DateTime.Now, CreatedBy = "System" },
            new Category(null, "Taxes", "taxes.png") { Id = 21, Created = DateTime.Now, CreatedBy = "System" },
            new Category(null, "Transfer", "transfer.png") { Id = 22, Created = DateTime.Now, CreatedBy = "System" },
            new Category(null, "Transport", "transport.png") { Id = 23, Created = DateTime.Now, CreatedBy = "System" },
            new Category(null, "Travel & Vacation", "travel.png") { Id = 24, Created = DateTime.Now, CreatedBy = "System" },
            new Category(null, "Others", "others.png") { Id = 25, Created = DateTime.Now, CreatedBy = "System" },
            new Category(1, "Electricity", "electric.png") { Id = 26, Created = DateTime.Now, CreatedBy = "System" },
            new Category(1, "Gas", "gas.png") { Id = 27, Created = DateTime.Now, CreatedBy = "System" },
            new Category(1, "Internet", "internet.png") { Id = 28, Created = DateTime.Now, CreatedBy = "System" },
            new Category(1, "Mobile", "mobile.png") { Id = 29, Created = DateTime.Now, CreatedBy = "System" },
            new Category(1, "Phone", "telephone.png") { Id = 30, Created = DateTime.Now, CreatedBy = "System" },
            new Category(1, "Water", "water.png") { Id = 31, Created = DateTime.Now, CreatedBy = "System" },
            new Category(2, "Alcohol & Bar", "alcoholic-drink.png") { Id = 32, Created = DateTime.Now, CreatedBy = "System" },
            new Category(2, "Coffee shops", "coffee.png") { Id = 33, Created = DateTime.Now, CreatedBy = "System" },
            new Category(2, "Fast Food", "fastfood.png") { Id = 34, Created = DateTime.Now, CreatedBy = "System" },
            new Category(2, "Restaurant", "restaurant.png") { Id = 35, Created = DateTime.Now, CreatedBy = "System" },
            new Category(3, "Books & Stationery", "books.png") { Id = 36, Created = DateTime.Now, CreatedBy = "System" },
            new Category(3, "School Fee", "schoolfee.png") { Id = 37, Created = DateTime.Now, CreatedBy = "System" },
            new Category(3, "Tuition Fee", "tuition.png") { Id = 38, Created = DateTime.Now, CreatedBy = "System" },
            new Category(4, "Amusement", "amusement.png") { Id = 39, Created = DateTime.Now, CreatedBy = "System" },
            new Category(4, "Arts", "arts.png") { Id = 40, Created = DateTime.Now, CreatedBy = "System" },
            new Category(4, "Cable or DTH", "cable.png") { Id = 41, Created = DateTime.Now, CreatedBy = "System" },
            new Category(4, "Movies & Cinema", "movies.png") { Id = 42, Created = DateTime.Now, CreatedBy = "System" },
            new Category(4, "Music", "music.png") { Id = 43, Created = DateTime.Now, CreatedBy = "System" },
            new Category(4, "Newspapers & Magazines", "newspaper.png") { Id = 44, Created = DateTime.Now, CreatedBy = "System" },
            new Category(4, "Games", "games.png") { Id = 45, Created = DateTime.Now, CreatedBy = "System" },
            new Category(5, "Birthday", "happybirthday.png") { Id = 46, Created = DateTime.Now, CreatedBy = "System" },
            new Category(5, "Get Together", "gettogether.png") { Id = 47, Created = DateTime.Now, CreatedBy = "System" },
            new Category(5, "Wedding", "wedding.png") { Id = 48, Created = DateTime.Now, CreatedBy = "System" },
            new Category(6, "Kids Activities", "kidsactivities.png") { Id = 49, Created = DateTime.Now, CreatedBy = "System" },
            new Category(6, "Old age care", "oldagecare.png") { Id = 50, Created = DateTime.Now, CreatedBy = "System" },
            new Category(7, "ATM Fee", "atm.png") { Id = 51, Created = DateTime.Now, CreatedBy = "System" },
            new Category(7, "Commission Fee", "commission.png") { Id = 52, Created = DateTime.Now, CreatedBy = "System" },
            new Category(7, "Late Fee", "latefee.png") { Id = 53, Created = DateTime.Now, CreatedBy = "System" },
            new Category(7, "Service Fee", "servicefee.png") { Id = 54, Created = DateTime.Now, CreatedBy = "System" },
            new Category(9, "Charity", "charity.png") { Id = 55, Created = DateTime.Now, CreatedBy = "System" },
            new Category(9, "Gift", "gift.png") { Id = 56, Created = DateTime.Now, CreatedBy = "System" },
            new Category(10, "Dentist", "dentist.png") { Id = 57, Created = DateTime.Now, CreatedBy = "System" },
            new Category(10, "Doctor", "doctor.png") { Id = 58, Created = DateTime.Now, CreatedBy = "System" },
            new Category(10, "Gym", "gym.png") { Id = 59, Created = DateTime.Now, CreatedBy = "System" },
            new Category(10, "Pharmacy", "pharmacy.png") { Id = 60, Created = DateTime.Now, CreatedBy = "System" },
            new Category(10, "Spa & Massage", "spamassage.png") { Id = 61, Created = DateTime.Now, CreatedBy = "System" },
            new Category(11, "House Maintenance", "housemaintenance.png") { Id = 62, Created = DateTime.Now, CreatedBy = "System" },
            new Category(11, "House Rent", "rent.png") { Id = 63, Created = DateTime.Now, CreatedBy = "System" },
            new Category(12, "Auto Insurance", "autoinsurance.png") { Id = 64, Created = DateTime.Now, CreatedBy = "System" },
            new Category(12, "Health Insurance", "healthinsurance.png") { Id = 65, Created = DateTime.Now, CreatedBy = "System" },
            new Category(12, "Property Insurance", "propertyinsurance.png") { Id = 66, Created = DateTime.Now, CreatedBy = "System" },
            new Category(15, "Car Loan", "carloan.png") { Id = 67, Created = DateTime.Now, CreatedBy = "System" },
            new Category(15, "Credit Card", "credit.png") { Id = 68, Created = DateTime.Now, CreatedBy = "System" },
            new Category(15, "Home Loan", "homeloan.png") { Id = 69, Created = DateTime.Now, CreatedBy = "System" },
            new Category(15, "Loan", "loan.png") { Id = 70, Created = DateTime.Now, CreatedBy = "System" },
            new Category(18, "Hair & Salon", "hairsalon.png") { Id = 71, Created = DateTime.Now, CreatedBy = "System" },
            new Category(18, "Laundry", "laundry.png") { Id = 72, Created = DateTime.Now, CreatedBy = "System" },
            new Category(20, "Clothing", "clothing.png") { Id = 73, Created = DateTime.Now, CreatedBy = "System" },
            new Category(20, "Electronics & Accessories", "electronics.png") { Id = 74, Created = DateTime.Now, CreatedBy = "System" },
            new Category(20, "Gifts &  Toys", "giftstoys.png") { Id = 75, Created = DateTime.Now, CreatedBy = "System" },
            new Category(20, "Health & Beauty", "healthandbeauty.png") { Id = 76, Created = DateTime.Now, CreatedBy = "System" },
            new Category(20, "Home & furnishing", "homeandfurnishing.png") { Id = 77, Created = DateTime.Now, CreatedBy = "System" },
            new Category(20, "Jewellery", "jewelry.png") { Id = 78, Created = DateTime.Now, CreatedBy = "System" },
            new Category(20, "Lawn & Garden", "lawnandgarden.png") { Id = 79, Created = DateTime.Now, CreatedBy = "System" },
            new Category(20, "Pets & Animals", "pets.png") { Id = 80, Created = DateTime.Now, CreatedBy = "System" },
            new Category(20, "Sports", "sports.png") { Id = 81, Created = DateTime.Now, CreatedBy = "System" },
            new Category(21, "Withholding Tax", "withholdingtaxes.png") { Id = 82, Created = DateTime.Now, CreatedBy = "System" },
            new Category(21, "Local Tax", "localtaxes.png") { Id = 83, Created = DateTime.Now, CreatedBy = "System" },
            new Category(21, "Property Tax", "propertytax.png") { Id = 84, Created = DateTime.Now, CreatedBy = "System" },
            new Category(21, "Sales Tax", "salestax.png") { Id = 85, Created = DateTime.Now, CreatedBy = "System" },
            new Category(23, "Car Maintenance", "carmaintenance.png") { Id = 86, Created = DateTime.Now, CreatedBy = "System" },
            new Category(23, "Fuel & Gas", "fuel.png") { Id = 87, Created = DateTime.Now, CreatedBy = "System" },
            new Category(23, "Public Transport", "publictransport.png") { Id = 88, Created = DateTime.Now, CreatedBy = "System" },
            new Category(23, "Taxi", "taxi.png") { Id = 89, Created = DateTime.Now, CreatedBy = "System" },
            new Category(23, "TNVS", "tnvs.png") { Id = 90, Created = DateTime.Now, CreatedBy = "System" },
            new Category(24, "Air Travel", "airtravel.png") { Id = 91, Created = DateTime.Now, CreatedBy = "System" },
            new Category(24, "Hotel", "hotel.png") { Id = 92, Created = DateTime.Now, CreatedBy = "System" },
            new Category(24, "Sea Travel", "seatravel.png") { Id = 93, Created = DateTime.Now, CreatedBy = "System" },
            new Category(24, "Rental Car", "tnvs.png") { Id = 94, Created = DateTime.Now, CreatedBy = "System" }
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