
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Curso.ECommerce.Application.Dto;
using Curso.ECommerce.Application.Models;
using Curso.ECommerce.Domain.models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Curso.ECommerce.Application.Service
{
    public class UserAppService : IUserAppService
    {
        private readonly List<User> userList;
        private readonly JwtConfiguration jwtConfiguration;

        public UserAppService(
            IOptions<List<User>> userList,
            IOptions<JwtConfiguration> options
        )
        {
            this.jwtConfiguration = options.Value;
            this.userList = userList.Value;
        }
        public async Task<string> LoginAsync(UserLoginDto user)
        {
            // Validaciones
            var userEntity = userList.Where(u => u.UserName == user.UserName && u.Password == user.Password).SingleOrDefault();
            if (userEntity == null)
            {
                throw new ArgumentException("Nombre de usuario o contraseña incorrectos");
            }
            // Generar Claims
            var claims = new List<Claim>();

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, userEntity.UserName));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()));

            // Generacion del token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfiguration.Key));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new JwtSecurityToken(
                jwtConfiguration.Issuer,
                jwtConfiguration.Audience,
                claims,
                expires: DateTime.UtcNow.Add(jwtConfiguration.Expires),
                signingCredentials: signIn);


            var jwt = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

            return jwt;
        }
    }
}