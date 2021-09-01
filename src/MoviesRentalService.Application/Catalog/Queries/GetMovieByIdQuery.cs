using Aloha.CQRS.Queries;
using MoviesRentalService.Application.Catalog.Responses;
using System;

namespace MoviesRentalService.Application.Catalog.Queries
{
    public record GetMovieByIdQuery(Guid Id) : IQuery<GetMovieByIdResponse>;
}
