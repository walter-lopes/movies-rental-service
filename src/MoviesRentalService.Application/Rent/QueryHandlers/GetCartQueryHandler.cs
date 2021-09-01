using Aloha.CQRS.Notifications;
using Aloha.CQRS.Queries;
using Aloha.Notifications;
using MoviesRentalService.Application.Rent.Queries;
using MoviesRentalService.Application.Rent.Responses;
using MoviesRentalService.Domain.Catalog;
using MoviesRentalService.Domain.Catalog.Repositories;
using MoviesRentalService.Domain.Rent;
using MoviesRentalService.Domain.Rent.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MoviesRentalService.Application.Rent.QueryHandlers
{
    public class GetCartQueryHandler : IQueryHandler<GetCartQuery, GetCartResponse>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly INotificationDispatcher _notificationDispatcher;

        public GetCartQueryHandler(ICartRepository cartRepository, IMovieRepository movieRepository, INotificationDispatcher notificationDispatcher)
        {
            _cartRepository = cartRepository;
            _movieRepository = movieRepository;
            _notificationDispatcher = notificationDispatcher;
        }

        public async Task<GetCartResponse> HandleAsync(GetCartQuery query)
        {
            Cart cart = await _cartRepository.GetByUserIdAsync(query.UserId);

            if (cart is null)
            {
                await _notificationDispatcher.PublishAsync(new DomainNotification(HttpStatusCode.NotFound, "Cart not found."));
                return new GetCartResponse();
            }

            if (cart.IsEmpty())
            {
                await _notificationDispatcher.PublishAsync(new DomainNotification(HttpStatusCode.BadRequest, "Cart is empty."));
                return new GetCartResponse();
            }

            IEnumerable<Guid> movieIds = cart.Items.Select(x => x.MovieId);

            IEnumerable<Movie> movies = await _movieRepository.GetByIdsAsync(movieIds);

            IDictionary<Guid, Movie> moviesMap = movies.ToDictionary(x => x.Id);

            return new GetCartResponse(cart, moviesMap);
        }
    }
}
