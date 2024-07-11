using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Events_Web_application.Application.MidleWare.CancellationTokenMidleware
{
    public class TaskcanceledExceptionFilter(ILoggerFactory loggerFactory) : ExceptionFilterAttribute
    {
        private readonly ILogger _logger = loggerFactory.CreateLogger<TaskcanceledExceptionFilter>();

        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is OperationCanceledException or TaskCanceledException)
            {
                _logger.LogInformation("Request was canceled");
                context.ExceptionHandled = true;
                context.Result = new StatusCodeResult(400);
            }
        }
    }
}
