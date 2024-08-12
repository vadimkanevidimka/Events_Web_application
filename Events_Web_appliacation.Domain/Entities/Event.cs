using System.ComponentModel.DataAnnotations;

namespace Events_Web_application.Domain.Entities
{
    public class Event : BaseEntity
    {
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime EventDateTime { get; set; }

        public string Location { get; set; } = string.Empty;
        public bool IsOpened { get; set; } = true;
        public EventCategory Category { get; set; }
        public int MaxParticipants { get; set; }
        public string NameOfHost { get; set; }
        public List<Participant> Participants { get; set; } = new List<Participant>();
        public Image EventImage { get; set; }
    }
}
