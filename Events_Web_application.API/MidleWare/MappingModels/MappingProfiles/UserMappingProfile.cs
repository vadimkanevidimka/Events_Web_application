using AutoMapper;
using Events_Web_application.API.MidleWare.MappingModels.DTOModels;
using Events_Web_application.Domain.Entities;

namespace Events_Web_application.API.MidleWare.MappingModels.MappingProfiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile() 
        {
            CreateMap<User, UserDTO>();
        }
    }
}
