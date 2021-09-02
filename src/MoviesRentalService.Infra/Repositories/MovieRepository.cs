using Aloha.CQRS.Queries;
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

        public async Task<PagedResult<Movie>> FullSearchAsync(string param, int page, int items)
        {
            var filterName = Builders<Movie>.Filter.Where(p => p.Name.ToLowerInvariant().Contains(param));

            var query = Collection.Find(p => p.Name.ToLowerInvariant().Contains(param));

            var totalTask = query.CountDocumentsAsync();
            var itemsTask = query.Skip(page * items).Limit(items).ToListAsync();

            await Task.WhenAll(totalTask, itemsTask);

            var customers = itemsTask.Result;
            var total = totalTask.Result;
            var totalPages = (int)Math.Ceiling((decimal)total / items);

            return PagedResult<Movie>.Create(customers, page, items, totalPages, total);
        }

        public async Task<Movie> GetByIdAsync(Guid id)
        {
            var filter = Builders<Movie>.Filter.Where(x => x.Id == id);

            return await Collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Movie>> GetByIdsAsync(IEnumerable<Guid> ids)
        {
            var filter = Builders<Movie>.Filter.In(x => x.Id, ids);

            return await Collection.Find(filter).ToListAsync();
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
