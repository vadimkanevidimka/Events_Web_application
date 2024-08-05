using Events_Web_application.Application.Services.DBServices.DBServicesGenerics;
using Events_Web_application.Application.Services.Validation;
using Events_Web_application.Domain.Entities;
using Events_Web_application.Infrastructure.DBContext;
using Microsoft.AspNetCore.Mvc;

namespace Events_Web_application.Application.Services.DBServices
{
    public class UsersService : DBServiceGeneric<User>, IDBService<User>
    {
        public UsersService(EWADBContext context) : base(context)
        {
            base._validator = new UserValidator();
        }
    }
}
