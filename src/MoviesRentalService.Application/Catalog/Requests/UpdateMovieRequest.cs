using MoviesRentalService.Application.Catalog.Commands;
using MoviesRentalService.Application.Catalog.Requests.Validators;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MoviesRentalService.Application.Catalog.Requests
{
    public class UpdateMovieRequest
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public UpdateMovieCommand ToCommand(Guid movieId)
        {
            return new UpdateMovieCommand()
            {
                Name = Name,
                Description = Description,
                Price = Price,
                Id = movieId
            };
        }

        public bool IsInvalid()
          => !new UpdateMovieRequestValidator()
                      .Validate(this)
                      .IsValid;

        public IEnumerable<string> GetErrors()
            => new UpdateMovieRequestValidator()
                        .Validate(this)
                        .Errors
                        .Select(x => x.ErrorMessage);
    }
}
