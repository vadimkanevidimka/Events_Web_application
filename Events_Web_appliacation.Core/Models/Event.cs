namespace Events_Web_application.Domain.Models
{
    public class Event
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime EventDateTime { get; set; }

        public string Location { get; set; } = string.Empty;
        public bool IsOpened { get; set; }
        public string Category { get; set; } = string.Empty;
        public int MaxParticipants { get; set; }
        public string NameOfHost { get; set; }
        public List<Participant> Participants { get; set; } = new List<Participant>();
        public Image EventImage { get; set; }
    }
}
