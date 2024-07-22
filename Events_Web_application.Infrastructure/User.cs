using System.ComponentModel.DataAnnotations;

namespace Events_Web_application_DataBase
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Password { get; set; } = string.Empty;
        [Required]
        public int Role { get; set; }
        [Required]
        public int ParticipantId { get; set; }
        [Required]
        public Participant Participant { get; set; }
    }
}
