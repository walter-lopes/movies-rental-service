using FluentAssertions;
using MoviesRentalService.Domain.Catalog;
using MoviesRentalService.Domain.Rental;
using System;
using System.Linq;
using Xunit;

namespace MoviesRentalService.UnitTests.Domain
{
    public class CartTests
    {
        [Fact]
        public void Should_Init_Cart()
        {
            var userId = Guid.NewGuid();

            var cart = new Cart(userId);

            cart.Id.Should().NotBeEmpty();
            cart.UserId.Should().Be(userId);
            cart.Items.Should().NotBeNull();
            cart.Items.Should().BeEmpty();
        }

        [Fact]
        public void Should_Add_New_Item()
        {
            var userId = Guid.NewGuid();
            var cart = new Cart(userId);
            string name = "The lord of the rings - Return of the king";
            string description = "Adventure movie";
            decimal price = 10.5M;
            var movie = new Movie(name, description, price);
            var cartItem = new RentalItem(movie);

            bool added = cart.Add(cartItem);

            cart.Items.Should().HaveCount(1);
            cart.Items.FirstOrDefault().MovieId.Should().Be(movie.Id);
            cart.Items.FirstOrDefault().Price.Should().Be(movie.Price);
            added.Should().BeTrue();
        }

        [Fact]
        public void Should_Not_Add_Same_Item()
        {
            var userId = Guid.NewGuid();
            var cart = new Cart(userId);
            string name = "The lord of the rings - Return of the king";
            string description = "Adventure movie";
            decimal price = 10.5M;

            var movie = new Movie(name, description, price);
            var cartItem = new RentalItem(movie);
            var cartItemTwo = new RentalItem(movie);

            cart.Add(cartItem);
            bool addedTwice = cart.Add(cartItemTwo);

            addedTwice.Should().BeFalse();
        }

        [Fact]
        public void Should_Add_Two_Items()
        {
            var userId = Guid.NewGuid();
            var cart = new Cart(userId);

            string name = "The lord of the rings - Return of the king";
            string description = "Adventure movie";
            decimal price = 10.5M;
            var returnOfTheKing = new Movie(name, description, price);

            name = "The lord of the rings - Return of the king";
            description = "Adventure movie";
            price = 10.5M;
            var theTwoTowers = new Movie(name, description, price);

            var cartItem = new RentalItem(returnOfTheKing);
            var cartItemTwo = new RentalItem(theTwoTowers);

            cart.Add(cartItem);
            bool addedTwice = cart.Add(cartItemTwo);

            addedTwice.Should().BeTrue();
            cart.Items.Should().HaveCount(2);
        }

        [Fact]
        public void Should_Clean_Cart()
        {
            var userId = Guid.NewGuid();
            var cart = new Cart(userId);
            string name = "The lord of the rings - Return of the king";
            string description = "Adventure movie";
            decimal price = 10.5M;

            var movie = new Movie(name, description, price);
            var cartItem = new RentalItem(movie);

            cart.Add(cartItem);
            cart.Clean();

            cart.Items.Should().NotBeNull();
            cart.Items.Should().BeEmpty();
        }
    }
}
