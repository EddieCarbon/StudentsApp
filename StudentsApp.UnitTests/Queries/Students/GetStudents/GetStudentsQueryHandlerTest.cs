
using Application.Configuration.Mapping;
using Application.Configuration.Queries.Students.GetAllStudents;
using Application.Dto.Student;
using AutoMapper;
using Core.Entities;
using Core.Repositories;
using FluentAssertions;
using Moq;

namespace StudentsApp.UnitTests.Queries.Students.GetStudents;

public class GetStudentsQueryHandlerTest
{
    private readonly Mock<IStudentRepository> _studentReadOnlyRepositoryMock;
    private readonly IMapper _mapper;
    
    public GetStudentsQueryHandlerTest()
    {
        _studentReadOnlyRepositoryMock = new Mock<IStudentRepository>();
        _mapper = MapperHelper.CreateMapper( new AutoMapperConfig().Initialize());
    }

    [Fact]
    public async Task Handle_Should_CallGetAllOnRepository_WhenGetStudentsQuery()
    {
        // Arrange
        
        _studentReadOnlyRepositoryMock.Setup(
            x => x.GetAll(
                It.IsAny<CancellationToken>())).Returns(Enumerable.Empty<Student>);

        var handler = new GetAllStudentsQueryHandler(
            _studentReadOnlyRepositoryMock.Object, _mapper);
        
        // Act

        await handler.Handle(new GetAllStudentsQuery(), default);
        
        // Assert
        
        _studentReadOnlyRepositoryMock.Verify(
            x => x.GetAll(It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Handle_Should_ReturnNotEmptyCollection_WhenGetStudentsQuery()
    {
        // Arrange
        var students = new List<Student>()
        {
            new Student()
            {
                Id = 2 ,
                Name = "Kuba",
                Surname = "Tomiczek",
                Email = "tomiczekkuba01@gmail.com",
                DateOfBirth = DateTime.Today,
                StartingStudyYear = 2022,
            },
            new Student()
            {
                Id = 2 ,
                Name = "Michal",
                Surname = "Tomiczek",
                Email = "michaltomiczek@gmail.com",
                DateOfBirth = DateTime.Today,
                StartingStudyYear = 2021,
            }
        };
        
        _studentReadOnlyRepositoryMock.Setup(
            x => x.GetAll(
                It.IsAny<CancellationToken>())).Returns(students);
        
        var handler = new GetAllStudentsQueryHandler(
            _studentReadOnlyRepositoryMock.Object, _mapper);
        
        // Act
        var studentsDto = await handler.Handle(new GetAllStudentsQuery(), default);
        
        // Assert
        studentsDto.Should().NotBeNull();
    }
}