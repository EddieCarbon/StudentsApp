using System.Net;
using Application.Dto.Student;
using Microsoft.AspNetCore.Mvc.Testing;
using FluentAssertions;
using Infrastructure.Context;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace StudentsApp.IntegrationTest.Controller;

public class StudentControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _httpClient;
    private readonly WebApplicationFactory<Program> _webApplicationFactory;

    public StudentControllerTests(WebApplicationFactory<Program> factory)
    {
        _webApplicationFactory = factory;
        _httpClient = _webApplicationFactory.CreateClient();
    }

    [Fact]
    public async Task GetAll_Should_ReturnListOfStudentsAndStatusOk()
    {
        // Arrange
        var scopedFactory = _webApplicationFactory.Services.GetService<IServiceScopeFactory>();
        using var scope = scopedFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetService<StudentAppContext>();

        // Act
        var response = await _httpClient.GetAsync("/api/Students");
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<ListStudentsDto>(content);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        result.Should().NotBeNull();
    }
}