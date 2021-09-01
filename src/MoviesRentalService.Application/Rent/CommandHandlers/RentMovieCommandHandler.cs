using Aloha.CQRS.Commands;
using MoviesRentalService.Application.Rent.Command;
using MoviesRentalService.Domain.Rent;
using MoviesRentalService.Domain.Rent.Repositories;
using System.Threading.Tasks;

namespace MoviesRentalService.Application.Rent.CommandHandlers
{
    public class RentMovieCommandHandler : ICommandHandler<RentMovieCommand>
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly ICartRepository _cartRepository;

        public RentMovieCommandHandler(IRentalRepository rentalRepository, ICartRepository cartRepository)
        {
            _rentalRepository = rentalRepository;
            _cartRepository = cartRepository;
        }

        public async Task HandleAsync(RentMovieCommand command)
        {
            var cart = await _cartRepository.GetByUserIdAsync(command.UserId);

            var rental = new Rental(cart);

            var movieIds = rental.GetMovies();

            bool exists = await _rentalRepository.ExistsByMovieIdsAsync(movieIds, command.UserId);

            if (exists)
            {
                return;
            }

            _rentalRepository.Insert(rental);

            cart.Clean();

            _cartRepository.Update(cart);
        }
    }
}
