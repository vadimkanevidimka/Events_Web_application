using Events_Web_appliacation.Domain.Abstractions;
using Events_Web_application.Domain.Entities;
using Events_Web_application.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;

namespace Events_Web_application.Infrastructure.Repositories
{
    public class ParticipantsRepository(EWADBContext context) :  GenericRepository<Participant>(context), IParticipantRepository
    {
        public async Task<IEnumerable<Participant>> GetAllParticipants()
        {
            return await _context.Participants.ToListAsync();
        }
    }
}
