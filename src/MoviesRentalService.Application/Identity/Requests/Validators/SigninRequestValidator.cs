using FluentValidation;

namespace MoviesRentalService.Application.Identity.Requests.Validators
{
    public class SigninRequestValidator : AbstractValidator<SigninRequest>
    {
        public SigninRequestValidator()
        {
            RuleFor(e => e.Email).NotEmpty().WithMessage("Email is required.");
            RuleFor(e => e.Password).NotEmpty().WithMessage("Password is required.");
        }
    }
}
