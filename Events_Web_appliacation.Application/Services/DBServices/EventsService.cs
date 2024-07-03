using Events_Web_appliacation.Domain.Abstractions;
using Events_Web_application.Domain.Models;
using Events_Web_application.Infrastructure.Repositories;
using Microsoft.Extensions.Logging;

namespace Events_Web_application.Application.Services.DBServices
{
    public class EventsService
    {
        private IRepository<Event> _eventsRepository { get; set; }
        private IRepository<User> _usersRepository { get; set; }

        public EventsService(EventsRepository eventsRepository, UsersRepository usersRepository) 
        {
            _eventsRepository = eventsRepository;
            _usersRepository = usersRepository;
        }
        public async Task<int> AddEvent(Event newevent) => 
            await _eventsRepository.Add(newevent);
        public async Task<int> Delete(Guid id) => 
            await _eventsRepository.Delete(id);
        public async Task<int> UpdateEvent(Event @event) => 
            await _eventsRepository.Update(@event);
        public async Task<IEnumerable<Event>> GetAllEvents() => 
            await _eventsRepository.GetAll();
        public async Task<Event> GetEventById(Guid id) => 
            await _eventsRepository.Get(id);
        public async Task<IEnumerable<Event>> GetBySearch(string search = "", string category = "", string location = "")
        {
            var query = await _eventsRepository.GetAll();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(e => e.Title.Contains(search));
            }

            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(e => e.Category.Contains(category));
            }

            if (!string.IsNullOrEmpty(location))
            {
                query = query.Where(e => e.Location.Contains(location));
            }

            return query.ToList();
        }

        public async Task<int> AddParticipantToEvent(Guid eventId, Guid userId)
        {
            var user = await _usersRepository.Get(userId);
            var evnt = await _eventsRepository.Get(eventId);
            evnt.Participants.Add(user.Participant);
            return await _eventsRepository.Update(evnt);
        }

        public async Task<int> DeleteParticipantFromEvent(Guid eventId, Guid userId)
        {
            var user = await _usersRepository.Get(userId);
            var evnt = await _eventsRepository.Get(eventId);
            evnt.Participants.Remove(user.Participant);
            return await _eventsRepository.Update(evnt);
        }
    }
}
