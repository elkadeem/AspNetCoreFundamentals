using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Fundamentals.Domain.Dto;
using AspNetCore.Fundamentals.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspNetCore.Fundamentals.WebApp.Pages.Employees
{
    public class EditModel : PageModel
    {
        private readonly EmployeeService _employeeService;
        public EditModel(EmployeeService employeeService)
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
            Employee.Id = id;
            var result = await _employeeService.UpdateEmployee(Employee);
            if (result)
                return RedirectToPage("./Index");

            return Page();
        }
    }
}