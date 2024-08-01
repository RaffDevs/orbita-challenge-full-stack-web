using System.Text.RegularExpressions;
using StudentAdmin.Core.Entities;
using StudentAdmin.Core.Exceptions;

namespace StudentAdmin.UnitTests.Core.Entities;

public class StudentTests
{
    [Fact]
    public void CpfIsOk_ValidateCpf_ReturnsString()
    {
        //Arrange
        var student = new Student(
            "123456789",
            "Dev Deverson",
            "deverson@mail.com",
            "322.868.270-60"
        );
        
        //Act
        var validCpf = Student.ValidateCpf(student.Cpf);
        
        //Assert
        Assert.NotNull(validCpf);
        Assert.NotEmpty(validCpf);
        Assert.IsType<string>(validCpf);
    }
    
    [Fact]
    public void CpfIsInvalid_ValidateCpf_ThrowsInvalidCpfException()
    {
        //Arrange
        var student = new Student(
            "123456789",
            "Dev Deverson",
            "deverson@mail.com",
            "322.868.270-63"
        );
        
        //Act
        var exception = Assert.Throws<InvalidCpfException>(() =>
        {
            Student.ValidateCpf(student.Cpf);
        });
        
        //Assert
        Assert.Equal(new InvalidCpfException().Message, exception.Message);
    }

    [Fact]
    public void FormatCpf_FormatCpfString_ReturnOnlyDigitString()
    {
        //Arrange
        var student = new Student(
            "123456789",
            "Dev Deverson",
            "deverson@mail.com",
            "322.868.270-63"
        );
        
        //Act
        var cpfString = Student.FormatCpf(student.Cpf);
        
        //Assert
        const string pattern = @"^\d+$";
        Assert.Matches(pattern, cpfString);
    }
    
}