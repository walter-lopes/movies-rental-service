using Microsoft.AspNetCore.Mvc;
using Aloha.CQRS.Commands;
using Aloha.CQRS.Notifications;
using Aloha.CQRS.Queries;
using Aloha.WebApi;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using MoviesRentalService.Application.Identity.Commands;
using MoviesRentalService.Infra.Identity;
using MoviesRentalService.Application.Identity.Requests;

namespace MoviesRentalService.Api.Controllers
{
    [ApiController]
    [Route("v1/accounts")]
    public class AccountController : AlohaController
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        public AccountController(ICommandDispatcher commandDispatcher,
                                 IQueryDispatcher queryDispatcher,
                                 INotificationDispatcher notificationDispatcher)
            : base(notificationDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        [AllowAnonymous]
        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] SignupRequest request)
        {
            if (request.IsInvalid())
            {
                await NotifyBadRequestErrorsAsync(request.GetErrors());
                return Response();
            }

            SignupCommand command = request.ToCommand();

            await _commandDispatcher.SendAsync(command);

            return Response();
        }

        [AllowAnonymous]
        [HttpPost("signin")]
        public async Task<IActionResult> Signin([FromBody] SigninRequest request)
        {
            if (request.IsInvalid())
            {
                await NotifyBadRequestErrorsAsync(request.GetErrors());
                return Response();
            }

            SigninCommand command = request.ToCommand();

            var token = await _commandDispatcher.SendAsync<IdentityToken>(command);

            return Response(token);
        }
    }
}
