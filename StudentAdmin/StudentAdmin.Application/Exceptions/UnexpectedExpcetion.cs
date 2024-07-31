namespace StudentAdmin.Application.Exceptions;

public class UnexpectedExpcetion : Exception
{
    public override string Message { get; }

    public UnexpectedExpcetion(string? message) : base(message)
    {
        Message = string.IsNullOrEmpty(message)
            ? "Oops, algo de errado não está certo! Contate o administrador!"
            : $"Eita, ocorreu o seguinte erro: {message}";
    }
}