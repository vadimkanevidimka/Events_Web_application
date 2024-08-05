using Events_Web_application.Application.Services.DBServices.DBServicesGenerics;
using Events_Web_application.Application.Services.Validation;
using Events_Web_application.Domain.Entities;
using Events_Web_application.Infrastructure.DBContext;

namespace Events_Web_application.Application.Services.DBServices
{
    public class ParticipantsService : DBServiceGeneric<Participant>, IDBService<Participant>
    {
        public ParticipantsService(EWADBContext context) : base(context)
        {
            base._validator = new ParticipantValidator();
        }

        public async Task<bool> IsUserRegisteredToEvent(Guid userId, Guid eventId)
        {
            var usr = await _context.FindAsync<User>(userId);
            var evnt = await _context.FindAsync<Event>(eventId);
            if (usr != null && evnt != null && evnt.Participants.Contains(usr.Participant)) return true;
            return false;
        }

    }
}
