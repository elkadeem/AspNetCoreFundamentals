using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Fundamentals.WebApp.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace AspNetCore.Fundamentals.WebApp.Areas.Identity.Pages.Admin
{
    public class UserRolesModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<UserRolesModel> _logger;

        public UserRolesModel(UserManager<AppUser> userManager
            , RoleManager<IdentityRole> roleManager
            , ILogger<UserRolesModel> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }
                
        public AppUser AppUser { get; set; }

        [BindProperty]
        public List<string> Roles { get; set; }

        public IList<SelectListItem> UserRoles { get; set; }

        public async Task OnGetAsync(string id)
        {
            AppUser = await _userManager.FindByIdAsync(id);
            var userRoles = await _userManager.GetRolesAsync(AppUser);
            FillRoles(userRoles);
        }

        private void FillRoles(IList<string> userRoles)
        {
            var roles = _roleManager.Roles.ToList();
            UserRoles = roles.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Name,
                Selected = userRoles.Any(e => e == c.Name),
            }).ToList();
        }

        public async Task<ActionResult> OnPostAsync(string id)
        {
            try
            {
                AppUser = await _userManager.FindByIdAsync(id);
                var userRoles = await _userManager.GetRolesAsync(AppUser);
                var deleted = userRoles.Where(c => !Roles.Any(e => e == c))
                    .ToList();

                var Added = Roles.Where(c => !userRoles.Any(e => e == c))
                    .ToList();

                IdentityResult result = IdentityResult.Success;
                if (deleted.Count > 0)
                    result = await _userManager.RemoveFromRolesAsync(AppUser, deleted);

                if (Added.Count > 0 && result.Succeeded)
                    result = await _userManager.AddToRolesAsync(AppUser, Added);

                if (result.Succeeded)
                    return RedirectToPage("./Index");

                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            FillRoles(Roles);
            return Page();
        }
    }
}