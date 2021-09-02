using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoviesRentalService.Domain.Rent.Repositories
{
    public interface IRentalRepository
    {
        void Insert(Rental rental);

        Task<bool> ExistsByMovieIdAsync(Guid movieId, Guid userId);

        Task<bool> ExistsByMovieIdsAsync(HashSet<Guid> movieIds, Guid userId);

        Task<IEnumerable<Rental>> GetAllAvailableByUserIdAsync(Guid userId);
    }
}
