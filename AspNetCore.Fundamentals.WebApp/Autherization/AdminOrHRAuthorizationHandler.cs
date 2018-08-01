using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AspNetCore.Fundamentals.WebApp.Autherization
{
    public class AdminOrHRAuthorizationHandler : IAuthorizationHandler
    {
        public async Task HandleAsync(AuthorizationHandlerContext context)
        {
            var pendingRequirements = context.PendingRequirements
                .Where(c => c is RolesRequirement || c is DepartmentsRequirement)
                .ToList();

            var roleRequirement = pendingRequirements.Where(c => c is RolesRequirement)
                .Cast<RolesRequirement>().FirstOrDefault();
            var departmentRequirement = pendingRequirements.Where(c => c is DepartmentsRequirement)
                .Cast<DepartmentsRequirement>().FirstOrDefault();

            bool inAnyRole = false;
            bool inDepartment = false;
            if (roleRequirement != null)
                inAnyRole = await IsUserInAnyRole(context.User, roleRequirement.Roles);

            if (departmentRequirement != null)
                inDepartment = await IsUserInAnyDepartment(context.User, departmentRequirement.Departments);

            if (inAnyRole || inDepartment)
            {
                foreach (var requirement in pendingRequirements)                    
                {
                    context.Succeed(requirement);
                }
            }

        }

        private Task<bool> IsUserInAnyRole(ClaimsPrincipal user, IEnumerable<string> roles)
        {
            if (roles == null || !roles.Any())
                return Task.FromResult(false);

            foreach (var role in roles)
            {
                if (user.IsInRole(role))
                    return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }

        private Task<bool> IsUserInAnyDepartment(ClaimsPrincipal user, IEnumerable<string> departments)
        {
            if (departments == null || !departments.Any())
                return Task.FromResult(false);

            var departmentClaimValue = user.Claims.FirstOrDefault(c => c.Type == "Department");
            if (departmentClaimValue == null || string.IsNullOrWhiteSpace(departmentClaimValue.Value))
                return Task.FromResult(false);

            var result = departments.Any(c => c.ToLower() == departmentClaimValue.Value.ToLower());

            return Task.FromResult(result);
        }
    }
}
