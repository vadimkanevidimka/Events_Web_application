using Events_Web_application.Application.Services.DBServices;
using Events_Web_application.Infrastructure.Repositories;

namespace Events_Web_application.Application.Services.UnitOfWork
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// Services for repositories
        /// </summary>
        public EventsService EventsService { get; }
        public ParticipantsService ParticipantsService { get; }
        public UsersService UsersService { get; }
        public ImagesService ImagesService { get; }
    }
}
