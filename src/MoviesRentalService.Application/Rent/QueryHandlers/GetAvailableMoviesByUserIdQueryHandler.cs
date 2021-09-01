using Aloha.CQRS.Queries;
using MoviesRentalService.Application.Rent.Queries;
using MoviesRentalService.Application.Rent.Responses;
using MoviesRentalService.Domain.Catalog.Repositories;
using MoviesRentalService.Domain.Rent.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesRentalService.Application.Rent.QueryHandlers
{
    public class GetAvailableMoviesByUserIdQueryHandler : IQueryHandler<GetAvailableMoviesByUserIdQuery, GetAvailableMoviesByUserIdResponse>
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IMovieRepository _movieRepository;

        public GetAvailableMoviesByUserIdQueryHandler(IRentalRepository rentalRepository, IMovieRepository movieRepository)
        {
            _rentalRepository = rentalRepository;
            _movieRepository = movieRepository;
        }

        public async Task<GetAvailableMoviesByUserIdResponse> HandleAsync(GetAvailableMoviesByUserIdQuery query)
        {
            var rentals = await _rentalRepository.GetAllAvailableByUserIdAsync(query.UserId);

            var movieIds = rentals.SelectMany(x => x.GetMovies());

            var movies = await _movieRepository.GetByIdsAsync(movieIds);

            return new GetAvailableMoviesByUserIdResponse(movies);
        }
    }
}
