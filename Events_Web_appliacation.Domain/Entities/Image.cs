using System.Text.Json.Serialization;

namespace Events_Web_application.Domain.Entities
{
    public class Image : BaseEntity
    {
        public string Base64URL { get; set; }
        public Guid EventId { get; set; }

        [JsonIgnore]
        public Event Event { get; set; }
    }
}
