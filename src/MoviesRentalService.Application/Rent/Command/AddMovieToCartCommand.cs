using Aloha.CQRS.Commands;
using System;

namespace MoviesRentalService.Application.Rent.Command
{
    public class AddMovieToCartCommand : ICommand
    {
        public Guid UserId { get; set; }

        public Guid MovieId { get; set; }
    }
}
