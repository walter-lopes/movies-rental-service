using System;

namespace MoviesRentalService.Domain.Identity
{
    public class User
    {
        public User(string name, string email, string role)
        {
            Id = Guid.NewGuid();
            Name = name;
            Email = email.ToLower();
            Role = role;
        }

        public void SetPasswordHash(string password)
                => this.Password = password;

        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public string Email { get; private set; }

        public string Password { get; private set; }

        public string Role { get; private set; }
    }
}
