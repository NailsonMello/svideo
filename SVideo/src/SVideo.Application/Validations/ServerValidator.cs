using FluentValidation;
using Microsoft.Extensions.Localization;
using SVideo.Application.Resources;
using SVideo.Domain.Dtos;

namespace SVideo.Application.Validations
{
    public class ServerValidator : AbstractValidator<ServerCreateDto>
    {
        public ServerValidator(IStringLocalizer<Resource> localizer)
        {
            RuleFor(e => e.Name)
                .NotEmpty()
                .WithMessage(string.Format(localizer["RequiredField"], localizer["Name"]));

            RuleFor(e => e.IpAddress)
                .NotEmpty()
                .WithMessage(string.Format(localizer["RequiredField"], localizer["IpAddress"]));

            RuleFor(e => e.Port)
                .GreaterThan(0)
                .WithMessage(string.Format(localizer["InvalidField"], localizer["Port"]));
        }
    }
}
