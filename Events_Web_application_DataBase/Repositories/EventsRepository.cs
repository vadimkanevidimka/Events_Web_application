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

        public async Task<Event> Get(Guid id, CancellationTokenSource cancellationToken)
        {
            try
            {
                return await _context.Events.Where(c => c.Id == id).Include(c => c.Participants).Include(c => c.EventImage).FirstAsync();
            }
            catch (Exception ex) 
            {
                await cancellationToken.CancelAsync();
                return default(Event);
            }
        }

        public async Task<int> Add(Event newevent, CancellationTokenSource cancellationToken)
        {
            try
            {
                _context.Events.Add(newevent);
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
                _context.Events.Remove(_context.Events.Where(c => c.Id == id).First());
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex) 
            {
                await cancellationToken.CancelAsync();
                return -1;
            }
        }

        public async Task<IEnumerable<Event>> GetAll(CancellationTokenSource cancellationToken)
        {
            try
            {
                return await _context.Events.Include(c => c.Participants).Include(c => c.EventImage).ToListAsync();
            }
            catch (Exception ex) 
            {
                await cancellationToken.CancelAsync();
                return Enumerable.Empty<Event>();
            }
        }

        public async Task<int> Update(Event newevent, CancellationTokenSource cancellationToken)
        {
            try
            {
                _context.Events.Update(newevent);
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
