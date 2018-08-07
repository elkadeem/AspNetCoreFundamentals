using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspNetCore.Fundamentals.ClientWebApp.Pages
{
    public class LogoutModel : PageModel
    {
        public ActionResult OnGet()
        {
            return SignOut(CookieAuthenticationDefaults.AuthenticationScheme, OpenIdConnectDefaults.AuthenticationScheme);
        }

        public ActionResult OnPost()
        {
            return SignOut(new AuthenticationProperties()
            {
                RedirectUri = "/"
            }, CookieAuthenticationDefaults.AuthenticationScheme
                , OpenIdConnectDefaults.AuthenticationScheme);
        }
    }
}