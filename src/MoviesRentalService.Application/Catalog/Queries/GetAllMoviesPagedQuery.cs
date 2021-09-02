using Aloha.CQRS.Queries;
using MoviesRentalService.Application.Catalog.Responses;

namespace MoviesRentalService.Application.Catalog.Queries
{
    public record GetAllMoviesPagedQuery(int Page, int Items) : IQuery<GetAllMoviesPagedResponse>;
}
