using Events_Web_application_DataBase.Repositories;
using Microsoft.EntityFrameworkCore;
namespace Events_Web_application_DataBase.Services.DBServices.Mocks
{
    public class UsersRepository : IRepository<User>
    {
        private readonly EWADBContext _context;
        public UsersRepository(EWADBContext context)
        {
            _context = context;
        }
        public int Add(User newuser)
        {
            if (newuser == null) throw new ArgumentNullException();
            _context.Users.Add(newuser);
            return _context.SaveChanges();
        }

        public int Delete(int id)
        {
            if (id < 0) throw new ArgumentOutOfRangeException();
            _context.Users.Remove(_context.Users.Where(c => c.Id == id).First());
            return _context.SaveChanges();
        }

        public User Get(int id)
        {
            if (id < 0) throw new ArgumentOutOfRangeException();
            return _context.Users.Where(c => c.Id == id).Include(c => c.Participant).Include(c => c.Participant.UserEvents).First();
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users.Include(c => c.Participant).ToList();
        }

        public User GetByEmail(string email)
        {
            if (string.IsNullOrEmpty(email)) throw new ArgumentOutOfRangeException();
            return _context.Users.Where(c => c.Email == email).First();
        }

        public IEnumerable<User> GetBySearch(string searchtext)
        {
            throw new NotImplementedException();
        }

        public int Update(User user)
        {
            if (user == null) throw new ArgumentNullException();
            _context.Users.Update(user);
            return _context.SaveChanges();
        }

        //public int Delete(User user)
        //{
        //    if (user == null) throw new ArgumentNullException();
        //    _context.Users.Remove(user);
        //    return _context.SaveChanges();
        //}
    }
}
