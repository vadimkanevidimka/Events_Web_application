using Events_Web_application.Application.Services;
using Events_Web_application.Application.Services.UnitOfWork;
using Events_Web_application.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Events_Web_application.API.Controllers
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
        public Task<User> Get(Guid id) => _unitOfWork.Users.Get(id, _cancellationTokenSource.Token);

        [HttpGet]
        public async Task<IEnumerable<User>> GetAll() => await _unitOfWork.Users.GetAll(_cancellationTokenSource);

        [HttpPost]
        public async Task<Guid> AddUser(string email, string password)
        {
            try
            {
                await _unitOfWork.Users.Add(new User
                {
                    Email = email,
                    Password = password.CalculateHash(),
                }, _cancellationTokenSource);
                return _unitOfWork.Users.GetAll(new CancellationTokenSource()).Result.Last().Id;
            }
            catch
            {
                return default;
            }
        }

        [HttpDelete]
        public async Task<int> Delete(Guid id)
        {
            return await _unitOfWork.Users.Delete(id, _cancellationTokenSource);
        }

        [HttpPatch]
        public async Task<int> Update(User user)
        {
            return await _unitOfWork.Users.Update(user, _cancellationTokenSource);
        }
    }
}
