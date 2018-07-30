using AspNetCore.Fundamentals.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCore.Fundamentals.Domain.Repository
{
    public interface IEmployeesRepository
    {
        Task Add(Employee employee);

        Task Update(Employee employee);

        Task Delete(Employee employee);

        Task<Employee> GetEmployeeById(Guid Id);

        Task<(List<Employee> Employees, int TotalItems)> GetEmployees(string idNo
            , string name, int pageIndex, int pageSize);

        Task Save();
    }
}
