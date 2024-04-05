using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class LexicalAnalyzer
{
    private const int BufferSize = 1024;
    private char[] buffer1 = new char[BufferSize];
    private char[] buffer2 = new char[BufferSize];
    private int buffer1Index = 0;
    private int buffer2Index = 0;
    private int currentBufferIndex = 0;
    private int currentBufferSize = 0;
    private bool endOfFile = false;

    private readonly HashSet<string> keywords = new HashSet<string> { "int", "float", "char", "bool" };
    private readonly Regex identifierRegex = new Regex(@"^[A-Za-z_][A-Za-z0-9_]*$");
    private readonly Regex integerRegex = new Regex(@"^\d+$");
    private readonly Regex operatorRegex = new Regex(@"^[\+\-\*/=]$");
    private readonly Regex specialCharRegex = new Regex(@"^[.,;(){}]$");

    private bool keywordProcessed = false;

    public void Analyze(string input)
    {
        int inputLength = input.Length;
        int remainingLength = inputLength;
        int startIndex = 0;

        while (!endOfFile)
        {
            int chunkSize = Math.Min(remainingLength, BufferSize);
            Array.Copy(input.ToCharArray(startIndex, chunkSize), 0, GetCurrentBuffer(), 0, chunkSize);
            currentBufferSize = chunkSize;
            startIndex += chunkSize;
            remainingLength -= chunkSize;

            for (int i = 0; i < currentBufferSize; i++)
            {
                char currentChar = GetNextChar();
                if (char.IsWhiteSpace(currentChar))
                {
                }
                else if (identifierRegex.IsMatch(currentChar.ToString()))
                {
                    string identifier = ReadIdentifier(currentChar);
                    if (!keywordProcessed && keywords.Contains(identifier))
                    {
                        Console.WriteLine("Keyword: " + identifier);
                        keywordProcessed = true;
                    }
                    else
                    {
                        Console.WriteLine("Identifier: " + identifier);
                    }
                    keywordProcessed = false; // Reset the flag after processing identifier
                }
                else if (integerRegex.IsMatch(currentChar.ToString()))
                {
                    string integer = ReadInteger(currentChar);
                    Console.WriteLine("Integer: " + integer);
                }
                else if (operatorRegex.IsMatch(currentChar.ToString()))
                {
                    Console.WriteLine("Operator: " + currentChar);
                }
                else if (specialCharRegex.IsMatch(currentChar.ToString()))
                {
                    Console.WriteLine("Special Character: " + currentChar);
                }
                else if (currentChar == '\0')
                {
                    endOfFile = true;
                }
                else
                {
                    Console.WriteLine("Invalid Character: " + currentChar);
                }
            }
        }
    }

    private char GetNextChar()
    {
        char currentChar = '\0';
        if (currentBufferIndex == 0)
        {
            if (buffer1Index < currentBufferSize)
            {
                currentChar = buffer1[buffer1Index++];
            }
            else
            {
                SwapBuffers();
                if (!endOfFile)
                {
                    currentChar = buffer1[buffer1Index++];
                }
            }
        }
        else
        {
            if (buffer2Index < currentBufferSize)
            {
                currentChar = buffer2[buffer2Index++];
            }
            else
            {
                SwapBuffers();
                if (!endOfFile)
                {
                    currentChar = buffer2[buffer2Index++];
                }
            }
        }
        return currentChar;
    }

    private void SwapBuffers()
    {
        currentBufferIndex = (currentBufferIndex + 1) % 2;
        if (currentBufferIndex == 0)
        {
            buffer2Index = 0;
        }
        else
        {
            buffer1Index = 0;
        }
    }

    private char[] GetCurrentBuffer()
    {
        return currentBufferIndex == 0 ? buffer1 : buffer2;
    }

    private string ReadIdentifier(char currentChar)
    {
        string identifier = currentChar.ToString();
        while (buffer1Index < currentBufferSize && identifierRegex.IsMatch(buffer1[buffer1Index].ToString()))
        {
            identifier += buffer1[buffer1Index++];
        }
        return identifier;
    }

    private string ReadInteger(char currentChar)
    {
        string integer = currentChar.ToString();
        while (buffer1Index < currentBufferSize && integerRegex.IsMatch(buffer1[buffer1Index].ToString()))
        {
            integer += buffer1[buffer1Index++];
        }
        return integer;
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Enter the input:");
        string input = Console.ReadLine();
        LexicalAnalyzer analyzer = new LexicalAnalyzer();
        analyzer.Analyze(input);
    }
}
