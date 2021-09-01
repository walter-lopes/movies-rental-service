using System.Threading.Tasks;

namespace MoviesRentalService.Domain.Identity.Repositories
{
    public interface IUserRepository
    {
        void Insert(User user);

        Task<bool> ExistsByEmailAsync(string email);

        Task<User> GetByEmailAsync(string email);
    }
}
