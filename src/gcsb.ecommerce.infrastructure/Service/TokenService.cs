using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gcsb.ecommerce.application.Interfaces.Services;
using gcsb.ecommerce.domain.Client;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace gcsb.ecommerce.infrastructure.Service
{
     public class TokenService : ITokenService
    {
        public string GenerateToken(Client client)
        {
            var secret = Environment.GetEnvironmentVariable("SECRET");
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Settings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, client.Name!),
                        new Claim(ClaimTypes.Role, client.Role!),
                        new Claim("Id", client.Id.ToString()),
                    }),
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials
                (new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}