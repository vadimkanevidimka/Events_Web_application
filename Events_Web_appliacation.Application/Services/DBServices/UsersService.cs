using Events_Web_appliacation.Domain.Abstractions;
using Events_Web_application.Application.Services.Exceptions;
using Events_Web_application.Domain.Entities;
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
        public async Task<int> AddUser(User user, CancellationTokenSource cancellationToken)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(user);
                if (await _usersRepository.Add(user, cancellationToken) == -1) throw new ServiceException(nameof(this.AddUser), user);
                cancellationToken.Token.ThrowIfCancellationRequested();
                return 1;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<int> DeleteUser(Guid id, CancellationTokenSource cancellationToken)
        {
            try
            {
                if(id ==  Guid.Empty) throw new ArgumentNullException(nameof(id));
                if (await _usersRepository.Delete(id, cancellationToken) == -1) throw new ServiceException(nameof(this.DeleteUser), id);
                return 1;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> UpdateUser(User user, CancellationTokenSource cancellationToken)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(user);
                if (await _usersRepository.Update(user, cancellationToken) == -1) throw new ServiceException(nameof(this.UpdateUser), user);
                return 1;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<User>> GetAllUsers(CancellationTokenSource cancellationToken)
        {
            try
            {
                cancellationToken.Token.ThrowIfCancellationRequested();
                return await _usersRepository.GetAll(cancellationToken);
            }
            catch (Exception ex)
            {
                throw new ServiceException(nameof(this.GetAllUsers), _usersRepository);
            }
        }
        public async Task<User> GetUsersById(Guid id, CancellationTokenSource cancellationToken)
        {
            try
            {
                if (id == Guid.Empty) throw new ArgumentNullException(nameof(id));
                return await _usersRepository.Get(id, cancellationToken.Token);
            }
            catch (Exception ex)
            {
                throw new ServiceException(nameof(this.GetUsersById), id);
            }
        }

        public async Task<User> GetUserByEmail(string email, CancellationTokenSource cancellationToken)
        {
            try
            {
                ArgumentNullException.ThrowIfNullOrWhiteSpace(email);
                var users = await _usersRepository.GetAll(cancellationToken);
                return users.Where(c => c.Email == email).First();
            }
            catch (Exception ex)
            {
                throw new ServiceException(nameof(this.GetUserByEmail), email);
            }
        }
    }
}
