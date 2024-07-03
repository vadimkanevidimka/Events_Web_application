using Events_Web_application.Domain.Models;

namespace Events_Web_appliacation.Domain.Abstractions
{
    public interface IParticipant : IRepository<Participant>
    {
        public List<Participant> GetAllParticipants();
        public Participant GetParticipant(int id);
        public int Add(Participant newparticipant);
        public int Update(Participant participant);
        public int Delete(int id);
        public int Delete(Participant participant);
    }
}
