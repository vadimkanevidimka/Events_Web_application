using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using System.Net.Mail;
using Events_Web_application_DataBase;
using static Events_Web_appliacation.Core.MidleWare.EmailNotificationService.EmailServiceBuilder;

namespace Events_Web_appliacation.Core.MidleWare.EmailNotificationService
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
