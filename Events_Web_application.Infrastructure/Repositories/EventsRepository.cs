﻿using Events_Web_appliacation.Domain.Abstractions;
using Events_Web_application.Domain.Entities;
using Events_Web_application.Domain.Models.Pagination;
using Events_Web_application.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace Events_Web_application.Infrastructure.Repositories
{
    public class EventsRepository : GenericRepository<Event>, IEventRepository
    {
        public EventsRepository(EWADBContext context) : base(context) 
        {}
        public async Task<int> AddParticipantToEvent(Guid eventid, Guid userid, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FindAsync(userid);
            var evnt = await _context.Events.FindAsync(eventid);
            evnt.Participants.Add(user.Participant);
            _context.Update(evnt);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteParticipantFromEvent(Guid eventid, Guid userid, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FindAsync(userid);
            var evnt = await _context.Events.FindAsync(eventid);
            evnt.Participants.Remove(user.Participant);
            _context.Update(evnt);
            return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Event>> GetBySearch(string search, string location, string category, PaginationParameters paginationParameters, CancellationToken cancellationToken)
        {
            var query = _context.Events.AsQueryable();

            if (!string.IsNullOrEmpty(search)) 
                query = query.Where(e => e.Title.Contains(search));

            if (!string.IsNullOrEmpty(category))
                query = query.Where(e => e.Category.Name.Contains(category));

            if (!string.IsNullOrEmpty(location))
                query = query.Where(e => e.Location.Contains(location));


            return await query.OrderBy(on => on.Title)
                .Skip((paginationParameters.PageNumber - 1) * paginationParameters.PageSize)
                .Take(paginationParameters.PageSize).ToListAsync(); ;
        }

        public async Task<IEnumerable<Event>> GetUsersEvents(Guid userId, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FindAsync(userId, cancellationToken);
            var evnts = await _context.Events.ToListAsync(cancellationToken);
            var usrevents = evnts.Where(e => e.Participants.Contains(user.Participant));
            return usrevents;
        }
    }
}
