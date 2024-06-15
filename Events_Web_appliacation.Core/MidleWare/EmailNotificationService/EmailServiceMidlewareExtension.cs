using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
