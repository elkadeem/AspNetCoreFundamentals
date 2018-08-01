using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Fundamentals.WebApp.Autherization
{
    public class RolesRequirement : IAuthorizationRequirement
    {
        public IReadOnlyList<string> Roles => _roles.AsReadOnly();
        private readonly List<string> _roles;
        public RolesRequirement(params string[] roles)
        {
            if (roles == null)
                _roles = new List<string>();

            _roles = roles.ToList();
        }
    }
}
