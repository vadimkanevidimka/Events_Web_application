using Events_Web_appliacation.Domain.Abstractions;
using Events_Web_application.Domain.Models;
using Events_Web_application.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;

namespace Events_Web_application.Infrastructure.Repositories
{
    public class ImageRepository : IRepository<Image>
    {
        private readonly EWADBContext _context;

        public ImageRepository(EWADBContext context)
        {
            _context = context;
        }
        public async Task<int> Delete(Guid id, CancellationTokenSource cancellationToken)
        {
            try
            {
                _context.Images.Remove(_context.Images.Where(c => c.Id == id).First());
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex) 
            {
                await cancellationToken.CancelAsync();
                return -1;
            }
        }

        public async Task<int> Add(Image newimage, CancellationTokenSource cancellationToken)
        {
            try
            {
                _context.Images.Add(newimage);
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex) 
            {
                await cancellationToken.CancelAsync();
                return -1;
            }
        }

        public async Task<Image> Get(Guid id, CancellationTokenSource cancellationToken)
        {
            try
            {
                return await _context.Images.Where(c => c.Id == id).FirstAsync();
            }
            catch (Exception ex) 
            {
                await cancellationToken.CancelAsync();
                return default(Image);
            }
        }


        public async Task<IEnumerable<Image>> GetAll(CancellationTokenSource cancellationToken)
        {
            try
            {
                return await _context.Images.ToListAsync();
            }
            catch (Exception ex)
            {
                await cancellationToken.CancelAsync();
                return Enumerable.Empty<Image>();
            }
        }

        public async Task<int> Update(Image newimage, CancellationTokenSource cancellationToken)
        {
            try
            {
                _context.Images.Update(newimage);
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex) 
            {
                await cancellationToken.CancelAsync();
                return -1;
            }
        }
    }
}