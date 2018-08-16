using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Linq;


namespace AspNetCore.Fundamentals.SecureWebAPI
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
            services.AddAuthentication(options => {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;                
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters.RoleClaimType = "role";
                    options.TokenValidationParameters.NameClaimType = "name";
                    options.TokenValidationParameters.ValidAudience = "resource_server";
                    options.TokenValidationParameters.ValidIssuer = "https://localhost:44370/";
                    options.Authority = "https://localhost:44370/";

                    //Get keys from Autherity and make one or more SigningCredentials
                    //We don't add any key it for sample only
                    //var siningKeys = new List<SigningCredentials>();                 
                    //Define Siging Key or keys
                    //options.TokenValidationParameters.IssuerSigningKey = siningKeys.Select(c => c.Key).First();
                    //Or you can assign collection of key
                    //options.TokenValidationParameters.IssuerSigningKeys = siningKeys.Select(c => c.Key);


                });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseSwaggerWithOAuth();
            app.UseMvc();
        }
    }
}
