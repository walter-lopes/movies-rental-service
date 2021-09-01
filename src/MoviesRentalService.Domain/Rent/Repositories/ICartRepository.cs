using MoviesRentalService.Domain.Rent;
using System;
using System.Threading.Tasks;

namespace MoviesRentalService.Domain.Rent.Repositories
{
    public interface ICartRepository
    {
        void Insert(Cart cart);

        void Update(Cart cart);

        Task<Cart> GetByUserIdAsync(Guid userId);
    }
}
