using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace LexicalAnalyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            string userInput = @"int count = 10;
                                float price = 20.5;
                                print(""Hello, world!"");
                                if (count > 5)
                                {
                                    print(""Count is greater than 5"");
                                }";

            List<SymbolTableEntry> symbolTable = new List<SymbolTableEntry>();

            TokenizeInput(userInput, symbolTable);

            DisplaySymbolTable(symbolTable);
        }

        static void TokenizeInput(string userInput, List<SymbolTableEntry> symbolTable)
        {
           
            string[] lines = userInput.Split('\n');

            // Regular expressions for token recognition
            Regex variableRegex = new Regex(@"^[A-Za-z_][A-Za-z0-9_]*$");
            Regex constantRegex = new Regex(@"^\d+(\.\d+)?$");
            Regex operatorRegex = new Regex(@"[+\-*/=<>]");

            // Line number counter
            int lineNumber = 1;

            // Iterate through each line
            foreach (string line in lines)
            {
                // Split the line into tokens by whitespace
                string[] tokens = line.Trim().Split(' ');

                // Iterate through each token
                foreach (string token in tokens)
                {
                    // Determine token type
                    string type = "";
                    if (variableRegex.IsMatch(token))
                        type = "Variable";
                    else if (constantRegex.IsMatch(token))
                        type = "Constant";
                    else if (operatorRegex.IsMatch(token))
                        type = "Operator";
                    else
                        type = "Other";

                 
                    symbolTable.Add(new SymbolTableEntry()
                    {
                        Lexeme = token,
                        Type = type,
                        LineNumber = lineNumber
                    });
                }

              
                lineNumber++;
            }
        }

        static void DisplaySymbolTable(List<SymbolTableEntry> symbolTable)
        {
            Console.WriteLine("Symbol Table:");
            foreach (var entry in symbolTable)
            {
                Console.WriteLine($"Lexeme: {entry.Lexeme}, Type: {entry.Type}, Line Number: {entry.LineNumber}");
            }
        }
    }

    public class SymbolTableEntry
    {
        public string Lexeme { get; set; }
        public string Type { get; set; }
        public int LineNumber { get; set; }
    }
}
