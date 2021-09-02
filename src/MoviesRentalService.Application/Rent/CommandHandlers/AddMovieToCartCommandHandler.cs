using Aloha.CQRS.Commands;
using Aloha.CQRS.Notifications;
using Aloha.Notifications;
using MoviesRentalService.Application.Rent.Command;
using MoviesRentalService.Domain.Catalog;
using MoviesRentalService.Domain.Catalog.Repositories;
using MoviesRentalService.Domain.Rent;
using MoviesRentalService.Domain.Rent.Repositories;
using MoviesRentalService.Infra.UoW;
using System.Net;
using System.Threading.Tasks;

namespace MoviesRentalService.Application.Rent.CommandHandlers
{
    public class AddMovieToCartCommandHandler : ICommandHandler<AddMovieToCartCommand>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly INotificationDispatcher _notificationDispatcher;
        private readonly IUnitOfWork _uoW;

        public AddMovieToCartCommandHandler(ICartRepository cartRepository, IMovieRepository movieRepository, INotificationDispatcher notificationDispatcher, IUnitOfWork uoW)
        {
            _cartRepository = cartRepository;
            _movieRepository = movieRepository;
            _notificationDispatcher = notificationDispatcher;
            _uoW = uoW;
        }

        public async Task HandleAsync(AddMovieToCartCommand command)
        {
            try
            {
                var cartTask = _cartRepository.GetByUserIdAsync(command.UserId);
                var movieTask = _movieRepository.GetByIdAsync(command.MovieId);

                await Task.WhenAll(cartTask, movieTask);

                Cart cart = cartTask.Result;
                Movie movie = movieTask.Result;

                if (cart is null)
                {
                    await _notificationDispatcher.PublishAsync(new DomainNotification(HttpStatusCode.NotFound, "Cart not found."));
                    return;
                }

                if (movie is null)
                {
                    await _notificationDispatcher.PublishAsync(new DomainNotification(HttpStatusCode.NotFound, "Movie not found."));
                    return;
                }

                var item = new RentalItem(movie);

                bool added = cart.Add(item);

                if (!added)
                {
                    await _notificationDispatcher.PublishAsync(new DomainNotification(HttpStatusCode.BadRequest, "Movie already in the cart."));
                    return;
                }

                _cartRepository.Update(cart);

                await _uoW.Commit();
            }
            catch (System.Exception)
            {
                await _notificationDispatcher.PublishAsync(new DomainNotification(HttpStatusCode.InternalServerError, "Internal Server Error."));
            }      
        }
    }
}
