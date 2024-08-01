using Moq;
using StudentAdmin.Application.Exceptions;
using StudentAdmin.Application.Queries.GetStudentById;
using StudentAdmin.Core.Entities;
using StudentAdmin.Core.Repositories;

namespace StudentAdmin.UnitTests.Application.Queries;

public class GetStudentByIdQueryHandlerTest
{
    [Fact]
    public async Task ExistsOneStudentWithId_GetStudentByIdQuery_ReturnStudentWithSameId()
    {
        // Arrange
        var student = new Student("123345456", "Rafael Veronez", "teste@mail.com", "317.723.630-57");

        var studentRepositoryMock = new Mock<IStudentRepository>();
        const string studentId = "317.723.630-58";
        studentRepositoryMock.Setup(st => st.GetByIdAsync(studentId).Result)
            .Throws<NotFoundStudentException>(() => new NotFoundStudentException());
        var query = new GetStudentByIdQuery(studentId);
        var getStudentByIdQueryHandler = new GetStudentByIdQueryHandler(studentRepositoryMock.Object);

        // Act
        var exception = await Assert.ThrowsAsync<NotFoundStudentException>(() =>
            getStudentByIdQueryHandler.Handle(query, new CancellationToken()));

        // Assert
        Assert.Equal(new NotFoundStudentException().Message, exception.Message);
        studentRepositoryMock.Verify(st => st.GetByIdAsync(studentId).Result, Times.Once);

    }

    [Fact]
    public async Task ExistsOneStudentWithAnotherId_GetById_ThrowsNotFoundStudentException()
    {
        // Arrange
        var student = new Student("123345456", "Rafael Veronez", "teste@mail.com", "317.723.630-57");

        var studentRepositoryMock = new Mock<IStudentRepository>();
        var studentId = "317.723.630-57";
        studentRepositoryMock.Setup(st => st.GetByIdAsync(studentId).Result)
            .Returns(student);
        var query = new GetStudentByIdQuery(studentId);
        var getStudentByIdQueryHandler = new GetStudentByIdQueryHandler(studentRepositoryMock.Object);

        // Act
        var studentViewModel = await getStudentByIdQueryHandler.Handle(query, new CancellationToken());

        // Assert
        Assert.NotNull(studentViewModel);
        Assert.Equal(student.Cpf, studentViewModel.Cpf);
        studentRepositoryMock.Verify(st => st.GetByIdAsync(studentId).Result, Times.Once);
    }
}