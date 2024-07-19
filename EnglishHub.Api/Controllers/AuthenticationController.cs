using EnglishHub.Authentication;
using EnglishHub.Domain.UseCases.SignInUseCase;
using EnglishHub.Domain.UseCases.SignOnUseCase;
using EnglishHub.Models;
using Microsoft.AspNetCore.Mvc;

namespace EnglishHub.Controllers;

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