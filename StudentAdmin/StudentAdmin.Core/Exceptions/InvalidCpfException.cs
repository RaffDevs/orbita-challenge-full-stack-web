namespace StudentAdmin.Core.Exceptions;

public class InvalidCpfException : Exception
{
    public override string Message { get; }

    public InvalidCpfException()
    {
        Message = "Cpf inválido! Por favor, insira um cpf válido.";
    }
}