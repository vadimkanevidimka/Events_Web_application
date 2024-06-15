using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Events_Web_application_DataBase
{
    public class Image
    {
        public int Id { get; set; }
        [Required]
        public string Base64URL { get; set; }
        [Required]
        public int EventId { get; set; }
        [Required]
        [JsonIgnore]
        public Event Event { get; set; }
    }
}
