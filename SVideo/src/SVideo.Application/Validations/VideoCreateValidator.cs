using FluentValidation;
using Microsoft.Extensions.Localization;
using SVideo.Application.Resources;
using SVideo.Domain.Dtos;

namespace SVideo.Application.Validations
{
    public class VideoCreateValidator : AbstractValidator<VideoCreateDto>
    {
        public VideoCreateValidator(IStringLocalizer<Resource> localizer)
        {
            RuleFor(e => e.Description)
                .NotEmpty()
                .WithMessage(string.Format(localizer["RequiredField"], localizer["Description"]));

            RuleFor(e => e.SizeInBytes)
                .NotNull()
                .WithMessage(string.Format(localizer["RequiredField"], localizer["VideoFile"]));

        }
    }
}
