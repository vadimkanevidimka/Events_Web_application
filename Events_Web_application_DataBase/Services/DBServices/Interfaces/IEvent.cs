using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events_Web_application_DataBase.Services.DBServices.Interfaces
{
    public interface IEvent
    {
        public Task<List<Event>> GetAll();
        public Task<List<Event>> GetBySearch(string searchtext);
        public Task<Event> GetById(int id);
        public Task<int> Add(Event newevent);
        public Task<int> Add(List<Event> newevents);
        public Task<int> Delete(int id);
        public Task<int> Delete(Event eventtodelete);
        public Task<int> Update(Event newevent);
        public Task<int> AddParticipantToEvent(int eventid, int userid);
        public Task<int> DeleteParticipantFromEvent(int eventid, int userid);
    }
}
