using Aloha.CQRS.Queries;
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

        Task<PagedResult<Movie>> FullSearchAsync(string param, int page, int items);
    }
}
