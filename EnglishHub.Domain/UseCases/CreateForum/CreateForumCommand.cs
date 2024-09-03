using EnglishHub.Domain.Models;
using MediatR;

namespace EnglishHub.Domain.UseCases.CreateForum;

public record CreateForumCommand(string Title) : IRequest<Forum>;