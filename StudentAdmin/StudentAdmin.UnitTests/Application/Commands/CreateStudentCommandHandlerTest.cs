using Moq;
using StudentAdmin.Application.Commands.CreateStudent;
using StudentAdmin.Application.Models.InputModels;
using StudentAdmin.Core.Entities;
using StudentAdmin.Core.Repositories;

namespace StudentAdmin.UnitTests.Application.Commands;

public class CreateStudentCommandHandlerTest
{
    [Fact]
    public async Task InputDataIsOk_CreateStudentCommand_ReturnsRaString()
    {
        //Arrange
        var studentInputData = new CreateStudentInputModel(
            "123456",
            "Ginivaldo",
            "Silva",
            "gini@mail.com",
            "431.584.090-46"
        );

        var studentEntity = new Student(
            "123456",
            "Ginivaldo Silva",
            "gini@mail.com",
            "431.584.090-46"
        );
        
        var studentRepository = new Mock<IStudentRepository>();
        studentRepository.Setup(repo => repo.CreateAsync(It.IsAny<Student>()))
            .ReturnsAsync(studentEntity);
        var createStudentCommand = new CreateStudentCommand(studentInputData);
        var createStudentCommandHandler = new CreateStudentCommandHandler(studentRepository.Object);

        //Act
        var studentId = await createStudentCommandHandler.Handle(createStudentCommand, new CancellationToken());

        //Assert
        Assert.Equal(studentInputData.Ra, studentId);
        studentRepository.Verify(st => st.CreateAsync(It.IsAny<Student>()), Times.Once);
    }
}