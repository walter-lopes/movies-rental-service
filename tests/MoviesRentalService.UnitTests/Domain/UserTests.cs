using FluentAssertions;
using MoviesRentalService.Domain.Identity;
using Xunit;

namespace MoviesRentalService.UnitTests.Domain
{
    public class UserTests
    {
        [Fact]
        public void Should_Init_User_Entity()
        {
            string name = "Michael Scott";
            string email = "michael.scott@dundermifflin.com";
            string role = "customer";

            var user = new User(name, email, role);

            user.Id.Should().NotBeEmpty();
            user.Email.Should().Be(email);
            user.Name.Should().Be(name);
            user.Role.Should().Be(role);
        }
    }
}
