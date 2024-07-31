using AutoMapper;
using EnglishHub.Api.Models;
using EnglishHub.Domain.UseCases.CreateForum;
using EnglishHub.Domain.UseCases.GetForum;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EnglishHub.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ForumController : ControllerBase
{
    [HttpGet]
    [Route("All")]
    public async Task<IActionResult> Get(
        [FromServices] IMediator mediator,
        [FromServices] IMapper mapper,
        CancellationToken cancellationToken)
    {
        return Ok((await mediator.Send(new GetForumQuery(),cancellationToken)).Select(mapper.Map<Forum>));
    }

    [HttpPost]
    [Route("AddForum")]
    public async Task<IActionResult> CreateForum(
        [FromBody] CreateForum createForum,
        [FromServices] IMediator mediator,
        [FromServices] IMapper mapper,
        CancellationToken cancellationToken)
    {
        var command = new CreateForumCommand(createForum.Title);

        return Ok(mapper.Map<Forum>(await mediator.Send(command,cancellationToken)));
    }
}