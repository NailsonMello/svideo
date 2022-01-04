using AutoMapper;
using FluentValidation.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SVideo.Application.Exceptions;
using SVideo.Application.Interfaces;
using SVideo.Application.Models.ViewModels;
using SVideo.Application.Resources;
using SVideo.Application.Services;
using SVideo.Domain.Dtos;
using SVideo.Domain.Entities;
using SVideo.Domain.Repositories;
using SVideo.Test.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SVideo.Test.Services
{
    [TestClass]
    public class VideoServiceTest
    {
        private IMapper _mapper;
        private Resource _resource;
        private Mock<IAbstractValidatorService> _validator;
        private Mock<IUnitOfWork> _unitOfWork;

        private Mock<IVideoRepository> _repository;

        private IVideoService _service;


        [TestInitialize]
        public void Setup()
        {
            _resource = ResourceFactory.Create();
            _mapper = AutoMapperFactory.Create();
            _unitOfWork = new Mock<IUnitOfWork>();
            _validator = new Mock<IAbstractValidatorService>();

            _repository = new Mock<IVideoRepository>();

            _service = new VideoService(
                _mapper,
                ResourceFactory.GetStringLocalizer(),
                 _repository.Object,
                 _validator.Object,
                 _unitOfWork.Object
                );
        }

        [TestMethod]
        public async Task CreateVideoAsync_Throw_Required()
        {
            _validator.Setup(x => x.VideoCreateValidator.Validate(It.IsAny<VideoCreateDto>()))
                .Returns(new ValidationResult());

            var task = _service.CreateVideoAsync(It.IsAny<Guid>(), null);
            var ex = await Assert.ThrowsExceptionAsync<BusinessException>(() => task);

            _unitOfWork.Verify(e => e.RollbackSip(), Times.Once);
            Assert.AreEqual(ex.Message, string.Format(_resource.GetMessage("Required"), _resource.GetMessage("Video")));
        }

        [TestMethod]
        public async Task CreateVideoAsync_Throw_ValidationFailure()
        {
            var failure = "test";
            _validator.Setup(x => x.VideoCreateValidator.Validate(It.IsAny<VideoCreateDto>()))
                .Returns(new ValidationResult(new List<ValidationFailure>() {
                    new ValidationFailure("test", failure)
                }));

            var task = _service.CreateVideoAsync(It.IsAny<Guid>(), new VideoCreateDto());
            var ex = await Assert.ThrowsExceptionAsync<ValidationException>(() => task);

            _unitOfWork.Verify(e => e.RollbackSip(), Times.Once);
            Assert.IsTrue(ex.Failures.All(e => e.Equals(failure)));
        }

        [TestMethod]
        public async Task CreateVideoAsync_Success()
        {
            _validator.Setup(x => x.VideoCreateValidator.Validate(It.IsAny<VideoCreateDto>()))
                .Returns(new ValidationResult());
            _repository.Setup(x => x.InsertAsync(It.IsAny<Video>()))
                .Returns(Task.FromResult(It.IsAny<bool>()));

            var result = await _service.CreateVideoAsync(It.IsAny<Guid>(), new VideoCreateDto());

            _unitOfWork.Verify(e => e.RollbackSip(), Times.Never);
            _repository.Verify(e => e.InsertAsync(It.IsAny<Video>()), Times.Once);
            Assert.AreEqual(result.Message, _resource.GetMessage("VideoCreated"));
        }

        [TestMethod]
        public async Task DeleteAsync_Success()
        {
            _repository.Setup(x => x.GetVideoByIdAndServerId(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns(It.IsAny<Video>());
            _repository.Setup(x => x.Delete(It.IsAny<Video>()))
                .Returns(Task.FromResult(It.IsAny<bool>()));

            var result = await _service.DeleteAsync(It.IsAny<Guid>(), It.IsAny<Guid>());

            _repository.Verify(x => x.GetVideoByIdAndServerId(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once);
            _repository.Verify(x => x.Delete(It.IsAny<Video>()), Times.Once);
            Assert.IsInstanceOfType(result.Data, typeof(bool));
        }

        [TestMethod]
        public void Download_Success()
        {
            _repository.Setup(x => x.GetVideoByIdAndServerId(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns(It.IsAny<Video>());

            var result = _service.Download(It.IsAny<Guid>(), It.IsAny<Guid>());

            _repository.Verify(x => x.GetVideoByIdAndServerId(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once);
            Assert.IsInstanceOfType(result, typeof(DownloadVideoViewModel));
        }

        [TestMethod]
        public void GetAllByServerId_Success()
        {
            _repository.Setup(x => x.GetAllByServerId(It.IsAny<Guid>()))
                .Returns(new List<Video>());

            var result = _service.GetAllByServerId(It.IsAny<Guid>());

            _repository.Verify(x => x.GetAllByServerId(It.IsAny<Guid>()), Times.Once);
            Assert.IsInstanceOfType(result.Data, typeof(IEnumerable<VideoViewModel>));
        }

        [TestMethod]
        public void GetByIdAndServerId_Success()
        {
            _repository.Setup(x => x.GetVideoByIdAndServerId(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns(It.IsAny<Video>);

            var result = _service.GetByIdAndServerId(It.IsAny<Guid>(), It.IsAny<Guid>());

            _repository.Verify(x => x.GetVideoByIdAndServerId(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once);
            Assert.IsInstanceOfType(result.Data, typeof(VideoViewModel));
        }
    }
}
