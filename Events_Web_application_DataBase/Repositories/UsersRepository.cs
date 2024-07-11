using Events_Web_appliacation.Domain.Abstractions;
using Events_Web_application.Domain.Models;
using Events_Web_application.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace Events_Web_application.Infrastructure.Repositories
{
    public class UsersRepository : IRepository<User>
    {
        private readonly EWADBContext _context;
        public UsersRepository(EWADBContext context) =>
            _context = context;
        public async Task<int> Add(User newuser, CancellationTokenSource cancellationToken)
        {
            try
            {
                await _context.Users.AddAsync(newuser);
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await cancellationToken.CancelAsync();
                return -1;
            }
        }

        public async Task<int> Delete(Guid id, CancellationTokenSource cancellationToken)
        {
            try
            {
                _context.Users.Remove(await _context.Users.Where(c => c.Id == id).FirstAsync());
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex) 
            {
                await cancellationToken.CancelAsync();
                return -1;
            }
        }

        public async Task<User> Get(Guid id, CancellationTokenSource cancellationToken)
        {
            try
            {
                return await _context.Users.Where(c => c.Id == id)
                    .Include(c => c.Participant)
                    .Include(c => c.Participant.UserEvents)
                    .FirstAsync();
            }
            catch 
            {
                await cancellationToken.CancelAsync();
                return default(User);
            }
        }

        public async Task<IEnumerable<User>> GetAll(CancellationTokenSource cancellationToken)
        {
            try
            {
                return await _context.Users.Include(c => c.Participant).ToListAsync();
            }
            catch (Exception ex) 
            {
                await cancellationToken.CancelAsync();
                return Enumerable.Empty<User>();
            }
        }

        public async Task<User> GetByEmail(string email, CancellationTokenSource cancellationToken)
        {
            try
            {
                return await _context.Users.Where(c => c.Email == email).FirstAsync();
            }
            catch (Exception ex) 
            {
                await cancellationToken.CancelAsync();
                return default(User);
            }
        }

        public async Task<int> Update(User user, CancellationTokenSource cancellationToken)
        {
            try
            {
                _context.Users.Update(user);
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex) 
            {
                await cancellationToken.CancelAsync();
                return -1;
            }
        }
    }
}
