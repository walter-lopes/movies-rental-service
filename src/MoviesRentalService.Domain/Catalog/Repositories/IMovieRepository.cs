using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoviesRentalService.Domain.Catalog.Repositories
{
    public interface IMovieRepository
    {
        void Insert(Movie movie);

        void Update(Movie movie);

        Task<Movie> GetByIdAsync(Guid id);

        Task<IEnumerable<Movie>> GetByIdsAsync(IEnumerable<Guid> ids);

        Task<IEnumerable<Movie>> FullSearchAsync(string @param);
    }
}
