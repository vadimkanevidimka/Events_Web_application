namespace Events_Web_appliacation.Domain.Abstractions
{
    public interface IRepository<T> where T : class
    {
        public Task<IEnumerable<T>> GetAll();
        public Task<T> Get(Guid id);
        public Task<int> Add(T item);
        public  Task<int> Update(T item);
        public Task<int> Delete(Guid id);
    }
}
