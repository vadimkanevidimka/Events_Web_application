using Events_Web_application_DataBase.Services.DBServices.Mocks;
using Events_Web_application_DataBase.Services.DBServices.Repositories;

namespace Events_Web_application_DataBase.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EWADBContext _context;
        private EventsRepository _eventsRepository;
        private UsersRepository _usersRepository;
        private ParticipantsRepository _participantsRepository;
        private ImageRepository _imageRepository;
        public UnitOfWork(EWADBContext context)
        {
            _context = context;
        }

        public EventsRepository Events
        {
            get
            {
                if (_eventsRepository == null)
                    _eventsRepository = new EventsRepository(_context);
                return _eventsRepository;
            }
        }

        public UsersRepository Users
        {
            get
            {
                if (_usersRepository == null)
                    _usersRepository = new UsersRepository(_context);
                return _usersRepository;
            }
        }

        public ParticipantsRepository Participants
        {
            get
            {
                if (_participantsRepository == null)
                    _participantsRepository = new ParticipantsRepository(_context);
                return _participantsRepository;
            }
        }

        public ImageRepository Images
        {
            get
            {
                if (_imageRepository == null)
                    _imageRepository = new ImageRepository(_context);
                return _imageRepository;
            }
        }
    }
}
