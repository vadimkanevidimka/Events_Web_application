using System.ComponentModel.DataAnnotations;

namespace Events_Web_application_DataBase
{
    public class Event
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public DateTime EventDateTime { get; set; }

        [Required]
        [MaxLength(300)]
        public string Location { get; set; } = string.Empty;
        [Required]
        public bool IsOpened { get; set; }
        [Required]
        [MaxLength(100)]
        public string Category { get; set; } = string.Empty;
        [Required]
        public int MaxParticipants { get; set; }
        [Required]
        public List<Participant> Participants { get; set; } = new List<Participant>();
        [Required]
        public Image EventImage { get; set; }
    }
}
