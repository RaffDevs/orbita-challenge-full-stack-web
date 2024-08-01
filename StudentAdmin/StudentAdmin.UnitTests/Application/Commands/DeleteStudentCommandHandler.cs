using Moq;
using StudentAdmin.Application.Commands.DeleteStudent;
using StudentAdmin.Application.Exceptions;
using StudentAdmin.Core.Entities;
using StudentAdmin.Core.Repositories;

namespace StudentAdmin.UnitTests.Application.Commands;

public class DeleteStudentCommandHandlerTest
{
    [Fact]
    public async Task StudentExists_DeleteStudentCommand_DeletesStudentSuccessfully()
    {
        // Arrange
        var studentId = "123456";
        var student = new Student(
            studentId,
            "Ginivaldo Silva",
            "gini@mail.com",
            "431.584.090-46"
        );

        var studentRepositoryMock = new Mock<IStudentRepository>();
        studentRepositoryMock.Setup(repo => repo.GetByIdAsync(studentId))
            .ReturnsAsync(student);
        studentRepositoryMock.Setup(repo => repo.DeleteAsync(It.IsAny<Student>()))
            .Returns(Task.CompletedTask);

        var deleteStudentCommand = new DeleteStudentCommand(studentId);
        var deleteStudentCommandHandler = new DeleteStudentCommandHandler(studentRepositoryMock.Object);

        // Act
        await deleteStudentCommandHandler.Handle(deleteStudentCommand, new CancellationToken());

        // Assert
        studentRepositoryMock.Verify(repo => repo.GetByIdAsync(studentId), Times.Once);
        studentRepositoryMock.Verify(repo => repo.DeleteAsync(student), Times.Once);
    }

    [Fact]
    public async Task StudentDoesNotExist_DeleteStudentCommand_ThrowsNotFoundStudentException()
    {
        // Arrange
        var studentId = "123456";

        var studentRepositoryMock = new Mock<IStudentRepository>();
        studentRepositoryMock.Setup(repo => repo.GetByIdAsync(studentId))
            .ReturnsAsync((Student?)null);

        var deleteStudentCommand = new DeleteStudentCommand(studentId);
        var deleteStudentCommandHandler = new DeleteStudentCommandHandler(studentRepositoryMock.Object);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundStudentException>(() =>
            deleteStudentCommandHandler.Handle(deleteStudentCommand, new CancellationToken()));
    }
}