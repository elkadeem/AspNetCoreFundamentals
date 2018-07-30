using AspNetCore.Fundamentals.Core.Entities;
using System;
using Xunit;

namespace AspNetCore.Fundamentals.UnitTesting
{
    public class ValueObjectsTest
    {
        [Fact]
        public void TwoObjectsWithSameValues_AreEqules()
        {
            var address1 = new Address("A", "B", "A");
            var address2 = new Address("A", "B", "A");

            Assert.True(address2.Equals(address1));
            Assert.True(address1 == address2);
            Assert.False(address1 != address2);
        }

        [Fact]
        public void TwoObjectsWithDifferentValues_AreNoEqules()
        {
            var address1 = new Address("A", "B", "A");
            var address2 = new Address("B", "A", "A");

            Assert.False(address2.Equals(address1));
            Assert.False(address1 == address2);
            Assert.True(address1 != address2);
        }
    }

    public class Address : ValueObject<Address>
    {
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
