namespace Events_Web_application.Application.Services.Exceptions
{
    internal interface IDBServiceException
    {
        public string Operation { get; set; }
        public object Value { get; set; }
    }
}
