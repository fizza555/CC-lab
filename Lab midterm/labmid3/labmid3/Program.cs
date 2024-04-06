using System;
using System.Linq;
using System.Text;

class PasswordGenerator
{
    static Random random = new Random();

    static string GeneratePassword(string firstName, string lastName, string regNumbers)
    {
        var pass = new StringBuilder();
        pass.Append(char.ToLower(firstName[0])).Append(char.ToLower(lastName[0]));

        foreach (var c in firstName.Where((_, i) => i % 2 != 0))
            pass.Append(c);

        foreach (var c in lastName.Where((_, i) => i % 2 == 0))
            pass.Append(c);

        pass.Append(char.ToUpper(lastName[random.Next(lastName.Length)]));

        var numToAdd = 4 - regNumbers.Length;
        if (numToAdd > 0)
        {
            pass.Append(regNumbers);
            numToAdd -= regNumbers.Length;
        }
        for (int i = 0; i < numToAdd; i++)
            pass.Append(random.Next(10));

        var specialChars = "!@#$%^&*()-_=+[]{}|;:,<.>/?";
        pass.Append(specialChars[random.Next(specialChars.Length)]);
        pass.Append(specialChars[random.Next(specialChars.Length)]);

        return pass.ToString().Substring(0, Math.Min(pass.Length, 16));
    }

    static void Main(string[] args)
    {
        Console.Write( " Please Enter your  first name: ");
        var fName = Console.ReadLine();

        Console.Write("Please Enter  your last name: ");
        var lName = Console.ReadLine();

        Console.Write("Please Enter  your registration number: ");
        var regNums = Console.ReadLine();

        var password = GeneratePassword(fName, lName, regNums);
        Console.WriteLine(" unique Password  generated: " + password);
    }
}
