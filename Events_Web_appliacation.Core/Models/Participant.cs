using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Events_Web_appliacation.Core.Models
{
    public class Participant
    {
        public Participant() => UserEvents = new List<Event>();
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(200)]
        public string LastName { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public DateTime RegistrationDate { get; set; }

        [JsonIgnore]
        public List<Event> UserEvents { get; set; }

        [Required]
        [MaxLength(200)]
        public string Email { get; set; }
    }

}
