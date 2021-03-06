﻿using System;
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
    public class DetailsModel : BasePageModel
    {
        private readonly EmployeeService _employeeService;
        public DetailsModel(EmployeeService employeeService
            , ILogger<DetailsModel> logger) : base(logger)
        {
            if (employeeService == null)
                throw new ArgumentNullException(nameof(employeeService));

            _employeeService = employeeService;
        }

        public EmployeeDto Employee { get; set; }

        public async Task OnGet(Guid id)
        {
           Employee = await   _employeeService.GetEmployeeById(id);
        }
    }
}