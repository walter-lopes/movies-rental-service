using Aloha.CQRS.Commands;
using Aloha.CQRS.Notifications;
using Aloha.Notifications;
using MoviesRentalService.Application.Catalog.Commands;
using MoviesRentalService.Domain.Catalog.Repositories;
using MoviesRentalService.Infra.UoW;
using System.Net;
using System.Threading.Tasks;

namespace MoviesRentalService.Application.Catalog.CommandHandlers
{
    public class UpdateMovieCommandHandler : ICommandHandler<UpdateMovieCommand>
    {
        private readonly IMovieRepository _repository;
        private readonly INotificationDispatcher _notificationDispatcher;
        private readonly IUnitOfWork _uoW;

        public UpdateMovieCommandHandler(IMovieRepository repository, INotificationDispatcher notificationDispatcher, IUnitOfWork uoW)
        {
            _repository = repository;
            _notificationDispatcher = notificationDispatcher;
            _uoW = uoW;
        }

        public async Task HandleAsync(UpdateMovieCommand command)
        {
            try
            {
                var movie = await _repository.GetByIdAsync(command.Id);

                if (movie is null)
                {
                    await _notificationDispatcher.PublishAsync(new DomainNotification(HttpStatusCode.NotFound, "Movie not found."));
                    return;
                }

                movie.Update(command.Name, command.Description, command.Price);

                _repository.Update(movie);

                await _uoW.Commit();
            }
            catch (System.Exception)
            {
                await _notificationDispatcher.PublishAsync(new DomainNotification(HttpStatusCode.InternalServerError, "Internal Server Error."));
            }      
        }
    }
}
