
using MoviesRentalService.Domain.Identity;

namespace MoviesRentalService.Infra.Identity
{
    public interface IJwtHandler
    {
        IdentityToken CreateToken(User user);
    }
}
