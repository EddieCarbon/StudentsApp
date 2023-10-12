using Application.Configuration.Commands.Students.CreateStudent;
using Application.Configuration.Mapping;
using AutoMapper;
using Core.Entities;
using Core.Repositories;
using Microsoft.Extensions.Logging;
using Moq;


namespace StudentsApp.UnitTests.Commands.Students.AddStudent
{
    public class AddStudentsCommandHandlerTest
    {
        private readonly Mock<IStudentRepository> _studentRepositoryMock;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateStudentCommandHandler> _logger;

        public AddStudentsCommandHandlerTest()
        {
            _studentRepositoryMock = new();
            _mapper = AutoMapperConfig.Initialize();
            _logger = Mock.Of<ILogger<CreateStudentCommandHandler>>();
        }


        [Fact]
        public async Task Handle_Should_CallAddOnRepository_WhenEmailIsUnique()
        {
            // Arrange
            var command = new CreateStudentCommand()
            {
                Name = "Kuba",
                Surname = "Tomiczek",
                Email = "tomiczekkuba@gmail.com",
                DateOfBirth = DateTime.Now.AddYears(-19),
                StartingStudyYear = 2021,
            };

            _studentRepositoryMock
                .Setup(x => x.Add(It.IsAny<Student>()));

            _studentRepositoryMock
                .Setup(x => x.IsAlreadyExistAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);
            
            var handler = new CreateStudentCommandHandler(
                _studentRepositoryMock.Object,
                _mapper,
                _logger );

            // Act
            var studentDto = await handler.Handle(command, default);

            // Assert
            _studentRepositoryMock.Verify(
                x => x.Add(It.Is<Student>(x => x.Id == studentDto.Id)),
                Times.Once);
        }

        [Fact]
        public async Task Handle_Should_ThrowException_WhenEmailIsNotUnique()
        {
            // Arrange
            var command = new CreateStudentCommand()
            {
                Name = "Kuba",
                Surname = "Tomiczek",
                Email = "tomiczekkuba@gmail.com",
                DateOfBirth = DateTime.Now.AddYears(-19),
                StartingStudyYear = 2021,
            };

            _studentRepositoryMock
                .Setup(x => x.Add(It.IsAny<Student>()));

            _studentRepositoryMock
                .Setup(x => x.IsAlreadyExistAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            var handler = new CreateStudentCommandHandler(
                _studentRepositoryMock.Object,
                _mapper,
                _logger);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async() => await handler.Handle(command, default));
        }
    }
}
