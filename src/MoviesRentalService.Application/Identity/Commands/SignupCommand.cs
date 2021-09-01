

using Aloha.CQRS.Commands;

namespace MoviesRentalService.Application.Identity.Commands
{
    public class SignupCommand : ICommand
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
