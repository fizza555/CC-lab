using System;

public class Parser
{
    private readonly string exp;
    private int pos;

    public Parser(string exp) => (this.exp, this.pos) = (exp, 0);

    public double Parse() => Expression();

    private double Expression()
    {
        double res = Term();

        while (pos < exp.Length)
        {
            char op = exp[pos];
            if (op != '+' && op != '-')
                break;

            pos++;

            double next = Term();

            res = (op == '+') ? res + next : res - next;
        }

        return res;
    }

    private double Term()
    {
        double res = Factor();

        while (pos < exp.Length)
        {
            char op = exp[pos];
            if (op != '*' && op != '/')
                break;

            pos++;

            double next = Factor();

            if (op == '*')
                res *= next;
            else if (next == 0)
                throw new DivideByZeroException("Division by zero error");
            else
                res /= next;
        }

        return res;
    }

    private double Factor()
    {
        double res;

        if (char.IsDigit(exp[pos]))
            res = Number();
        else if (exp[pos] == '(')
        {
            pos++;
            res = Expression();
            pos++; // move past the closing parenthesis
        }
        else
            throw new ArgumentException("Invalid expression");

        return res;
    }

    private double Number()
    {
        int start = pos;
        while (pos < exp.Length && (char.IsDigit(exp[pos]) || exp[pos] == '.'))
            pos++;

        string numStr = exp.Substring(start, pos - start);
        return double.Parse(numStr);
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter an arithmetic expression:");
        string input = Console.ReadLine();
        Parser parser = new Parser(input);
        try
        {
            double result = parser.Parse();
            Console.WriteLine($"Result: {result}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
