using Events_Web_application.Application.Services.UnitOfWork;
using Events_Web_application.Domain.Models;
using Events_Web_application_DataBase.Services;
using Microsoft.AspNetCore.Mvc;

namespace Events_Web_application.Controllers
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
            await _unitOfWork.UsersService.AddUser(user, new CancellationTokenSource());
            return Ok();
        }
    }
}
