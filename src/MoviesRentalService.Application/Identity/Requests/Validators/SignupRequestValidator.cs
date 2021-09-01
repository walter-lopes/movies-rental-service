using FluentValidation;

namespace MoviesRentalService.Application.Identity.Requests.Validators
{
    public class SignupRequestValidator : AbstractValidator<SignupRequest>
    {
        public SignupRequestValidator()
        {
            RuleFor(e => e.Email).NotEmpty().WithMessage("Email is required.");
            RuleFor(e => e.Password).NotEmpty().WithMessage("Password is required.");
            RuleFor(e => e.Name).NotEmpty().WithMessage("Name is required.");
        }
    }
}
