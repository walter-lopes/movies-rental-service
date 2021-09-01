using System;
using System.Collections.Generic;
using System.Linq;

namespace MoviesRentalService.Domain.Rent
{
    public class Cart
    {
        public Cart(Guid userId)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Items = new HashSet<RentalItem>();
        }

        public Guid Id { get; set; }

        public HashSet<RentalItem> Items { get; set; }

        public Guid UserId { get; set; }


        public bool Add(RentalItem item)
        {
            if (Items.Contains(item))
            {
                return false;
            }

            Items.Add(item);
            return true;
        }

        public void Clean()
        {
            Items = new HashSet<RentalItem>();
        }

        public bool IsEmpty() => !Items.Any();
    }
}
