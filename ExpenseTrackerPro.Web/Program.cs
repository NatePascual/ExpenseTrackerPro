using ExpenseTrackerPro.Application.Extensions;
using ExpenseTrackerPro.Infrastructure.Extensions;
using ExpenseTrackerPro.Web.Components;
using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using MudBlazor.Services;

namespace ExpenseTrackerPro.Web
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

            StaticWebAssetsLoader.UseStaticWebAssets(builder.Environment, builder.Configuration);
            builder.Services.AddMudServices();
            // Add services to the container.
            builder.Services.AddRazorComponents();
            builder.Services.AddApplicationLayer();
            builder.Services.AddInfrastructure(configuration);
            builder.Services.AddLazyCache();
            builder.Services.AddRazorComponents().AddInteractiveServerComponents();
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

            
            app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
