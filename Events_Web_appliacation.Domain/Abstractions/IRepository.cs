using Events_Web_application.Domain.Entities;

namespace Events_Web_appliacation.Domain.Abstractions
{
    public interface IRepository<T> where T : class
    {
        public Task<IEnumerable<T>> GetAll(CancellationTokenSource cancellationToken);
        public Task<T> Get(Guid id, CancellationToken cancellationToken);
        public Task<int> Add(T item, CancellationTokenSource cancellationToken);
        public Task<int> Add(List<T> newitems, CancellationTokenSource cancellationToken);
        public  Task<int> Update(T item, CancellationTokenSource cancellationToken);
        public Task<int> Delete(Guid id, CancellationTokenSource cancellationToken);
        public Task<int> Delete(T item, CancellationTokenSource cancellationToken);

    }
}
