using System;

class RecursiveDescentParser
{
    static string input;
    static int position;

    static void Main()
    {
        Console.WriteLine("Enter the input string:");
        input = Console.ReadLine();
        position = 0;

        Console.WriteLine("\nInput          Action");
        Console.WriteLine("---------------------");

        if (S() && position == input.Length - 1)
            Console.WriteLine("---------------------\nString is successfully parsed.");
        else
            Console.WriteLine("---------------------\nError in parsing string.");
    }

    static bool S()
    {
        Console.WriteLine("{0,-15} S -> X$", input[position]);
        if (X() && position == input.Length - 1 && input[position] == '$')
            return true;
        return false;
    }


    static bool X()
    {
        Console.WriteLine("{0,-15} X -> Y X'", input[position]);
        if (Y() && X_())
            return true;
        return false;
    }

    static bool X_()
    {
        if (input[position] == '%')
        {
            Console.WriteLine("{0,-15} X' -> % Y X'", input[position]);
            position++;
            if (Y() && X_())
                return true;
            return false;
        }
        else
        {
            Console.WriteLine("{0,-15} X' -> ε", input[position]);
            return true; // ε production
        }
    }

    static bool Y()
    {
        Console.WriteLine("{0,-15} Y -> Z Y'", input[position]);
        if (Z() && Y_())
            return true;
        return false;
    } static bool Y_()
    {
        if (input[position] == '&')
        {
            Console.WriteLine("{0,-15} Y' -> & Z Y'", input[position]);
            position++;
            if (Z() && Y_())
                return true;
            return false;
        }
        else
        {
            Console.WriteLine("{0,-15} Y' -> ε", input[position]);
            return true; // ε production
        }
    }
    static bool Z()
    {
        if (input[position] == 'k')
        {
            Console.WriteLine("{0,-15} Z -> k X k", input[position]);
            position++;
            if (X() && input[position] == 'k')
            {
                position++;
                return true;
            }
        }
        else if (input[position] == 'g')
        {
            Console.WriteLine("{0,-15} Z -> g", input[position]);
            position++;
            return true;
        }
        return false;
    }
}
