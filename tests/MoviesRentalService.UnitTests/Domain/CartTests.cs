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
            int stock = 10;
            decimal price = 10.5M;
            var movie = new Movie(name, description, stock, price);
            var cartItem = new CartItem(movie);

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
            int stock = 10;
            decimal price = 10.5M;

            var movie = new Movie(name, description, stock, price);
            var cartItem = new CartItem(movie);

            cart.Add(cartItem);
            bool addedTwice = cart.Add(cartItem);

            addedTwice.Should().BeFalse();
        }
    }
}
