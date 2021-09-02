using Aloha.CQRS.Commands;
using System;

namespace MoviesRentalService.Application.Rent.Command
{
    public record RentMovieCommand(Guid UserId) : ICommand;
}
