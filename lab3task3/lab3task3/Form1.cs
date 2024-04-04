using System.Text.RegularExpressions;

namespace lab3task3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string document = "This documnet contain different types of words like mango,orange,melon etc";

            
            MatchCollection matches = Regex.Matches(document, @"\b[tm]\w+");

           

            foreach (Match match in matches)
            {
                string word = match.Value;
                int length = word.Length;

               
                dataGridView1.Rows.Add(word, length);
            }
        }
    }
}
