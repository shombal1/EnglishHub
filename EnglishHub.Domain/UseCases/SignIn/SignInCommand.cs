using EnglishHub.Domain.Authentication;
using MediatR;

namespace EnglishHub.Domain.UseCases.SignIn;

public record SignInCommand(string Login,string Password) :IRequest<(IIdentity identity,string token)>;