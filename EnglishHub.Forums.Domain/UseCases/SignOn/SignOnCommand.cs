using EnglishHub.Forums.Domain.Authentication;
using MediatR;

namespace EnglishHub.Forums.Domain.UseCases.SignOn;

public record SignOnCommand(string Login,string Password) : IRequest<IIdentity>;