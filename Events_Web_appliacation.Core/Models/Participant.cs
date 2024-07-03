using System.Text.Json.Serialization;

namespace Events_Web_application.Domain.Models
{
    public class Participant
    {
        public Participant() => UserEvents = new List<Event>();
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public DateTime RegistrationDate { get; set; }

        [JsonIgnore]
        public List<Event> UserEvents { get; set; }
    }

}
