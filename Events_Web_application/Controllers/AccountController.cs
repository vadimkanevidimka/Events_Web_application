﻿using Microsoft.AspNetCore.Mvc;
using Events_Web_application.Domain.Models;
using Events_Web_application.Application.Services.UnitOfWork;
using Events_Web_application.Application.Services.AuthServices;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace TokenApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private TokenGenerator _tokenGenerator;
        private readonly CancellationTokenSource _cancellationTokenSource;
        public AccountController(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _tokenGenerator = new TokenGenerator(_unitOfWork, _configuration);
            _cancellationTokenSource = new CancellationTokenSource();
        }

        [HttpPost]
        public async Task<IActionResult> Token([FromBody] User loginUser)
        {
            var TokenGenerationResult = await _tokenGenerator.GenerateAccessToken(default, loginUser.Email, loginUser.Password);
            AccesToken accesToken = TokenGenerationResult.Item1;
            ///
            User user = await _unitOfWork.UsersService.GetUsersById(TokenGenerationResult.Item2, _cancellationTokenSource);
            user.AsscesToken = accesToken;
            await _unitOfWork.UsersService.UpdateUser(user, _cancellationTokenSource);
            ///
            var response = new
            {
                access_token = user.AsscesToken,
                id = user.Id,
                userrole = user.Role,
            };

            return Ok(response);
        }

        [HttpPost]
        [Route("refresh-token")]
        public async Task<IActionResult> RefreshToken(AccesToken token)
        {
            if (token is null)
            {
                return BadRequest("Invalid client request");
            }

            string? accessToken = token.Token;
            string? refreshToken = token.RefreshToken;

            var principal = GetPrincipalFromExpiredToken(accessToken);
            if (principal == null)
            {
                return BadRequest("Invalid access token or refresh token");
            }

#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            Guid userId = Guid.Parse(principal.Identity.Name);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

            var user = await _unitOfWork.UsersService.GetUsersById(userId, _cancellationTokenSource);

            if (user == null || user.AsscesToken.RefreshToken != refreshToken || user.AsscesToken.ExpirationRTDateTime <= DateTime.Now)
            {
                return BadRequest("Invalid access token or refresh token");
            }

            var result = await _tokenGenerator.GenerateAccessToken();
            user.AsscesToken = result.Item1;
            await _unitOfWork.UsersService.UpdateUser(user, _cancellationTokenSource);

            var response = new
            {
                access_token = user.AsscesToken,
                id = user.Id,
                userrole = user.Role,
            };

            return Ok(response);
        }

        private ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"])),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;

        }

        [Authorize]
        [HttpPost("{id}")]
        public async Task<IActionResult> Revoke(Guid userId)
        {
            var user = await _unitOfWork.UsersService.GetUsersById(userId, _cancellationTokenSource);
            if (user == null) return BadRequest("Invalid user name");

            user.AsscesToken.RefreshToken = string.Empty;
            user.AsscesToken.ExpirationRTDateTime = default;
            await _unitOfWork.UsersService.UpdateUser(user, _cancellationTokenSource);

            return NoContent();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> RevokeAll()
        {
            var users =await _unitOfWork.UsersService.GetAllUsers(new CancellationTokenSource());
            foreach (var user in users)
            {
                user.AsscesToken.RefreshToken = string.Empty;
                user.AsscesToken.ExpirationRTDateTime = default;
                await _unitOfWork.UsersService.UpdateUser(user, _cancellationTokenSource);
            }

            return NoContent();
        }
    }
}