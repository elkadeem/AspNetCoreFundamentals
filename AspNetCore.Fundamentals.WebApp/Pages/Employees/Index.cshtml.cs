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
    public class IndexModel : BasePageModel
    {
        private readonly EmployeeService _employeeService;

        public IndexModel(EmployeeService employeeService
            , ILogger<DetailsModel> logger) : base(logger)
        {
            if (employeeService == null)
                throw new ArgumentNullException(nameof(employeeService));

            _employeeService = employeeService;
        }

        public IList<EmployeeDto> Employees { get; private set; }

        public int TotalItemsCount { get; private set; }

        [BindProperty(SupportsGet = true)]
        public SearchModel Critiria { get; set; }

        public async Task OnGetAsync()
        {
            var result = await _employeeService.GetEmployees(Critiria.IdNo
                , Critiria.Name
                , Critiria.PageNumber - 1
                , Critiria.PageSize);
            Employees = result.Employees;
            TotalItemsCount = result.TotalItems;
        }

        public async Task<ActionResult> OnPostDeleteAsync(Guid id)
        {
            try
            {
                var employee = await _employeeService.GetEmployeeById(id);
                if (employee == null)
                    return NotFound();

                var result = await _employeeService.DeleteEmployee(employee);
                if (result)
                    return RedirectToPage("./Index");

            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, ex.Message);                
            }
            
            return Page();
        }

        public class SearchModel
        {
            public string IdNo { get; set; }

            public string Name { get; set; }

            public int PageNumber { get; set; } = 1;

            public int PageSize { get; set; } = 10;
        }
    }
}