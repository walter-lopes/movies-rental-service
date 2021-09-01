using MoviesRentalService.Domain.Catalog;
using System;

namespace MoviesRentalService.Application.Catalog.Responses
{
    public class GetMovieByIdResponse
    {
        public GetMovieByIdResponse(Movie movie)
        {
            Id = movie.Id;
            Name = movie.Name;
            Description = movie.Description;
            Price = movie.Price;
        }

        public GetMovieByIdResponse() { }

        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public decimal Price { get; private set; }
    }
}
