using Microsoft.AspNetCore.Mvc;
using Events_Web_application.Application.Services.UnitOfWork;
using AutoMapper;
using Events_Web_application.Domain.Entities;
using Events_Web_application.API.MidleWare.MappingModels.MappingProfiles;
using Events_Web_application.Application.Services.Exceptions;
using FluentValidation;
using Events_Web_application.Application.Services.DBServices.DBServicesGenerics;
using Events_Web_application.API.MidleWare.MappingModels.DTOModels;
using Events_Web_application.Application.Services.DBServices;
using Microsoft.AspNetCore.Authorization;
using Events_Web_application.Domain.Models.Pagination;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Events_Web_application.Controllers
{
    public class EventController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly EventsService _dbService;
        private readonly CancellationTokenSource _cancellationTokenSource;
        
        public EventController(IUnitOfWork unitOfWork, IMapper mapper, IDBService<Event> service)
        {
            _unitOfWork = unitOfWork;
            _cancellationTokenSource = new CancellationTokenSource();
            _mapper = mapper;
            _dbService = service as EventsService;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Event @event)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                if(await _dbService.IsRecordValid(@event, _cancellationTokenSource.Token))
                {
                    var transactionresult = await _unitOfWork.Events.Add(@event, _cancellationTokenSource);
                    _unitOfWork.Commit();
                    await _unitOfWork.Save();
                    return Ok(transactionresult);
                }
                else return BadRequest();
            }
            catch(ServiceException ex)
            {
                _unitOfWork.Rollback();
                return Problem(ex.Message, ex.Operation);
            }
        }

        [HttpGet]
        public async Task<IEnumerable<EventDTO>> GetAll()
        {
            try
            {
                var events = await _unitOfWork.Events.GetAll(_cancellationTokenSource);
                await _dbService.GetEventsImagesFromCache(events);
                return _mapper.Map<IEnumerable<Event>, IEnumerable<EventDTO>>(events);
            }
            catch(ServiceException ex) 
            {
                return null;
            }
        }

        [HttpGet]
        public async Task<Event> GetById(Guid id)
        {
            try
            {
                var @event = await _unitOfWork.Events.Get(id, _cancellationTokenSource.Token);
                await _dbService.GetEventImageFromCache(@event);
                return @event;
            }
            catch(ServiceException ex) 
            {
                return null;
            }
        }

        [HttpGet]
        public async Task<List<EventDTO>> GetBySearch(string search, string category, string location, [FromQuery] PaginationParameters paginationParameters)
        {
           var events = await _unitOfWork.Events.GetBySearch(search, category, location, paginationParameters, _cancellationTokenSource.Token);
           await _dbService.GetEventsImagesFromCache(events);
           return _mapper.Map<List<Event>, List<EventDTO>>(events.ToList());

        }

        [HttpPost, Authorize]
        public async Task<int> AddParticipantToEvent(Guid eventid, Guid userid)
        {
            try
            {
                if(!await _dbService.IsUserRegisteredToEvent(userid, eventid))
                {
                    _unitOfWork.CreateTransaction();
                    await _unitOfWork.Events.AddParticipantToEvent(eventid, userid, _cancellationTokenSource.Token);
                    _unitOfWork.Commit();
                    await _unitOfWork.Save();
                    return 1;
                }
                return 0;
            }
            catch(ServiceException ex) 
            {
                _unitOfWork.Rollback();
                return 0;
            }
        }

        [HttpDelete]
        public async Task<int> Delete(Guid id)
        {
            return await _unitOfWork.Events.Delete(id, _cancellationTokenSource);
        }

        [HttpPost]
        public async Task<int> DeleteParticipantFromEvent(Guid eventid, Guid userid)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                await _unitOfWork.Events.DeleteParticipantFromEvent(eventid, userid, _cancellationTokenSource.Token);
                _unitOfWork.Commit();
                await _unitOfWork.Save();
                return 1;
            }
            catch (ServiceException ex)
            {
                _unitOfWork.Rollback();
                return 0;
            } 
        }

        [HttpGet]
        public async Task<IEnumerable<Event>> GetUsersEvents(Guid userid)
        {
            return await _unitOfWork.Events.GetUsersEvents(userid, _cancellationTokenSource.Token);
        }

        [HttpPatch]
        public async Task<ActionResult<Event>> Update([FromBody] Event @event)
        {
            try
            {
                if (await _dbService.IsRecordExist(@event, _cancellationTokenSource.Token) && await _dbService.IsRecordValid(@event, _cancellationTokenSource.Token))
                {
                    await _unitOfWork.Events.Update(@event, _cancellationTokenSource);
                    return Ok(_unitOfWork.Events.Get(@event.Id, _cancellationTokenSource.Token));
                }
                else return BadRequest();
            }
            catch (ServiceException ex)
            {
                return Problem(ex.Message, ex.Operation);
            }
        }
    }
}
