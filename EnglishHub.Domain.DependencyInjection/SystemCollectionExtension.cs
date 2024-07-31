using EnglishHub.Domain.Authentication;
using EnglishHub.Domain.Authorization;
using EnglishHub.Domain.Models;
using EnglishHub.Domain.monitoring;
using EnglishHub.Domain.UseCases.CreateForum;
using EnglishHub.Domain.UseCases.CreateTopic;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace EnglishHub.Domain.DependencyInjection;


public static class SystemCollectionExtension
{
    public static IServiceCollection AddForumDomain(this IServiceCollection services)
    {
        services
            .AddScoped<IAuthenticationService,AuthenticationService>()
            .AddScoped<ISymmetricEncryptor,TripleDesSymmetricEncryptorDecryptor>()
            .AddScoped<ISymmetricDecryptor,TripleDesSymmetricEncryptorDecryptor>()
            .AddScoped<IPasswordManager,PasswordManager>();
        
        services
            .AddScoped<IIntentionResolve, TopicIntentionResolve>()
            .AddScoped<IIntentionResolve,ForumIntentionResolve>()
            .AddScoped<IIdentityProvider, IdentityProvider>()
            .AddScoped<IIntentionManager, IntentionManager>();

        services.AddValidatorsFromAssemblyContaining<Forum>();

        services.AddSingleton<DomainMetrics>();
        
        services.AddMediatR(cfg => {
            cfg.AddOpenBehavior(typeof(MonitoringPipelineBehavior<,>))
                .RegisterServicesFromAssembly(typeof(Forum).Assembly);
        });
        
        return services;
    }
}