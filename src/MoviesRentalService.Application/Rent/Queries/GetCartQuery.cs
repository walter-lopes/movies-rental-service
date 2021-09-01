using Aloha.CQRS.Queries;
using MoviesRentalService.Application.Rent.Responses;
using System;

namespace MoviesRentalService.Application.Rent.Queries
{
    public record GetCartQuery(Guid UserId) : IQuery<GetCartResponse>;
}
