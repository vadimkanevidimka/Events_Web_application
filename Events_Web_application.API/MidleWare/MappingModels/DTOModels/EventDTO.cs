using Events_Web_application.Domain.Entities;

namespace Events_Web_application.API.MidleWare.MappingModels.DTOModels
{
    public class EventDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime EventDateTime { get; set; }
        public string Location { get; set; } = string.Empty;
        public int CountOfParticipants { get; set; }
        public int MaxParticipants { get; set; }
        public EventCategory Category { get; set; }
        public Image EventImage { get; set; }
    }
}
