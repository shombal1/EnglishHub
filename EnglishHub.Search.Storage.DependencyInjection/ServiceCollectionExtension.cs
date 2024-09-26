using EnglishHub.Search.Domain.UseCase.Index;
using EnglishHub.Search.Domain.UseCase.SearchEntity;
using EnglishHub.Search.Storage.Models;
using EnglishHub.Search.Storage.Storage;
using Microsoft.Extensions.DependencyInjection;
using OpenSearch.Client;

namespace EnglishHub.Search.Storage.DependencyInjection;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddSearchStorage(this IServiceCollection services,string connectionString)
    {
        services.AddScoped<IIndexStorage, IndexStorage>();
        services.AddScoped<ISearchEntityStorage, SearchEntityStorage>();

        services.AddSingleton<IOpenSearchClient>(new OpenSearchClient(new Uri(connectionString))
        {
            ConnectionSettings =
            {
                DefaultIndices = { [typeof(SearchEntity)] = "english-hub-search-v1" }
            }
        });
        
        return services;
    }
}