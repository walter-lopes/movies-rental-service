using MoviesRentalService.Application.Identity.Commands;
using MoviesRentalService.Application.Identity.Requests.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
namespace MoviesRentalService.Application.Identity.Requests
{
    public class SignupRequest
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }
        public SignupCommand ToCommand()
        {
            return new SignupCommand()
            {
                Email = Email,
                Password = Password,
                Name = Name
            };
        }

        public bool IsInvalid()
          => !new SignupRequestValidator()
                      .Validate(this)
                      .IsValid;

        public IEnumerable<string> GetErrors()
            => new SignupRequestValidator()
                        .Validate(this)
                        .Errors
                        .Select(x => x.ErrorMessage);
    }
}
