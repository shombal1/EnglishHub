using EnglishHub.Domain.Authentication;
using EnglishHub.Domain.Authorization;
using EnglishHub.Domain.Models;
using EnglishHub.Domain.UseCases.CreateForum;
using EnglishHub.Domain.UseCases.CreateTopic;
using EnglishHub.Domain.UseCases.GetForum;
using EnglishHub.Domain.UseCases.GetTopic;
using EnglishHub.Domain.UseCases.SignIn;
using EnglishHub.Domain.UseCases.SignOn;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace EnglishHub.Domain.DependencyInjection;


public static class SystemCollectionExtension
{
    public static IServiceCollection AddForumDomain(this IServiceCollection service)
    {
        service
            .AddScoped<IGetForumUseCase, GetForumUseCase>()
            .AddScoped<ICreateTopicUseCase, CreateTopicUseCase>()
            .AddScoped<IGetTopicUseCase, GetTopicUseCase>()
            .AddScoped<ICreateForumUseCase,CreateForumUseCase>()
            .AddScoped<ISignInUseCase,SignInUseCase>()
            .AddScoped<ISignOnUseCase,SignOnUseCase>()
            .AddScoped<IAuthenticationService,AuthenticationService>()
            .AddScoped<ISymmetricEncryptor,TripleDesSymmetricEncryptorDecryptor>()
            .AddScoped<ISymmetricDecryptor,TripleDesSymmetricEncryptorDecryptor>()
            .AddScoped<IPasswordManager,PasswordManager>();
        
        service
            .AddScoped<IIntentionResolve, TopicIntentionResolve>()
            .AddScoped<IIntentionResolve,ForumIntentionResolve>()
            .AddScoped<IIdentityProvider, IdentityProvider>()
            .AddScoped<IIntentionManager, IntentionManager>();

        service
            .AddValidatorsFromAssemblyContaining<Forum>();
        
        return service;
    }
}