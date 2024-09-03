using AutoMapper;
using EnglishHub.Api;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace EnglishHub.E2E;

public class MapperConfigurationShould(WebApplicationFactory<Program> factory)
    : IClassFixture<WebApplicationFactory<Program>>
{
    [Fact]
    public void BeValid()
    {
        var configuration = factory.Services.GetRequiredService<IMapper>().ConfigurationProvider;
        configuration.Invoking(c => c.AssertConfigurationIsValid()).Should().NotThrow();
    }
}