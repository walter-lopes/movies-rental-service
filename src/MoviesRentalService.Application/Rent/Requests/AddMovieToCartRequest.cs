using System;

namespace MoviesRentalService.Application.Rent.Requests
{
    public class AddMovieToCartRequest
    {
        public Guid MovieId { get; set; }
    }
}
