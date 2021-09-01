using Aloha.CQRS.Commands;
using MoviesRentalService.Application.Catalog.Commands;
using MoviesRentalService.Domain.Catalog.Repositories;
using System.Threading.Tasks;

namespace MoviesRentalService.Application.Catalog.CommandHandlers
{
    public class UpdateMovieCommandHandler : ICommandHandler<UpdateMovieCommand>
    {
        private readonly IMovieRepository _repository;

        public UpdateMovieCommandHandler(IMovieRepository repository)
        {
            _repository = repository;
        }

        public async Task HandleAsync(UpdateMovieCommand command)
        {
            var movie = await _repository.GetByIdAsync(command.Id);

            movie.Update(command.Name, command.Description, command.Price);

            _repository.Update(movie);
        }
    }
}
