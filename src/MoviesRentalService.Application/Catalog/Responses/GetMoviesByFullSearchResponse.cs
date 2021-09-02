using Aloha.CQRS.Queries;
using MoviesRentalService.Domain.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MoviesRentalService.Application.Catalog.Responses
{
    public class GetMoviesByFullSearchResponse : PagedResultBase
    {
        public GetMoviesByFullSearchResponse(PagedResult<Movie> pagedMovies) 
            : base(pagedMovies.CurrentPage, pagedMovies.ResultsPerPage, pagedMovies.TotalPages, pagedMovies.TotalResults)
        {
            Movies = pagedMovies.Items.Select(movie => new GetMovieByFullSearchResponse(movie));
        }

        public GetMoviesByFullSearchResponse() { }

        public IEnumerable<GetMovieByFullSearchResponse> Movies { get; set; }
    }

    public class GetMovieByFullSearchResponse
    {
        public GetMovieByFullSearchResponse(Movie movie)
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
