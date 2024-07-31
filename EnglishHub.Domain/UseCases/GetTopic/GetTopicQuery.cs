using EnglishHub.Domain.Models;
using MediatR;

namespace EnglishHub.Domain.UseCases.GetTopic;

public record class GetTopicQuery(Guid ForumId,int Skip,int Take): IRequest<IEnumerable<Topic>>;