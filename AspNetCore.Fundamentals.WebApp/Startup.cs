using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AspNetCore.Fundamentals.Domain.Repository;
using AspNetCore.Fundamentals.Domain.Services;
using AspNetCore.Fundamentals.Store;
using AspNetCore.Fundamentals.WebApp.Autherization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NJsonSchema;
using NSwag.AspNetCore;

namespace AspNetCore.Fundamentals.WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<EmployeeDbContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("EmployeeConnetionString"));
            });

            services.ConfigOpenIddict();

            services.AddScoped<IEmployeesRepository, EmployeesRepository>();
            services.AddTransient<EmployeeService>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("OnlyAdmins", config =>
                {
                    config.RequireRole("Admin");
                });

                //The relation is And
                options.AddPolicy("AdminOrHR", config =>
                {
                    config.RequireRole("Admin")

                    //.RequireClaim("Department", "HR")
                    ;
                });

                //The relation is And
                options.AddPolicy("AdminOrHR2", config =>
                {
                    config.AddRequirements(new RolesRequirement("Admin")
                        , new DepartmentsRequirement("HR"));
                });
            });

            services.AddSingleton<IAuthorizationHandler, AdminOrHRAuthorizationHandler>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddRazorPagesOptions(options =>
                 {
                     options.AllowAreas = true;
                     // Set Autherization on folder
                     options.Conventions.AuthorizeFolder("/Employees");

                     options.Conventions.AuthorizeAreaFolder("Identity", "/Admin", "OnlyAdmins");

                 });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
                        
            app.UseSwaggerUi3(typeof(Startup).GetTypeInfo().Assembly, settings =>
            {                
                settings.GeneratorSettings.DefaultPropertyNameHandling =
                    PropertyNameHandling.CamelCase;
                                
                settings.OAuth2Client = new OAuth2ClientSettings()
                {
                    ClientId = "swagger",
                    AppName = "swagger",                   
                };
            });

            app.UseAuthentication();

            OpenIddictExtension.InitializeAsync(app.ApplicationServices).GetAwaiter().GetResult();
            app.UseMvc();

        }
    }
}
