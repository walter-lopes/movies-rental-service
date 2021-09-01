using Aloha.CQRS.Notifications;
using Aloha.CQRS.Queries;
using Aloha.Notifications;
using MoviesRentalService.Application.Catalog.Queries;
using MoviesRentalService.Application.Catalog.Responses;
using MoviesRentalService.Domain.Catalog.Repositories;
using System.Net;
using System.Threading.Tasks;

namespace MoviesRentalService.Application.Catalog.QueryHandlers
{
    public class GetMovieByIdQueryHandler : IQueryHandler<GetMovieByIdQuery, GetMovieByIdResponse>
    {
        private readonly IMovieRepository _repository;
        private readonly INotificationDispatcher _notificationDispatcher;

        public GetMovieByIdQueryHandler(IMovieRepository repository, INotificationDispatcher notificationDispatcher)
        {
            _repository = repository;
            _notificationDispatcher = notificationDispatcher;
        }

        public async Task<GetMovieByIdResponse> HandleAsync(GetMovieByIdQuery query)
        {
            var movie = await _repository.GetByIdAsync(query.Id);

            if (movie is null)
            {
                await _notificationDispatcher.PublishAsync(new DomainNotification(HttpStatusCode.NotFound, "Movie not found."));
                return new GetMovieByIdResponse();
            }

            return new GetMovieByIdResponse(movie);
        }
    }
}
