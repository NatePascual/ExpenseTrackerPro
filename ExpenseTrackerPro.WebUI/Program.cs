using ExpenseTrackerPro.WebUI.Components;
using ExpenseTrackerPro.Application.Extensions;
using ExpenseTrackerPro.Infrastructure.Extensions;
using ExpenseTrackerPro.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
namespace ExpenseTrackerPro.WebUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .Build();

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents();
            builder.Services.AddApplicationLayer();
            builder.Services.AddInfrastructure(configuration);

            //if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            //{
            //    builder.Services.AddDbContext<ApplicationDbContext>(options =>
            //             options.UseInMemoryDatabase("ExpenseTrackerPro"));
            //}
            //else
            //{
            //    builder.Services.AddDbContext<ApplicationDbContext>(options =>
            //             options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            //}

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>();

            app.Run();
        }
    }
}
