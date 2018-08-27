using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.WsFederation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore.Fundamentals.WSFedAD
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

            //var handler = new System.Net.Http.HttpClientHandler();
            //handler.Proxy = new WebProxy(new Uri("http://127.0.0.1:8888/"), true);
            //System.Net.Http.HttpClient client = new System.Net.Http.HttpClient(handler);

            services.AddAuthentication(sharedOptions =>
            {
                sharedOptions.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                sharedOptions.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                sharedOptions.DefaultChallengeScheme = WsFederationDefaults.AuthenticationScheme;
            })
                .AddWsFederation(options =>
                {
                    //Microsoft.AspNetCore.Authentication.WsFederation.WsFederationDefaults.AuthenticationScheme
                    // MetadataAddress represents the Active Directory instance used to authenticate users.
                    options.MetadataAddress = "https://login.microsoftonline.com/fb45ee2f-4833-45c5-811f-6b09cdac3578/federationmetadata/2007-06/federationmetadata.xml";

                    // For AAD, use the App ID URI from the app registration's Properties blade:
                    options.Wtrealm = "https://elkadeemhotmail.onmicrosoft.com/516f8ce1-59ec-4cd8-ad5a-6b3c2cb12ae3";
                    //options.Backchannel = client;
                })
                 .AddCookie();

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
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
