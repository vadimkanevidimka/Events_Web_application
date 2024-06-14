using System.ComponentModel.DataAnnotations;

namespace Events_Web_appliacation.Core.Models
{
    public class Event
    {
        private Event(string title, string description, DateTime eventDateTime, string location, string category, int maxParticipants, byte[]? image)
        {
            Title = title;
            Description = description;
            EventDateTime = eventDateTime;
            Location = location;
            Category = category;
            MaxParticipants = maxParticipants;
            Image = image;
            Participants = new List<Participant>();
        }

        public int Id { get; }

        [Required]
        [MaxLength(200)]
        public string Title { get; } = string.Empty;

        [MaxLength(1000)]
        public string Description { get; } = string.Empty;

        [Required]
        public DateTime EventDateTime { get; }

        [Required]
        [MaxLength(300)]
        public string Location { get; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Category { get; } = string.Empty;

        [Required]
        public int MaxParticipants { get; }

        public List<Participant> Participants { get; } = new List<Participant>();

        public byte[]? Image { get; }

        public static (Event Event, string Error) Create(string title, string description, DateTime eventDateTime, string location, string category, int maxParticipants, byte[]? image)
        {
            var error = string.Empty;
            if (string.IsNullOrEmpty(title)) error = "Title is null";
            var _event = new Event(title, description, eventDateTime, location, category, maxParticipants, image);
            return (_event, error);
        }
    }
}
