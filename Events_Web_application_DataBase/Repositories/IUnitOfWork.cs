using Events_Web_application_DataBase.Services.DBServices.Mocks;
using Events_Web_application_DataBase.Services.DBServices.Repositories;

namespace Events_Web_application_DataBase.Repositories
{
    public interface IUnitOfWork
    {
        public EventsRepository Events { get; }
        public UsersRepository Users { get; }
        public ParticipantsRepository Participants { get; }
        public ImageRepository Images { get; }
    }
}
