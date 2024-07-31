namespace Events_Web_application.Domain.Entities
{
    public class UsersEvents
    {
        public Guid Id { get; set; }
        public Guid ParticipantId { get; set; }
        public Guid EventId { get; set; }
        public bool ParticipantStatus { get; set; }
        public DateTime CheckInTime { get; set; }
    }
}
