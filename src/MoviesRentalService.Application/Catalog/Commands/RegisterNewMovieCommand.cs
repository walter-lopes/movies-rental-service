using Aloha.CQRS.Commands;

namespace MoviesRentalService.Application.Catalog.Commands
{
    public class RegisterNewMovieCommand : ICommand
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }
    }
}
