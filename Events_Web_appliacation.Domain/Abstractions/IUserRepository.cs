using Events_Web_application.Domain.Entities;

namespace Events_Web_application.Domain.Abstractions
{
    public interface IUserRepository : IRepository<User>
    {
        public Task<User> GetByEmail(string email, CancellationToken cancellationToken);
    }
}
