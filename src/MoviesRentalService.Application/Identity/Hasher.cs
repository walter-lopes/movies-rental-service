using Microsoft.AspNetCore.Identity;
using MoviesRentalService.Domain.Identity;

namespace MoviesRentalService.Application.Identity
{
    public class Hasher : IHasher
    {
        private readonly IPasswordHasher<User> _passwordHasher;

        public Hasher(IPasswordHasher<User> passwordHasher)
        {
            _passwordHasher = passwordHasher;
        }

        public string Create(User user, string secret)
                => _passwordHasher.HashPassword(user, secret);

        public bool IsValid(User user, string secret)
         => _passwordHasher.VerifyHashedPassword(user, user.Password, secret) != PasswordVerificationResult.Failed;
    }
}
