using System;
using System.Text.RegularExpressions;

class Program
{
    static void Main(string[] args)
    {

        string pattern = @"^(?:\d{1,6}(?:\.\d{0,6})?|\.\d{1,6})$";


        while (true)
        {
            Console.WriteLine("Enter a floating-point number (with a length not greater than 6):");
            string input = Console.ReadLine();

        
            if (Regex.IsMatch(input, pattern))
            {
                Console.WriteLine($"{input} is a valid floating-point number with length not greater than 6.");
            }
            else
            {
                Console.WriteLine($"{input} is not a valid floating-point number with length not greater than 6.");
            }

            Console.WriteLine("Do you want to check another number? (yes/no)");
            string choice = Console.ReadLine().ToLower();
            if (choice != "yes")
                break;
        }
    }
}
