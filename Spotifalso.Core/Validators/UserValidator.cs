using FluentValidation;
using Spotifalso.Core.Models;

namespace Spotifalso.Core.Validators
{
    public class UserValidator : AbstractValidator<User>
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
        }
    }

}
