using Events_Web_application.Application.Services;
using Events_Web_application.Application.Services.UnitOfWork;
using Events_Web_application.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Events_Web_application.API.Controllers
{
    public class RegController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public RegController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> Registration([FromBody] User user)
        {
            user.Participant.RegistrationDate = DateTime.Now;
            user.Password = user.Password.CalculateHash();
            user.Role = user.Email.Contains("admin") ? 2 : 1;
            await _unitOfWork.Users.Add(user, new CancellationTokenSource());
            return Ok();
        }
    }
}
