using EnglishHub.Forums.Domain.UseCases.SignIn;
using EnglishHub.Forums.Domain.UseCases.SignOn;
using EnglishHub.Forums.Api.Authentication;
using EnglishHub.Forums.Api.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EnglishHub.Forums.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticationController : ControllerBase
{
    [HttpPost]
    [Route("SignOn")]
    public async Task<IActionResult> SignOn(
        [FromBody] SignOn request,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        return Ok(await mediator.Send(new SignOnCommand(request.Login, request.Password), cancellationToken));
    }

    [HttpPost]
    [Route("SignIn")]
    public async Task<IActionResult> SignIn(
        [FromBody] SignIn request,
        [FromServices] IMediator mediator,
        [FromServices] IAuthenticationTokenStorage tokenStorage,
        CancellationToken cancellationToken)
    {
        var (identity, token) =
            await mediator.Send(new SignInCommand(request.Login, request.Password), cancellationToken);

        tokenStorage.Store(HttpContext, token);

        return Ok(identity);
    }
}