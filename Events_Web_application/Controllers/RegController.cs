using Events_Web_application_DataBase;
using Events_Web_application_DataBase.Repositories;
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
        public IActionResult Registration([FromBody] User user)
        {
            user.Participant.RegistrationDate = DateTime.Now;
            user.Password = user.Password.CalculateHash();
            user.Role = user.Email.Contains("admin") ? 2 : 1;
            _unitOfWork.Users.Add(user);
            return Ok();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return RedirectToAction("GetAll", "Users");
        }
    }
}
