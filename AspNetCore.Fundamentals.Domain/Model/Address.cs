using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCore.Fundamentals.Domain.Model
{
    public class Address
    {
        private Address()
        {
        }

        public Address(string city, string line1, string line2)
        {
            City = city ?? throw new ArgumentNullException(nameof(city));
            Line1 = line1 ?? throw new ArgumentNullException(nameof(line1));
            Line2 = line2;
        }

        public string City { get; private set; }

        public string Line1 { get; private set; }

        public string Line2 { get; private set; }
    }
}
