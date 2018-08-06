using AspNetCore.Fundamentals.WebApp.Models;
using Microsoft.Extensions.DependencyInjection;
using OpenIddict.Abstractions;
using OpenIddict.Core;
using OpenIddict.EntityFrameworkCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Fundamentals.WebApp.Autherization
{
    public static class OpenIddictExtension
    {
        public static IServiceCollection ConfigOpenIddict(this IServiceCollection services)
        {
            services.AddOpenIddict().AddCore(options =>
            {
                options.UseEntityFrameworkCore()
                .UseDbContext<IdentityDbContext>();
            }).AddServer(options =>
            {
                options.UseMvc();

                options.AddDevelopmentSigningCertificate();                

                options.EnableAuthorizationEndpoint("/connect/authorize")
                .EnableTokenEndpoint("/connect/token");

                options.AllowAuthorizationCodeFlow();
                options.AllowClientCredentialsFlow();
                options.AllowImplicitFlow();
                options.AllowPasswordFlow();
                options.AllowRefreshTokenFlow();
                                
                options.UseJsonWebTokens();
                options.AcceptAnonymousClients();
            });
            
            return services;
        }

        public static async Task InitializeAsync(IServiceProvider services)
        {
            // Create a new service scope to ensure the database context is correctly disposed when this methods returns.
            using (var scope = services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<IdentityDbContext>();
                await context.Database.EnsureCreatedAsync();

                var manager = scope.ServiceProvider.GetRequiredService<OpenIddictApplicationManager<OpenIddictApplication>>();

                if (await manager.FindByClientIdAsync("mvc") == null)
                {
                    var descriptor = new OpenIddictApplicationDescriptor
                    {
                        ClientId = "mvc",
                        ClientSecret = "901564A5-E7FE-42CB-B10D-61EF6A8F3654",
                        DisplayName = "MVC client application",
                        PostLogoutRedirectUris = { new Uri("https://localhost:44370/signout-callback-oidc") },
                        RedirectUris = { new Uri("https://localhost:44370/signin-oidc") },
                        Permissions =
                        {
                            OpenIddictConstants.Permissions.Endpoints.Authorization,
                            OpenIddictConstants.Permissions.Endpoints.Logout,
                            OpenIddictConstants.Permissions.Endpoints.Token,
                            OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode,
                            OpenIddictConstants.Permissions.GrantTypes.RefreshToken,
                            OpenIddictConstants.Permissions.Scopes.Email,
                            OpenIddictConstants.Permissions.Scopes.Profile,
                            OpenIddictConstants.Permissions.Scopes.Roles
                        }
                    };

                    await manager.CreateAsync(descriptor);
                }

                // To test this sample with Postman, use the following settings:
                //
                // * Authorization URL: http://localhost:54540/connect/authorize
                // * Access token URL: http://localhost:54540/connect/token
                // * Client ID: postman
                // * Client secret: [blank] (not used with public clients)
                // * Scope: openid email profile roles
                // * Grant type: authorization code
                // * Request access token locally: yes
                if (await manager.FindByClientIdAsync("postman") == null)
                {
                    var descriptor = new OpenIddictApplicationDescriptor
                    {
                        ClientId = "postman",
                        DisplayName = "Postman",
                        RedirectUris = { new Uri("https://www.getpostman.com/oauth2/callback") },                        
                        Permissions =
                        {
                            OpenIddictConstants.Permissions.Endpoints.Authorization,
                            OpenIddictConstants.Permissions.Endpoints.Token,
                            OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode,
                            OpenIddictConstants.Permissions.GrantTypes.Password,
                            OpenIddictConstants.Permissions.Scopes.Email,
                            OpenIddictConstants.Permissions.Scopes.Profile,
                            OpenIddictConstants.Permissions.Scopes.Roles
                        }
                    };

                    await manager.CreateAsync(descriptor);
                }



                if (await manager.FindByClientIdAsync("swagger") == null)
                {
                    var descriptor = new OpenIddictApplicationDescriptor
                    {
                        ClientId = "swagger",
                        DisplayName = "swagger",
                        PostLogoutRedirectUris = { new Uri($"http://localhost:53817/swagger/ui/o2c.html") },
                        RedirectUris = { new Uri($"http://localhost:53817/swagger/ui/o2c.html") },
                        Permissions =
                        {
                            OpenIddictConstants.Permissions.Endpoints.Authorization,                            
                            OpenIddictConstants.Permissions.GrantTypes.Implicit,                            
                            OpenIddictConstants.Permissions.Scopes.Email,
                            OpenIddictConstants.Permissions.Scopes.Profile,
                            OpenIddictConstants.Permissions.Scopes.Roles
                        }
                    };

                    await manager.CreateAsync(descriptor);
                }
            }
        }
    }
}
