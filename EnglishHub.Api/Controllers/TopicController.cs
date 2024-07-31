using AutoMapper;
using EnglishHub.Api.Models;
using EnglishHub.Domain.UseCases.CreateTopic;
using EnglishHub.Domain.UseCases.GetTopic;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EnglishHub.Api.Controllers;

public class TopicController : ControllerBase
{
    [HttpPost]
    [Route("AddTopic")]
    public async Task<IActionResult> AddTopic(
        Guid forumId, 
        [FromBody] CreateTopic body,
        [FromServices] IMediator mediator,
        [FromServices] IMapper mapper,
        CancellationToken cancellationToken)
    {
        var command = new CreateTopicCommand(forumId, body.Title);
        Domain.Models.Topic topic = await mediator.Send(command,cancellationToken);

        return Ok(mapper.Map<Topic>(topic));
    }

    [HttpGet]
    [Route("GetTopics")]
    public async Task<IActionResult> GetTopic(
        Guid forumId,
        int skip,int take,
        [FromServices] IMediator mediator,
        [FromServices] IMapper mapper,
        CancellationToken cancellationToken)
    {
        var request = new GetTopicQuery(forumId, skip, take);
        IEnumerable<Domain.Models.Topic> topics = await mediator.Send(request,cancellationToken);

        return Ok(topics.Select(mapper.Map<Topic>));
    }
}