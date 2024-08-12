using System.Text.Json.Serialization;

namespace Events_Web_application.Domain.Entities
{
    public class EventCategory : BaseEntity
    {
        public EventCategory() => Events = new List<Event>();
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [JsonIgnore]
        public ICollection<Event> Events { get; set; }
    }
}
