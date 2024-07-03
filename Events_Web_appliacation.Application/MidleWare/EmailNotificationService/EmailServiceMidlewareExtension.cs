using Microsoft.AspNetCore.Builder;

namespace Events_Web_appliacation.Core.MidleWare.EmailNotificationService
{
    public static class EmailServiceMidlewareExtension
    {
        public static void UseEmailServiceMidleware(this IApplicationBuilder app, EmailServiceBuilder emailServiceBuilder)
        {
            app.UseMiddleware<EmailMidleware>();
        }
    }
}
