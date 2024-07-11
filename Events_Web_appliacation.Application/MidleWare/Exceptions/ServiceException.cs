using Events_Web_application.Domain.Models;

namespace Events_Web_application.Application.MidleWare.Exceptions
{
    internal class ServiceException : Exception, IDBServiceException
    {
        public string Operation { get; set; }
        public object Value { get; set; }
        public ServiceException(string Operation, object Value) : base() 
        {

        }
    }
}
