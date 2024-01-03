using ExpenseTrackerPro.Application.Common.Interfaces;
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
        //services.AddDbContext<ApplicationDbContext>(options =>
        //            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddDbContext<ApplicationDbContext>(options =>
                     options.UseSqlServer(configuration.GetConnectionString("DevelopmentConnection")));

        //services.AddDbContext<ApplicationDbContext>(options =>
        //           options.UseSqlServer(configuration.GetConnectionString("TestConnection")));

        //services.AddDbContext<ApplicationDbContext>(options =>
        //             options.UseSqlServer(configuration.GetConnectionString("ProductionConnection")));


        //services.AddDbContext<ApplicationDbContext>(options =>
        //             options.UseSqlServer(configuration.GetConnectionString("ProductionConnectionV1")));

        services.AddHttpContextAccessor();
        services.AddTransient<IDateTime, DateTimeService>();
        services.AddSingleton<ICurrentUserService, CurrentUserService>();
      

        services.AddTransient(typeof(IRepositoryAsync<>), typeof(RepositoryAsync<>));
        services.AddTransient<IUnitOfWork, UnitOfWork>();


    }
}