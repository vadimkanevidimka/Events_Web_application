using Events_Web_appliacation.Domain.Abstractions;
using Events_Web_application.Application.Services.DBServices.DBServicesGenerics;
using Events_Web_application.Application.Services.Validation;
using Events_Web_application.Domain.Entities;
using Events_Web_application.Infrastructure.DBContext;

namespace Events_Web_application.Application.Services.DBServices
{
    public class EventsService : DBServiceGeneric<Event>, IDBService<Event>
    {
        public EventsService(EWADBContext context) : base(context)
        {
            _validator = new EventValidator();
        }
        public delegate void EventUpdateHandler(string message);
        public event EventUpdateHandler EmailNotificationAboutEventUpdate;

        public async Task<bool> IsUserRegisteredToEvent(Guid userId, Guid eventId)
        {
            var usr = await _context.FindAsync<User>(userId);
            var evnt = await _context.FindAsync<Event>(eventId);
            if (usr != null && evnt != null && evnt.Participants.Contains(usr.Participant)) return true;
            return false;
        }
    }
}