using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspNetCore.Fundamentals.ClientWebApp.Pages
{
    public class LoginModel : PageModel
    {
        public ActionResult OnGet()
        {
            return Challenge(new AuthenticationProperties { RedirectUri = "/" }
            , OpenIdConnectDefaults.AuthenticationScheme);
        }
    }
}