using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Events_Web_application_DataBase.Services;
using Events_Web_application.Domain.Models;
using Events_Web_application.Application.Services.UnitOfWork;

namespace TokenApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public AccountController(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> Token([FromBody] User loginUser)
        {
            var identity = await GetIdentity(loginUser.Email, loginUser.Password);
            if (identity == null)
            {
                return BadRequest(new { errorText = "Invalid username or password." });
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    claims: identity.Claims,
                    expires: DateTime.UtcNow.AddMinutes(AuthOptions.LIFETIME),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                id = identity.Name,
                userrole = _unitOfWork.UsersService.GetUsersById(Guid.Parse(identity.Name)).Result.Role,
            };

            return Ok(response);
        }

        private async Task<ClaimsIdentity> GetIdentity(string email, string password)
        {
            User user = await _unitOfWork.UsersService.GetUserByEmail(email);
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
    }
}