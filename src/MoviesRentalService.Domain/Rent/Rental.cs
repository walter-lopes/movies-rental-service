using MoviesRentalService.Domain.Rent;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MoviesRentalService.Domain.Rent
{
    public class Rental
    {
        public Rental(Cart cart)
        {
            Id = Guid.NewGuid();
            Start = DateTime.Now;
            Expires = DateTime.Now.AddDays(3);
            UserId = cart.UserId;
            Items = cart.Items;
        }

        public Guid Id { get; private set; }

        public Guid UserId { get; private set; }

        public DateTime Start { get; private set; }

        public DateTime Expires { get; private set; }

        public decimal Total
        {
            get
            {
                return CalculateTotal();
            }
            private set { }
        }

        public HashSet<RentalItem> Items { get; private set; }

        private decimal CalculateTotal()
        {
            return Items.Sum(x => x.Price);
        }
    }
}
