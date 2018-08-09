using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspNetCore.FundamentalsWithGoogleOnly.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            return Challenge(new AuthenticationProperties
            {
                RedirectUri = "/",
            });
        }

        public ActionResult Logout(string returnurl)
        {
            return SignOut(new AuthenticationProperties
            {
                RedirectUri = "/",
            });
        }



    }
}
