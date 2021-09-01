using Aloha.CQRS.Notifications;
using Aloha.CQRS.Queries;
using Aloha.Notifications;
using MoviesRentalService.Application.Rent.Queries;
using MoviesRentalService.Application.Rent.Responses;
using MoviesRentalService.Domain.Catalog.Repositories;
using MoviesRentalService.Domain.Rent.Repositories;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MoviesRentalService.Application.Rent.QueryHandlers
{
    public class GetAvailableMoviesByUserIdQueryHandler : IQueryHandler<GetAvailableMoviesByUserIdQuery, GetAvailableMoviesByUserIdResponse>
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly INotificationDispatcher _notificationDispatcher;

        public GetAvailableMoviesByUserIdQueryHandler(IRentalRepository rentalRepository, IMovieRepository movieRepository, INotificationDispatcher notificationDispatcher)
        {
            _rentalRepository = rentalRepository;
            _movieRepository = movieRepository;
            _notificationDispatcher = notificationDispatcher;
        }

        public async Task<GetAvailableMoviesByUserIdResponse> HandleAsync(GetAvailableMoviesByUserIdQuery query)
        {
            var rentals = await _rentalRepository.GetAllAvailableByUserIdAsync(query.UserId);

            if (rentals is null || !rentals.Any())
            {
                await _notificationDispatcher.PublishAsync(new DomainNotification(HttpStatusCode.NotFound, "Rentals not found."));
                return new GetAvailableMoviesByUserIdResponse();
            }

            var movieIds = rentals.SelectMany(x => x.GetMovies());

            var movies = await _movieRepository.GetByIdsAsync(movieIds);

            return new GetAvailableMoviesByUserIdResponse(movies);
        }
    }
}
