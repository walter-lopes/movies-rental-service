using MoviesRentalService.Domain.Catalog;
using MoviesRentalService.Domain.Rent;
using System;

namespace MoviesRentalService.Domain.Rental
{
    public class RentalItem
    {
        public RentalItem(Movie movie)
        {
            MovieId = movie.Id;
            Price = movie.Price;
        }

        public Guid MovieId { get; private set; }

        public decimal Price { get; private set; }

        public override bool Equals(object obj)
        {
            if (obj is not RentalItem item)
            {
                return false;
            }

            return this.MovieId.Equals(item.MovieId);
        }

        public override int GetHashCode()
        {
            return this.MovieId.GetHashCode();
        }
    }
}
