using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace EnglishHub.E2E;

public class MapperConfigurationShould : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public MapperConfigurationShould(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }
    
    [Fact]
    public void BeValid()
    {
        var configuration = _factory.Services.GetRequiredService<IMapper>().ConfigurationProvider;
        configuration.Invoking(c => c.AssertConfigurationIsValid()).Should().NotThrow();
    }
}