using Events_Web_application.Domain.Abstractions;
using Events_Web_application.Domain.Entities;
using Events_Web_application.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;

namespace Events_Web_application.Infrastructure.Repositories
{
    public class UsersRepository(EWADBContext context) : GenericRepository<User>(context), IUserRepository
    {
        public async Task<User> GetByEmail(string email, CancellationToken cancellationToken)
        {
            try
            {
                return await _context.Users.Where(c => c.Email == email).FirstAsync();
            }
            catch (Exception ex) 
            {
                cancellationToken.ThrowIfCancellationRequested();
                return default(User);
            }
        }
    }
}
