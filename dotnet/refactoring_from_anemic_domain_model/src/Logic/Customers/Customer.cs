using System;
using System.Collections.Generic;
using System.Linq;
using CSharpFunctionalExtensions;

namespace Logic.Entities
{
    public class Customer : Entity
    {
        private readonly string _email;
        private readonly IList<PurchasedMovie> _purchasedMovies = new List<PurchasedMovie>();
        private decimal _moneySpent;
        private string _name;

        public Customer(CustomerName name, Email email)
        {
            _name = name ?? throw new ArgumentNullException(nameof(name));
            _email = email ?? throw new ArgumentNullException(nameof(email));

            MoneySpent = Dollars.Of(0m);
            Status = CustomerStatus.Regular;
        }

        protected Customer()
        {
        }

        public virtual bool HasPurchasedMovie(Movie movie)
        {
            return _purchasedMovies.Any(x => x.Movie == movie && !x.ExpirationDate.IsExpired);
        }

        public virtual Email Email => (Email)_email;

        public virtual Dollars MoneySpent
        {
            get => Dollars.Of(_moneySpent);
            protected set => _moneySpent = value;
        }

        public virtual CustomerName Name
        {
            get => (CustomerName)_name;
            set => _name = value;
        }

        public virtual IReadOnlyList<PurchasedMovie> PurchasedMovies => _purchasedMovies.ToList();

        public virtual CustomerStatus Status { get; protected set; }

        public virtual Result CanPromote()
        {
            if (Status.IsAdvanced)
                return Result.Fail("The customer already has the Advanced status");

            if (PurchasedMovies.Count(x => x.ExpirationDate == ExpirationDate.Infinite || x.ExpirationDate.Date >= DateTime.UtcNow.AddDays(-30)) < 2)
                return Result.Fail("at least 2 active movies during the last 30 days");

            if (PurchasedMovies.Where(x => x.PurchaseDate > DateTime.UtcNow.AddYears(-1)).Sum(x => x.Price) < 100m)
                return Result.Fail("at least 100 dollars spent during the last year");

            Status = Status.Promote();
            return Result.Ok();
        }

        public virtual void Promote()
        {
            if (CanPromote().IsFailure)
                throw new Exception();

            Status = Status.Promote();
        }

        public virtual void PurchasedMovie(Movie movie)
        {
            if (HasPurchasedMovie(movie))
                throw new Exception();

            ExpirationDate expirationDate = movie.GetExpirationDate();
            Dollars price = movie.CalculatePrice(Status);
            var purchasedMovie = new PurchasedMovie(movie, this, price, expirationDate);
            _purchasedMovies.Add(purchasedMovie);
            MoneySpent += price;
        }
    }
}