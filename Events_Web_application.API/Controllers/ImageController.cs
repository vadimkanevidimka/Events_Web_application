﻿using Events_Web_application.Application.Services.UnitOfWork;
using Events_Web_application.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Events_Web_application.API.Controllers
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
        public async Task<Image> Get(Guid id) => await _unitOfWork.Images.Get(id, _cancellationTokenSource.Token);

        [HttpGet]
        public async Task<IEnumerable<Image>> GetAll() => await _unitOfWork.Images.GetAll(_cancellationTokenSource);

        [HttpPost]
        public async Task<int> AddImage(Guid eventId, Image newimage) => await _unitOfWork.Images.AddImage(eventId, newimage, _cancellationTokenSource.Token);

        [HttpDelete]
        public async Task<int> Delete(Guid id) =>
            await _unitOfWork.Images.Delete(id, _cancellationTokenSource);

        [HttpPatch]
        public async Task<int> Update(Image image) =>
            await _unitOfWork.Images.Update(image, _cancellationTokenSource);
    }
}
