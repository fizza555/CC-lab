using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace lab4
{
    public class LexicalAnalyzer
    {
        // Define two buffers for input processing
        private string buffer1;
        private string buffer2;
        private bool usingBuffer1; // Flag to indicate which buffer is currently in use

        public LexicalAnalyzer(string input)
        {
            buffer1 = input; // Initialize buffer1 with the input
            buffer2 = string.Empty;
            usingBuffer1 = true; // Start with buffer1
        }

        // Method to switch to the other buffer
        private void SwitchBuffer()
        {
            usingBuffer1 = !usingBuffer1;
        }

        // Method to tokenize the input
        public List<string> Tokenize()
        {
            List<string> tokens = new List<string>();
            string[] lines = (buffer1 + buffer2).Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string line in lines)
            {
                tokens.AddRange(TokenizeLine(line));
            }

            return tokens;
        }

        // Method to tokenize each line of input
        private List<string> TokenizeLine(string line)
        {
            List<string> tokens = new List<string>();

            // Use regular expression to split the line into tokens
            string[] parts = Regex.Split(line, @"(\s+|\+|\-|\*|\/|\%|\=|\(|\)|\{|\}|;|,)");

            foreach (string part in parts)
            {
                // Add non-empty parts as tokens
                if (!string.IsNullOrWhiteSpace(part))
                {
                    tokens.Add(part.Trim());
                }
            }

            return tokens;
        }

        // Method to read characters into the current buffer
        private void ReadIntoBuffer(string input)
        {
            if (usingBuffer1)
            {
                buffer1 += input;
            }
            else
            {
                buffer2 += input;
            }
        }

        // Method to check if the current buffer contains a complete token
        private bool IsTokenComplete()
        {
            // Implement your logic to determine if a token is complete
            // For example, you can check for whitespace or specific characters
            // that indicate the end of a token
            // Here, we'll assume a token is complete when a whitespace is encountered
            string currentBuffer = usingBuffer1 ? buffer1 : buffer2;
            return currentBuffer.Contains(" ");
        }

        // Method to get the complete token from the current buffer
        private string GetToken()
        {
            string currentBuffer = usingBuffer1 ? buffer1 : buffer2;
            int index = currentBuffer.IndexOf(" ");
            string token = currentBuffer.Substring(0, index);
            currentBuffer = currentBuffer.Substring(index + 1); // Remove the token from the buffer
            return token;
        }

        // Method to process any remaining characters in the buffer after tokenization
        private void ProcessRemainingCharacters(List<string> tokens)
        {
            string currentBuffer = usingBuffer1 ? buffer1 : buffer2;
            while (currentBuffer.Length > 0)
            {
                // Process the remaining characters in the buffer
                // and add them to the tokens list if needed
                if (!string.IsNullOrWhiteSpace(currentBuffer))
                {
                    tokens.Add(currentBuffer.Trim());
                }
                currentBuffer = string.Empty;
            }
        }
    }

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sourceCode = richTextBox1.Text;

            LexicalAnalyzer lexicalAnalyzer = new LexicalAnalyzer(sourceCode);
            List<string> tokens = lexicalAnalyzer.Tokenize();

            richTextBox2.Clear();
            foreach (string token in tokens)
            {
                richTextBox2.AppendText(token + "\n");
            }
        }
    }
}
