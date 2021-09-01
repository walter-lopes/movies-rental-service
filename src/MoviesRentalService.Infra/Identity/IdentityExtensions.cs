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
            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
            Claim claim = claimsIdentity?.FindFirst(JwtRegisteredClaimNames.UniqueName);

            if (claim == null)
                return Guid.Empty;

            return new Guid(claim.Value);
        }
    }
}
