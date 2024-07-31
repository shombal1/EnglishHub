using EnglishHub.Api.Authentication;
using EnglishHub.Api.Models;
using EnglishHub.Domain.UseCases.SignIn;
using EnglishHub.Domain.UseCases.SignOn;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EnglishHub.Api.Controllers;

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