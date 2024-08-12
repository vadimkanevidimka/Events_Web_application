using Events_Web_appliacation.Domain.Abstractions;
using Events_Web_application.Domain.Entities;
using Events_Web_application.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;

namespace Events_Web_application.Infrastructure.Repositories
{
    public class ImagesRepository(EWADBContext context) : GenericRepository<Image>(context), IRepository<Image>
    {
        public async Task<int> AddImage(Guid eventId, Image newimage, CancellationToken cancellationToken)
        {
            var evnt = await _context.Events.Where(c => c.Id == eventId)
                .Include(c => c.Participants)
                .Include(c => c.EventImage)
                .FirstAsync(cancellationToken);

            evnt.EventImage = newimage;
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}