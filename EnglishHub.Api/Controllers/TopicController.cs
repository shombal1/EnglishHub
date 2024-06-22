using EnglishHub.Domain.UseCases.CreateTopicUseCase;
using EnglishHub.Domain.UseCases.GetTopicUseCase;
using EnglishHub.Models;
using Microsoft.AspNetCore.Mvc;

namespace EnglishHub.Controllers;

public class TopicController : ControllerBase
{
    private readonly ICreateTopicUseCase _createTopicUseCase;
    private readonly IGetTopicUseCase _getTopicUseCase;

    public TopicController(ICreateTopicUseCase createTopicUseCase,IGetTopicUseCase getTopicUseCase)
    {
        _createTopicUseCase = createTopicUseCase;
        _getTopicUseCase = getTopicUseCase;
    }

    [HttpPost]
    [Route("AddTopic")]
    public async Task<IActionResult> AddTopic(Guid forumId, [FromBody] CreateTopic body)
    {
        var command = new CreateTopicCommand(forumId, body.Title);
        Domain.Models.Topic topic = await _createTopicUseCase.Execute(command);

        return Ok(new Topic()
        {
            Id = topic.Id,
            PublicationAt = topic.PublicationAt,
            Title = topic.Title
        });
    }

    [HttpGet]
    [Route("GetTopics")]
    public async Task<IActionResult> GEtTopic(Guid forumId, int skip,int take)
    {
        var request = new GetTopicRequest(forumId, skip, take);
        IEnumerable<Domain.Models.Topic> topics = await _getTopicUseCase.Execute(request);

        return Ok(topics.Select(t => new Topic()
        {
            Id = t.Id,
            PublicationAt = t.PublicationAt,
            Title = t.Title
        }));
    }
}