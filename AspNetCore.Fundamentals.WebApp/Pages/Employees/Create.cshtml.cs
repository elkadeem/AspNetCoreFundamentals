using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Fundamentals.Domain.Dto;
using AspNetCore.Fundamentals.Domain.Services;
using AspNetCore.Fundamentals.WebApp.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace AspNetCore.Fundamentals.WebApp.Pages.Employees
{
    [Authorize(Roles ="Admin")]
    [Authorize(Roles = "Employee")]
    public class CreateModel : BasePageModel
    {
        private readonly EmployeeService _employeeService;

        public CreateModel(EmployeeService employeeService
            , ILogger<CreateModel> logger) : base(logger)
        {
            if (employeeService == null)
                throw new ArgumentNullException(nameof(employeeService));

            _employeeService = employeeService;
        }

        [BindProperty]
        public EmployeeDto Employee { get; set; }
        
        public async Task<ActionResult> OnPostAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                    return Page();

                var result = await _employeeService.AddEmployee(Employee);
                if (result)
                    return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, ex.Message);                
            }
            

            return Page();
        }
    }
}