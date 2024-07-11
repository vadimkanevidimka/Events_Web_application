using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Events_Web_application.Domain.Models
{
    public class EventCategory
    {
        public EventCategory() => Events = new List<Event>();
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [JsonIgnore]
        public ICollection<Event> Events { get; set; }
    }
}
