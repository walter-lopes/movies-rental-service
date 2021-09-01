using MoviesRentalService.Domain.Catalog;
using MoviesRentalService.Domain.Rent;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MoviesRentalService.Application.Rent.Responses
{
    public class GetAvailableMoviesByUserIdResponse
    {
        public GetAvailableMoviesByUserIdResponse(IEnumerable<Movie> movies, Rental rental)
        {
            Movies = movies.Select(movie => new GetAvailableMovieByUserIdResponse(movie, rental));
        }

        public IEnumerable<GetAvailableMovieByUserIdResponse> Movies { get; set; }
    }

    public class GetAvailableMovieByUserIdResponse
    {
        public GetAvailableMovieByUserIdResponse(Movie movie, Rental rental)
        {
            Name = movie.Name;
            Description = movie.Description;
            Start = rental.Start;
            Expires = rental.Expires;
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime Start { get; set; }

        public DateTime Expires { get; set; }
    }
}
