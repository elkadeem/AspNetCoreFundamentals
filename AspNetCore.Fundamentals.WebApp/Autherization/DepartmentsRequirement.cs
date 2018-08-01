using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Fundamentals.WebApp.Autherization
{
    public class DepartmentsRequirement : IAuthorizationRequirement
    {
        public IReadOnlyList<string> Departments => _departments.AsReadOnly();
        private readonly List<string> _departments;
        public DepartmentsRequirement(params string[] departments)
        {
            if (departments == null)
                _departments = new List<string>();

            _departments = departments.ToList();
        }
    }
}
