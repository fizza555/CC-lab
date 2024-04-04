using System.Text.RegularExpressions;

namespace lab3_task2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] numbers = { "8e4", "5e-2", "6e9", "123", "3.14", "-5.2e3" };

            // Add rows to the DataGridView
            foreach (string number in numbers)
            {
                bool isValid = Regex.IsMatch(number, @"^\d+[eE][+-]?\d+$");
                dataGridView1.Rows.Add(number, isValid ? "Yes" : "No");
            }

        }
    }
}
