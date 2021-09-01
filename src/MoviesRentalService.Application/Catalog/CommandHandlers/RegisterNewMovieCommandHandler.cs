using Aloha.CQRS.Commands;
using Aloha.CQRS.Notifications;
using Aloha.Notifications;
using MoviesRentalService.Application.Catalog.Commands;
using MoviesRentalService.Domain.Catalog;
using MoviesRentalService.Domain.Catalog.Repositories;
using MoviesRentalService.Infra.UoW;
using System.Net;
using System.Threading.Tasks;

namespace MoviesRentalService.Application.Catalog.CommandHandlers
{
    public class RegisterNewMovieCommandHandler : ICommandHandler<RegisterNewMovieCommand>
    {
        private readonly IMovieRepository _repository;
        private readonly INotificationDispatcher _notificationDispatcher;
        private readonly IUnitOfWork _uoW;

        public RegisterNewMovieCommandHandler(IMovieRepository repository, INotificationDispatcher notificationDispatcher, IUnitOfWork uoW)
        {
            _repository = repository;
            _notificationDispatcher = notificationDispatcher;
            _uoW = uoW;
        }

        public async Task HandleAsync(RegisterNewMovieCommand command)
        {
            try
            {
                var movie = new Movie(command.Name, command.Description, command.Price);

                _repository.Insert(movie);

                await _uoW.Commit();
            }
            catch (System.Exception)
            {
                await _notificationDispatcher.PublishAsync(new DomainNotification(HttpStatusCode.InternalServerError, "Internal Server Error."));
            }   
        }
    }
}
