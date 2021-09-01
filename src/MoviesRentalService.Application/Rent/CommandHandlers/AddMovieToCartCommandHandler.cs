using Aloha.CQRS.Commands;
using MoviesRentalService.Application.Rent.Command;
using MoviesRentalService.Domain.Catalog;
using MoviesRentalService.Domain.Catalog.Repositories;
using MoviesRentalService.Domain.Rent;
using MoviesRentalService.Domain.Rent.Repositories;
using System.Threading.Tasks;

namespace MoviesRentalService.Application.Rent.CommandHandlers
{
    public class AddMovieToCartCommandHandler : ICommandHandler<AddMovieToCartCommand>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMovieRepository _movieRepository;

        public AddMovieToCartCommandHandler(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task HandleAsync(AddMovieToCartCommand command)
        {
            var cartTask = _cartRepository.GetByUserIdAsync(command.UserId);
            var movieTask = _movieRepository.GetByIdAsync(command.MovieId);

            await Task.WhenAll(cartTask, movieTask);

            Cart cart = cartTask.Result;
            Movie movie = movieTask.Result;

            var item = new RentalItem(movie);

            cart.Add(item);

            _cartRepository.Update(cart);
        }
    }
}
