using Events_Web_application.Application.Services.UnitOfWork;
using Events_Web_application.Domain.Entities;
using Events_Web_application_DataBase.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Events_Web_application.Application.Services.AuthServices
{
    public class TokenGenerator
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        public TokenGenerator(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        public async Task<(AccesToken, Guid)> GenerateAccessToken(Guid userId = default, string Email ="", string Password = "")
        {
            var identity = userId == Guid.Empty ? await GetIdentity(Email, Password) : await GetIdentity(userId);
            if (identity == null)
            {
                return (default(AccesToken), default(Guid));
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwt = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidAudience"],
                    audience: _configuration["JWT:ValidIssuer"],
                    claims: identity.Claims,
                    expires: DateTime.UtcNow.AddMinutes(int.Parse(_configuration["JWT:TokenValidityInMinutes"])),
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"])), SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var accestoken = new AccesToken()
            {
                Token = encodedJwt,
                ExpirationJWTDateTime = jwt.ValidTo,
                RefreshToken = GenerateRefreshToken(),
                ExpirationRTDateTime = DateTime.UtcNow.AddDays(int.Parse(_configuration["JWT:RefreshTokenValidityInDays"]))
            };
            return (accestoken, Guid.Parse(identity.Name));
        }

        private async Task<ClaimsIdentity> GetIdentity(string email, string password)
        {
            User user = await _unitOfWork.Users.GetByEmail(email, new CancellationTokenSource().Token);
            if (user != null && user.Password == password.CalculateHash())
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Id.ToString()),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString())
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            return null;
        }

        private async Task<ClaimsIdentity> GetIdentity(Guid userId)
        {
            User user = await _unitOfWork.Users.Get(userId, new CancellationTokenSource().Token);
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Id.ToString()),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString())
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            return null;
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}
