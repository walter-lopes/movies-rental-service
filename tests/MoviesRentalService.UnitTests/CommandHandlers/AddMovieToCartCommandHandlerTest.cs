using Aloha.CQRS.Notifications;
using Aloha.Notifications;
using Moq;
using MoviesRentalService.Application.Rent.Command;
using MoviesRentalService.Application.Rent.CommandHandlers;
using MoviesRentalService.Domain.Catalog;
using MoviesRentalService.Domain.Catalog.Repositories;
using MoviesRentalService.Domain.Rent;
using MoviesRentalService.Domain.Rent.Repositories;
using MoviesRentalService.Infra.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MoviesRentalService.UnitTests.CommandHandlers
{
    public class AddMovieToCartCommandHandlerTest
    {
        private Mock<ICartRepository> cartRepository;
        private Mock<IMovieRepository> movieRepository;
        private Mock<IRentalRepository> rentalRepository;
        private Mock<INotificationDispatcher> notificationDispatcher;
        private Mock<IUnitOfWork> uow;
        private Guid movieId;
        private Guid userId;

        public AddMovieToCartCommandHandlerTest()
        {
            movieId = Guid.NewGuid();
            userId = Guid.NewGuid();

            cartRepository = new Mock<ICartRepository>();
            movieRepository = new Mock<IMovieRepository>();
            rentalRepository = new Mock<IRentalRepository>();
            notificationDispatcher = new Mock<INotificationDispatcher>();
            uow = new Mock<IUnitOfWork>();
        }

        [Fact]
        public async Task Given_A_Movie_That_Was_Rented_Should_Notify_This_Movie_Is_Already_Rented()
        {
            var command = new AddMovieToCartCommand(movieId, userId);

            rentalRepository.Setup(x => x.ExistsByMovieIdAsync(movieId, userId)).ReturnsAsync(true);

            var handler = new AddMovieToCartCommandHandler(cartRepository.Object, movieRepository.Object, rentalRepository.Object, notificationDispatcher.Object, uow.Object);

            await handler.HandleAsync(command);

            notificationDispatcher.Verify(x => x.PublishAsync(It.IsAny<DomainNotification>()), Times.Once);
            cartRepository.Verify(x => x.GetByUserIdAsync(It.IsAny<Guid>()), Times.Never);
        }

        [Fact]
        public async Task Given_A_Movie_That_Not_Exist_Should_Notify_This_Movie_Was_Not_Found()
        {
            var command = new AddMovieToCartCommand(movieId, userId);
            var cart = new Cart(userId);

            movieRepository.Setup(x => x.GetByIdAsync(movieId)).ReturnsAsync((Movie)null);
            cartRepository.Setup(x => x.GetByUserIdAsync(userId)).ReturnsAsync(cart);

            var handler = new AddMovieToCartCommandHandler(cartRepository.Object, movieRepository.Object, rentalRepository.Object, notificationDispatcher.Object, uow.Object);

            await handler.HandleAsync(command);

            cartRepository.Verify(x => x.GetByUserIdAsync(userId), Times.Once);
            movieRepository.Verify(x => x.GetByIdAsync(movieId), Times.Once);
            notificationDispatcher.Verify(x => x.PublishAsync(It.IsAny<DomainNotification>()), Times.Once);
            cartRepository.Verify(x => x.Update(cart), Times.Never);
        }

        [Fact]
        public async Task Given_A_Cart_That_Not_Exist_Should_Notify_This_Cart_Was_Not_Found()
        {
            var command = new AddMovieToCartCommand(movieId, userId);
            var movie = new Movie("mock movie", "mock description", 100);

            movieRepository.Setup(x => x.GetByIdAsync(movieId)).ReturnsAsync(movie);
            cartRepository.Setup(x => x.GetByUserIdAsync(userId)).ReturnsAsync((Cart)null);

            var handler = new AddMovieToCartCommandHandler(cartRepository.Object, movieRepository.Object, rentalRepository.Object, notificationDispatcher.Object, uow.Object);

            await handler.HandleAsync(command);

            cartRepository.Verify(x => x.GetByUserIdAsync(userId), Times.Once);
            movieRepository.Verify(x => x.GetByIdAsync(movieId), Times.Once);
            notificationDispatcher.Verify(x => x.PublishAsync(It.IsAny<DomainNotification>()), Times.Once);
            cartRepository.Verify(x => x.Update(It.IsAny<Cart>()), Times.Never);
        }

        [Fact]
        public async Task Given_A_Movie_That_Already_Exists_In_The_Cart_Should_Notify_This_Movie_Already_In_The_Cart()
        {
            var command = new AddMovieToCartCommand(movieId, userId);
            var movie = new Movie("mock movie", "mock description", 100);
            var cart = new Cart(userId);
            cart.Add(new RentalItem(movie));

            movieRepository.Setup(x => x.GetByIdAsync(movieId)).ReturnsAsync(movie);
            cartRepository.Setup(x => x.GetByUserIdAsync(userId)).ReturnsAsync(cart);

            var handler = new AddMovieToCartCommandHandler(cartRepository.Object, movieRepository.Object, rentalRepository.Object, notificationDispatcher.Object, uow.Object);

            await handler.HandleAsync(command);

            cartRepository.Verify(x => x.GetByUserIdAsync(userId), Times.Once);
            movieRepository.Verify(x => x.GetByIdAsync(movieId), Times.Once);
            notificationDispatcher.Verify(x => x.PublishAsync(It.IsAny<DomainNotification>()), Times.Once);
            cartRepository.Verify(x => x.Update(It.IsAny<Cart>()), Times.Never);
        }

        [Fact]
        public async Task Given_A_Valid_Command_Should_Add_Movie_In_Cart()
        {
            var command = new AddMovieToCartCommand(movieId, userId);
            var movie = new Movie("mock movie", "mock description", 100);
            var cart = new Cart(userId);

            movieRepository.Setup(x => x.GetByIdAsync(movieId)).ReturnsAsync(movie);
            cartRepository.Setup(x => x.GetByUserIdAsync(userId)).ReturnsAsync(cart);

            var handler = new AddMovieToCartCommandHandler(cartRepository.Object, movieRepository.Object, rentalRepository.Object, notificationDispatcher.Object, uow.Object);

            await handler.HandleAsync(command);

            cart.Add(new RentalItem(movie));

            cartRepository.Verify(x => x.GetByUserIdAsync(userId), Times.Once);
            movieRepository.Verify(x => x.GetByIdAsync(movieId), Times.Once);
            notificationDispatcher.Verify(x => x.PublishAsync(It.IsAny<DomainNotification>()), Times.Never);
            cartRepository.Verify(x => x.Update(cart), Times.Once);
        }
    }
}
