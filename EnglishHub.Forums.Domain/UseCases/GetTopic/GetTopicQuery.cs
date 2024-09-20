using EnglishHub.Forums.Domain.Models;
using MediatR;

namespace EnglishHub.Forums.Domain.UseCases.GetTopic;

public record class GetTopicQuery(Guid ForumId,int Skip,int Take): IRequest<IEnumerable<Topic>>;