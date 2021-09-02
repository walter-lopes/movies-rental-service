using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MoviesRentalService.Infra.Identity;
using System.Text;

namespace MoviesRentalService.Api
{
    public static class JWTConfig
    {
        public static void AddJwt(this IServiceCollection services)
        {
            IConfiguration configuration;
            using (var serviceProvider = services.BuildServiceProvider())
            {
                configuration = serviceProvider.GetService<IConfiguration>();
            }
            var options = configuration.GetOptions<JwtOptions>("jwt");
            var key = Encoding.ASCII.GetBytes(options.SecretKey);
            services.AddSingleton(options);
            services.AddSingleton<IJwtHandler, JwtHandler>();
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

        }

        internal static TModel GetOptions<TModel>(this IConfiguration configuration, string sectionName) where TModel : new()
        {
            var model = new TModel();
            configuration.GetSection(sectionName).Bind(model);
            return model;
        }
    }
}
