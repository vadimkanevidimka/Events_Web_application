using System.ComponentModel.DataAnnotations;

namespace Events_Web_appliacation.Core.Models
{
    public class User
    {
        private User(string email, string password, int role, int participantId, Participant participant)
        {
            Email = email;
            Password = password;
            Role = role;
            ParticipantId = participantId;
            Participant = participant;
        }

        public int Id { get; }

        [Required]
        [MaxLength(100)]
        public string Email { get; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Password { get; } = string.Empty;
        [Required]
        public int Role { get; }
        [Required]
        public int ParticipantId { get; }
        [Required]
        public Participant Participant { get; }

        public static (User Event, string Error) Create(string email, string password, int role, int participantId, Participant participant)
        {
            var error = string.Empty;
            if (string.IsNullOrEmpty(email)) error = "Email is null";
            var user = new User(email, password, role, participantId, participant);
            return (user, error);
        }
    }
}
