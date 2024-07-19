using System.Net.Http.Json;
using EnglishHub.Domain.Authentication;
using EnglishHub.Models;
using FluentAssertions;

namespace EnglishHub.E2E;

public class AuthenticationEndPointShould : IClassFixture<EnglishHubApplicationFactory>
{
    private readonly EnglishHubApplicationFactory _factory;

    public AuthenticationEndPointShould(EnglishHubApplicationFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task SignInAfterSignOn()
    {
        var client = _factory.CreateClient();

        var signOnResponse = await client.PostAsync("Authentication/SignOn",
            JsonContent.Create(new SignOn() { Login = "admin", Password = "qwerty" }));
        User? createdUser = await signOnResponse.Content.ReadFromJsonAsync<User>();
        
        signOnResponse.IsSuccessStatusCode.Should().BeTrue();
        
        var signInResponse = await client.PostAsync("Authentication/SignIn",
            JsonContent.Create(new SignIn() { Login = "admin", Password = "qwerty" }));
        User? signInUser = await signInResponse.Content.ReadFromJsonAsync<User>();
        
        signInResponse.IsSuccessStatusCode.Should().BeTrue();
        
        signInResponse.Headers.Should().ContainKey("EnglishHub-AuthenticationKey");
        signInUser.Should().NotBeNull().And.BeEquivalentTo(createdUser);
    }
}