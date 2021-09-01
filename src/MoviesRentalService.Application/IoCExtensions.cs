using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using MoviesRentalService.Application.Identity;
using MoviesRentalService.Domain.Identity;

namespace MoviesRentalService.Application
{
    public static class IoCExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            // Identity
            services.AddScoped<IHasher, Hasher>();
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

            return services;
        }
    }
}
