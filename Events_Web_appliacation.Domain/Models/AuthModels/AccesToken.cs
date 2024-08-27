namespace Events_Web_application.Domain.Models.AuthModels
{
    public class AccesToken
    {
        public Guid Id { get; set; }
        public string Token { get; set; }
        public DateTime ExpirationJWTDateTime { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpirationRTDateTime { get; set; }
    }
}
