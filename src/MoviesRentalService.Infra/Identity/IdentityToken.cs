using System;

namespace MoviesRentalService.Infra.Identity
{
    public class IdentityToken
    {
        public string Email { get; set; }

        public Guid UserId { get; set; }

        public string AccessToken { get; set; }

        public DateTime Expires { get; set; }
    }
}
