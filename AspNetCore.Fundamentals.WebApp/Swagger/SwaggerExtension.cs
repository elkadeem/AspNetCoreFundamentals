using Microsoft.AspNetCore.Builder;
using NJsonSchema;
using NSwag;
using NSwag.AspNetCore;
using NSwag.SwaggerGeneration.Processors.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace AspNetCore.Fundamentals.WebApp
{
    public static class SwaggerExtension
    {
        public static void UseSwaggerWithOAuth(this IApplicationBuilder app)
        {
            app.UseSwaggerUi3(typeof(Startup).GetTypeInfo().Assembly, settings =>
            {
                settings.GeneratorSettings.DefaultPropertyNameHandling =
                    PropertyNameHandling.CamelCase;

                settings.OAuth2Client = new OAuth2ClientSettings()
                {
                    ClientId = "swagger",
                    AppName = "swagger",
                };

                settings.GeneratorSettings.DocumentProcessors.Add(new SecurityDefinitionAppender("oauth2", new SwaggerSecurityScheme
                {
                    Type = SwaggerSecuritySchemeType.OAuth2,
                    Description = "Foo",
                    Flow = SwaggerOAuth2Flow.Implicit,
                    AuthorizationUrl = "/connect/authorize",
                    TokenUrl = "/connect/token",
                    Scopes = new Dictionary<string, string>{
                        { "email", "Read access to protected resources" },
                        { "profile", "Write access to protected resources" },
                        { "roles", "Write access to protected resources" }
                    }
                }));

                settings.GeneratorSettings.OperationProcessors.Add(new OperationSecurityScopeProcessor("oauth2"));
            });
        }
    }
}
