using Events_Web_appliacation.Domain.Abstractions;
using Events_Web_application.Domain.Models;
using Events_Web_application.Infrastructure.Repositories;
using Microsoft.Extensions.Logging;

namespace Events_Web_application.Application.Services.DBServices
{
    public class ImagesService
    {
        private IRepository<Image> _imageRepository { get; set; }
        private IRepository<Event> _eventRepository { get; set; }
        

        public ImagesService(ImageRepository imageRepository, EventsRepository eventsRepository)
        {
            _imageRepository = imageRepository;
            _eventRepository = eventsRepository;
        }
        public async Task<int> AddImage(Guid eventId, Image image)
        {
            var evnt = await _eventRepository.Get(eventId);
            evnt.EventImage = image;
            return await _eventRepository.Update(evnt);
        }
        public async Task<int> DeleteImage(Guid id) =>
            await _imageRepository.Delete(id);
        public async Task<int> UpdateImage(Image image) =>
            await _imageRepository.Update(image);
        public async Task<IEnumerable<Image>> GetAllImages() =>
            await _imageRepository.GetAll();
        public async Task<Image> GetImageById(Guid id) =>
            await _imageRepository.Get(id);
    }
}
