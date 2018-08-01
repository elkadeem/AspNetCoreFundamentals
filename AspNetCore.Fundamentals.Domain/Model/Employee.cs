using System;
using System.Collections.Generic;
using System.Text;
using AspNetCore.Fundamentals.Core.Entities;

namespace AspNetCore.Fundamentals.Domain.Model
{
    public class Employee : Entity<Guid>
    {
        private string _idNo;
        private string _name;
        private double _salary;

        private Employee()
        {

        }

        public Employee(string idNo, string name)
        {
            if (string.IsNullOrWhiteSpace(idNo))
                throw new ArgumentNullException(nameof(idNo));

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            Id = Guid.NewGuid();
            IdNo = idNo;
            Name = name;
        }

        public string IdNo
        {
            get
            {
                return _idNo;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException(nameof(value));

                _idNo = value;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException(nameof(value));

                _name = value;
            }
        }

        public DateTime BirthDate { get; set; }

        public Address Address { get; set; }

        public double Salary
        {
            get
            {
                return _salary;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value));
                _salary = value;
            }
        }

        public string Phone { get; set; }

        public string Email { get; set; }

    }
}
