using Events_Web_application_DataBase;
using Events_Web_application_DataBase.Repositories;
using Events_Web_application_DataBase.Services;
using Microsoft.AspNetCore.Mvc;

namespace Events_Web_application.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public UsersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("{id}")]
        public User Get(int id) => _unitOfWork.Users.Get(id);

        [HttpGet]
        public List<User> GetAll() => _unitOfWork.Users.GetAll().ToList();

        [HttpPost]
        public User AddUser(string email, string password)
        {
            try
            {
                _unitOfWork.Users.Add(new User 
                {
                    Email = email,
                    Password = password.CalculateHash(),
                });
                return _unitOfWork.Users.Get(1);
            }
            catch
            {
                return _unitOfWork.Users.Get(1);
            }
        }

        [HttpDelete]
        public int Delete(int id) 
        {
            return _unitOfWork.Users.Delete(id);
        }

        [HttpPatch]
        public int Update(User user)
        {
            return _unitOfWork.Users.Update(user);
        }
    }
}
