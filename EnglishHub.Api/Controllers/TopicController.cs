using AutoMapper;
using EnglishHub.Api.Models;
using EnglishHub.Domain.UseCases.CreateTopic;
using EnglishHub.Domain.UseCases.GetTopic;
using Microsoft.AspNetCore.Mvc;

namespace EnglishHub.Api.Controllers;

public class TopicController : ControllerBase
{
    [HttpPost]
    [Route("AddTopic")]
    public async Task<IActionResult> AddTopic(
        Guid forumId, 
        [FromBody] CreateTopic body,
        [FromServices] ICreateTopicUseCase createTopicUseCase,
        [FromServices] IMapper mapper,
        CancellationToken cancellationToken)
    {
        var command = new CreateTopicCommand(forumId, body.Title);
        Domain.Models.Topic topic = await createTopicUseCase.Execute(command,cancellationToken);

        return Ok(mapper.Map<Topic>(topic));
    }

    [HttpGet]
    [Route("GetTopics")]
    public async Task<IActionResult> GetTopic(
        Guid forumId,
        int skip,int take,
        [FromServices] IGetTopicUseCase getTopicUseCase,
        [FromServices] IMapper mapper,
        CancellationToken cancellationToken)
    {
        var request = new GetTopicRequest(forumId, skip, take);
        IEnumerable<Domain.Models.Topic> topics = await getTopicUseCase.Execute(request,cancellationToken);

        return Ok(topics.Select(mapper.Map<Topic>));
    }
}