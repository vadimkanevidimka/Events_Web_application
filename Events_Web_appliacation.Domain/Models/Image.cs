using System.Text.Json.Serialization;

namespace Events_Web_application.Domain.Models
{
    public class Image
    {
        public Guid Id { get; set; }
        public string Base64URL { get; set; }
        public Guid EventId { get; set; }
        [JsonIgnore]
        public Event Event { get; set; }
    }
}
