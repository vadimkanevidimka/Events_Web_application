using Events_Web_application_DataBase.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Events_Web_application_DataBase.Services.DBServices.Mocks
{
    public class EventsRepository : IRepository<Event>
    {
        private readonly EWADBContext _context;
        public EventsRepository(EWADBContext context)
        {
            _context = context;
        }

        public Event Get(int id)
        {
            if (id <= 0) throw new ArgumentOutOfRangeException("id");
            return _context.Events.Where(c => c.Id == id).Include(c => c.Participants).Include(c=> c.EventImage).First();
        }

        public int Add(Event newevent)
        {
            if (_context == null || newevent == null) throw new ArgumentNullException("context");
            _context.Events.Add(newevent);
            return _context.SaveChanges();
        }   
        public int Delete(int id)
        {
            if (_context == null) throw new ArgumentNullException("context");
            if (id <= 0) throw new ArgumentOutOfRangeException("id");
            _context.Events.Remove(_context.Events.Where(c => c.Id == id).First());
            return _context.SaveChanges();
        }

        public IEnumerable<Event> GetAll()
        {
            return _context.Events.Include(c => c.Participants).Include(c => c.EventImage).ToList();
        }

        public IEnumerable<Event> GetBySearch(string searchtext)
        {
            if (string.IsNullOrEmpty(searchtext)) throw new ArgumentNullException();
            return _context.Events.Where(c => c.Title.Contains(searchtext)).Include(c => c.Participants).Include(c => c.EventImage).ToList();
        }

        public int Update(Event newevent)
        {
            if (newevent == null) throw new ArgumentNullException("new event is null");
            _context.Events.Update(newevent);
            return _context.SaveChanges();
        }

















        //public async Task<List<Event>> GetAll()
        //{
        //    return await _context.Events.Include(c => c.Participants).ToListAsync();
        //}

        //public async Task<Event> GetById(int id)
        //{
        //    if(id <= 0) throw new ArgumentOutOfRangeException("id");
        //    return await _context.Events.Where(c => c.Id == id).Include(c => c.Participants).FirstOrDefaultAsync();
        //}

        //public async Task<List<Event>> GetBySearch(string searchtext)
        //{
        //    if (string.IsNullOrEmpty(searchtext)) throw new ArgumentNullException();
        //    return await _context.Events.Where(c => c.Title.Contains(searchtext)).Include(c => c.Participants).ToListAsync();
        //}

        //public Task<int> Update(Event newevent)
        //{
        //    if (newevent == null) throw new ArgumentNullException("new event is null");
        //    _context.Events.Update(newevent);
        //    return _context.SaveChangesAsync();
        //}

        //public async Task<int> Add(Event newevent)
        //{
        //    if (_context == null || newevent == null) throw new ArgumentNullException("context");
        //    _context.Add(newevent);
        //    return await _context.SaveChangesAsync();
        //}

        //public async Task<int> Add(List<Event> newevents)
        //{
        //    if (_context == null || newevents == null) throw new ArgumentNullException("context");
        //    await _context.AddRangeAsync(newevents);
        //    return await _context.SaveChangesAsync();
        //}

        //public async Task<int> AddParticipantToEvent(int eventid, int userid)
        //{
        //    if(eventid <= 0 || userid <= 0) throw new ArgumentOutOfRangeException("id");
        //    if(_context.Events.Any(c => c.Id == eventid) && _context.Participants.Any(c => c.Id == eventid))
        //    {
        //        var evnt = _context.Events.Where(c => c.Id == eventid).First();
        //        evnt.Participants.Add(_context.Participants.Where(c => c.Id == userid).First());
        //        _context.Update(evnt);
        //    }
        //    return await _context.SaveChangesAsync();
        //}

        //public async Task<int> Delete(int id)
        //{
        //    if (_context == null) throw new ArgumentNullException("context");
        //    _context.Events.Remove(_context.Events.Where(c => c.Id == id).First());
        //    return await _context.SaveChangesAsync();
        //}

        //public async Task<int> Delete(Event eventtodelete)
        //{
        //    if (_context == null) throw new ArgumentNullException("context");
        //    _context.Events.Remove(eventtodelete);
        //    return await _context.SaveChangesAsync();
        //}

        //public async Task<int> DeleteParticipantFromEvent(int eventid, int userid)
        //{
        //    if (eventid <= 0 || userid <= 0) throw new ArgumentOutOfRangeException("id");
        //    if(_context.Events.Any(c => c.Id == eventid) && _context.Participants.Any(c => c.Id == eventid))
        //    {
        //        var participant = _context.Participants.Where(c => c.Id == userid).First();
        //        var _event = _context.Events.Where(c => c.Id == eventid).Include(c => c.Participants).First();
        //        _event.Participants.Remove(participant);
        //    }
        //    return await _context.SaveChangesAsync();
        //}
    }
}
