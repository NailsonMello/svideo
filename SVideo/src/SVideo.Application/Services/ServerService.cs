using AutoMapper;
using Microsoft.Extensions.Localization;
using SVideo.Application.Exceptions;
using SVideo.Application.Interfaces;
using SVideo.Application.Models;
using SVideo.Application.Models.ViewModels;
using SVideo.Application.Resources;
using SVideo.Application.Validators;
using SVideo.Domain.Dtos;
using SVideo.Domain.Entities;
using SVideo.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SVideo.Application.Services
{
    public class ServerService : IServerService
    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<Resource> _localizer;
        private readonly IAbstractValidatorService _validatorService;
        private readonly IUnitOfWork _unitOfWork;

        private readonly IServerRepository _repository;

        public ServerService(
            IMapper mapper,
            IStringLocalizer<Resource> localizer,
            IServerRepository repository,
            IAbstractValidatorService validatorService,
            IUnitOfWork unitOfWork
            )
        {
            _mapper = mapper;
            _localizer = localizer;
            _repository = repository;
            _validatorService = validatorService;
            _unitOfWork = unitOfWork;
        }

        public async Task<ApplicationResponseStruct<Guid>> CreateAsync(ServerCreateDto dto)
        {
            try
            {
                if (dto == null)
                    throw new BusinessException(string.Format(_localizer["Required"], _localizer["Server"]));

                _validatorService.ServerCreateValidator.Validate(dto).CheckErrors();
                _unitOfWork.BeginTransactionSip();

                var server = _mapper.Map<Server>(dto);

                await _repository.InsertAsync(server);

                _unitOfWork.CommitSip();

                return new ApplicationResponseStruct<Guid>(true, _localizer["ServerCreated"], server.Id);
            }
            catch
            {
                _unitOfWork.RollbackSip();
                throw;
            }
        }

        public async Task<ApplicationResponseStruct<bool>> DeleteAsync(Guid id)
        {
            Server server = await _repository.Find(id);
            bool result = await _repository.Delete(server);
            return new ApplicationResponseStruct<bool>(true, _localizer["DeletedServer"], result);
        }

        public async Task<ApplicationResponseList<ServerViewModel>> GetAsyncAll()
        {
            var servers = await _repository.FindAll();
            var result = _mapper.Map<IList<ServerViewModel>>(servers);
            return new ApplicationResponseList<ServerViewModel>(true, _localizer["Servers"], result);
        }

        public async Task<ApplicationResponseItem<ServerViewModel>> GetAsyncById(Guid id)
        {
            var servers = await _repository.Find(id);
            var result = _mapper.Map<ServerViewModel>(servers);
            result ??= new ServerViewModel();
            return new ApplicationResponseItem<ServerViewModel>(true, _localizer["Server"], result);
        }

        public async Task<ApplicationResponseStruct<bool>> IsAvailable(Guid id)
        {
            bool isAvailable = await _repository.ExistsById(id);
            return new ApplicationResponseStruct<bool>(true,
               isAvailable ? _localizer["IsAvailable"] : _localizer["IsNotAvailable"], isAvailable);
        }

        public async Task<ApplicationResponseStruct<Guid>> UpdateAsync(Guid serverId, ServerUpdateDto dto)
        {
            try
            {
                if (dto == null)
                    throw new BusinessException(string.Format(_localizer["Required"], _localizer["Server"]));

                dto.Id = serverId;

                _validatorService.ServerUpdateValidator.Validate(dto).CheckErrors();
                _unitOfWork.BeginTransactionSip();

                var server = _mapper.Map<Server>(dto);

                await _repository.Update(server);

                _unitOfWork.CommitSip();

                return new ApplicationResponseStruct<Guid>(true, _localizer["ServerChanged"], server.Id);
            }
            catch
            {
                _unitOfWork.RollbackSip();
                throw;
            }
        }
    }
}
