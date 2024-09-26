using EnglishHub.Search.Domain;
using EnglishHub.Search.Domain.UseCase.Index;
using EnglishHub.Search.Domain.UseCase.SearchEntity;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EnglishHub.Search.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class SearchController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    [Route("Index")]
    public async Task<IActionResult> Index(Guid entityId,SearchEntityType searchEntityType,string? title,string? text,CancellationToken cancellationToken)
    {
        await mediator.Send(new IndexCommand(entityId, searchEntityType, title, text), cancellationToken);
        
        return Ok();
    }
    
    [HttpGet]
    [Route("Search")]
    public async Task<IActionResult> Index(string query,CancellationToken cancellationToken)
    {
        var (responses, totalCount) = await mediator.Send(new SearchEntityQuery(query), cancellationToken);
        return Ok( new {Responses = responses.ToArray(),TotalCount = totalCount});
    }
}