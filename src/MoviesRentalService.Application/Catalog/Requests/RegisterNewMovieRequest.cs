using MoviesRentalService.Application.Catalog.Commands;
using MoviesRentalService.Application.Catalog.Requests.Validators;
using System.Collections.Generic;
using System.Linq;

namespace MoviesRentalService.Application.Catalog.Requests
{
    public class RegisterNewMovieRequest
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public RegisterNewMovieCommand ToCommand()
        {
            return new RegisterNewMovieCommand()
            {
                Name = Name,
                Description = Description,
                Price = Price
            };
        }

        public bool IsInvalid()
          => !new RegisterNewMovieRequestValidator()
                      .Validate(this)
                      .IsValid;

        public IEnumerable<string> GetErrors()
            => new RegisterNewMovieRequestValidator()
                        .Validate(this)
                        .Errors
                        .Select(x => x.ErrorMessage);
    }
}
