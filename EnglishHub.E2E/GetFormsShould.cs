using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

namespace EnglishHub.E2E;

public class GetFormsShould(EnglishHubApplicationFactory factory) : IClassFixture<EnglishHubApplicationFactory>
{
    [Fact]
    public async Task GetForms()
    {
        using var client = factory.CreateClient();

        var response = await client.GetAsync("Forum/All");

        response.Invoking(r => r.EnsureSuccessStatusCode()).Should().NotThrow();
        
        var result = await response.Content.ReadAsStringAsync();

        result.Should().BeEquivalentTo("[]");
    }
}