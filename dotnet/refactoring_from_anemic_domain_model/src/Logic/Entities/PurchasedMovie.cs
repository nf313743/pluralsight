using System;

namespace Logic.Entities
{
    public class PurchasedMovie : Entity
    {
        private DateTime? _expirationDate;
        private decimal _price;
        public virtual long MovieId { get; set; }

        public virtual Movie Movie { get; set; }

        public virtual long CustomerId { get; set; }

        public virtual Dollars Price
        {
            get => Dollars.Of(_price);
            set => _price = value;
        }

        public virtual DateTime PurchaseDate { get; set; }

        public virtual ExpirationDate ExpirationDate
        {
            get => (ExpirationDate)_expirationDate;
            set => _expirationDate = value;
        }
    }
}