using Events_Web_application_DataBase;
using Microsoft.AspNetCore.Mvc;
using Events_Web_application_DataBase.Repositories;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Events_Web_application.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class EventController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public EventController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public int Add([FromBody] Event @event)
        {
            return _unitOfWork.Events.Add(@event);
        }

        [HttpGet]
        public List<Event> GetAll()
        {
            return _unitOfWork.Events.GetAll().ToList();
        }

        [HttpGet]
        public Event GetById(int id)
        {
            return _unitOfWork.Events.Get(id);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> GetBySearch(string search = "", string category = "", string location = "")
        {
            var query = _unitOfWork.Events.GetAll();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(e => e.Title.Contains(search));
            }

            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(e => e.Category.Contains(category));
            }

            if (!string.IsNullOrEmpty(location))
            {
                query = query.Where(e => e.Location.Contains(location));
            }

            return query.ToList();
        }

        [HttpPost]
        public int AddParticipantToEvent(int eventid, int userid)
        {
            var participant = _unitOfWork.Participants.Get(userid);
            var evnt = _unitOfWork.Events.Get(eventid);
            evnt.Participants.Add(participant);
            return _unitOfWork.Events.Update(evnt);
        }

        [HttpDelete]
        public int Delete(int id)
        {
            return _unitOfWork.Events.Delete(id);
        }

        [HttpPost]
        public int DeleteParticipantFromEvent(int eventid, int userid)
        {
            var participant = _unitOfWork.Participants.Get(userid);
            var evnt = _unitOfWork.Events.Get(eventid);
            evnt.Participants.Remove(participant);
            return _unitOfWork.Events.Update(evnt);
        }
        [HttpGet]
        public List<Event> GetUsersEvents(int userid)
        {
            var user = _unitOfWork.Users.Get(userid);
            var userevents = _unitOfWork.Events.GetAll().Where(c => c.Participants.Contains(user.Participant)).ToList();
            return userevents;
        }

        [HttpPatch]
        public ActionResult<Event> Update([FromBody] Event @event)
        {
            @event.Participants = null;
            _unitOfWork.Events.Update(@event);
            return Ok(_unitOfWork.Events.Get(@event.Id));
        }
    }
}
