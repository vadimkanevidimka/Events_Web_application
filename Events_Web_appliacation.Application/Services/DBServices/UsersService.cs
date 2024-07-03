using Events_Web_appliacation.Domain.Abstractions;
using Events_Web_application.Domain.Models;
using Events_Web_application.Infrastructure.Repositories;

namespace Events_Web_application.Application.Services.DBServices
{
    public class UsersService
    {
        private IRepository<User> _usersRepository { get; set; }

        public UsersService(UsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }
        public async Task<int> AddUser(User user) =>
            await _usersRepository.Add(user);
        public async Task<int> DeleteUser(Guid id) =>
            await _usersRepository.Delete(id);
        public async Task<int> UpdateUser(User user) =>
            await _usersRepository.Update(user);
        public async Task<IEnumerable<User>> GetAllUsers() =>
            await _usersRepository.GetAll();
        public async Task<User> GetUsersById(Guid id) =>
            await _usersRepository.Get(id);
        public async Task<User> GetUserByEmail(string email)
        {
            var users = await _usersRepository.GetAll();
            return users.Where(c => c.Email == email).First();
        }
    }
}
