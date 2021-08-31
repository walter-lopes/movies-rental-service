using FluentAssertions;
using MoviesRentalService.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            string password = "AJHDGAHFT53673ghGFGF";
            string role = "customer";

            var user = new User(name, email, password, role);

            user.Email.Should().Be(email);
            user.Name.Should().Be(name);
            user.Password.Should().Be(password);
            user.Role.Should().Be(role);
        }
    }
}
