using Aloha.CQRS.Queries;
using MoviesRentalService.Application.Catalog.Responses;

namespace MoviesRentalService.Application.Catalog.Queries
{
    public record GetMoviesByFullSearchQuery(string FullSearch) : IQuery<GetMoviesByFullSearchResponse>;
}
