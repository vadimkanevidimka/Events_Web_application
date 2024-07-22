using Events_Web_application.Application.Services.UnitOfWork;
using Events_Web_application.Domain.Models;
using Events_Web_application_DataBase.Services;
using Microsoft.AspNetCore.Mvc;

namespace Events_Web_application.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly CancellationTokenSource _cancellationTokenSource;
        public UsersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _cancellationTokenSource = new CancellationTokenSource();
        }

        [HttpGet("{id}")]
        public Task<User> Get(Guid id) => _unitOfWork.UsersService.GetUsersById(id, _cancellationTokenSource);

        [HttpGet]
        public async Task<IEnumerable<User>> GetAll() => await _unitOfWork.UsersService.GetAllUsers(_cancellationTokenSource);

        [HttpPost]
        public async Task<Guid> AddUser(string email, string password)
        {
            try
            {
                await _unitOfWork.UsersService.AddUser(new User 
                {
                    Email = email,
                    Password = password.CalculateHash(),
                }, _cancellationTokenSource);
                return _unitOfWork.UsersService.GetAllUsers(new CancellationTokenSource()).Result.Last().Id;
            }
            catch
            {
                return default;
            }
        }

        [HttpDelete]
        public async Task<int> Delete(Guid id) 
        {
            return await _unitOfWork.UsersService.DeleteUser(id, _cancellationTokenSource);
        }

        [HttpPatch]
        public async Task<int> Update(User user)
        {
            return await _unitOfWork.UsersService.UpdateUser(user, _cancellationTokenSource);
        }
    }
}
