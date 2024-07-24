using EnglishHub.Api.Authentication;
using EnglishHub.Api.Models;
using EnglishHub.Domain.UseCases.SignIn;
using EnglishHub.Domain.UseCases.SignOn;
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
        [FromServices] ISignOnUseCase signOnUseCase,
        CancellationToken cancellationToken)
    {
        return Ok(await signOnUseCase.Execute(new SignOnCommand(request.Login, request.Password), cancellationToken));
    }

    [HttpPost]
    [Route("SignIn")]
    public async Task<IActionResult> SignIn(
        [FromBody] SignIn request,
        [FromServices] ISignInUseCase signOnUseCase,
        [FromServices] IAuthenticationTokenStorage tokenStorage,
        CancellationToken cancellationToken)
    {
        var (identity, token) =
            await signOnUseCase.Execute(new SignInCommand(request.Login, request.Password), cancellationToken);

        tokenStorage.Store(HttpContext, token);

        return Ok(identity);
    }
}