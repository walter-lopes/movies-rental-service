using Aloha.CQRS.Commands;
using Aloha.CQRS.Notifications;
using Aloha.Notifications;
using MoviesRentalService.Application.Rent.Command;
using MoviesRentalService.Domain.Rent.Repositories;
using MoviesRentalService.Infra.UoW;
using System;
using System.Net;
using System.Threading.Tasks;

namespace MoviesRentalService.Application.Rent.CommandHandlers
{
    public class CleanCartCommandHandler : ICommandHandler<CleanCartCommand>
    {
        private readonly ICartRepository _cartRepository;
        private readonly INotificationDispatcher _notificationDispatcher;
        private readonly IUnitOfWork _uoW;

        public CleanCartCommandHandler(ICartRepository cartRepository, INotificationDispatcher notificationDispatcher, IUnitOfWork uoW)
        {
            _cartRepository = cartRepository;
            _notificationDispatcher = notificationDispatcher;
            _uoW = uoW;
        }

        public async Task HandleAsync(CleanCartCommand command)
        {
            try
            {
                var cart = await _cartRepository.GetByUserIdAsync(command.Userid);

                cart.Clean();

                _cartRepository.Update(cart);

                await _uoW.Commit();
            }
            catch (Exception)
            {
                await _notificationDispatcher.PublishAsync(new DomainNotification(HttpStatusCode.InternalServerError, "Internal Server Error."));
            }   
        }
    }
}
