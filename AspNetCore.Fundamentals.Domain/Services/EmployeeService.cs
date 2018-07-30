using AspNetCore.Fundamentals.Domain.Dto;
using AspNetCore.Fundamentals.Domain.Model;
using AspNetCore.Fundamentals.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Fundamentals.Domain.Services
{
    public class EmployeeService
    {
        private readonly IEmployeesRepository _repository;
        public EmployeeService(IEmployeesRepository repository)
        {
            _repository = repository;
        }

#pragma warning disable S4457 // Parameter validation in "async"/"await" methods should be wrapped
        public virtual async Task<bool> AddEmployee(EmployeeDto model)
#pragma warning restore S4457 // Parameter validation in "async"/"await" methods should be wrapped
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            Employee employee = new Employee(model.IdNo, model.Name)
            {
                Address = new Address(model.City, model.Line1, model.Line2),
                BirthDate = model.BirthDate,
            };

            await _repository.Add(employee);
            await _repository.Save();
            return true;
        }

#pragma warning disable S4457 // Parameter validation in "async"/"await" methods should be wrapped
        public virtual async Task<bool> UpdateEmployee(EmployeeDto model)
#pragma warning restore S4457 // Parameter validation in "async"/"await" methods should be wrapped
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            Employee employee = await _repository.GetEmployeeById(model.Id);
            if (employee == null)
                throw new ArgumentException("Employee is not exist", nameof(model));

            employee.IdNo = model.IdNo;
            employee.Address = new Address(model.City, model.Line1, model.Line2);
            employee.BirthDate = model.BirthDate;
            employee.Name = model.Name;

            await _repository.Update(employee);
            await _repository.Save();
            return true;
        }

#pragma warning disable S4457 // Parameter validation in "async"/"await" methods should be wrapped
        public virtual async Task<bool> DeleteEmployee(EmployeeDto model)
#pragma warning restore S4457 // Parameter validation in "async"/"await" methods should be wrapped
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            Employee employee = await _repository.GetEmployeeById(model.Id);
            if (employee == null)
                throw new ArgumentException("Employee is not exist", nameof(model));

            await _repository.Delete(employee);
            await _repository.Save();
            return true;
        }

        public virtual async Task<EmployeeDto> GetEmployeeById(Guid id)
        {            
            Employee employee = await _repository.GetEmployeeById(id);
            return employee == null? null : new EmployeeDto {
                BirthDate = employee.BirthDate,
                City = employee.Address?.City,
                Id = employee.Id,
                IdNo = employee.IdNo,
                Line1 = employee.Address?.Line1,
                Line2 = employee.Address?.Line2,
                Name = employee.Name,
            };
        }

        public virtual Task<(List<EmployeeDto> Employees, int TotalItems)> GetEmployees(string idNo, string name
            , int pageIndex, int pageSize)
        {
            if (pageIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(pageIndex));

            if (pageSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(pageSize));

            return GetUsers(idNo, name, pageIndex, pageSize);
        }

        private async Task<(List<EmployeeDto> Employees, int TotalItems)> GetUsers(string idNo, string name, int pageIndex, int pageSize)
        {
            var result = await _repository.GetEmployees(idNo, name, pageIndex, pageSize);
            var items = result.Employees.Select(employee => new EmployeeDto
            {
                BirthDate = employee.BirthDate,
                City = employee.Address?.City,
                Id = employee.Id,
                IdNo = employee.IdNo,
                Line1 = employee.Address?.Line1,
                Line2 = employee.Address?.Line2,
                Name = employee.Name,
            }).ToList();

            return (Employees: items, result.TotalItems);
        }
    }
}
