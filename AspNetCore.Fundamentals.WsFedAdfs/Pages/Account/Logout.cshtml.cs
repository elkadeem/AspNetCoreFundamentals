using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspNetCore.FundamentalsWithGoogleOnly.Pages.Account
{
    [AllowAnonymous]
    public class LogoutModel : PageModel
    {
        public ActionResult OnPost(string returnUrl = null)
        {
            return SignOut(new Microsoft.AspNetCore.Authentication.AuthenticationProperties
            {
                RedirectUri = returnUrl?? "/"
            }
           , Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme
           , Microsoft.AspNetCore.Authentication.WsFederation.WsFederationDefaults.AuthenticationScheme
           );
        }
    }
}