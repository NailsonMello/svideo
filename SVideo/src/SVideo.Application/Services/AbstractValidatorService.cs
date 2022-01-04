using FluentValidation;
using SVideo.Application.Interfaces;
using SVideo.Domain.Dtos;

namespace SVideo.Application.Services
{
    public class AbstractValidatorService : IAbstractValidatorService
    {
        public AbstractValidatorService(
            IValidator<ServerCreateDto> serverCreateValidator,
            IValidator<ServerUpdateDto> serverUpdateValidator,
            IValidator<VideoCreateDto> videoCreateValidator)
        {
            ServerCreateValidator = serverCreateValidator;
            ServerUpdateValidator = serverUpdateValidator;
            VideoCreateValidator = videoCreateValidator;
        }

        public IValidator<ServerCreateDto> ServerCreateValidator { get; }

        public IValidator<ServerUpdateDto> ServerUpdateValidator { get; }
        public IValidator<VideoCreateDto> VideoCreateValidator { get; }
    }
}
