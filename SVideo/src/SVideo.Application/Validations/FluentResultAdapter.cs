using FluentValidation.Results;
using SVideo.Application.Exceptions;
using System.Linq;

namespace SVideo.Application.Validators
{
    public static class FluentResultAdapter
    {
        public static void CheckErrors(this ValidationResult result)
        {
            if (!(result?.IsValid ?? true))
            {
                var failures = result.Errors.Where(f => f != null).ToList();
                if (failures.Count > 0)
                {
                    var errors = failures.Select(e => e.ErrorMessage).Distinct().ToList();
                    throw new ValidationException(errors);
                }
            }
        }
    }
}