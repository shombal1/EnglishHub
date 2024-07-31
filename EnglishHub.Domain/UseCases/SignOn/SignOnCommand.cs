using EnglishHub.Domain.Authentication;
using MediatR;

namespace EnglishHub.Domain.UseCases.SignOn;

public record SignOnCommand(string Login,string Password) : IRequest<IIdentity>;