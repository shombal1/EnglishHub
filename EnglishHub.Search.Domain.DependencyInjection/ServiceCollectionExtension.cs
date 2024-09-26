using EnglishHub.Search.Domain.Models;
using EnglishHub.Search.Domain.UseCase.Index;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace EnglishHub.Search.Domain.DependencyInjection;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddSearchDomain(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<SearchEntity>();
        
        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssemblyContaining(typeof(SearchEntity));
        });
        
        return services;
    }
}