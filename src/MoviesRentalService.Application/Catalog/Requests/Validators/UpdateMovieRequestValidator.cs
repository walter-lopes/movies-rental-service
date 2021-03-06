using FluentValidation;

namespace MoviesRentalService.Application.Catalog.Requests.Validators
{
    public class UpdateMovieRequestValidator : AbstractValidator<UpdateMovieRequest>
    {
        public UpdateMovieRequestValidator()
        {
            RuleFor(e => e.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(e => e.Description).NotEmpty().WithMessage("Description is required.");
            RuleFor(e => e.Price).GreaterThan(0).WithMessage("Price should be greater than 0.");
        }
    }
}
