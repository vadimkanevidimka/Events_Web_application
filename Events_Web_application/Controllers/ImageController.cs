using Events_Web_application.Application.Services.UnitOfWork;
using Events_Web_application.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Events_Web_application.Controllers
{
    public class ImageController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly CancellationTokenSource _cancellationTokenSource;
        public ImageController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _cancellationTokenSource = new CancellationTokenSource();
        }

        [HttpGet("{id}")]
        public async Task<Image> Get(Guid id) => await _unitOfWork.ImagesService.GetImageById(id, _cancellationTokenSource);

        [HttpGet]
        public async Task<IEnumerable<Image>> GetAll() => await _unitOfWork.ImagesService.GetAllImages(_cancellationTokenSource);

        [HttpPost]
        public async Task<int> AddImage(Guid eventId, Image newimage) => await _unitOfWork.ImagesService.AddImage(eventId, newimage, _cancellationTokenSource);

        [HttpDelete]
        public async Task<int> Delete(Guid id) =>
            await _unitOfWork.ImagesService.DeleteImage(id, _cancellationTokenSource);

        [HttpPatch]
        public async Task<int> Update(Image image) => 
            await _unitOfWork.ImagesService.UpdateImage(image, _cancellationTokenSource);
    }
}
