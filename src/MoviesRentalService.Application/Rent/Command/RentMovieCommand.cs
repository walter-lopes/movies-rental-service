using Aloha.CQRS.Commands;
using System;

namespace MoviesRentalService.Application.Rent.Command
{
    public class RentMovieCommand : ICommand
    {
        public Guid UserId { get; set; }
    }
}
