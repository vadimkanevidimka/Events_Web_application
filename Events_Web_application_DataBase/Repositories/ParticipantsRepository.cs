using Events_Web_appliacation.Domain.Abstractions;
using Events_Web_application.Domain.Models;
using Events_Web_application.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;

namespace Events_Web_application.Infrastructure.Repositories
{
    public class ParticipantsRepository : IRepository<Participant>
    {
        private readonly EWADBContext _context;
        public ParticipantsRepository(EWADBContext context)
        {
            _context = context;
        }

        public async Task<int> Add(Participant newparticipant)
        {
            _context.Participants.Add(newparticipant);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(Guid id)
        {
            _context.Participants.Remove(await _context.Participants.Where(c => c.Id.Equals(id)).FirstAsync());
            return await _context.SaveChangesAsync();
        }

        public async Task<Participant> Get(Guid id)
        {
            return await _context.Participants
                .Where(c => c.Id.Equals(_context.Users.Where(c => c.Id == id).FirstOrDefault().Participant.Id))
                .Include(c => c.UserEvents).FirstAsync();
        }

        public async Task<IEnumerable<Participant>> GetAll()
        {
            return await _context.Participants.ToListAsync();
        }

        public async Task<IEnumerable<Participant>> GetBySearch(string searchtext)
        {
            return await _context.Participants.ToListAsync();
        }

        public async Task<int> Update(Participant participant)
        {
            _context.Participants.Update(participant);
            return await _context.SaveChangesAsync();
        }
    }
}
