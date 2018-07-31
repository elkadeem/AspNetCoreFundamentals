using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Fundamentals.Domain.Dto;
using AspNetCore.Fundamentals.Domain.Services;
using AspNetCore.Fundamentals.WebApp.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace AspNetCore.Fundamentals.WebApp.Pages.Employees
{
    public class EditModel : BasePageModel
    {
        private readonly EmployeeService _employeeService;
        public EditModel(EmployeeService employeeService
            , ILogger<DetailsModel> logger) : base(logger)
        {
            if (employeeService == null)
                throw new ArgumentNullException(nameof(employeeService));

            _employeeService = employeeService;
        }

        [BindProperty]
        public EmployeeDto Employee { get; set; }

        public async Task OnGet(Guid id)
        {
            Employee = await _employeeService.GetEmployeeById(id);
        }

        public async Task<ActionResult> OnPostAsync(Guid id)
        {
            try
            {
                Employee.Id = id;
                var result = await _employeeService.UpdateEmployee(Employee);
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