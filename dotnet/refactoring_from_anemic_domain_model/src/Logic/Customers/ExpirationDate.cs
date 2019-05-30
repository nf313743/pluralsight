using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace Logic.Entities
{
    public class ExpirationDate : ValueObject
    {
        public DateTime? Date { get; }

        private ExpirationDate(DateTime? date)
        {
            Date = date;
        }

        public static readonly ExpirationDate Infinite = new ExpirationDate(null);

        public bool IsExpired => this != Infinite && Date < DateTime.UtcNow;

        public static Result<ExpirationDate> Create(DateTime date)
        {
            return Result.Ok(new ExpirationDate(date));
        }

        public static explicit operator ExpirationDate(DateTime? date)
        {
            if (date.HasValue)
                return Create(date.Value).Value;

            return Infinite;
        }

        public static implicit operator DateTime? (ExpirationDate date) => date.Date;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Date;
        }
    }
}