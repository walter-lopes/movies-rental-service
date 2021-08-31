using System;
using System.Collections.Generic;

namespace MoviesRentalService.Domain.Rental
{
    public class Cart
    {
        public Cart(Guid userId)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Items = new HashSet<CartItem>();
        }

        public Guid Id { get; set; }

        public HashSet<CartItem> Items { get; set; }

        public Guid UserId { get; set; }


        public bool Add(CartItem item)
        {
            if (Items.Contains(item))
            {
                return false;
            }

            Items.Add(item);
            return true;
        }
    }
}
