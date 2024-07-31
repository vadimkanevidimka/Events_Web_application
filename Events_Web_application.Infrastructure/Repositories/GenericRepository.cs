using Events_Web_appliacation.Domain.Abstractions;
using Events_Web_application.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;

namespace Events_Web_application.Infrastructure.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        protected readonly EWADBContext _context;

        public GenericRepository(EWADBContext context) 
        {
            _context = context;
        }

        public async Task<int> Add(T item, CancellationTokenSource cancellationToken)
        {
            await _context.AddAsync<T>(item, cancellationToken.Token);
            return await _context.SaveChangesAsync(cancellationToken.Token);
        }

        public async Task<int> Add(List<T> newitems, CancellationTokenSource cancellationToken)
        {
            await _context.AddRangeAsync(newitems, cancellationToken.Token);
            return await _context.SaveChangesAsync(cancellationToken.Token);
        }

        public async Task<int> Delete(Guid id, CancellationTokenSource cancellationToken)
        {
            _context.Remove<T>(await _context.FindAsync<T>(id));
            return await _context.SaveChangesAsync(cancellationToken.Token);
        }

        public async Task<int> Delete(T item, CancellationTokenSource cancellationToken)
        {
            _context.Remove<T>(item);
            return await _context.SaveChangesAsync();
        }
        
        public async Task<T> Get(Guid id, CancellationToken cancellationToken)
        {
            return await _context.FindAsync<T>(id, cancellationToken);
        }

        public async Task<IEnumerable<T>> GetAll(CancellationTokenSource cancellationToken)
        {
            return await Task.Run(() => _context.Set<T>().AsEnumerable());
        }

        public async Task<int> Update(T item, CancellationTokenSource cancellationToken)
        {
            _context.Update<T>(item);
            return await _context.SaveChangesAsync(cancellationToken.Token);
        }
    }
}
