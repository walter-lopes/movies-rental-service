using Microsoft.IdentityModel.Tokens;
using MoviesRentalService.Domain.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace MoviesRentalService.Infra.Identity
{
    public class JwtHandler : IJwtHandler
    {
        private readonly JwtOptions _options;
        private readonly SecurityKey _issuerSigningKey;
        private readonly SigningCredentials _signingCredentials;
        private readonly TokenValidationParameters _tokenValidationParameters;

        public JwtHandler(JwtOptions options)
        {
            _options = options;
            _issuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));
            _signingCredentials = new SigningCredentials(_issuerSigningKey, SecurityAlgorithms.HmacSha256);
            _tokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = _issuerSigningKey,
                ValidIssuer = _options.Issuer,
                ValidAudience = _options.ValidAudience,
                ValidateAudience = _options.ValidateAudience,
                ValidateLifetime = _options.ValidateLifetime
            };
        }

        public IdentityToken CreateToken(User user)
        {
            var now = DateTime.Now;

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email.ToString()),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var expires = now.AddMinutes(60);

            var jwt = new JwtSecurityToken(
                issuer: _options.Issuer,
                claims: claims,
                notBefore: now,
                expires: expires,
                signingCredentials: _signingCredentials
            );

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new IdentityToken
            {
                Email = user.Email,
                UserId = user.Id,
                AccessToken = token,
                Expires = expires
            };
        }
    }
}
