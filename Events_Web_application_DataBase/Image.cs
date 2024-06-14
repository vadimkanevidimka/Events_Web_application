using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
