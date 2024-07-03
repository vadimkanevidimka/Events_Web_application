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
        public async Task<int> Delete(Guid id)
        {
            _context.Images.Remove(_context.Images.Where(c => c.Id == id).First());
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Add(Image newimage)
        {
            _context.Images.Add(newimage);
            return await _context.SaveChangesAsync();
        }

        public async Task<Image> Get(Guid id) => 
            await _context.Images.Where(c => c.Id == id).FirstAsync();


        public async Task<IEnumerable<Image>> GetAll() => 
            await _context.Images.ToListAsync();

        public async Task<int> Update(Image newimage)
        {
            _context.Images.Update(newimage);
            return await _context.SaveChangesAsync();
        }
    }
}