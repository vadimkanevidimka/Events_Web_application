namespace Events_Web_appliacation.Domain.Abstractions
{
    public interface IRepository<T> where T : class
    {
        public Task<IEnumerable<T>> GetAll(CancellationTokenSource cancellationToken);
        public Task<T> Get(Guid id, CancellationTokenSource cancellationToken);
        public Task<int> Add(T item, CancellationTokenSource cancellationToken);
        public  Task<int> Update(T item, CancellationTokenSource cancellationToken);
        public Task<int> Delete(Guid id, CancellationTokenSource cancellationToken);
    }
}
