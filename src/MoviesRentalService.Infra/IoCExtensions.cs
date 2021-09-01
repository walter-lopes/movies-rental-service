using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MoviesRentalService.Domain.Catalog.Repositories;
using MoviesRentalService.Domain.Identity.Repositories;
using MoviesRentalService.Domain.Rent.Repositories;
using MoviesRentalService.Infra.Repositories;
using MoviesRentalService.Infra.UoW;

namespace MoviesRentalService.Infra
{
    public static class IoCExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<IRentalRepository, RentalRepository>();
            services.AddScoped<IMovieRepository, MovieRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

        public static IServiceCollection AddMongoDb(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IDbContext>(sp =>
            {
                return new MongoContext()
                {
                    ConnectionString = configuration.GetSection("Mongo:ConnectionString").Value,
                    DataBase = configuration.GetSection("Mongo:DataBase").Value
                };
            });

            return services;
        }
    }
}
