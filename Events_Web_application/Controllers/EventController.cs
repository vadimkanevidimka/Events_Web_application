using Microsoft.AspNetCore.Mvc;
using Events_Web_application.Domain.Models;
using Events_Web_application.Application.Services.UnitOfWork;
using Microsoft.Extensions.Logging;

namespace Events_Web_application.Controllers
{
    public class EventController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public EventController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<int> Add([FromBody] Event @event)
        {
            return await _unitOfWork.EventsService.AddEvent(@event);
        }

        [HttpGet]
        public async Task<IEnumerable<Event>> GetAll()
        {
            return await _unitOfWork.EventsService.GetAllEvents();
        }

        [HttpGet]
        public async Task<Event> GetById(Guid id)
        {
            return await _unitOfWork.EventsService.GetEventById(id);
        }

        [HttpGet]
        public async Task<IEnumerable<Event>> GetBySearch(string search = "", string category = "", string location = "") => 
            await _unitOfWork.EventsService.GetBySearch(search, category, location);

        [HttpPost]
        public async Task<int> AddParticipantToEvent(Guid eventid, Guid userid)
        {
            return await _unitOfWork.EventsService.AddParticipantToEvent(eventid, userid);
        }

        [HttpDelete]
        public async Task<int> Delete(Guid id)
        {
            return await _unitOfWork.EventsService.Delete(id);
        }

        [HttpPost]
        public async Task<int> DeleteParticipantFromEvent(Guid eventid, Guid userid)
        {
            return await _unitOfWork.EventsService.DeleteParticipantFromEvent(eventid, userid);
        }
        [HttpGet]
        public async Task<IEnumerable<Event>> GetUsersEvents(Guid userid, string search = "", string category = "", string location = "")
        {
            return await _unitOfWork.EventsService.GetBySearch(search, category, location);
        }

        [HttpPatch]
        public async Task<ActionResult<Event>> Update([FromBody] Event @event)
        {
            //var participantsEmails = @event.Participants.Select(c => c.Email).ToList();
            //var email = new EmailServiceBuilder().SetMailSender("vadimdeg6@gmail.com")
            //    .SetMailRecievers(participantsEmails.ToArray())
            //    .SetCreditials("vadimdeg6@gmail.com", "_51683Bv435")
            //    .SetMailContent("Event had been updated", 
            //    $"<div class=\"col-lg-8 shadow rounded\">" +
            //    $"<div class=\"pin-details\">" +
            //    $"<h1 class=\"pin-title\">{@event.Title}</h1>" +
            //    $"<p class=\"pin-description\"><strong>Description: </strong>{@event.Description}</p>" +
            //    $"<p><strong>Date:</strong>{{new Date({@event.EventDateTime}).toLocaleString()}}</p>" +
            //    $"<p><strong>Location:</strong>{@event.Location}</p><p><strong>Category:</strong> {@event.Category}</p>" +
            //    $"<p><strong>Available Seats:</strong> {@event.MaxParticipants - @event.Participants.Count}</p>" +
            //    $"</div>" +
            //    $"</div>");
            //    var sender = email.Build();
            //@event.Participants = null;
            await _unitOfWork.EventsService.UpdateEvent(@event);
            //await sender.SendAsync();
            return Ok(_unitOfWork.EventsService.GetEventById(@event.Id));
        }
    }
}
