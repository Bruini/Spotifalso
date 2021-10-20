using FluentValidation;
using Spotifalso.Aplication.Inputs;

namespace Spotifalso.Aplication.Validators
{
    public class UserValidator : AbstractValidator<UserInput>
    {
        public UserValidator()
        {
            RuleFor(x => x.Nickname)
                .NotEmpty()
                .WithMessage("Nickname is required");

            RuleFor(x => x.Nickname)
                .MaximumLength(100)
                .WithMessage("The maximum length of nickname is 100 characters");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password is required");

            RuleFor(x => x.Bio)
                .MaximumLength(500)
                .WithMessage("The maximum length of biography is 500 characters");

            RuleFor(x => x.Role)
                .NotNull()
                .WithMessage("Role is required");
        }
    }

}
