using Events_Web_appliacation.Domain.Abstractions;
using Events_Web_application.Domain.Models;
using Events_Web_application.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;
namespace Events_Web_application.Infrastructure.Repositories
{
    public class EventsRepository : IRepository<Event>
    {
        private readonly EWADBContext _context;
        public EventsRepository(EWADBContext context) => 
            _context = context;

        public async Task<Event> Get(Guid id) =>
           await _context.Events.Where(c => c.Id == id).Include(c => c.Participants).Include(c => c.EventImage).FirstAsync();

        public async Task<int> Add(Event newevent)
        {
            _context.Events.Add(newevent);
            return await _context.SaveChangesAsync();
        }
        public async Task<int> Delete(Guid id)
        {
            _context.Events.Remove(_context.Events.Where(c => c.Id == id).First());
            return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Event>> GetAll() => 
            await _context.Events.Include(c => c.Participants).Include(c => c.EventImage).ToListAsync();

        public async Task<int> Update(Event newevent)
        {
            _context.Events.Update(newevent);
            return await _context.SaveChangesAsync();
        }
    }
}
