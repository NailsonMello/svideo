using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SVideo.API.Controllers;
using SVideo.Application.Interfaces;
using SVideo.Test.Settings;

namespace SVideo.Test.Controllers
{
    [TestClass]
    public class RecyclerControllerTest
    {
        private IMapper _mapper;
        private Mock<IRecyclerService> _mockService;

        private RecyclerController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mapper = AutoMapperFactory.Create();
            _mockService = new Mock<IRecyclerService>();

            _controller = new RecyclerController(_mapper, _mockService.Object);
        }

        [TestMethod]
        public void Status_MustReturnOk()
        {
            _mockService.Setup(r => r.StatusRunning())
                .Returns(It.IsAny<string>());

            var result = _controller.Status();

            _mockService.Verify(r => r.StatusRunning(), Times.Once);
            Assert.IsInstanceOfType(result, typeof(ActionResult));
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void Update_MustReturnNoContent()
        {
            // Arrange
            _mockService.Setup(r => r.UpdateRunning());

            // Act
            ActionResult result = _controller.Update();
            NoContentResult okResult = result as NoContentResult;

            // Assert
            _mockService.Verify(r => r.UpdateRunning());
            Assert.IsInstanceOfType(result, typeof(ActionResult));
            Assert.AreEqual(StatusCodes.Status204NoContent, okResult.StatusCode);
        }

        [TestMethod]
        public void Delete_MustReturnNoContent()
        {
            // Arrange
            _mockService.Setup(r => r.RemoveVideo(It.IsAny<int>()));

            // Act
            ActionResult result = _controller.Delete(It.IsAny<int>());
            NoContentResult okResult = result as NoContentResult;

            // Assert
            _mockService.Verify(r => r.RemoveVideo(It.IsAny<int>()));
            Assert.IsInstanceOfType(result, typeof(ActionResult));
            Assert.AreEqual(StatusCodes.Status204NoContent, okResult.StatusCode);
        }
    }
}
