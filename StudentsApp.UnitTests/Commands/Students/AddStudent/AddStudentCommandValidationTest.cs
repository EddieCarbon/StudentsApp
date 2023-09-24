using Application.Configuration.Commands.Students.CreateStudent;
using Core.Repositories;
using FluentValidation.TestHelper;
using Moq;

namespace StudentsApp.UnitTests.Commands.Students.AddStudent
{
    public class AddStudentCommandValidationTest
    {
        private readonly Mock<IStudentRepository> _studentRepositoryMock;

        public AddStudentCommandValidationTest()
        {
            _studentRepositoryMock = new();
        }

        [Fact]
        public void ValidateResult_Should_Not_HaveAnyValidationErrors_WhenAddStudentCommandIsValid()
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
                .Setup(x => x.IsAlreadyExistAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            var validator = new CreateStudentCommandValidation();

            // Act
            var validationResult = validator.TestValidate(command);

            // Assert
            validationResult.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void ValidateResult_Should_HaveValidationErrorsForEmail_WhenEmailIsEmpty()
        {
            // Arrange
            var command = new CreateStudentCommand()
            {
                Name = "Kuba",
                Surname = "Tomiczek",
                Email = string.Empty,
                DateOfBirth = DateTime.Now.AddYears(-19),
                StartingStudyYear = 2021,
            };

            _studentRepositoryMock
                .Setup(x => x.IsAlreadyExistAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            var validator = new CreateStudentCommandValidation();

            // Act
            var validationResult = validator.TestValidate(command);

            // Assert
            validationResult.ShouldHaveValidationErrorFor(x => x.Email);
        }

        [Fact]
        public void ValidateResult_Should_HaveValidationErrorsForEmail_WhenEmailHasMoreThan100Characters()
        {
            // Arrange
            var command = new CreateStudentCommand()
            {
                Name = "Kuba",
                Surname = "Tomiczek",
                Email = "aaaaaaaaaa@aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
                DateOfBirth = DateTime.Now.AddYears(-19),
                StartingStudyYear = 2021,
            };

            _studentRepositoryMock
                .Setup(x => x.IsAlreadyExistAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            var validator = new CreateStudentCommandValidation();

            // Act
            var validationResult = validator.TestValidate(command);

            // Assert
            validationResult.ShouldHaveValidationErrorFor(x => x.Email);
        }

        [Fact]
        public void ValidateResult_Should_HaveValidationErrorsForEmail_WhenEmailIsNotValid()
        {
            // Arrange
            var command = new CreateStudentCommand()
            {
                Name = "Kuba",
                Surname = "Tomiczek",
                Email = "j.tomiczek.gmail.com",
                DateOfBirth = DateTime.Now.AddYears(-19),
                StartingStudyYear = 2021,
            };

            _studentRepositoryMock
                .Setup(x => x.IsAlreadyExistAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            var validator = new CreateStudentCommandValidation();

            // Act
            var validationResult = validator.TestValidate(command);

            // Assert
            validationResult.ShouldHaveValidationErrorFor(x => x.Email);
        }
    }
}
