using Events_Web_application.Domain.Models;

namespace Events_Web_appliacation.Domain.Abstractions
{
    public interface IEvent : IRepository<Event>
    {
        public Task<List<Event>> GetAll();
        public Task<List<Event>> GetBySearch(string searchtext);
        public Task<Event> GetById(int id);
        public Task<int> Add(Event newevent);
        public Task<int> Add(List<Event> newevents);
        public Task<int> Delete(int id);
        public Task<int> Delete(Event eventtodelete);
        public Task<int> Update(Event newevent);
        public Task<int> AddParticipantToEvent(int eventid, int userid);
        public Task<int> DeleteParticipantFromEvent(int eventid, int userid);
    }
}
