namespace Events_Web_application.API.MidleWare.CancellationTokenMidleware
{
    public class TaskCancellationHandlingMiddleware(RequestDelegate next, ILogger<TaskCancellationHandlingMiddleware> logger)
    {
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception _) when (_ is OperationCanceledException or TaskCanceledException)
            {
                logger.LogInformation("Task canceled");
            }
        }
    }
}
