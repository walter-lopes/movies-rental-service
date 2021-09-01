using Aloha.CQRS.Commands;
using Aloha.CQRS.Notifications;
using Aloha.Notifications;
using MoviesRentalService.Application.Identity.Commands;
using MoviesRentalService.Domain.Identity;
using MoviesRentalService.Domain.Identity.Repositories;
using MoviesRentalService.Domain.Rent;
using MoviesRentalService.Domain.Rent.Repositories;
using MoviesRentalService.Infra.UoW;
using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MoviesRentalService.Application.Identity.CommandHandlers
{
    public class SignupCommandHandler : ICommandHandler<SignupCommand>
    {
        private readonly IHasher _hasher;
        private readonly IUserRepository _userRepository;
        private readonly INotificationDispatcher _notificationDispatcher;
        private readonly IUnitOfWork _uoW;
        private readonly ICartRepository _cartRepository;

        public SignupCommandHandler(IHasher hasher, IUserRepository userRepository, INotificationDispatcher notificationDispatcher, IUnitOfWork uoW, ICartRepository cartRepository)
        {
            _hasher = hasher;
            _userRepository = userRepository;
            _notificationDispatcher = notificationDispatcher;
            _uoW = uoW;
            _cartRepository = cartRepository;
        }

        public async Task HandleAsync(SignupCommand command)
        {
            try
            {
                bool userExists = await _userRepository.ExistsByEmailAsync(command.Email.ToLower());

                if (userExists)
                {
                    await _notificationDispatcher.PublishAsync(new DomainNotification(HttpStatusCode.Conflict, "Email already exists."));
                    return;
                }

                var user = new User(command.Email, command.Name, "customer");
                string passwordHash = _hasher.Create(user, command.Password);

                user.SetPasswordHash(passwordHash);

                _userRepository.Insert(user);

                var cart = new Cart(user.Id);

                _cartRepository.Insert(cart);

                await _uoW.Commit();
            }
            catch (Exception)
            {
                await _notificationDispatcher.PublishAsync(new DomainNotification(HttpStatusCode.InternalServerError, "Internal Server Error."));
            }        
        }
    }
}
