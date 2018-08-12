using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.FundamentalsWithGoogleOnly.Components
{
    //https://docs.microsoft.com/en-us/aspnet/core/mvc/views/view-components?view=aspnetcore-2.1
    [ViewComponent(Name = "AuthenticationProperties")]
    public class AuthenticationPropertiesComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userResult = await HttpContext.AuthenticateAsync();
            var user = userResult.Principal;
            var authProperties = userResult.Properties;
            if (!userResult.Succeeded || user == null || !user.Identities.Any(c => c.IsAuthenticated))
                authProperties = new AuthenticationProperties();

            return View("Default", authProperties);
        }
    }
}
