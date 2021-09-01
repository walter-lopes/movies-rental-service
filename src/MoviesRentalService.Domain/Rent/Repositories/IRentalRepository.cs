using System;
using System.Threading.Tasks;

namespace MoviesRentalService.Domain.Rent.Repositories
{
    public interface IRentalRepository
    {
        void Insert(Rental rental);

        Task<bool> ExistsByMovieIdAsync(Guid movieId, Guid userId);

        Task<Rental> GetAllAvailableByUserId(Guid userId);
    }
}
