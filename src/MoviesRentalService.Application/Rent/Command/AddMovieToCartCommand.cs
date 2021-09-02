using Aloha.CQRS.Commands;
using System;

namespace MoviesRentalService.Application.Rent.Command
{
    public record AddMovieToCartCommand(Guid MovieId, Guid UserId) : ICommand;
}
