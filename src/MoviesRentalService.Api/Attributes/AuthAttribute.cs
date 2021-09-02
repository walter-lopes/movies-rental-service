using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace MoviesRentalService.Api.Attributes
{
    public class AuthAttribute : AuthorizeAttribute
    {
        public AuthAttribute(string policy = "") : base(policy)
        {
            AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme;
        }
    }
}
