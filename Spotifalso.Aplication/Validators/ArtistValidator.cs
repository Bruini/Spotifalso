using FluentValidation;
using Spotifalso.Aplication.Inputs;

namespace Spotifalso.Aplication.Validators
{
    public class ArtistValidator : AbstractValidator<ArtistInput>
    {
        public ArtistValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required");

            RuleFor(x => x.Name)
                .MaximumLength(100)
                .WithMessage("The maximum length of name is 100 characters");

            RuleFor(x => x.DisplayName)
                .NotEmpty()
                .WithMessage("DisplayName is required");

            RuleFor(x => x.DisplayName)
                .MaximumLength(100)
                .WithMessage("The maximum length of display name is 100 characters");

            RuleFor(x => x.Bio)
                .MaximumLength(500)
                .WithMessage("The maximum length of biography is 500 characters");
        }
    }
}
