using MoviesRentalService.Application.Identity.Commands;
using MoviesRentalService.Application.Identity.Requests.Validators;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MoviesRentalService.Application.Identity.Requests
{
    public class SigninRequest
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public SigninCommand ToCommand()
        {
            return new SigninCommand()
            {
                Email = Email,
                Password = Password
            };
        }

        public bool IsInvalid()
          => !new SigninRequestValidator()
                      .Validate(this)
                      .IsValid;

        public IEnumerable<string> GetErrors()
            => new SigninRequestValidator()
                        .Validate(this)
                        .Errors
                        .Select(x => x.ErrorMessage);
    }
}
