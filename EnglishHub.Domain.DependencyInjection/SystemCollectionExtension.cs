using EnglishHub.Domain.Authentication;
using EnglishHub.Domain.Authorization;
using EnglishHub.Domain.Models;
using EnglishHub.Domain.UseCases.CreateTopicUseCase;
using EnglishHub.Domain.UseCases.GetForumUseCase;
using EnglishHub.Domain.UseCases.GetTopicUseCase;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace EnglishHub.Domain.DependencyInjection;


public static class SystemCollectionExtension
{
    public static IServiceCollection AddForumDomain(this IServiceCollection service)
    {
        service
            .AddScoped<IForumUseCase, ForumUseCase>()
            .AddScoped<ICreateTopicUseCase, CreateTopicUseCase>()
            .AddScoped<IGetTopicUseCase, GetTopicUseCase>();
        
        service
            .AddScoped<IIntentionResolve, TopicIntentionResolve>()
            .AddScoped<IIdentityProvider, IdentityProvider>()
            .AddScoped<IIntentionManager, IntentionManager>();

        service
            .AddValidatorsFromAssemblyContaining<Forum>();
        
        return service;
    }
}