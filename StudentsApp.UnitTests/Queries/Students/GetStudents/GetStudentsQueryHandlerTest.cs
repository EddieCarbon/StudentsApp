
using Application.Configuration.Mapping;
using Application.Configuration.Queries.Students.GetAllStudents;
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
        _mapper = AutoMapperConfig.Initialize();
    }

    [Fact]
    public async Task Handle_Should_CallGetAllOnRepository_WhenGetStudentsQuery()
    {
        // Arrange
        _studentReadOnlyRepositoryMock
            .Setup(x => x.GetAll())
            .Returns(new List<Student>()
            .AsQueryable());
        
        var handler = new GetAllStudentsQueryHandler(
            _studentReadOnlyRepositoryMock.Object, _mapper);
        
        // Act
        await handler.Handle(new GetAllStudentsQuery(), default);
        
        // Assert
        _studentReadOnlyRepositoryMock.Verify(
            x => x.GetAll(), Times.Once);
    }

    [Fact]
    public async Task Handle_Should_ReturnNotEmptyCollection_WhenGetStudentsQuery()
    {
        // Arrange
        var Students = new List<Student>()
        {
            new Student()
            {
                 Id = 1,
                Name = "Kuba",
                Surname = "Tomiczek",
                Email = "tomiczekkuba@gmail.com",
                DateOfBirth = DateTime.Now,
                StartingStudyYear = 2021,
                Address = new StudentAddress
                {
                    StudentAddressId = 1,
                    Street = "Wesola",
                    Number = 3,
                    City = "Zywiec",
                    ZipCode = 12345,
                    Country = "Poland",
                },
                CurrentDepartmentId = 1,
                Department = new Department
                {
                    DepartmentId = 1,
                    DepartmentName = "NameDepartment",
                    BuildingNumber = "3a",
                }
            },
            new Student()
            {
                Id = 2,
                Name = "Kuba",
                Surname = "Tomiczek",
                Email = "tomiczekkuba@gmail.com",
                DateOfBirth = DateTime.Now,
                StartingStudyYear = 2021,
                Address = new StudentAddress
                {
                    StudentAddressId = 2,
                    Street = "Wesola",
                    Number = 3,
                    City = "Zywiec",
                    ZipCode = 12345,
                    Country = "Poland",
                },
                CurrentDepartmentId = 2,
                Department = new Department
                {
                    DepartmentId = 2,
                    DepartmentName = "NameDepartment",
                    BuildingNumber = "3a",
                }
            },
        };
        
        var querableStudents = Students.AsQueryable();

        _studentReadOnlyRepositoryMock
            .Setup(x => x.GetAll())
            .Returns(querableStudents);
        
        var handler = new GetAllStudentsQueryHandler(_studentReadOnlyRepositoryMock.Object, _mapper);
        
        // Act
        var studentsDto = await handler.Handle(new GetAllStudentsQuery(), default);
        
        // Assert
        studentsDto.Should().NotBeNull();
    }
}