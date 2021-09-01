using FluentAssertions;
using MoviesRentalService.Domain.Catalog;
using Xunit;

namespace MoviesRentalService.UnitTests.Domain
{
    public class MovieTests
    {
        [Fact]
        public void Should_Init_Movie()
        {
            string name = "The lord of the rings";
            string description = "Adventure movie";
            decimal price = 10.5M;

            var movie = new Movie(name, description,price);

            movie.Id.Should().NotBeEmpty();
            movie.Name.Should().Be(name);
            movie.Description.Should().Be(description);
            movie.Price.Should().Be(price);
        }

        [Fact]
        public void Should_Update_Movie()
        {
            string name = "The lord of the rings - Return of the king";
            string description = "Adventure movie";
            int stock = 10;
            decimal price = 10.5M;
            
            var movie = new Movie(name, description, price);

            name = "The lord of the rings - The Two Towers";
            description = "Adventure movie updated";
            stock = 5;
            price = 8.5M;

            movie.Update(name, description, price);
            movie.Id.Should().NotBeEmpty();
            movie.Name.Should().Be(name);
            movie.Description.Should().Be(description);
            movie.Price.Should().Be(price);
        }
    }
}
