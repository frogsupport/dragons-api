using Dragons.Api.Models;
using FluentValidation;

namespace Dragons.Api.Validation
{
    public class DragonValidator : AbstractValidator<Dragon>
    {
        public DragonValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Type).NotEmpty();
            RuleFor(x => x.Size).NotEmpty();
        }
    }
}
