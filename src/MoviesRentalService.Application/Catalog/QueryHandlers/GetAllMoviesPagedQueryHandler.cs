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
    public class GetAllMoviesPagedQueryHandler : IQueryHandler<GetAllMoviesPagedQuery, GetAllMoviesPagedResponse>
    {
        private readonly IMovieRepository _repository;
        private readonly INotificationDispatcher _notificationDispatcher;

        public GetAllMoviesPagedQueryHandler(IMovieRepository repository, INotificationDispatcher notificationDispatcher)
        {
            _repository = repository;
            _notificationDispatcher = notificationDispatcher;
        }

        public  async Task<GetAllMoviesPagedResponse> HandleAsync(GetAllMoviesPagedQuery query)
        {
            var moviesPaged = await _repository.GetAllPagedAsync(query.Page, query.Items);

            if (moviesPaged.Items is null || !moviesPaged.Items.Any())
            {
                await _notificationDispatcher.PublishAsync(new DomainNotification(HttpStatusCode.NotFound, "Movies not found."));
                return new GetAllMoviesPagedResponse();
            }

            return new GetAllMoviesPagedResponse(moviesPaged);
        }
    }
}
