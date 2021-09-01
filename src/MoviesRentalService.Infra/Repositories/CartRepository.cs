using MongoDB.Driver;
using MoviesRentalService.Domain.Rent;
using MoviesRentalService.Domain.Rent.Repositories;
using System;
using System.Threading.Tasks;

namespace MoviesRentalService.Infra.Repositories
{
    public class CartRepository : BaseRepository<Cart>, ICartRepository
    {
        public CartRepository(IDbContext context)
            : base(context, "carts") { }

        public async Task<Cart> GetByUserIdAsync(Guid userId)
        {
            var filter = Builders<Cart>.Filter.Where(x => x.UserId == userId);

            return await Collection.Find(filter).FirstOrDefaultAsync();
        }

        public void Insert(Cart cart)
        {
            DbContext.AddCommand(async () => await Collection.InsertOneAsync(cart));
        }

        public void Update(Cart cart)
        {
            var filter = Builders<Cart>.Filter.Where(x => x.Id == cart.Id);

            DbContext.AddCommand(async () => await Collection.FindOneAndReplaceAsync(c => c.Id == cart.Id, cart));
        }
    }
}
