using Events_Web_application.Domain.Entities;

namespace Events_Web_appliacation.Domain.Abstractions
{
    public interface IEventRepository : IRepository<Event>
    {
        public Task<IEnumerable<Event>> GetBySearch(string search, string category, string location, CancellationToken cancellationToken);
        public Task<int> AddParticipantToEvent(Guid eventid, Guid userid, CancellationToken cancellationToken);
        public Task<int> DeleteParticipantFromEvent(Guid eventid, Guid userid, CancellationToken cancellationToken);
        public Task<IEnumerable<Event>> GetUsersEvents(Guid userId, CancellationToken cancellationToken);
    }
}
