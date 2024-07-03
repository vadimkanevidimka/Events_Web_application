using Events_Web_application.Application.Services.DBServices;
using Events_Web_application.Infrastructure.DBContext;
using Events_Web_application.Infrastructure.Repositories;

namespace Events_Web_application.Application.Services.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// Global Data Base Context
        /// </summary>
        private readonly EWADBContext _context;

        /// <summary>
        /// Services for Repositories
        /// </summary>
        private EventsService _eventsService;
        private ParticipantsService _participantsService;
        private UsersService _usersService;
        private ImagesService _imagesService;
        public UnitOfWork(EWADBContext context)
        {
            _context = context;
        }

        public EventsService EventsService
        {
            get
            {
                if (_eventsService == null)
                    _eventsService = new EventsService(new EventsRepository(_context), new UsersRepository(_context));
                return _eventsService;
            }
        }

        public ParticipantsService ParticipantsService
        {
            get
            {
                if (_participantsService == null)
                    _participantsService = new ParticipantsService(new ParticipantsRepository(_context));
                return _participantsService;
            }
        }

        public UsersService UsersService
        {
            get
            {
                if (_usersService == null)
                    _usersService = new UsersService(new UsersRepository(_context));
                return _usersService;
            }
        }

        public ImagesService ImagesService
        {
            get
            {
                if (_imagesService == null)
                    _imagesService = new ImagesService(new ImageRepository(_context), new EventsRepository(_context));
                return _imagesService;
            }
        }
    }
}
