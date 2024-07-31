using Events_Web_application.Domain.Entities;

namespace Events_Web_appliacation.Domain.Abstractions
{
    public interface IUserRepository : IRepository<User>
    {
        public Task<User> GetByEmail(string email, CancellationToken cancellationToken);
    }
}
