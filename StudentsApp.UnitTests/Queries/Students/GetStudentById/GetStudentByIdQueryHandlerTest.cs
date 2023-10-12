using System;
using Application.Configuration.Mapping;
using Application.Configuration.Queries.Students.GetStudentById;
using AutoMapper;
using Core.Entities;
using Core.Repositories;
using FluentAssertions;
using Moq;
using System.Reflection.Emit;
using System.Threading.Tasks;


namespace StudentsApp.UnitTests.Queries.Students.GetStudentById
{
    public class GetStudentByIdQueryHandlerTest
    {
        private readonly Mock<IStudentRepository> _studentyRepositoryMock;
        private readonly IMapper _mapper;

        public GetStudentByIdQueryHandlerTest()
        {
            _studentyRepositoryMock = new Mock<IStudentRepository>();
            _mapper = AutoMapperConfig.Initialize();
        }

        [Fact]
        public async Task Handle_Should_CallGetByIdOnRepository_WhenGetStudentByIdQuery()
        {
            // Arrange
            _studentyRepositoryMock
                .Setup(x => x.GetById(It.IsAny<int>()))
                .Returns(new Student());

            var handler = new GetStudentByIdQueryHandler(
                _studentyRepositoryMock.Object, _mapper);

            // Act
            await handler.Handle(new GetStudentByIdQuery(1), default);

            // Assert
            _studentyRepositoryMock.Verify(x => x.GetById(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_ReturnStudentByIdQuery_WhenGetStudentByIdQuery()
        {
            // Arrange
            var student = new Student()
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
            };  

            _studentyRepositoryMock
                .Setup(x => x.GetById(It.IsAny<int>()))
                .Returns(student);

            var handler = new GetStudentByIdQueryHandler(
                _studentyRepositoryMock.Object, _mapper);

            // Act
            var studentDto = await handler.Handle(new GetStudentByIdQuery(1), default);

            // Assert
            studentDto.Should().NotBeNull();
            studentDto.Id.Should().Be(student.Id);
            studentDto.Name.Should().Be(student.Name);
            studentDto.Surname.Should().Be(student.Surname);
            studentDto.Email.Should().Be(student.Email);
            studentDto.DateOfBirth.Should().Be(student.DateOfBirth);
            studentDto.StartingStudyYear.Should().Be(student.StartingStudyYear);
        }

        [Fact]
        public async Task Handle_Should_ReturnNull_WhenGetStudentByIdQuery()
        {
            // Arrange
            _studentyRepositoryMock
                .Setup(x => x.GetById(It.IsAny<int>()))
                .Returns((Student)null);

            var handler = new GetStudentByIdQueryHandler(
                _studentyRepositoryMock.Object, _mapper);

            // Act
            var studentDto = await handler.Handle(new GetStudentByIdQuery(1), default);

            // Assert
            studentDto.Should().BeNull();
        }
    }
}
