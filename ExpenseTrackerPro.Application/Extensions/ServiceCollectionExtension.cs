using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MediatR;
using ExpenseTrackerPro.Application.Common.Behaviours;
using FluentValidation;
using ExpenseTracker.Application.Features.BankOrInstitutions;
using ExpenseTrackerPro.Application.Features.Accounts;
using ExpenseTrackerPro.Application.Features.AccountTypes;
using ExpenseTrackerPro.Application.Features.Currencies;
using ExpenseTrackerPro.Application.Features.ExpenseCategories;
using ExpenseTrackerPro.Application.Features.IncomeCategories;
using ExpenseTrackerPro.Application.Features.Transactions;


namespace ExpenseTrackerPro.Application.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddApplicationLayer(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
        services.AddTransient<IAccountRepository, AccountRepository>();
        services.AddTransient<IAccountTypeRepository, AccountTypeRepository>();
        services.AddTransient<ICategoryRepository, CategoryRepository>();
        services.AddTransient<ICurrencyRepository, CurrencyRepository>();
        services.AddTransient<IIncomeCategoryRepository, IncomeCategoryRepository>();
        services.AddTransient<IInstitutionRepository, InstitutionRepository>();
        services.AddTransient<ICreateTransaction, CreateTransaction>();
    }
}