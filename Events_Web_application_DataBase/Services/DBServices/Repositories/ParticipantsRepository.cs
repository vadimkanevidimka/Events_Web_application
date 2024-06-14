using Events_Web_application_DataBase.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Events_Web_application_DataBase.Services.DBServices.Mocks
{
    public class ParticipantsRepository : IRepository<Participant>
    {
        private readonly EWADBContext _context;
        public ParticipantsRepository(EWADBContext context) 
        {
            _context = context;
        }

        public int Add(Participant newparticipant)
        {
            if (newparticipant == null) throw new ArgumentNullException();
            _context.Participants.Add(newparticipant);
            return _context.SaveChanges();
        }

        public int Delete(int id)
        {
            if (id < 0) throw new ArgumentOutOfRangeException();
            _context.Participants.Remove(_context.Participants.Where(c => c.Id == id).First());
            return _context.SaveChanges();
        }

        public Participant Get(int id)
        {
            if (id < 0) throw new ArgumentOutOfRangeException();
            return _context.Participants.Where(c => c.Id == id).Include(c => c.UserEvents).First();
        }

        public IEnumerable<Participant> GetAll()
        {
            return _context.Participants.ToList();
        }

        public IEnumerable<Participant> GetBySearch(string searchtext)
        {
            if (string.IsNullOrEmpty(searchtext)) throw new ArgumentNullException();
            return _context.Participants.ToList();
        }

        public int Update(Participant participant)
        {
            if (participant == null) throw new ArgumentNullException();
            _context.Participants.Update(participant);
            return _context.SaveChanges();
        }


















        //public Participant GetParticipant(int id)
        //{
        //    if(id < 0) throw new ArgumentOutOfRangeException();
        //    return _context.Participants.Where(c => c.Id == id).First();
        //}

        //public int Update(Participant participant)
        //{
        //    if(participant == null) throw new ArgumentNullException();
        //    _context.Participants.Update(participant);
        //    return _context.SaveChanges();
        //}
        //public int Add(Participant newparticipant)
        //{
        //    if (newparticipant == null) throw new ArgumentNullException();
        //    _context.Participants.Add(newparticipant);
        //    return _context.SaveChanges();
        //}

        //public int Delete(int id)
        //{
        //    if(id < 0) throw new ArgumentOutOfRangeException();
        //    _context.Participants.Remove(_context.Participants.Where(c => c.Id == id).First());
        //    return _context.SaveChanges();
        //}

        //public int Delete(Participant participant)
        //{
        //    if(participant == null) throw new ArgumentNullException();
        //    _context.Participants.Remove(participant);
        //    return _context.SaveChanges();
        //}
        //public List<Participant> GetAllParticipants()
        //{
        //    return _context.Participants.ToList();
        //}
    }
}
