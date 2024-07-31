using System.Text.RegularExpressions;
using StudentAdmin.Core.Exceptions;

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

    public void Update(string fullName, string email)
    {
        FullName = fullName;
        Email = email;
    }
    
    private static string FormatCpf(string cpf)
    {
        try
        {
            return Regex.Replace(cpf, @"\D", "");
        }
        catch (Exception ex)
        {
            throw new InvalidCpfException();
        }
    }

    public static string ValidateCpf(string cpf)
    {
        cpf = FormatCpf(cpf);
        
        if (new string(cpf[0], 11) == cpf)
        {
            throw new InvalidCpfException();
        }

        int[] multipliers1 = [10, 9, 8, 7, 6, 5, 4, 3, 2];
        var sum1 = 0;

        for (var i = 0; i < 9; i++)
        {
            sum1 += int.Parse(cpf[i].ToString()) * multipliers1[i];
        }

        var leftovers1 = (sum1 % 11);
        var firstDigit = leftovers1 < 2 ? 0 : 11 - leftovers1;

        if (int.Parse(cpf[9].ToString()) != firstDigit)
        {
            throw new InvalidCpfException();
        }
        
        int[] multipliers2 = [11, 10, 9, 8, 7, 6, 5, 4, 3, 2];
        var sum2 = 0;

        for (var i = 0; i < 10; i++)
        {
            sum2 += int.Parse(cpf[i].ToString()) * multipliers2[i];
        }

        var leftovers2 = (sum2 % 11);
        var secondDigit = leftovers2 < 2 ? 0 : 11 - leftovers2;

        if (int.Parse(cpf[10].ToString()) != secondDigit)
        {
            throw new InvalidCpfException();
        }

        return cpf;
    }

    
    
}