using FluentValidation;
using Spotifalso.Aplication.Inputs;

namespace Spotifalso.Aplication.Validators
{
    public class MusicValidator : AbstractValidator<MusicInput>
    {
        public MusicValidator()
        {
            RuleFor(x => x.Title)
              .NotEmpty()
              .WithMessage("Title is required");

            RuleFor(x => x.Title)
                .MaximumLength(100)
                .WithMessage("The maximum length of title is 100 characters");

            RuleFor(x => x.Duration)
                .NotNull()
                .WithMessage("Duration is required");

            RuleFor(x => x.ReleaseDate)
                .NotNull()
                .WithMessage("Release date is required");
        }
    }
}
