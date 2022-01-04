using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SVideo.Application.Interfaces;
using SVideo.Application.Services;
using SVideo.Domain.Entities;
using SVideo.Domain.Repositories;
using System.Collections.Generic;

namespace SVideo.Test.Services
{
    [TestClass]
    public class RecyclerServiceTest
    {
        private Mock<IVideoRepository> _videoRepository;
        private Mock<IRecyclerRepository> _recyclerRepository;

        private IRecyclerService _service;


        [TestInitialize]
        public void Setup()
        {
            _videoRepository = new Mock<IVideoRepository>();
            _recyclerRepository = new Mock<IRecyclerRepository>();

            _service = new RecyclerService(
                 _recyclerRepository.Object,
                 _videoRepository.Object
                );
        }

        [TestMethod]
        public void RemoveVideo_Success()
        {
            try
            {
                _videoRepository.Setup(x => x.ListVideoForDays(It.IsAny<int>()))
               .Returns(It.IsAny<IList<Video>>());
                _videoRepository.Setup(x => x.RemoveAllList(It.IsAny<IList<Video>>()));

                _service.RemoveVideo(It.IsAny<int>());

                _videoRepository.Verify(x => x.ListVideoForDays(It.IsAny<int>()), Times.Once);
                _videoRepository.Verify(x => x.RemoveAllList(It.IsAny<IList<Video>>()), Times.Once);
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.IsTrue(false);
            }
        }

        [TestMethod]
        public void StatusRunning_Success()
        {
            _recyclerRepository.Setup(x => x.StatusRunning())
               .Returns(It.IsAny<string>());

            var result = _service.StatusRunning();
            result ??= new string("");

            _recyclerRepository.Verify(x => x.StatusRunning(), Times.Once);
            Assert.IsInstanceOfType(result, typeof(string));
        }

        [TestMethod]
        public void UpdateRunning_Success()
        {
            try
            {
                _recyclerRepository.Setup(x => x.Get())
                    .Returns(It.IsAny<Recycler>());
                _recyclerRepository.Setup(x => x.Update(It.IsAny<Recycler>()));

                _service.UpdateRunning();

                _recyclerRepository.Verify(e => e.Get(), Times.Once);
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.IsTrue(false);
            }
        }
    }
}
