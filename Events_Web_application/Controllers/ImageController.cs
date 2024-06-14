using Events_Web_application_DataBase;
using Events_Web_application_DataBase.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Events_Web_application.Controllers
{
    public class ImageController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ImageController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("{id}")]
        public Image Get(int id) => _unitOfWork.Images.Get(id);

        [HttpGet]
        public List<Image> GetAll() => _unitOfWork.Images.GetAll().ToList();

        [HttpPost]
        public Image AddImage(int eventId, Image newimage)
        {
            try
            {
                var evnt = _unitOfWork.Events.Get(eventId);
                evnt.EventImage = newimage;
                _unitOfWork.Events.Update(evnt);
                return _unitOfWork.Images.GetAll().Last();
            }
            catch
            {
                return _unitOfWork.Images.GetAll().Last();
            }
        }

        [HttpDelete]
        public int Delete(int id)
        {
            return _unitOfWork.Images.Delete(id);
        }

        [HttpPatch]
        public int Update(Image image)
        {
            return _unitOfWork.Images.Update(image);
        }
    }
}
