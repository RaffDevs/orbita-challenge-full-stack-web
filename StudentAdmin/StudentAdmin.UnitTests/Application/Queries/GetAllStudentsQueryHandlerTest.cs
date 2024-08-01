using Moq;
using StudentAdmin.Application.Queries.GetAllStudents;
using StudentAdmin.Application.Queries.GetStudentById;
using StudentAdmin.Core.Entities;
using StudentAdmin.Core.Repositories;

namespace StudentAdmin.UnitTests.Application.Queries;

public class GetAllStudentsQueryHandlerTest
{
    [Fact]
    public async Task ExistsTwoStudents_GetAll_ReturnTwoStudentViewModels()
    {
        // Arrange
        var students = new List<Student>
        {
            new Student("123345456", "Rafael Veronez", "teste@mail.com", "45176585898"),
            new Student("123345456", "Rafael Veronez", "teste@mail.com", "322.868.270-60"),
            new Student("123345456", "Rafael Veronez", "teste@mail.com", "317.723.630-57")
        };
        var studentRepositoryMock = new Mock<IStudentRepository>();
        studentRepositoryMock.Setup(st => st.GetAllAsync(null).Result)
            .Returns(students);
        var query = new GetAllStudentsQuery(null);
        var getAllStudentQueryHandler = new GetAllStudentsQueryHandler(studentRepositoryMock.Object);

        // Act
        var studentViewModelList = await getAllStudentQueryHandler.Handle(query, new CancellationToken());
        
        // Assert
        Assert.NotNull(studentViewModelList);
        Assert.NotEmpty(studentViewModelList);
        Assert.Equal(students.Count, studentViewModelList.Count);
        studentRepositoryMock.Verify(st => st.GetAllAsync(null).Result, Times.Once);
    }
    
    
}