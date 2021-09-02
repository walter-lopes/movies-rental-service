using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

namespace MoviesRentalService.Infra.Identity
{
    public static class IdentityExtensions
    {
        public static Guid GetUserId(this IIdentity identity)
        {
            if (identity.Name == null)
                return Guid.Empty;

            return new Guid(identity.Name);
        }
    }
}
