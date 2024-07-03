using Events_Web_appliacation.Domain.Abstractions;
using Events_Web_application.Domain.Models;
using Events_Web_application.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;

namespace Events_Web_application.Infrastructure.Repositories
{
    public class UsersRepository : IRepository<User>
    {
        private readonly EWADBContext _context;
        public UsersRepository(EWADBContext context) =>
            _context = context;
        public async Task<int> Add(User newuser)
        {
            await _context.Users.AddAsync(newuser);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(Guid id)
        {
            _context.Users.Remove(await _context.Users.Where(c => c.Id == id).FirstAsync());
            return await _context.SaveChangesAsync();
        }

        public async Task<User> Get(Guid id) => 
           await _context.Users.Where(c => c.Id == id).Include(c => c.Participant).Include(c => c.Participant.UserEvents).FirstAsync();

        public async Task<IEnumerable<User>> GetAll() => 
           await _context.Users.Include(c => c.Participant).ToListAsync();

        public async Task<User> GetByEmail(string email) => 
            await _context.Users.Where(c => c.Email == email).FirstAsync();

        public Task<IEnumerable<User>> GetBySearch(string searchtext) => throw new NotImplementedException();

        public async Task<int> Update(User user)
        {
            _context.Users.Update(user);
            return await _context.SaveChangesAsync();
        }
    }
}
