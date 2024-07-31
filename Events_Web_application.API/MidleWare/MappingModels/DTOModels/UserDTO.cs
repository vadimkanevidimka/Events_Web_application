using Events_Web_application.Application.Services.AuthServices;

namespace Events_Web_application.API.MidleWare.MappingModels.DTOModels
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public AccesToken? AsscesToken { get; set; }
        public int Role { get; set; }
    }
}
