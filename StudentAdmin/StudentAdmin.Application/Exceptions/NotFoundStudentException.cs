namespace StudentAdmin.Application.Exceptions;

public class NotFoundStudentException : Exception
{
    public override string Message { get; }

    public NotFoundStudentException()
    {
        Message = "Nenhum registro encontrado!";
    }
}