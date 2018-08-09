using Microsoft.AspNetCore.Authentication.Google;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AspNetCore.FundamentalsWithGoogleOnly
{
    public static class ClaimIdentityExtension
    {
        public static string GetImageUrl(this System.Security.Principal.IIdentity identity)
        {
            ClaimsIdentity claimIdentity = identity as ClaimsIdentity;

            if (claimIdentity == null)
                return string.Empty;

            if(claimIdentity.AuthenticationType == GoogleDefaults.DisplayName)
            {
                var claim = claimIdentity.FindFirst("urn:google:image");
                if (claim != null)
                    return claim.Value;
            }

            return string.Empty;

        }

        public static string GetProfileUrl(this System.Security.Principal.IIdentity identity)
        {
            ClaimsIdentity claimIdentity = identity as ClaimsIdentity;

            if (claimIdentity == null)
                return string.Empty;

            if (claimIdentity.AuthenticationType == GoogleDefaults.DisplayName)
            {
                var claim = claimIdentity.FindFirst("urn:google:profile");
                if (claim != null)
                    return claim.Value;
            }

            return string.Empty;

        }

    }
}
