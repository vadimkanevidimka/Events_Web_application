using Microsoft.AspNetCore.Mvc;
using Events_Web_application.Domain.Models;
using Events_Web_application.Application.Services.UnitOfWork;
using Events_Web_appliacation.Core.MidleWare.EmailNotificationService;
using AutoMapper;
using Events_Web_application.Domain.Models.MappingModels.MappingProfiles;
using Events_Web_application.Domain.Models.MappingModels;

namespace Events_Web_application.Controllers
{
    public class EventController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly CancellationTokenSource _cancellationTokenSource;
        private readonly IMapper _mapper;
        public EventController(IUnitOfWork unitOfWork, IConfiguration configuration, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _cancellationTokenSource = new CancellationTokenSource();
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<int> Add([FromBody] Event @event)
        {
            return await _unitOfWork.EventsService.AddEvent(@event);
        }

        [HttpGet]
        public async Task<IEnumerable<EventMappingProfile>> GetAll()
        {
            return _mapper.Map<IEnumerable<EventMappingProfile>>(await _unitOfWork.EventsService.GetAllEvents(_cancellationTokenSource));
        }

        [HttpGet]
        public async Task<Event> GetById(Guid id)
        {
            return await _unitOfWork.EventsService.GetEventById(id, _cancellationTokenSource);
        }

        [HttpGet]
        public async Task<List<ListEventMap>> GetBySearch(string search = "", string category = "", string location = "") =>
           _mapper.Map<List<Event>, List<ListEventMap>>(await _unitOfWork.EventsService.GetBySearch(search, category, location, _cancellationTokenSource) as List<Event>);

        [HttpPost]
        public async Task<int> AddParticipantToEvent(Guid eventid, Guid userid)
        {

            return await _unitOfWork.EventsService.AddParticipantToEvent(eventid, userid, _cancellationTokenSource);
        }

        [HttpDelete]
        public async Task<int> Delete(Guid id)
        {
            return await _unitOfWork.EventsService.Delete(id, _cancellationTokenSource);
        }

        [HttpPost]
        public async Task<int> DeleteParticipantFromEvent(Guid eventid, Guid userid)
        {
            return await _unitOfWork.EventsService.DeleteParticipantFromEvent(eventid, userid, _cancellationTokenSource);
        }

        [HttpGet]
        public async Task<IEnumerable<Event>> GetUsersEvents(Guid userid)
        {
            return await _unitOfWork.EventsService.GetUserEvents(userid, _cancellationTokenSource);
        }

        [HttpPatch]
        public async Task<ActionResult<Event>> Update([FromBody] Event @event)
        {
            await _unitOfWork.EventsService.UpdateEvent(@event, _cancellationTokenSource);
            return Ok(_unitOfWork.EventsService.GetEventById(@event.Id, _cancellationTokenSource));
        }

        private async void SendToEmails(string[] emails, Event @event)
        {
            var email = new EmailServiceBuilder();
            var sender = email.Build();
            await sender.SendAsync();
        }
    }
}
