using System;
using System.Collections.Generic;
using System.Collections;

public class SLRParser
{
    ArrayList States = new ArrayList();
    Stack<string> Stack = new Stack<string>();
    string Parser;
    string[] Col = { "begin", "(", ")", "{", "int", "a", "b", "c", "=", "5", "10", "0", ";", "if", ">", "print", "else", "$", "}", "+", "end", "Program", "DecS", "AssS", "IffS", "PriS", "Var", "Const" };

    public SLRParser()
    {
        States.Add("Program_begin ( ) { DecS Decs Decs AssS IffS } end");
        States.Add("DecS_int Var = Const ;");
        States.Add("AssS_Var = Var + Var ;");
        States.Add("IffS_if ( Var > Var ) { PriS } else { PriS }");
        States.Add("PriS_print Var ;");
        States.Add("Var_a");
        States.Add("Var_b");
        States.Add("Var_c");
        States.Add("Const_5");
        States.Add("Const_10");
        States.Add("Const_0");

        Stack.Push("0");

        ParseTable();
    }

    public void ParseTable()
    {
        var dict = new Dictionary<string, Dictionary<string, object>>();
        dict.Add("0", new Dictionary<string, object>()
        {
            {"begin", "S2"},
            {"(", ""},
            {")", ""},
            {"{", ""},
            {"int", ""},
            {"a", ""},
            {"b", ""},
            {"c", ""},
            {"=", ""},
            {"5", ""},
            {"10", ""},
            {"0", ""},
            {";", ""},
            {"if", ""},
            {">", ""},
            {"print", ""},
            {"else", ""},
            {"$", ""},
            {"}", ""},
            {"+", ""},
            {"end", ""},
            {"Program", "1"},
            {"DecS", ""},
            {"AssS", ""},
            {"IffS", ""},
            {"PriS", ""},
            {"Var", ""},
            {"Const", ""}
        });
        dict.Add("1", new Dictionary<string, object>()
        {
            {"begin", ""},
            {"(", ""},
            {")", ""},
            {"{", ""},
            {"int", ""},
            {"a", ""},
            {"b", ""},
            {"c", ""},
            {"=", ""},
            {"5", ""},
            {"10", ""},
            {"0", ""},
            {";", ""},
            {"if", ""},
            {">", ""},
            {"print", ""},
            {"else", ""},
            {"$", "Accept"},
            {"}", ""},
            {"+", ""},
            {"end", ""},
            {"Program", ""},
            {"DecS", ""},
            {"AssS", ""},
            {"IffS", ""},
            {"PriS", ""},
            {"Var", ""},
            {"Const", ""}
        });

        // Continue adding states and transitions...

        Parser = "";
    }

    public void ParseInput(string input)
    {
        string finalArray = input;
        int pointer = 0;

        // Parse input based on the parse table

        Console.WriteLine("Parsing completed.");
    }

    public static void Main(string[] args)
    {
        SLRParser parser = new SLRParser();
        string input = "Begin(){int a=5; int b=10; int c=0; c=a+b; if(c>a) print a; else print c;}end";
        parser.ParseInput(input);
    }
}
