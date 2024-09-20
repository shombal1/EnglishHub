using EnglishHub.Forums.Domain.Models;
using MediatR;

namespace EnglishHub.Forums.Domain.UseCases.CreateForum;

public record CreateForumCommand(string Title) : IRequest<Forum>;