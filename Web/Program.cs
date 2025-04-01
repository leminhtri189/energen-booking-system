using BusinessLogicLayer;
using DataAccessLayer;
using Microsoft.AspNetCore.Authentication.Cookies;
using Shared;

namespace Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var appServices = builder.Services;
            var appConfiguration = builder.Configuration;

            appServices
            .ConfigureDataAccessLayer(appConfiguration)
            .ConfigureBusinessLogicLayer(appConfiguration)
            .ConfigureSharedLibrary(appConfiguration);

            appServices.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(config => {
                    config.Cookie.Name = "cookie";
                    config.LoginPath = "/Account/Login";
                    config.AccessDeniedPath = "/Account/Login";
                    config.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                });

            // Add services to the container.
            appServices.AddAutoMapper(typeof(Program));
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
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
