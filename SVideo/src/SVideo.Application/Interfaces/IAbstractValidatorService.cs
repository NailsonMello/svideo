using FluentValidation;
using SVideo.Domain.Dtos;

namespace SVideo.Application.Interfaces
{
    public interface IAbstractValidatorService
    {
        IValidator<ServerCreateDto> ServerCreateValidator { get; }
        IValidator<ServerUpdateDto> ServerUpdateValidator { get; }
        IValidator<VideoCreateDto> VideoCreateValidator { get; }
    }
}
