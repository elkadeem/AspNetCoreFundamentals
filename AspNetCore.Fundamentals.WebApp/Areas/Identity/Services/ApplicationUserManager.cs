using AspNetCore.Fundamentals.WebApp.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Fundamentals.WebApp.Areas.Identity.Services
{
    public class ApplicationUserManager : UserManager<AppUser>
    {
        public ApplicationUserManager(IUserStore<AppUser> store
            , IOptions<IdentityOptions> optionsAccessor
            , IPasswordHasher<AppUser> passwordHasher
            , IEnumerable<IUserValidator<AppUser>> userValidators
            , IEnumerable<IPasswordValidator<AppUser>> passwordValidators
            , ILookupNormalizer keyNormalizer
            , IdentityErrorDescriber errors
            , IServiceProvider services
            , ILogger<UserManager<AppUser>> logger) : base(store
                , optionsAccessor
                , passwordHasher
                , userValidators
                , passwordValidators
                , keyNormalizer
                , errors
                , services
                , logger)
        {

        }

        
    }
}
