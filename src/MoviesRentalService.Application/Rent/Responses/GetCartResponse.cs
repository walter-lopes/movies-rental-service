using MoviesRentalService.Domain.Catalog;
using MoviesRentalService.Domain.Rent;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MoviesRentalService.Application.Rent.Responses
{
    public class GetCartResponse
    {
        public GetCartResponse(Cart cart, IDictionary<Guid, Movie> movieMap)
        {
            Items = cart.Items.Select(x => new GetCartMovieItemRespose(x, movieMap));

            Total = Items.Sum(x => x.Price);
        }

        public IEnumerable<GetCartMovieItemRespose> Items { get; set; }

        public decimal Total { get; set; }
    }

    public class GetCartMovieItemRespose
    {
        public GetCartMovieItemRespose(RentalItem item, IDictionary<Guid, Movie> movieMap)
        {
            Id = item.MovieId;
            Name = movieMap[Id].Name;
            Price = item.Price;
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}
