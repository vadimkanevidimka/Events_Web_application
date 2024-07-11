using Events_Web_appliacation.Domain.Abstractions;
using Events_Web_application.Application.MidleWare.Exceptions;
using Events_Web_application.Domain.Models;
using Events_Web_application.Infrastructure.Repositories;
using Microsoft.Extensions.Logging;
using System.Threading;

namespace Events_Web_application.Application.Services.DBServices
{
    public class EventsService
    {
        private IRepository<Event> _eventsRepository { get; set; }
        private IRepository<User> _usersRepository { get; set; }

        public delegate void EventUpdateHandler(string message);
        public event EventUpdateHandler EmailNotificationAboutEventUpdate;

        public EventsService(EventsRepository eventsRepository, UsersRepository usersRepository)
        {
            _eventsRepository = eventsRepository;
            _usersRepository = usersRepository;
        }
        public async Task<int> AddEvent(Event newevent) => 
            await _eventsRepository.Add(newevent, new CancellationTokenSource());
        public async Task<int> Delete(Guid id, CancellationTokenSource cancellationToken) => 
            await _eventsRepository.Delete(id, cancellationToken);
        public async Task<int> UpdateEvent(Event @event, CancellationTokenSource cancellationToken) => 
            await _eventsRepository.Update(@event, cancellationToken);
        public async Task<IEnumerable<Event>> GetAllEvents(CancellationTokenSource cancellationToken) => 
            await _eventsRepository.GetAll(cancellationToken);
        public async Task<Event> GetEventById(Guid id, CancellationTokenSource cancellationToken) => 
            await _eventsRepository.Get(id, cancellationToken);
        public async Task<IEnumerable<Event>> GetBySearch(string search, string category, string location, CancellationTokenSource cancellationToken)
        {
            var query = await _eventsRepository.GetAll(cancellationToken);

            if (!string.IsNullOrEmpty(search)) query = query.Where(e => e.Title.Contains(search));

            if (!string.IsNullOrEmpty(category))
                query = query.Where(e => e.Category.Name.Contains(category));

            if (!string.IsNullOrEmpty(location))
                query = query.Where(e => e.Location.Contains(location));

            return query.ToList();
        }

        public async Task<IEnumerable<Event>> GetUserEvents(Guid userId, CancellationTokenSource cancellationToken)
        {
            try
            {
                var user = await _usersRepository.Get(userId, cancellationToken);
                var evnts = await _eventsRepository.GetAll(cancellationToken);
                var usrevents = evnts.Where(e => e.Participants.Contains(user.Participant));
                return usrevents;
            }
            catch (TaskCanceledException)
            {
                cancellationToken.Token.ThrowIfCancellationRequested();
                throw;
            }
        }

        public async Task<int> AddParticipantToEvent(Guid eventId, Guid userId, CancellationTokenSource cancellationToken)
        {
            try
            {
                var user = await _usersRepository.Get(userId, cancellationToken);
                var evnt = await _eventsRepository.Get(eventId, cancellationToken);
                evnt.Participants.Add(user.Participant);
                return await _eventsRepository.Update(evnt, cancellationToken);
            }
            catch (TaskCanceledException)
            {
                cancellationToken.Token.ThrowIfCancellationRequested();
                throw;
            }
        }

        public async Task<int> DeleteParticipantFromEvent(Guid eventId, Guid userId, CancellationTokenSource cancellationToken)
        {
            try
            {
                var user = await _usersRepository.Get(userId, cancellationToken);
                var evnt = await _eventsRepository.Get(eventId, cancellationToken);
                evnt.Participants.Remove(user.Participant);
                return await _eventsRepository.Update(evnt, cancellationToken);
            }
            catch (TaskCanceledException)
            {
                cancellationToken.Token.ThrowIfCancellationRequested();
                throw new ServiceException(nameof(this.DeleteParticipantFromEvent), new { eventId, userId});
            }
        }
    }
}