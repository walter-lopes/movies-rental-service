using Aloha.CQRS.Commands;
using System;

namespace MoviesRentalService.Application.Catalog.Commands
{
    public class UpdateMovieCommand : ICommand
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }
    }
}
