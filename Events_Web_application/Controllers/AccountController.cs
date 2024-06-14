using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Events_Web_application_DataBase;
using Events_Web_application_DataBase.Services;
using Events_Web_application_DataBase.Repositories;

namespace TokenApp.Controllers
{
    //[Route("api/{controller}")]
    public class AccountController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public AccountController(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public IActionResult Token([FromBody] User loginUser)
        {
            var identity = GetIdentity(loginUser.Email, loginUser.Password);
            if (identity == null)
            {
                return BadRequest(new { errorText = "Invalid username or password." });
            }

            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                id = identity.Name,
                userrole = _unitOfWork.Users.Get(int.Parse(identity.Name)).Role,
            };

            return Ok(response);
        }

        private ClaimsIdentity GetIdentity(string email, string password)
        {
            User user = _unitOfWork.Users.GetByEmail(email);
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