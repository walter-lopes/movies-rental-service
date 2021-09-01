using Aloha.CQRS.Queries;
using MoviesRentalService.Application.Rent.Queries;
using MoviesRentalService.Application.Rent.Responses;
using MoviesRentalService.Domain.Catalog;
using MoviesRentalService.Domain.Catalog.Repositories;
using MoviesRentalService.Domain.Rent;
using MoviesRentalService.Domain.Rent.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesRentalService.Application.Rent.QueryHandlers
{
    public class GetCartQueryHandler : IQueryHandler<GetCartQuery, GetCartResponse>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMovieRepository _movieRepository;

        public GetCartQueryHandler(ICartRepository cartRepository, IMovieRepository movieRepository)
        {
            _cartRepository = cartRepository;
            _movieRepository = movieRepository;
        }

        public async Task<GetCartResponse> HandleAsync(GetCartQuery query)
        {
            Cart cart = await _cartRepository.GetByUserIdAsync(query.UserId);

            IEnumerable<Guid> movieIds = cart.Items.Select(x => x.MovieId);

            IEnumerable<Movie> movies = await _movieRepository.GetByIdsAsync(movieIds);

            IDictionary<Guid, Movie> moviesMap = movies.ToDictionary(x => x.Id);

            return new GetCartResponse(cart, moviesMap);
        }
    }
}
