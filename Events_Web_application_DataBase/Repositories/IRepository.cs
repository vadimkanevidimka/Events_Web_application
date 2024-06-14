using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events_Web_application_DataBase.Repositories
{
    public interface IRepository<T> where T : class
    {
        public IEnumerable<T> GetAll();
        public IEnumerable<T> GetBySearch(string searchtext);
        public T Get(int id);
        public int Add(T item);
        public int Update(T item);
        public int Delete(int id);
    }
}
