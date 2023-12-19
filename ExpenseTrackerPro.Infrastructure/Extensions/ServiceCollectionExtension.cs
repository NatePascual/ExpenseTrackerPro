using ExpenseTrackerPro.Application.Common.Interfaces;
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

        //if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        //{
        //    services.AddDbContext<ApplicationDbContext>(options =>
        //             options.UseInMemoryDatabase("ExpenseTrackerPro"));
        //}
        //else
        //{
         services.AddDbContext<ApplicationDbContext>(options =>
                     options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
       // }
        services.AddHttpContextAccessor();
        services.AddTransient<IDateTime, DateTimeService>();
        services.AddSingleton<ICurrentUserService, CurrentUserService>();

    }
}