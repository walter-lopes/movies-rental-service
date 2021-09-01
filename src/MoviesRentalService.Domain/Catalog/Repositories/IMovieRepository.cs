using System;
using System.Threading.Tasks;

namespace MoviesRentalService.Domain.Catalog.Repositories
{
    public interface IMovieRepository
    {
        void Insert(Movie movie);

        void Update(Movie movie);

        Task<Movie> GetByIdAsync(Guid id);
    }
}
