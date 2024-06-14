using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events_Web_application_DataBase.Services.DBServices.Interfaces
{
    public interface IUser
    {
        public List<User> GetAll();
        public int Add(User newuser);
        public int Delete(int id);
        public int Update(User user);
        public User Get(int id);
        public User GetByEmail(string email);
    }
}
