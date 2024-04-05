using System;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter your input:");
        string input = Console.ReadLine();
        string pattern = @"(?:\|\||&&)";

        MatchCollection matches = Regex.Matches(input, pattern);

        if (matches.Count > 0)
        {
            Console.WriteLine("Logical operators found:");
            foreach (Match match in matches)
            {
                Console.WriteLine("Logical operator: " + match.Value);
            }
        }
        else
        {
            Console.WriteLine("No logical operators found.");
        }
    }
}
