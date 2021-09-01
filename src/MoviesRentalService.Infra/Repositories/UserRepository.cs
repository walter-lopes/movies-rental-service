using MongoDB.Driver;
using MoviesRentalService.Domain.Identity;
using MoviesRentalService.Domain.Identity.Repositories;
using System.Threading.Tasks;

namespace MoviesRentalService.Infra.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IDbContext context)
            : base(context, "users") { }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            var filter = Builders<User>.Filter.Where(x => x.Email == email);

            return await Collection.Find(filter).AnyAsync();
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            var filter = Builders<User>.Filter.Where(x => x.Email == email);

            return await Collection.Find(filter).FirstOrDefaultAsync();
        }

        public void Insert(User user)
        {
            DbContext.AddCommand(async () => await Collection.InsertOneAsync(user));
        }
    }
}
