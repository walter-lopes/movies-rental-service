using MoviesRentalService.Domain.Identity;

namespace MoviesRentalService.Application.Identity
{
    public interface IHasher
    {
        string Create(User user, string secret);

        bool IsValid(User user, string secret);
    }
}
