using EnglishHub.Forums.Domain.Authentication;
using MediatR;

namespace EnglishHub.Forums.Domain.UseCases.SignIn;

public record SignInCommand(string Login,string Password) :IRequest<(IIdentity identity,string token)>;