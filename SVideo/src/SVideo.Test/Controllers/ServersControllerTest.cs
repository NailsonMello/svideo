using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SVideo.API.Controllers;
using SVideo.API.Models.Request;
using SVideo.Application.Interfaces;
using SVideo.Application.Models;
using SVideo.Application.Models.ViewModels;
using SVideo.Domain.Dtos;
using SVideo.Test.Settings;
using System;
using System.Threading.Tasks;

namespace SVideo.Test.Controllers
{
    [TestClass]
    public class ServersControllerTest
    {
        private IMapper _mapper;
        private Mock<IServerService> _mockServerService;
        private Mock<IVideoService> _mockVideoService;

        private ServersController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mapper = AutoMapperFactory.Create();
            _mockServerService = new Mock<IServerService>();
            _mockVideoService = new Mock<IVideoService>();

            _controller = new ServersController(_mapper, _mockServerService.Object, _mockVideoService.Object);
        }

        [TestMethod]
        public async Task GetById_MustReturnOk()
        {
            // Arrange
            _mockServerService.Setup(r => r.GetAsyncById(It.IsAny<Guid>()))
                .Returns(Task.FromResult(It.IsAny<ApplicationResponseItem<ServerViewModel>>()));

            // Act
            ActionResult result = await _controller.GetById(It.IsAny<Guid>());
            OkObjectResult okResult = result as OkObjectResult;

            // Assert
            _mockServerService.Verify(r => r.GetAsyncById(It.IsAny<Guid>()));
            Assert.IsInstanceOfType(result, typeof(ActionResult));
            Assert.IsInstanceOfType(okResult, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetAll_MustReturnOk()
        {
            // Arrange
            _mockServerService.Setup(r => r.GetAsyncAll())
                .Returns(Task.FromResult(It.IsAny<ApplicationResponseList<ServerViewModel>>()));

            // Act
            ActionResult result = await _controller.GetAll();
            OkObjectResult okResult = result as OkObjectResult;

            // Assert
            _mockServerService.Verify(r => r.GetAsyncAll());
            Assert.IsInstanceOfType(result, typeof(ActionResult));
            Assert.IsInstanceOfType(okResult, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task IsAvailable_MustReturnOk()
        {
            // Arrange
            _mockServerService.Setup(r => r.IsAvailable(It.IsAny<Guid>()))
                .Returns(Task.FromResult(It.IsAny<ApplicationResponseStruct<bool>>()));

            // Act
            ActionResult result = await _controller.IsAvailable(It.IsAny<Guid>());
            OkObjectResult okResult = result as OkObjectResult;

            // Assert
            _mockServerService.Verify(r => r.IsAvailable(It.IsAny<Guid>()));
            Assert.IsInstanceOfType(result, typeof(ActionResult));
            Assert.IsInstanceOfType(okResult, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task Create_MustReturnOk()
        {
            _mockServerService.Setup(r => r.CreateAsync(It.IsAny<ServerCreateDto>()))
                .Returns(Task.FromResult(It.IsAny<ApplicationResponseStruct<Guid>>()));

            var result = await _controller.Create(It.IsAny<ServerRequest>());

            _mockServerService.Verify(r => r.CreateAsync(It.IsAny<ServerCreateDto>()), Times.Once);
            Assert.IsInstanceOfType(result, typeof(ActionResult));
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task Update_MustReturnOk()
        {
            _mockServerService.Setup(r => r.UpdateAsync(It.IsAny<Guid>(), It.IsAny<ServerUpdateDto>()))
                .Returns(Task.FromResult(It.IsAny<ApplicationResponseStruct<Guid>>()));

            var result = await _controller.Update(It.IsAny<Guid>(), It.IsAny<ServerRequest>());

            _mockServerService.Verify(r => r.UpdateAsync(It.IsAny<Guid>(), It.IsAny<ServerUpdateDto>()), Times.Once);
            Assert.IsInstanceOfType(result, typeof(ActionResult));
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task Delete_MustReturnOk()
        {
            _mockServerService.Setup(r => r.DeleteAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult(It.IsAny<ApplicationResponseStruct<bool>>()));

            var result = await _controller.Delete(It.IsAny<Guid>());

            _mockServerService.Verify(r => r.DeleteAsync(It.IsAny<Guid>()), Times.Once);
            Assert.IsInstanceOfType(result, typeof(ActionResult));
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task CreateVideo_MustReturnOk()
        {
            _mockVideoService.Setup(r => r.CreateVideoAsync(It.IsAny<Guid>(), It.IsAny<VideoCreateDto>()))
                .Returns(Task.FromResult(It.IsAny<ApplicationResponseStruct<Guid>>()));

            var result = await _controller.CreateVideo(It.IsAny<Guid>(), It.IsAny<VideoRequest>());

            _mockVideoService.Verify(r => r.CreateVideoAsync(It.IsAny<Guid>(), It.IsAny<VideoCreateDto>()), Times.Once);
            Assert.IsInstanceOfType(result, typeof(ActionResult));
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task DeleteVideo_MustReturnOk()
        {
            // Arrange
            _mockVideoService.Setup(r => r.DeleteAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns(Task.FromResult(It.IsAny<ApplicationResponseStruct<bool>>()));

            // Act
            var result = await _controller.DeleteVideo(It.IsAny<Guid>(), It.IsAny<Guid>());

            // Assert
            _mockVideoService.Verify(r => r.DeleteAsync(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once);
            Assert.IsInstanceOfType(result, typeof(ActionResult));
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void GetAllVideosByServerId_MustReturnOk()
        {
            // Arrange
            _mockVideoService.Setup(r => r.GetAllByServerId(It.IsAny<Guid>()))
                .Returns(It.IsAny<ApplicationResponseList<VideoViewModel>>());

            // Act
            ActionResult result = _controller.GetAllVideosByServerId(It.IsAny<Guid>());
            OkObjectResult okResult = result as OkObjectResult;

            // Assert
            _mockVideoService.Verify(r => r.GetAllByServerId(It.IsAny<Guid>()));
            Assert.IsInstanceOfType(result, typeof(ActionResult));
            Assert.IsInstanceOfType(okResult, typeof(OkObjectResult));
        }

        [TestMethod]
        public void GetVideoByIdAndServerId_MustReturnOk()
        {
            // Arrange
            _mockVideoService.Setup(r => r.GetByIdAndServerId(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns(It.IsAny<ApplicationResponseItem<VideoViewModel>>());

            // Act
            ActionResult result = _controller.GetVideoByIdAndServerId(It.IsAny<Guid>(), It.IsAny<Guid>());
            OkObjectResult okResult = result as OkObjectResult;

            // Assert
            _mockVideoService.Verify(r => r.GetByIdAndServerId(It.IsAny<Guid>(), It.IsAny<Guid>()));
            Assert.IsInstanceOfType(result, typeof(ActionResult));
            Assert.IsInstanceOfType(okResult, typeof(OkObjectResult));
        }

        [TestMethod]
        public void Download_MustReturnOk()
        {
            _mockVideoService.Setup(r => r.Download(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns(It.IsAny<DownloadVideoViewModel>);

            var result = _controller.Download(It.IsAny<Guid>(), It.IsAny<Guid>());

            _mockVideoService.Verify(r => r.Download(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once);
            Assert.IsInstanceOfType(result, typeof(FileContentResult));
        }
    }
}
