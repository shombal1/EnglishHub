using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

namespace EnglishHub.E2E;

public class GetFormsShould : IClassFixture<EnglishHubApplicationFactory>
{
    private readonly EnglishHubApplicationFactory _factory;

    public GetFormsShould(EnglishHubApplicationFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task GetForms()
    {
        using var client = _factory.CreateClient();

        var response = await client.GetAsync("Forum/All");

        response.Invoking(r => r.EnsureSuccessStatusCode()).Should().NotThrow();
        
        var result = await response.Content.ReadAsStringAsync();

        result.Should().BeEquivalentTo("[]");
    }
}