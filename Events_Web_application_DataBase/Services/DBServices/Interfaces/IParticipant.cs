using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Events_Web_application_DataBase.Repositories;

namespace Events_Web_application_DataBase.Services.DBServices.Interfaces
{
    public interface IParticipant : IRepository<Participant>
    {
        public List<Participant> GetAllParticipants();
        public Participant GetParticipant(int id);
        public int Add(Participant newparticipant);
        public int Update(Participant participant);
        public int Delete(int id);
        public int Delete(Participant participant);
    }
}
