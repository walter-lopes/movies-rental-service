using MongoDB.Driver;
using MoviesRentalService.Domain.Rent;
using MoviesRentalService.Domain.Rent.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesRentalService.Infra.Repositories
{
    public class RentalRepository : BaseRepository<Rental>, IRentalRepository
    {
        public RentalRepository(IDbContext context)
         : base(context, "rentals") { }

        public async Task<bool> ExistsByMovieIdsAsync(HashSet<Guid> movieIds, Guid userId)
        {
            var filterIn = Builders<Rental>.Filter.Where(x => x.Items.Any(y => movieIds.Contains(y.MovieId)));

            var filter = Builders<Rental>.Filter.Where(p => p.UserId == userId && p.Expires < DateTime.Now);

            return await Collection.Find(filter & filterIn).AnyAsync();
        }

        public async Task<IEnumerable<Rental>> GetAllAvailableByUserIdAsync(Guid userId)
        {
            var filter = Builders<Rental>.Filter.Where(p => p.UserId == userId && p.Expires < DateTime.Now);

            return await Collection.Find(filter).ToListAsync();
        }

        public void Insert(Rental rental)
        {
            DbContext.AddCommand(async () => await Collection.InsertOneAsync(rental));
        }
    }
}
