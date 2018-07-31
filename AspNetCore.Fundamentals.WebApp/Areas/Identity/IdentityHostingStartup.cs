using System;
using AspNetCore.Fundamentals.WebApp.Areas.Identity.Data;
using AspNetCore.Fundamentals.WebApp.Areas.Identity.Services;
using AspNetCore.Fundamentals.WebApp.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(AspNetCore.Fundamentals.WebApp.Areas.Identity.IdentityHostingStartup))]
namespace AspNetCore.Fundamentals.WebApp.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            services.AddDbContext<IdentityDbContext>(options =>
                options.UseSqlServer(context.Configuration.GetConnectionString("IdentityDbContextConnection"))
                //options.UseInMemoryDatabase(Guid.NewGuid().ToString())
                        );

                //services.AddDefaultIdentity<AppUser>()
                //    .AddEntityFrameworkStores<IdentityDbContext>();

                services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityDbContext>()
                .AddDefaultTokenProviders()
                .AddDefaultUI();

                services.Configure<IdentityOptions>(options =>
                {
                    options.Password.RequiredLength = 8;

                    options.Lockout.MaxFailedAccessAttempts = 10;
                    options.Lockout.AllowedForNewUsers = true;

                    options.SignIn.RequireConfirmedEmail = true;

                    options.User.RequireUniqueEmail = true;
                });

                //services.ConfigureApplicationCookie(options => {
                //    options.AccessDeniedPath = "/Identity/Account/AccessDenied";                    
                //    options.Cookie.HttpOnly = true;
                //    options.ExpireTimeSpan = TimeSpan.FromMinutes(20);

                //    options.LoginPath = "/Identity/Account/Login";
                //    options.LogoutPath = "/Identity/Account/Logout";
                //});

                services.AddSingleton<IEmailSender, EmailSender>();

            });
        }
    }
}