using System.Net;
using System.Net.Http.Json;
using EnglishHub.Forums.Api.Models;
using EnglishHub.Forums.Domain.Authentication;
using FluentAssertions;

namespace EnglishHub.Forums.E2E;

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

        signInUser.Should().NotBeNull();
        signInUser.UserId.Should().Be(createdUser!.UserId);

        var createForumResponse = await client.PostAsync("Forum/AddForum", JsonContent.Create(new CreateForum() { Title = "wdwdwd" }));
        createForumResponse.IsSuccessStatusCode.Should().BeTrue();
    }
}