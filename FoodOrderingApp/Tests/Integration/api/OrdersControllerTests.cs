using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using App.Contracts.BLL;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using Public.DTO.v1;
using Public.DTO.v1.Identity;
using Tests.Helpers;
using Xunit.Abstractions;

namespace Tests.Integration.api;

public class OrdersControllerTests(CustomWebAppFactory<Program> factory, ITestOutputHelper testOutputHelper) 
    : IClassFixture<CustomWebAppFactory<Program>>
{
    private HttpClient GetClient() => factory.CreateClient(new WebApplicationFactoryClientOptions
    {
        AllowAutoRedirect = false
    });
    
    private readonly JsonSerializerOptions _camelCaseJsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    
    private async Task<JwtResponse?> RegisterNewUser(
        string email = "email@example.ee", 
        string password = "Password.123",
        string firstname = "Firstname", 
        string lastname = "Lastname", 
        string address = "Address",
        int expiresInSeconds = 1000
        )
    {
        var url = $"/api/v1/identity/account/register?expiresInSeconds={expiresInSeconds}";
        if (url == null) throw new ArgumentNullException(nameof(url));

        var registerData = new Register
        {
            Email = email,
            Password = password,
            FirstName = firstname,
            LastName = lastname,
            Address = address
        };

        var data = JsonContent.Create(registerData);

        var client = GetClient();
        var response = await client.PostAsync(url, data);

        var jwt = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<JwtResponse>(jwt, _camelCaseJsonSerializerOptions);
    }

    [Fact(DisplayName = "GET - check that orders controller loads")]
    public async Task GetOrdersUnauthorizedTest()
    {
        // Arrange
        var client = GetClient();

        // Act
        var response = await client.GetAsync($"/api/v1.0/Orders/");

        // Assert
        response.Should()
            .HaveStatusCode(HttpStatusCode.Unauthorized);

        testOutputHelper.WriteLine(await response.Content.ReadAsStringAsync());
    }

    [Fact]
    public async Task GetOrders_ReturnsListForAuthenticatedUser()
    {
        // Arrange
        var client = GetClient();
        var jwt = await RegisterNewUser();
        if (jwt == null) Assert.Fail("Unable to authorize");
        
        var request = new HttpRequestMessage(HttpMethod.Get, $"/api/v1.0/Orders/");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt.Token);

        // Act
        var result = await client.SendAsync(request);

        // Assert
        result.Should().BeSuccessful();
    }
}