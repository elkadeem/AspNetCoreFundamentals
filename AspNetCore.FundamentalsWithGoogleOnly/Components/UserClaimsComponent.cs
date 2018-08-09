using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AspNetCore.FundamentalsWithGoogleOnly.Components
{
    //https://docs.microsoft.com/en-us/aspnet/core/mvc/views/view-components?view=aspnetcore-2.1
    [ViewComponent(Name = "UserClaims")]
    public class UserClaims : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            if (this.UserClaimsPrincipal != null)
            {
                return View("Default", UserClaimsPrincipal.Claims);
            }

            //Don't return null model.
            return View("Default", new List<Claim>());
        }
    }
}
