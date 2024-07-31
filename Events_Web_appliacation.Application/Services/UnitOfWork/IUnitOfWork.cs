using Events_Web_application.Infrastructure.Repositories;

namespace Events_Web_application.Application.Services.UnitOfWork
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// Repositories
        /// </summary>
        public EventsRepository Events { get; }
        public ParticipantsRepository Participants { get; }
        public UsersRepository Users { get; }
        public ImagesRepository Images { get; }

        /// <summary>
        /// Transaction logic
        /// </summary>
        void CreateTransaction();
        void Commit();
        void Rollback();
        Task Save();
    }
}
