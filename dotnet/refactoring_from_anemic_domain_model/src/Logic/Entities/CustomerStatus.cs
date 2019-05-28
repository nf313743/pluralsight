using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace Logic.Entities
{
    public enum CustomerStatusType
    {
        Regular = 1,
        Advanced = 2
    }

    public class CustomerStatus : ValueObject
    {
        public static readonly CustomerStatus Regular = new CustomerStatus(CustomerStatusType.Regular, ExpirationDate.Infinite);
        private readonly DateTime? _expirationDate;

        private CustomerStatus(CustomerStatusType type, ExpirationDate expirationDate)
        {
            Type = type;
            _expirationDate = expirationDate ?? throw new ArgumentNullException(nameof(expirationDate));
        }

        private CustomerStatus()
        {
        }

        public ExpirationDate ExpirationDate => (ExpirationDate)_expirationDate;

        public bool IsAdvanced => Type == CustomerStatusType.Advanced && !ExpirationDate.IsExpired;

        public CustomerStatusType Type { get; }

        public CustomerStatus Promote()
        {
            return new CustomerStatus(CustomerStatusType.Advanced, (ExpirationDate)DateTime.UtcNow.AddYears(1));
        }

        public decimal GetDiscount() => IsAdvanced ? 0.25m : 0m;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return ExpirationDate;
            yield return Type;
        }
    }
}