using Events_Web_application_DataBase.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Events_Web_application_DataBase.Services.DBServices.Repositories
{
    public class ImageRepository : IRepository<Image>
    {
        private readonly EWADBContext _context;

        public ImageRepository(EWADBContext context)
        {
            _context = context;
        }
        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public int Add(Image newimage)
        {
            if (_context == null || newimage == null) throw new ArgumentNullException("context");
            _context.Images.Add(newimage);
            return _context.SaveChanges();
        }

        public Image Get(int id)
        {
            if (id <= 0) throw new ArgumentOutOfRangeException("id");
            return _context.Images.Where(c => c.Id == id).First();
        }

        public IEnumerable<Image> GetAll()
        {
            return _context.Images.ToList();
        }

        public IEnumerable<Image> GetBySearch(string searchtext)
        {
            if (string.IsNullOrEmpty(searchtext)) throw new ArgumentNullException();
            return _context.Images.Where(c => c.Base64URL.Contains(searchtext)).ToList();
        }

        public int Update(Image newimage)
        {
            if (newimage == null) throw new ArgumentNullException("new event is null");
            _context.Images.Update(newimage);
            return _context.SaveChanges();
        }
    }
}
