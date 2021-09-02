using Aloha.CQRS.Commands;
using Aloha.CQRS.Notifications;
using Aloha.Notifications;
using MoviesRentalService.Application.Rent.Command;
using MoviesRentalService.Domain.Rent;
using MoviesRentalService.Domain.Rent.Repositories;
using MoviesRentalService.Infra.UoW;
using System.Net;
using System.Threading.Tasks;

namespace MoviesRentalService.Application.Rent.CommandHandlers
{
    public class RentMovieCommandHandler : ICommandHandler<RentMovieCommand>
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly ICartRepository _cartRepository;
        private readonly INotificationDispatcher _notificationDispatcher;
        private readonly IUnitOfWork _uoW;

        public RentMovieCommandHandler(IRentalRepository rentalRepository, ICartRepository cartRepository, INotificationDispatcher notificationDispatcher, IUnitOfWork uoW)
        {
            _rentalRepository = rentalRepository;
            _cartRepository = cartRepository;
            _notificationDispatcher = notificationDispatcher;
            _uoW = uoW;
        }

        public async Task HandleAsync(RentMovieCommand command)
        {
            try
            {
                var cart = await _cartRepository.GetByUserIdAsync(command.UserId);

                if (cart.IsEmpty())
                {
                    await _notificationDispatcher.PublishAsync(new DomainNotification(HttpStatusCode.BadRequest, "The cart is empty."));
                    return;
                }

                var rental = new Rental(cart);

                var movieIds = rental.GetMovies();

                bool alreadyRent = await _rentalRepository.ExistsByMovieIdsAsync(movieIds, command.UserId);

                if (alreadyRent)
                {
                    await _notificationDispatcher.PublishAsync(new DomainNotification(HttpStatusCode.BadRequest, "The movie already rent."));
                    return;
                }

                _rentalRepository.Insert(rental);

                cart.Clean();

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
