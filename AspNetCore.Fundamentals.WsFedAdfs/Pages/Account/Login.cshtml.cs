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
    public class LoginModel : PageModel
    {
        public ActionResult OnGet()
        {
            return Challenge(new Microsoft.AspNetCore.Authentication.AuthenticationProperties()
            {
                RedirectUri = "/"
            }, Microsoft.AspNetCore.Authentication.WsFederation.WsFederationDefaults.AuthenticationScheme);
        }
    }
}