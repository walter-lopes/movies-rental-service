using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoviesRentalService.Domain.Rent.Repositories
{
    public interface IRentalRepository
    {
        void Insert(Rental rental);

        Task<bool> ExistsByMovieIdsAsync(IEnumerable<Guid> movieIds, Guid userId);

        Task<Rental> GetAllAvailableByUserId(Guid userId);
    }
}
