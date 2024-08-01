using Moq;
using StudentAdmin.Application.Commands.UpdateStudent;
using StudentAdmin.Application.Exceptions;
using StudentAdmin.Application.Models.InputModels;
using StudentAdmin.Core.Entities;
using StudentAdmin.Core.Repositories;

namespace StudentAdmin.UnitTests.Application.Commands;

public class UpdateStudentCommandHandlerTest
{
    [Fact]
    public async Task StudentExists_UpdateStudentCommand_UpdatesStudentSuccessfully()
    {
        // Arrange
        var studentId = "123456";
        var updateModel = new UpdateStudentInputModel(
            "Ginivaldo",
            "Silva",
            "gini@mail.com",
            true
        );

        var existingStudent = new Student(
            studentId,
            "Ginivaldo",
            "giniupdated@mail.com",
            "431.584.090-46"
        );

        var expectedFullName = $"{updateModel.FirstName} {updateModel.LastName}";

        var studentRepositoryMock = new Mock<IStudentRepository>();
        studentRepositoryMock.Setup(repo => repo.GetByIdAsync(studentId))
            .ReturnsAsync(existingStudent);
        studentRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Student>()))
            .Returns(Task.CompletedTask);

        var updateStudentCommand = new UpdateStudentCommand(studentId, updateModel);
        var updateStudentCommandHandler = new UpdateStudentCommandHandler(studentRepositoryMock.Object);

        // Act
        await updateStudentCommandHandler.Handle(updateStudentCommand, new CancellationToken());

        // Assert
        studentRepositoryMock.Verify(repo => repo.GetByIdAsync(studentId), Times.Once);
        studentRepositoryMock.Verify(repo => repo.UpdateAsync(It.Is<Student>(s =>
            s.FullName == expectedFullName &&
            s.Email == updateModel.Email &&
            s.IsActive == updateModel.IsActive
        )), Times.Once);
    }

    [Fact]
    public async Task StudentDoesNotExist_UpdateStudentCommand_ThrowsNotFoundStudentException()
    {
        // Arrange
        var studentId = "123456";
        var updateModel = new UpdateStudentInputModel(
            "Ginivaldo",
            "Silva",
            "gini@mail.com",
            true
        );

        var studentRepositoryMock = new Mock<IStudentRepository>();
        studentRepositoryMock.Setup(repo => repo.GetByIdAsync(studentId))
            .ReturnsAsync((Student?)null);

        var updateStudentCommand = new UpdateStudentCommand(studentId, updateModel);
        var updateStudentCommandHandler = new UpdateStudentCommandHandler(studentRepositoryMock.Object);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<NotFoundStudentException>(() =>
            updateStudentCommandHandler.Handle(updateStudentCommand, new CancellationToken()));
        Assert.Equal(new NotFoundStudentException().Message, exception.Message);
    }
}