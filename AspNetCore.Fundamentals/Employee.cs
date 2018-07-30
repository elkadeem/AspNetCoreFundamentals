using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCore.Fundamentals.Model
{
    public class Employee
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime BirthDate { get; set; }

        public string Address { get; set; }
    }
}
