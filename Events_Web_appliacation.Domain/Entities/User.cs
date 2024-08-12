using Events_Web_application.Application.Services.AuthServices;

namespace Events_Web_application.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public AccesToken? AsscesToken { get; set; }
        public int Role { get; set; }
        public Guid ParticipantId { get; set; }
        public Participant? Participant { get; set; }
    }
}
