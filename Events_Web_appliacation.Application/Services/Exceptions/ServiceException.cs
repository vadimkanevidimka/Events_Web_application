namespace Events_Web_application.Application.Services.Exceptions
{
    public class ServiceException : Exception, IDBServiceException
    {
        public string Operation { get; set; }
        public object Value { get; set; }
        public ServiceException(string Operation, object Value, string Message) : base(Message) 
        {

        }
    }
}
