using Events_Web_appliacation.Domain.Abstractions;
using Events_Web_application.Domain.Models;
using Events_Web_application.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace Events_Web_application.Infrastructure.Repositories
{
    public class ParticipantsRepository : IRepository<Participant>
    {
        private readonly EWADBContext _context;
        public ParticipantsRepository(EWADBContext context)
        {
            _context = context;
        }

        public async Task<int> Add(Participant newparticipant, CancellationTokenSource cancellationToken)
        {
            try
            {
                _context.Participants.Add(newparticipant);
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
                _context.Participants.Remove(await _context.Participants.Where(c => c.Id.Equals(id)).FirstAsync());
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await cancellationToken.CancelAsync();
                return -1;
            }
            
        }

        public async Task<Participant> Get(Guid id, CancellationTokenSource cancellationToken)
        {
            try
            {
                return await _context.Participants
                    .Where(c => c.Id.Equals(_context.Users.Where(c => c.Id == id).FirstOrDefault().Participant.Id))
                    .Include(c => c.UserEvents).FirstAsync();
            }
            catch (Exception ex) 
            {
                await cancellationToken.CancelAsync();
                return default(Participant);
            }
        }

        public async Task<IEnumerable<Participant>> GetAll(CancellationTokenSource cancellationToken)
        {
            try
            {
                return await _context.Participants.ToListAsync();
            }
            catch(Exception ex)
            {
                await cancellationToken.CancelAsync();
                return Enumerable.Empty<Participant>();
            }
        }

        public async Task<int> Update(Participant participant, CancellationTokenSource cancellationToken)
        {
            try
            {
                _context.Participants.Update(participant);
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
