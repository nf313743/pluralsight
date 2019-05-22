using System;
using System.Collections.Generic;

namespace Logic.Entities
{
    public class Customer : Entity
    {
        private string _email;
        private decimal _moneySpent;
        private string _name;
        private DateTime? _statusExpirationDate;

        public virtual Email Email
        {
            get => (Email)_email;
            set => _email = value;
        }

        public virtual Dollars MoneySpent
        {
            get => Dollars.Of(_moneySpent);
            set => _moneySpent = value;
        }

        public virtual CustomerName Name
        {
            get => (CustomerName)_name;
            set => _name = value;
        }

        public virtual IList<PurchasedMovie> PurchasedMovies { get; set; }

        public virtual CustomerStatus Status { get; set; }

        public virtual ExpirationDate StatusExpirationDate
        {
            get => (ExpirationDate)_statusExpirationDate;
            set => _statusExpirationDate = value;
        }
    }
}