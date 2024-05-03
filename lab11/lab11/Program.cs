using System;

public class SyntaxDirectedTranslation
{
    // Function to evaluate an expression
    public int EvaluateExpression(string expression)
    {
        // Tokenize the expression
        string[] tokens = expression.Split(' ');

        // Perform syntax-directed translation
        int result = E(tokens, 0);

        return result;
    }

    // Syntax-directed translation for E -> E + T | T
    private int E(string[] tokens, int index)
    {
        int value = T(tokens, index);

        if (index + 1 < tokens.Length && tokens[index + 1] == "+")
        {
            int nextValue = E(tokens, index + 2);
            value += nextValue;
        }

        return value;
    }

    // Syntax-directed translation for T -> T * F | F
    private int T(string[] tokens, int index)
    {
        int value = F(tokens, index);

        if (index + 1 < tokens.Length && tokens[index + 1] == "*")
        {
            int nextValue = T(tokens, index + 2);
            value *= nextValue;
        }

        return value;
    }

    // Syntax-directed translation for F -> ( E ) | id
    private int F(string[] tokens, int index)
    {
        if (tokens[index] == "(")
        {
            return E(tokens, index + 1);
        }
        else
        {
            // Assuming id represents integers, parse it to int and return
            return int.Parse(tokens[index]);
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        SyntaxDirectedTranslation translator = new SyntaxDirectedTranslation();

        string expression = "5 + 2 * 3";
        int result = translator.EvaluateExpression(expression);
        Console.WriteLine($"Result: {result}");
    }
}
