using AspNetCore.Fundamentals.Domain.Model;
using AspNetCore.Fundamentals.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace AspNetCore.Fundamentals.Store
{
    public class EmployeesRepository : IEmployeesRepository
    {
        private readonly EmployeeDbContext _dbContext;

        public EmployeesRepository(EmployeeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task Add(Employee employee)
        {
            if (employee == null)
                throw new ArgumentNullException(nameof(employee));
            _dbContext.Employees.Add(employee);
            return Task.CompletedTask;
        }

        public Task Delete(Employee employee)
        {
            if (employee == null)
                throw new ArgumentNullException(nameof(employee));
            _dbContext.Employees.Remove(employee);
            return Task.CompletedTask;
        }

        public Task<Employee> GetEmployeeById(Guid Id)
        {
            return _dbContext.Employees.FindAsync(Id);
        }

        public async Task<(List<Employee> Employees, int TotalItems)> GetEmployees(string idNo, string name, int pageIndex, int pageSize)
        {
            var query = _dbContext.Employees.AsNoTracking();
            if (!string.IsNullOrWhiteSpace(idNo))
                query = query.Where(c => c.IdNo == idNo);
            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(c => c.Name.ToLower().Contains(idNo.ToLower()));

            int totalItemsCount = await query.CountAsync();
            var items = await query.OrderBy(c => c.Name)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (Employees: items, TotalItems: totalItemsCount);

        }

        public Task Save()
        {
            return _dbContext.SaveChangesAsync();
        }

        public Task Update(Employee employee)
        {
            var entity = _dbContext.Entry<Employee>(employee);
            if (entity.State == EntityState.Detached)
                _dbContext.Employees.Update(employee);

            return Task.CompletedTask;
        }
    }
}
