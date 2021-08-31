using MoviesRentalService.Domain.Catalog;
using System;

namespace MoviesRentalService.Domain.Rental
{
    public class CartItem
    {
        public CartItem(Movie movie)
        {
            MovieId = movie.Id;
            Price = movie.Price;
        }

        public Guid MovieId { get; set; }

        public decimal Price { get; set; }
    }
}
