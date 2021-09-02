using Aloha.CQRS.Notifications;
using Aloha.Notifications;
using Moq;
using MoviesRentalService.Application.Rent.Command;
using MoviesRentalService.Application.Rent.CommandHandlers;
using MoviesRentalService.Domain.Catalog;
using MoviesRentalService.Domain.Rent;
using MoviesRentalService.Domain.Rent.Repositories;
using MoviesRentalService.Infra.UoW;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace MoviesRentalService.UnitTests.CommandHandlers
{
    public class RentMovieCommandHandlerTest
    {
        private Mock<ICartRepository> cartRepository;
        private Mock<IRentalRepository> rentalRepository;
        private Mock<INotificationDispatcher> notificationDispatcher;
        private Mock<IUnitOfWork> uow;
        private Guid userId;

        public RentMovieCommandHandlerTest()
        {
            userId = Guid.NewGuid();

            cartRepository = new Mock<ICartRepository>();
            rentalRepository = new Mock<IRentalRepository>();
            notificationDispatcher = new Mock<INotificationDispatcher>();
            uow = new Mock<IUnitOfWork>();
        }

        [Fact]
        public async Task Given_An_Empty_Cart_Should_Notify_Cart_Is_Empty()
        {
            var command = new RentMovieCommand(userId);

            var handler = new RentMovieCommandHandler(rentalRepository.Object, cartRepository.Object, notificationDispatcher.Object, uow.Object);

            await handler.HandleAsync(command);

            notificationDispatcher.Verify(x => x.PublishAsync(It.IsAny<DomainNotification>()), Times.Once);
            cartRepository.Verify(x => x.GetByUserIdAsync(userId), Times.Once);
            rentalRepository.Verify(x => x.ExistsByMovieIdsAsync(It.IsAny<HashSet<Guid>>(), userId), Times.Never);
        }

        [Fact]
        public async Task Given_A_Valid_Command_Should_Rent_A_Movie()
        {
            var command = new RentMovieCommand(userId);
            var cart = new Cart(userId);
            var movie = new Movie("mock movie", "mock description", 100);
            cart.Add(new RentalItem(movie));
            var hashSet = new HashSet<Guid>() { movie.Id };

            cartRepository.Setup(x => x.GetByUserIdAsync(userId)).ReturnsAsync(cart);

            var handler = new RentMovieCommandHandler(rentalRepository.Object, cartRepository.Object, notificationDispatcher.Object, uow.Object);

            await handler.HandleAsync(command);

            cart.Clean();

            notificationDispatcher.Verify(x => x.PublishAsync(It.IsAny<DomainNotification>()), Times.Never);
            cartRepository.Verify(x => x.GetByUserIdAsync(userId), Times.Once);
            rentalRepository.Verify(x => x.ExistsByMovieIdsAsync(It.IsAny<HashSet<Guid>>(), userId), Times.Once);
            rentalRepository.Verify(x => x.Insert(It.IsAny<Rental>()), Times.Once);
            cartRepository.Verify(x => x.Update(cart), Times.Once);
            uow.Verify(x => x.Commit(), Times.Once);
        }

    }
}
