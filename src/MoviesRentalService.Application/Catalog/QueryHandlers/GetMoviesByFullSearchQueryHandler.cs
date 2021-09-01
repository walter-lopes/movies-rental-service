using Aloha.CQRS.Notifications;
using Aloha.CQRS.Queries;
using Aloha.Notifications;
using MoviesRentalService.Application.Catalog.Queries;
using MoviesRentalService.Application.Catalog.Responses;
using MoviesRentalService.Domain.Catalog.Repositories;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MoviesRentalService.Application.Catalog.QueryHandlers
{
    public class GetMoviesByFullSearchQueryHandler : IQueryHandler<GetMoviesByFullSearchQuery, GetMoviesByFullSearchResponse>
    {
        private readonly IMovieRepository _repository;
        private readonly INotificationDispatcher _notificationDispatcher;

        public GetMoviesByFullSearchQueryHandler(IMovieRepository repository, INotificationDispatcher notificationDispatcher)
        {
            _repository = repository;
            _notificationDispatcher = notificationDispatcher;
        }

        public async Task<GetMoviesByFullSearchResponse> HandleAsync(GetMoviesByFullSearchQuery query)
        {
            var movies = await _repository.FullSearchAsync(query.FullSearch);

            if (movies is null || !movies.Any())
            {
                await _notificationDispatcher.PublishAsync(new DomainNotification(HttpStatusCode.NotFound, "Movies not found."));
                return new GetMoviesByFullSearchResponse();
            }

            return new GetMoviesByFullSearchResponse(movies);
        }
    }
}
