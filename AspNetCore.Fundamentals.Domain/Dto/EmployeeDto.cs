using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCore.Fundamentals.Domain.Dto
{
    public class EmployeeDto
    {
        public Guid Id { get; set; }

        public string IdNo { get; set; }

        public string Name { get; set; }

        public DateTime BirthDate { get; set; }

        public string City { get; set; }

        public string Line1 { get; set; }

        public string Line2 { get; set; }
    }
}
