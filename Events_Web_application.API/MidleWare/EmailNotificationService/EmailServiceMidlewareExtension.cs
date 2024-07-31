namespace Events_Web_application.API.MidleWare.EmailNotificationService
{
    public static class EmailServiceMidlewareExtension
    {
        public static void UseEmailServiceMidleware(this IApplicationBuilder app, EmailServiceBuilder emailServiceBuilder)
        {
            app.UseMiddleware<EmailMidleware>();
        }
    }
}
