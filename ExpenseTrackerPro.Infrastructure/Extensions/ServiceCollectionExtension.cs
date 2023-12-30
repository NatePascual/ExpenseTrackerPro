using ExpenseTracker.Application.Features.BankOrInstitutions;
using ExpenseTrackerPro.Application.Common.Interfaces;
using ExpenseTrackerPro.Application.Features.Accounts;
using ExpenseTrackerPro.Application.Features.AccountTypes;
using ExpenseTrackerPro.Application.Features.Currencies;
using ExpenseTrackerPro.Application.Features.ExpenseCategories;
using ExpenseTrackerPro.Application.Features.IncomeCategories;
using ExpenseTrackerPro.Application.Infrastructure;
using ExpenseTrackerPro.Application.Services;
using ExpenseTrackerPro.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExpenseTrackerPro.Infrastructure.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
         services.AddDbContext<ApplicationDbContext>(options =>
                     options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddHttpContextAccessor();
        services.AddTransient<IDateTime, DateTimeService>();
        services.AddSingleton<ICurrentUserService, CurrentUserService>();


        services.AddTransient(typeof(IRepositoryAsync<>), typeof(RepositoryAsync<>));
        services.AddTransient<IAccountRepository, AccountRepository>();
        services.AddTransient<IAccountTypeRepository, AccountTypeRepository>();
        services.AddTransient<ICategoryRepository, CategoryRepository>();
        services.AddTransient<ICurrencyRepository, CurrencyRepository>();
        services.AddTransient<IIncomeCategoryRepository, IncomeCategoryRepository>();
        services.AddTransient<IInstitutionRepository,InstitutionRepository>();
        services.AddTransient<IUnitOfWork, UnitOfWork>();

    }
}