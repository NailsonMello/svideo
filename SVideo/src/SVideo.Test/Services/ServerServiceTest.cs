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
    public class ServerServiceTest
    {
        private IMapper _mapper;
        private Resource _resource;
        private Mock<IAbstractValidatorService> _validator;
        private Mock<IUnitOfWork> _unitOfWork;

        private Mock<IServerRepository> _repository;

        private IServerService _service;


        [TestInitialize]
        public void Setup()
        {
            _resource = ResourceFactory.Create();
            _mapper = AutoMapperFactory.Create();
            _unitOfWork = new Mock<IUnitOfWork>();
            _validator = new Mock<IAbstractValidatorService>();

            _repository = new Mock<IServerRepository>();

            _service = new ServerService(
                _mapper,
                ResourceFactory.GetStringLocalizer(),
                 _repository.Object,
                 _validator.Object,
                 _unitOfWork.Object
                );
        }

        [TestMethod]
        public async Task CreateAsync_Throw_Required()
        {
            _validator.Setup(x => x.ServerCreateValidator.Validate(It.IsAny<ServerCreateDto>()))
                .Returns(new ValidationResult());

            var task = _service.CreateAsync(null);
            var ex = await Assert.ThrowsExceptionAsync<BusinessException>(() => task);

            _unitOfWork.Verify(e => e.RollbackSip(), Times.Once);
            Assert.AreEqual(ex.Message, string.Format(_resource.GetMessage("Required"), _resource.GetMessage("Video")));
        }

        [TestMethod]
        public async Task CreateAsync_Throw_ValidationFailure()
        {
            var failure = "test";
            _validator.Setup(x => x.ServerCreateValidator.Validate(It.IsAny<ServerCreateDto>()))
                .Returns(new ValidationResult(new List<ValidationFailure>() {
                    new ValidationFailure("test", failure)
                }));

            var task = _service.CreateAsync(new ServerCreateDto());
            var ex = await Assert.ThrowsExceptionAsync<ValidationException>(() => task);

            _unitOfWork.Verify(e => e.RollbackSip(), Times.Once);
            Assert.IsTrue(ex.Failures.All(e => e.Equals(failure)));
        }

        [TestMethod]
        public async Task CreateAsync_Success()
        {
            _validator.Setup(x => x.ServerCreateValidator.Validate(It.IsAny<ServerCreateDto>()))
                .Returns(new ValidationResult());
            _repository.Setup(x => x.InsertAsync(It.IsAny<Server>()))
                .Returns(Task.FromResult(It.IsAny<bool>()));

            var result = await _service.CreateAsync(new ServerCreateDto());

            _unitOfWork.Verify(e => e.RollbackSip(), Times.Never);
            _repository.Verify(e => e.InsertAsync(It.IsAny<Server>()), Times.Once);
            Assert.AreEqual(result.Message, _resource.GetMessage("ServerCreated"));
        }

        [TestMethod]
        public async Task DeleteAsync_Success()
        {
            _repository.Setup(x => x.Find(It.IsAny<Guid>()))
                .Returns(Task.FromResult(It.IsAny<Server>()));
            _repository.Setup(x => x.Delete(It.IsAny<Server>()))
                .Returns(Task.FromResult(It.IsAny<bool>()));

            var result = await _service.DeleteAsync(It.IsAny<Guid>());

            _repository.Verify(x => x.Find(It.IsAny<Guid>()), Times.Once);
            _repository.Verify(x => x.Delete(It.IsAny<Server>()), Times.Once);
            Assert.IsInstanceOfType(result.Data, typeof(bool));
        }

        [TestMethod]
        public async Task GetAsyncAll_Success()
        {
            _repository.Setup(x => x.FindAll())
                .Returns(Task.FromResult(It.IsAny<IList<Server>>()));

            var result = await _service.GetAsyncAll();

            _repository.Verify(x => x.FindAll(), Times.Once);
            Assert.IsInstanceOfType(result.Data, typeof(IEnumerable<ServerViewModel>));
        }

        [TestMethod]
        public async Task GetAsyncById_Success()
        {
            _repository.Setup(x => x.Find(It.IsAny<Guid>()))
               .Returns(Task.FromResult(It.IsAny<Server>()));

            var result = await _service.GetAsyncById(It.IsAny<Guid>());

            _repository.Verify(x => x.Find(It.IsAny<Guid>()), Times.Once);
            Assert.IsInstanceOfType(result.Data, typeof(ServerViewModel));
        }

        [TestMethod]
        public async Task IsAvailable_Success()
        {
            _repository.Setup(x => x.ExistsById(It.IsAny<Guid>()))
               .Returns(Task.FromResult(It.IsAny<bool>()));

            var result = await _service.IsAvailable(It.IsAny<Guid>());

            _repository.Verify(x => x.ExistsById(It.IsAny<Guid>()), Times.Once);
            Assert.IsInstanceOfType(result.Data, typeof(bool));
        }

        [TestMethod]
        public async Task UpdateAsync_Throw_Required()
        {
            _validator.Setup(x => x.ServerUpdateValidator.Validate(It.IsAny<ServerUpdateDto>()))
                .Returns(new ValidationResult());

            var task = _service.UpdateAsync(It.IsAny<Guid>(), null);
            var ex = await Assert.ThrowsExceptionAsync<BusinessException>(() => task);

            _unitOfWork.Verify(e => e.RollbackSip(), Times.Once);
            Assert.AreEqual(ex.Message, string.Format(_resource.GetMessage("Required"), _resource.GetMessage("Video")));
        }

        [TestMethod]
        public async Task UpdateAsync_Throw_ValidationFailure()
        {
            var failure = "test";
            _validator.Setup(x => x.ServerUpdateValidator.Validate(It.IsAny<ServerUpdateDto>()))
                .Returns(new ValidationResult(new List<ValidationFailure>() {
                    new ValidationFailure("test", failure)
                }));

            var task = _service.UpdateAsync(It.IsAny<Guid>(), new ServerUpdateDto());
            var ex = await Assert.ThrowsExceptionAsync<ValidationException>(() => task);

            _unitOfWork.Verify(e => e.RollbackSip(), Times.Once);
            Assert.IsTrue(ex.Failures.All(e => e.Equals(failure)));
        }

        [TestMethod]
        public async Task UpdateAsync_Success()
        {
            _validator.Setup(x => x.ServerUpdateValidator.Validate(It.IsAny<ServerUpdateDto>()))
                .Returns(new ValidationResult());
            _repository.Setup(x => x.Update(It.IsAny<Server>()))
                .Returns(Task.FromResult(It.IsAny<bool>()));

            var result = await _service.UpdateAsync(It.IsAny<Guid>(), new ServerUpdateDto());

            _unitOfWork.Verify(e => e.RollbackSip(), Times.Never);
            _repository.Verify(e => e.Update(It.IsAny<Server>()), Times.Once);
            Assert.AreEqual(result.Message, _resource.GetMessage("ServerChanged"));
        }

    }
}
