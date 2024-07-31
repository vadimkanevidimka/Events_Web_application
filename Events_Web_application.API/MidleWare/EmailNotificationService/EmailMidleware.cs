using static Events_Web_application.API.MidleWare.EmailNotificationService.EmailServiceBuilder;

namespace Events_Web_application.API.MidleWare.EmailNotificationService
{
    internal class EmailMidleware
    {
        private readonly RequestDelegate _next;
        private readonly EmailService _emailService;

        public EmailMidleware(RequestDelegate next, EmailServiceBuilder emailServiceBuilder)
        {
            this._next = next;
            _emailService = emailServiceBuilder.Build();
        }

        public async Task InvokeAsync(HttpContext context)
        {
            
            if (_emailService != null)
            {
                await _emailService.SendAsync();
            }
            else
            {
                await _next.Invoke(context);
            }
        }
    }
}
