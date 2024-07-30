namespace StudentAdmin.Core.Entities;

public class Student
{
    public string Ra { get; private set; }
    public string FullName { get; private set; }
    public string Email { get; private set; }
    public string Cpf { get; private set; }

    public Student(string ra, string fullName, string email, string cpf)
    {
        Ra = ra;
        FullName = fullName;
        Email = email;
        Cpf = cpf;
    }

    public void SetFullName(string fullName) => FullName = fullName;
    public void SetEmail(string email) => Email = email;

    private string ValidateCpf(string cpf)
    {
        throw new NotImplementedException();
    }
    
}