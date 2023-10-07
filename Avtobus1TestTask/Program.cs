using Microsoft.EntityFrameworkCore;
using Avtobus1TestTask.Data;
using MySqlConnector;
using Avtobus1TestTask.Services.Interfaces;
using Avtobus1TestTask.Services;
using Avtobus1TestTask.Controllers;

namespace Avtobus1TestTask
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddScoped<ApplicationDbContext>();
            builder.Services.AddScoped<IUrlShortenerService, UrlShortenerService>();
            string connection = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContextPool<ApplicationDbContext>(options => options.UseMySql(connection, ServerVersion.AutoDetect(connection)));

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}