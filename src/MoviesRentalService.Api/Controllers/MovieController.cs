using Aloha.CQRS.Commands;
using Aloha.CQRS.Notifications;
using Aloha.CQRS.Queries;
using Aloha.WebApi;
using Microsoft.AspNetCore.Mvc;
using MoviesRentalService.Api.Attributes;
using MoviesRentalService.Application.Catalog.Commands;
using MoviesRentalService.Application.Catalog.Queries;
using MoviesRentalService.Application.Catalog.Requests;
using System;
using System.Threading.Tasks;

namespace MoviesRentalService.Api.Controllers
{
    [ApiController]
    [Route("v1/movies")]
    public class MovieController : AlohaController
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        public MovieController(ICommandDispatcher commandDispatcher,
                                IQueryDispatcher queryDispatcher,
                               INotificationDispatcher notificationDispatcher)
            : base(notificationDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        [HttpPost]
        [AuthorizeRoles("admin")]
        public async Task<IActionResult> Post([FromBody] RegisterNewMovieRequest request)
        {
            if (request.IsInvalid())
            {
                await NotifyBadRequestErrorsAsync(request.GetErrors());
                return Response();
            }

            RegisterNewMovieCommand command = request.ToCommand();

            await _commandDispatcher.SendAsync(command);

            return Response();
        }

        [HttpGet("full-search/{params}/{page}/{items}")]
        public async Task<IActionResult> FullSearch(string @params, int page = 0, int items = 1)
        {
            GetMoviesByFullSearchQuery query = new(@params, page, items);

            var response = await _queryDispatcher.QueryAsync(query);

            return Response(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            GetMovieByIdQuery query = new(id);

            var response = await _queryDispatcher.QueryAsync(query);

            return Response(response);
        }
    }
}
