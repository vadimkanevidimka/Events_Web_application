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
        public async Task<int> AddImage(Guid eventId, Image image, CancellationTokenSource cancellationToken)
        {
            try
            {
                var evnt = await _eventRepository.Get(eventId, cancellationToken);
                evnt.EventImage = image;
                return await _eventRepository.Update(evnt, cancellationToken);
            }
            catch (TaskCanceledException) 
            {
                cancellationToken.Token.ThrowIfCancellationRequested();
                throw;
            }
        }
        public async Task<int> DeleteImage(Guid id, CancellationTokenSource cancellationToken) =>
            await _imageRepository.Delete(id, cancellationToken);
        public async Task<int> UpdateImage(Image image, CancellationTokenSource cancellationToken) =>
            await _imageRepository.Update(image, cancellationToken);
        public async Task<IEnumerable<Image>> GetAllImages(CancellationTokenSource cancellationToken) =>
            await _imageRepository.GetAll(cancellationToken);
        public async Task<Image> GetImageById(Guid id, CancellationTokenSource cancellationToken) =>
            await _imageRepository.Get(id, cancellationToken);
    }
}
