using Events_Web_application.Application.Services.DBServices.DBServicesGenerics;
using Events_Web_application.Application.Services.Exceptions;
using Events_Web_application.Application.Services.Validation;
using Events_Web_application.Domain.Entities;
using Events_Web_application.Infrastructure.DBContext;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
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
            foreach (Event @event in events)
                await GetEventImageFromCache(@event);
            return events;
        }

        public async Task<Event> GetEventImageFromCache(Event @event)
        {
            var ImageString = await _cache.GetStringAsync(@event.Id.ToString());

            @event.EventImage = (ImageString == null) ?
            _context.Images.Where(c => c.EventId == @event.Id).First() : JsonSerializer.Deserialize<Image>(ImageString);

            await Console.Out.WriteLineAsync($"For {@event.Title} image was upload from cache");

            if(string.IsNullOrEmpty(ImageString)) await AddImageToCache(@event);

            return @event;
        }

        /// <summary>
        /// Add images to redis data cache
        /// </summary>
        /// <param name="events"></param>
        /// <returns>The number of images that was added to database.</returns>
        public async Task<int> AddImageToCache(Event @event)
        {
            try
            {
                StringBuilder imagestring = new StringBuilder();
                var cacheobj = _cache.Get(@event.Id.ToString());
                if (cacheobj == null)
                {
                    imagestring.Append(JsonSerializer.Serialize(@event.EventImage));
                    // сохраняем строковое представление объекта в формате json в кэш на 2 минуты
                    await _cache.SetStringAsync(@event.Id.ToString(), imagestring.ToString(), new DistributedCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(3)
                    });
                }
                imagestring.Clear();
                return 1;
            }
            catch (Exception ex)
            {
                throw new ServiceException(nameof(AddImageToCache), @event, ex.Message);
            }
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