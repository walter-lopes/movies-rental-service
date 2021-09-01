

using Aloha.CQRS.Commands;
using MoviesRentalService.Infra.Identity;

namespace MoviesRentalService.Application.Identity.Commands
{
    public class SigninCommand : ICommand<IdentityToken>
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
