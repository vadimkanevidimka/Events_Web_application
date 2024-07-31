using AutoMapper;
using Events_Web_application.API.MidleWare.MappingModels.DTOModels;
using Events_Web_application.Domain.Entities;

namespace Events_Web_application.API.MidleWare.MappingModels.MappingProfiles
{
    public class EventMappingProfile : Profile
    {
        public EventMappingProfile()
        {
            CreateMap<Event, EventDTO>().ForMember(dest => dest.CountOfParticipants, opt => opt.MapFrom(src => src.Participants.Count));
        }
    }
}
