using FluentAssertions;
using MoviesRentalService.Domain.Catalog;
using MoviesRentalService.Domain.Rent;
using System;
using Xunit;

namespace MoviesRentalService.UnitTests.Domain
{
    public class RentalTests
    {
        [Fact]
        public void Should_Rent_A_Movie()
        {
            var userId = Guid.NewGuid();
            var cart = new Cart(userId);

            string name = "The lord of the rings - Return of the king";
            string description = "Adventure movie";
            decimal price = 10.5M;
            var returnOfTheKing = new Movie(name, description, price);

            var cartItem = new RentalItem(returnOfTheKing);

            cart.Add(cartItem);


            var rental = new Rental(cart);

            rental.Id.Should().NotBeEmpty();
            rental.Start.Should().BeCloseTo(DateTime.Now);
            rental.Expires.Should().BeCloseTo(DateTime.Now.AddDays(3));
            rental.Total.Should().Be(10.5M);
            rental.Items.Should().HaveCount(1);
        }

        [Fact]
        public void Should_Rent_Many_Movies()
        {
            var userId = Guid.NewGuid();
            var cart = new Cart(userId);

            string name = "The lord of the rings - Return of the king";
            string description = "Adventure movie";
            decimal price = 10.5M;
            var returnOfTheKing = new Movie(name, description, price);

            name = "The lord of the rings - Return of the king";
            description = "Adventure movie";
            price = 20;
            var theTwoTowers = new Movie(name, description, price);

            var cartItem = new RentalItem(returnOfTheKing);
            var cartItemTwo = new RentalItem(theTwoTowers);

            cart.Add(cartItem);
            cart.Add(cartItemTwo);

            var rental = new Rental(cart);

            rental.Id.Should().NotBeEmpty();
            rental.Start.Should().BeCloseTo(DateTime.Now);
            rental.Expires.Should().BeCloseTo(DateTime.Now.AddDays(3));
            rental.Total.Should().Be(30.5M);
            rental.Items.Should().HaveCount(2);
        }
    }
}
