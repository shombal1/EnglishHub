using EnglishHub.Domain.Authentication;
using EnglishHub.Domain.Authorization;
using EnglishHub.Domain.Models;
using EnglishHub.Domain.UseCases.CreateForumUseCase;
using EnglishHub.Domain.UseCases.CreateTopicUseCase;
using EnglishHub.Domain.UseCases.GetForumUseCase;
using EnglishHub.Domain.UseCases.GetTopicUseCase;
using EnglishHub.Domain.UseCases.SignInUseCase;
using EnglishHub.Domain.UseCases.SignOnUseCase;
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