using MoviesRentalService.Domain.Catalog;
using MoviesRentalService.Domain.Rent;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MoviesRentalService.Application.Rent.Responses
{
    public class GetAvailableMoviesByUserIdResponse
    {
        public GetAvailableMoviesByUserIdResponse(IEnumerable<Movie> movies)
        {
            Movies = movies.Select(movie => new GetAvailableMovieByUserIdResponse(movie));
        }

        public IEnumerable<GetAvailableMovieByUserIdResponse> Movies { get; set; }
    }

    public class GetAvailableMovieByUserIdResponse
    {
        public GetAvailableMovieByUserIdResponse(Movie movie)
        {
            Name = movie.Name;
            Description = movie.Description;
        }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
