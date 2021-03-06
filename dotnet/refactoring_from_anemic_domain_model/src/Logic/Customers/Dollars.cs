﻿using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace Logic.Entities
{
    public class Dollars : ValueObject
    {
        private const decimal MaxDollarAmount = 1_000_000m;
        public decimal Value { get; }

        private Dollars(decimal value)
        {
            Value = value;
        }

        public bool IsZero => Value == 0m;

        public static Result<Dollars> Create(decimal dollarAmount)
        {
            if (dollarAmount < 0)
                return Result.Fail<Dollars>("Dollar amount cannot be negative");

            if (dollarAmount > MaxDollarAmount)
                return Result.Fail<Dollars>("Dollar amount cannot be greater that " + MaxDollarAmount);

            if (dollarAmount % 0.01m > 0m)
                return Result.Fail<Dollars>("Dollar amount cannot contain part of a penny");

            return Result.Ok(new Dollars(dollarAmount));
        }

        public static Dollars Of(decimal dollarAmount) => Create(dollarAmount).Value;

        public static implicit operator decimal(Dollars dollars) => dollars.Value;

        public static Dollars operator *(Dollars dollars, decimal multiplier)
        {
            return new Dollars(dollars.Value * multiplier);
        }

        public static Dollars operator +(Dollars dollars1, Dollars dollars2)
        {
            return new Dollars(dollars1.Value + dollars1.Value);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}