using Aloha.CQRS.Commands;
using System;

namespace MoviesRentalService.Application.Rent.Command
{
    public record CleanCartCommand(Guid Userid) : ICommand;
}
