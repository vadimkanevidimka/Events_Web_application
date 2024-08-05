using Events_Web_application.Application.Services.DBServices.DBServicesGenerics;
using Events_Web_application.Application.Services.Validation;
using Events_Web_application.Domain.Entities;
using Events_Web_application.Infrastructure.DBContext;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using System.Text.Json;

namespace Events_Web_application.Application.Services.DBServices
{
    public class EventsService : DBServiceGeneric<Event>, IDBService<Event>
    {
        private readonly IDistributedCache _cache;
        public EventsService(EWADBContext context, IDistributedCache distributedCache) : base(context)
        {
            base._validator = new EventValidator();
            _cache = distributedCache;
        }

        public async Task<IEnumerable<Event>> GetEventsImagesFromCache(IEnumerable<Event> events)
        {
            foreach(Event @event in events){
                var ImageString = await _cache.GetStringAsync(@event.Id.ToString());
                    @event.EventImage = (ImageString == null) ? 
                    _context.Images.Where(c => c.EventId == @event.Id).First() : 
                    JsonSerializer.Deserialize<Image>(ImageString);
            }
            await AddImagesToCache(events);
            return events;
        }

        /// <summary>
        /// Add images to redis data cache
        /// </summary>
        /// <param name="events"></param>
        /// <returns>The number of images that was added to database.</returns>
        public async Task<int> AddImagesToCache(IEnumerable<Event> events)
        {
            StringBuilder imagestring = new StringBuilder();
            foreach (Event e in events) 
            {
                imagestring.Append(JsonSerializer.Serialize(e.EventImage));
                // сохраняем строковое представление объекта в формате json в кэш на 2 минуты
                await _cache.SetStringAsync(e.Id.ToString(), imagestring.ToString(), new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(3)
                });
                imagestring.Clear();
            }
            return events.Count();
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