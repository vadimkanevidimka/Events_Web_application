using Events_Web_application_DataBase;
using Microsoft.AspNetCore.Mvc;
using Events_Web_application_DataBase.Repositories;
using Events_Web_appliacation.Core.MidleWare.EmailNotificationService;

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
        public async Task<ActionResult<Event>> Update([FromBody] Event @event)
        {
            var participantsEmails = @event.Participants.Select(c => c.Email).ToList();
            var email = new EmailServiceBuilder().SetMailSender("vadimdeg6@gmail.com")
                .SetMailRecievers(participantsEmails.ToArray())
                .SetCreditials("vadimdeg6@gmail.com", "_51683Bv435")
                .SetMailContent("Event had been updated", 
                $"<div class=\"col-lg-8 shadow rounded\">" +
                $"<div class=\"pin-details\">" +
                $"<h1 class=\"pin-title\">{@event.Title}</h1>" +
                $"<p class=\"pin-description\"><strong>Description: </strong>{@event.Description}</p>" +
                $"<p><strong>Date:</strong>{{new Date({@event.EventDateTime}).toLocaleString()}}</p>" +
                $"<p><strong>Location:</strong>{@event.Location}</p><p><strong>Category:</strong> {@event.Category}</p>" +
                $"<p><strong>Available Seats:</strong> {@event.MaxParticipants - @event.Participants.Count}</p>" +
                $"</div>" +
                $"</div>");
                var sender = email.Build();
            @event.Participants = null;
            _unitOfWork.Events.Update(@event);
            await sender.SendAsync();
            return Ok(_unitOfWork.Events.Get(@event.Id));
        }
    }
}
