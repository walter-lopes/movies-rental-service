using MongoDB.Driver;

namespace MoviesRentalService.Infra.Repositories
{
    public class BaseRepository<T>
    {
        protected IDbContext DbContext { get; set; }

        protected IMongoCollection<T> Collection;

        public BaseRepository(IDbContext context, string collection)
        {
            this.DbContext = context;
            this.Collection = DbContext.Context.GetCollection<T>(collection);
        }
    }
}
