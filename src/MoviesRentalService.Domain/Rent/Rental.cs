using MoviesRentalService.Domain.Rental;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public DateTime Start { get; set; }

        public DateTime Expires { get; set; }

        public decimal Total
        {
            get
            {
                return CalculateTotal();
            }
            private set { }
        }

        public HashSet<RentalItem> Items { get; set; }

        public decimal CalculateTotal()
        {
            return Items.Sum(x => x.Price);
        }
    }
}
