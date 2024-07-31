using Events_Web_application.Application.Services.DBServices.DBServicesGenerics;
using Events_Web_application.Domain.Entities;
using Events_Web_application.Infrastructure.DBContext;

namespace Events_Web_application.Application.Services.DBServices
{
    public class ImagesService : DBServiceGeneric<Image>, IDBService<Image>
    {
        public ImagesService(EWADBContext context) : base(context) 
        {
            
        }
    }
}
