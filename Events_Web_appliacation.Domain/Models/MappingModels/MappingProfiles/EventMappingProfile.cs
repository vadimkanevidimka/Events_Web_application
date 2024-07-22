using AutoMapper;

namespace Events_Web_application.Domain.Models.MappingModels.MappingProfiles
{
    public class EventMappingProfile : Profile
    {
        public EventMappingProfile()
        {
            CreateMap<Event, ListEventMap>().ForMember(dest => dest.CountOfParticipants, opt => opt.MapFrom(src => src.Participants.Count));
        }
    }
}
