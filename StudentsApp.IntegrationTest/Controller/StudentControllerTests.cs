using System.Net;
using Application.Dto.Student;
using Microsoft.AspNetCore.Mvc.Testing;
using FluentAssertions;
using Infrastructure.Context;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using System.Reflection.Emit;
using System.Diagnostics.Metrics;
using System.Text;
using Application.Dto.StudentAddress;

namespace StudentsApp.IntegrationTest.Controller;

public class StudentControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _httpClient;
    private readonly WebApplicationFactory<Program> _webApplicationFactory;

    public StudentControllerTests(WebApplicationFactory<Program> factory)
    {
        _webApplicationFactory = factory
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    var dbContextOption = services
                        .SingleOrDefault(services => services.ServiceType == typeof(DbContextOptions<StudentAppContext>));
                    services.Remove(dbContextOption);
                    services.AddDbContext<StudentAppContext>(options => options.UseInMemoryDatabase("StudentsDb"));
                });
            });
        _httpClient = _webApplicationFactory.CreateClient();
    }

    [Fact]
    public async Task GetAll_Should_ReturnListOfStudentsAndStatusOk()
    {
        // Arrange
        var scopedFactory = _webApplicationFactory.Services.GetService<IServiceScopeFactory>();
        using var scope = scopedFactory.CreateScope();
        var _dbContext = scope.ServiceProvider.GetService<StudentAppContext>();

        _dbContext.Students.AddRange(new List<Student>
        {
            new Student()
            {
                Id = 1,
                Name = "Test1",
                Surname = "Test1",
                Email = "test1@example.com",
                DateOfBirth = new DateTime(1990, 1, 1),
                StartingStudyYear = 2010,
                CurrentDepartmentId = 1,
            },
            new Student()
            {
                Id = 2,
                Name = "Test2",
                Surname = "Test2",
                Email = "test2@example.com",
                DateOfBirth = new DateTime(1991, 1, 1),
                StartingStudyYear = 2012,
                CurrentDepartmentId = 2,
            }
        });
        await _dbContext.SaveChangesAsync();

        // Act
        var response = await _httpClient.GetAsync("/api/Students");
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<ListStudentsDto>(content);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        result.Students.Should().NotBeNullOrEmpty();   
    }

    [Fact]
    public async Task Post_Should_ReturnNewStudentAndStatusCodeCreated()
    {
        // Arrange
        var command = new CreateStudentDto()
        {
            Name = "Test1",
            Surname = "Test1",
            Email = "test25@example.com",
            DateOfBirth = new DateTime(1990, 1, 1),
            StartingStudyYear = 2010,
            CreateStudentAddress = new CreateStudentAddress()
            {
                Street = "Wesola",
                Number = 2,
                City = "Zywiec",
                ZipCode = 34350,
                Country = "Poland",
            },
            CurrentDepartmentId = 1,
        };

        var jsonString = JsonConvert.SerializeObject(command);
        var stringContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

        // Act
        var response = await _httpClient.PostAsync("/api/Students", stringContent);
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<StudentDto>(content);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        result.Should().NotBeNull();
        result.Should().BeOfType<StudentDto>();

        Assert.Equal(result.Name, command.Name);
    }

    [Fact]
    public async Task Delete_Should_ReturnStatusCodeNoContent()
    {
        var scopedFactory = _webApplicationFactory.Services.GetService<IServiceScopeFactory>();
        using var scope = scopedFactory.CreateScope();
        var _dbContext = scope.ServiceProvider.GetService<StudentAppContext>();

        var student = _dbContext.Students.Add(
            new Student()
            {
                Name = "Test1",
                Surname = "Test1",
                Email = "test1@example.com",
                DateOfBirth = new DateTime(1990, 1, 1),
                StartingStudyYear = 2010,
                CurrentDepartmentId = 1,
            });

        await _dbContext.SaveChangesAsync();

        int studentId = student.Entity.Id;

        // Act
        var response = await _httpClient.DeleteAsync($"/api/Students/{studentId}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}