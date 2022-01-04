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
    public class VideoService : IVideoService
    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<Resource> _localizer;
        private readonly IAbstractValidatorService _validatorService;
        private readonly IUnitOfWork _unitOfWork;

        private readonly IVideoRepository _repository;

        public VideoService(
            IMapper mapper,
            IStringLocalizer<Resource> localizer,
            IVideoRepository repository,
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

        public async Task<ApplicationResponseStruct<Guid>> CreateVideoAsync(Guid idServer, VideoCreateDto dto)
        {
            try
            {
                if (dto == null)
                    throw new BusinessException(string.Format(_localizer["Required"], _localizer["Video"]));

                _validatorService.VideoCreateValidator.Validate(dto).CheckErrors();


                _unitOfWork.BeginTransactionSip();

                var video = _mapper.Map<Video>(dto);
                video.IdServer = idServer;
                video.CreatedAt = DateTime.UtcNow;
       
                await _repository.InsertAsync(video);

                _unitOfWork.CommitSip();

                return new ApplicationResponseStruct<Guid>(true, _localizer["VideoCreated"], video.Id);
            }
            catch
            {
                _unitOfWork.RollbackSip();
                throw;
            }
        }

        public async Task<ApplicationResponseStruct<bool>> DeleteAsync(Guid serverId, Guid videoId)
        {
            Video video = _repository.GetVideoByIdAndServerId(serverId, videoId);
            bool result = await _repository.Delete(video);
            return new ApplicationResponseStruct<bool>(true, _localizer["DeletedVideo"], result);
        }

        public DownloadVideoViewModel Download(Guid serverId, Guid videoId)
        {
            Video video = _repository.GetVideoByIdAndServerId(serverId, videoId);
            var result =  _mapper.Map<DownloadVideoViewModel>(video);
            result ??= new DownloadVideoViewModel();
            return result;
        }

        public ApplicationResponseList<VideoViewModel> GetAllByServerId(Guid idServer)
        {
            List<Video> videos = _repository.GetAllByServerId(idServer);
            var result = _mapper.Map<IList<VideoViewModel>>(videos);
            return new ApplicationResponseList<VideoViewModel>(true, _localizer["Video"], result);
        }

        public ApplicationResponseItem<VideoViewModel> GetByIdAndServerId(Guid serverId, Guid videoId)
        {
            Video video = _repository.GetVideoByIdAndServerId(serverId, videoId);
            var result = _mapper.Map<VideoViewModel>(video);
            result ??= new VideoViewModel();
            return new ApplicationResponseItem<VideoViewModel>(true, _localizer["Video"], result);
        }
    }
}
