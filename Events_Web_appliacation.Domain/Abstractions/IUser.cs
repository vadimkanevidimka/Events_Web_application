using Events_Web_application.Domain.Models;

namespace Events_Web_appliacation.Domain.Abstractions
{
    public interface IUser : IRepository<User>
    {
        public List<User> GetAll();
        public int Add(User newuser);
        public int Delete(int id);
        public int Update(User user);
        public User Get(int id);
        public User GetByEmail(string email);
    }
}
