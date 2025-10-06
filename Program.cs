using Financials.Components;
using Financials.Data;
using Financials.Data.Services;
using Microsoft.EntityFrameworkCore;

namespace Financials
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();
            
            // Add controllers for API endpoints
            builder.Services.AddControllers();
            
            // Add real database services
            builder.Services.AddScoped<AssetService>();
            builder.Services.AddScoped<DebtService>();
            builder.Services.AddScoped<InvestmentService>();
            builder.Services.AddScoped<SavingsService>();
            builder.Services.AddScoped<DatabaseInitializationService>();
            
            // Add database context
            builder.Services.AddDbContext<FinancialsContext>(options =>
                options.UseSqlite("Data Source=financials.sqlite"));

            var app = builder.Build();

            // Initialize database with seed data
            using (var scope = app.Services.CreateScope())
            {
                var dbInitService = scope.ServiceProvider.GetRequiredService<DatabaseInitializationService>();
                await dbInitService.InitializeDatabaseAsync();
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = ctx =>
                {
                    // Ensure CSS files are served with proper content type
                    if (ctx.File.Name.EndsWith(".css"))
                    {
                        ctx.Context.Response.Headers.Append("Content-Type", "text/css");
                    }
                }
            });
            app.UseRouting();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();
            
            // Map API controllers
            app.MapControllers();

            app.Run();
        }
    }
}