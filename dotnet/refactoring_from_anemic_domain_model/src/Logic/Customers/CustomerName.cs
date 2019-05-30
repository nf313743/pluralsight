using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace Logic.Entities
{
    public class CustomerName : ValueObject
    {
        private CustomerName(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static Result<CustomerName> Create(string customerName)
        {
            customerName = (customerName ?? string.Empty).Trim();

            if (customerName.Length == 0)
                return Result.Fail<CustomerName>("Customer name should not be empty");

            if (customerName.Length > 100)
                return Result.Fail<CustomerName>("Customer name is too long");

            return Result.Ok(new CustomerName(customerName));
        }

        public static explicit operator CustomerName(string customerName) => Create(customerName).Value;

        public static implicit operator string(CustomerName customerName) => customerName.Value;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}