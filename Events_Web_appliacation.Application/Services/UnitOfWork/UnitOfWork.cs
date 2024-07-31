using Events_Web_application.Application.Services.DBServices;
using Events_Web_application.Infrastructure.DBContext;
using Events_Web_application.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Events_Web_application.Application.Services.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        /// <summary>
        /// Global Data Base Context
        /// </summary>
        private readonly EWADBContext _context;
        private IDbContextTransaction? _objTran = null;

        /// <summary>
        /// Services for Repositories
        /// </summary>
        private EventsRepository _events;
        private ParticipantsRepository _participants;
        private UsersRepository _users;
        private ImagesRepository _images;

        /// <summary>
        /// State
        /// </summary>
        private bool disposed = false;
        public UnitOfWork(EWADBContext context)
        {
            _context = context;
        }

        public EventsRepository Events
        {
            get
            {
                if (_events == null)
                    _events = new EventsRepository(_context);
                return _events;
            }
        }

        public ParticipantsRepository Participants
        {
            get
            {
                if (_participants == null)
                    _participants = new ParticipantsRepository(_context);
                return _participants;
            }
        }

        public UsersRepository Users
        {
            get
            {
                if (_users == null)
                    _users = new UsersRepository(_context);
                return _users;
            }
        }

        public ImagesRepository Images
        {
            get
            {
                if (_images == null)
                    _images = new ImagesRepository(_context);
                return _images;
            }
        }

        public void Commit()
        {
            _objTran?.Commit();
        }

        public void CreateTransaction()
        {
            _objTran = _context.Database.BeginTransaction();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Rollback()
        {
            _objTran?.Rollback();
            _objTran?.Dispose();
        }

        public async Task Save()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
