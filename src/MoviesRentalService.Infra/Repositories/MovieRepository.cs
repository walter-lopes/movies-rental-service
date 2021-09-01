using MongoDB.Driver;
using MoviesRentalService.Domain.Catalog;
using MoviesRentalService.Domain.Catalog.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoviesRentalService.Infra.Repositories
{
    public class MovieRepository : BaseRepository<Movie>, IMovieRepository
    {
        public MovieRepository(IDbContext context)
         : base(context, "movies") { }

        public async Task<IEnumerable<Movie>> FullSearchAsync(string param)
        {
            var filterName = Builders<Movie>.Filter.Where(p => p.Name.ToLowerInvariant().Contains(param));

            return await Collection.Find(filterName).ToListAsync();
        }

        public async Task<Movie> GetByIdAsync(Guid id)
        {
            var filter = Builders<Movie>.Filter.Where(x => x.Id == id);

            return await Collection.Find(filter).FirstOrDefaultAsync();
        }

        public void Insert(Movie movie)
        {
            DbContext.AddCommand(async () => await Collection.InsertOneAsync(movie));
        }

        public void Update(Movie movie)
        {
            DbContext.AddCommand(async () => await Collection.FindOneAndReplaceAsync(m => m.Id == movie.Id, movie));
        }
    }
}
