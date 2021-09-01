using Aloha.CQRS.Commands;
using Aloha.CQRS.Notifications;
using Aloha.Notifications;
using MoviesRentalService.Application.Identity.Commands;
using MoviesRentalService.Domain.Identity;
using MoviesRentalService.Domain.Identity.Repositories;
using MoviesRentalService.Infra.Identity;
using System;
using System.Net;
using System.Threading.Tasks;

namespace MoviesRentalService.Application.Identity.CommandHandlers
{
    public class SigninCommandHandler : ICommandHandler<SigninCommand, IdentityToken>
    {
        private readonly IHasher _hasher;
        private readonly IUserRepository _userRepository;
        private readonly INotificationDispatcher _notificationDispatcher;
        private readonly IJwtHandler _jwtHandler;

        public SigninCommandHandler(IHasher hasher, IUserRepository userRepository, INotificationDispatcher notificationDispatcher, IJwtHandler jwtHandler)
        {
            _hasher = hasher;
            _userRepository = userRepository;
            _notificationDispatcher = notificationDispatcher;
            _jwtHandler = jwtHandler;
        }

        public async Task<IdentityToken> HandleAsync(SigninCommand command)
        {
            try
            {
                User user = await _userRepository.GetByEmailAsync(command.Email);

                if (user is null || !_hasher.IsValid(user, command.Password))
                {
                    await _notificationDispatcher.PublishAsync(new DomainNotification(HttpStatusCode.BadRequest, "Email or password invalid."));

                    return new IdentityToken();
                }

                var identityToken = _jwtHandler.CreateToken(user);

                return identityToken;
            }
            catch (Exception)
            {
                await _notificationDispatcher.PublishAsync(new DomainNotification(HttpStatusCode.InternalServerError, "Internal Server Error."));
                return new IdentityToken();
            }    
        }
    }
}
