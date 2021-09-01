using Aloha.CQRS.Commands;
using MoviesRentalService.Application.Catalog.Commands;
using MoviesRentalService.Domain.Catalog;
using MoviesRentalService.Domain.Catalog.Repositories;
using System.Threading.Tasks;

namespace MoviesRentalService.Application.Catalog.CommandHandlers
{
    public class RegisterNewMovieCommandHandler : ICommandHandler<RegisterNewMovieCommand>
    {
        private readonly IMovieRepository _repository;

        public RegisterNewMovieCommandHandler(IMovieRepository repository)
        {
            _repository = repository;
        }

        public Task HandleAsync(RegisterNewMovieCommand command)
        {
            var movie = new Movie(command.Name, command.Description, command.Price);

            _repository.Insert(movie);

            return Task.CompletedTask;
        }
    }
}
