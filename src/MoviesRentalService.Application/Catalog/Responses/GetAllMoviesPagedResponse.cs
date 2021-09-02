using Aloha.CQRS.Queries;
using MoviesRentalService.Domain.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MoviesRentalService.Application.Catalog.Responses
{
    public class GetAllMoviesPagedResponse : PagedResultBase
    {
        public GetAllMoviesPagedResponse(PagedResult<Movie> pagedMovies)
            : base(pagedMovies.CurrentPage, pagedMovies.ResultsPerPage, pagedMovies.TotalPages, pagedMovies.TotalResults)
        {
            Movies = pagedMovies.Items.Select(movie => new GetAllMoviePagedResponse(movie));
        }

        public GetAllMoviesPagedResponse() { }

        public IEnumerable<GetAllMoviePagedResponse> Movies { get; set; }
    }

    public class GetAllMoviePagedResponse
    {
        public GetAllMoviePagedResponse(Movie movie)
        {
            Id = movie.Id;
            Name = movie.Name;
            Description = movie.Description;
            Price = movie.Price;
        }

        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public decimal Price { get; private set; }
    }
}
