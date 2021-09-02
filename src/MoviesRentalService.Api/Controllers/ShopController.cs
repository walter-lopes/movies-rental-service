using Aloha.CQRS.Commands;
using Aloha.CQRS.Notifications;
using Aloha.CQRS.Queries;
using Aloha.WebApi;
using Microsoft.AspNetCore.Mvc;
using MoviesRentalService.Api.Attributes;
using MoviesRentalService.Application.Rent.Command;
using MoviesRentalService.Application.Rent.Queries;
using MoviesRentalService.Application.Rent.Requests;
using MoviesRentalService.Infra.Identity;
using System;
using System.Threading.Tasks;

namespace MoviesRentalService.Api.Controllers
{
    [ApiController]
    [Route("v1/shop")]
    public class ShopController : AlohaController
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        public ShopController(ICommandDispatcher commandDispatcher,
                                IQueryDispatcher queryDispatcher,
                               INotificationDispatcher notificationDispatcher)
            : base(notificationDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        [HttpPost("rent")]
        [AuthorizeRoles("customer")]
        public async Task<IActionResult> Rent()
        {
            Guid userId = User.Identity.GetUserId();

            RentMovieCommand command = new(userId);

            await _commandDispatcher.SendAsync(command);

            return Response();
        }

        [HttpPost("cart/clean")]
        [AuthorizeRoles("customer")]
        public async Task<IActionResult> CleanCart()
        {
            Guid userId = User.Identity.GetUserId();

            CleanCartCommand command = new(userId);

            await _commandDispatcher.SendAsync(command);

            return Response();
        }

        [HttpPost("cart")]
        [AuthorizeRoles("customer")]
        public async Task<IActionResult> AddMovieToCart(AddMovieToCartRequest request)
        {
            Guid userId = User.Identity.GetUserId();
            AddMovieToCartCommand command = new(request.MovieId, userId);

            await _commandDispatcher.SendAsync(command);

            return Response();
        }

        [HttpGet("cart")]
        [AuthorizeRoles("customer")]
        public async Task<IActionResult> GetCart()
        {
            Guid userId = User.Identity.GetUserId();

            GetCartQuery query = new(userId);

            var response = await _queryDispatcher.QueryAsync(query);

            return Response(response);
        }

        [HttpGet("available-movies")]
        [AuthorizeRoles("customer")]
        public async Task<IActionResult> GetAvailableMovies()
        {
            Guid userId = User.Identity.GetUserId();

            GetAvailableMoviesByUserIdQuery query = new(userId);

            var response = await _queryDispatcher.QueryAsync(query);

            return Response(response);
        }
    }
}
