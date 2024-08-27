using Events_Web_application.Domain.Entities;

namespace Events_Web_application.Domain.Abstractions
{
    public interface IParticipantRepository : IRepository<Participant>
    {
        public Task<IEnumerable<Participant>> GetAllParticipants();
    }
}
