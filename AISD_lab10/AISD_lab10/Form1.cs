using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AISD_lab10
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox2.SelectionStart = 0;
            richTextBox2.SelectionLength = richTextBox2.TextLength;
            richTextBox2.SelectionColor = Color.Black;

            // Spliting onto words first text
            string firstText = richTextBox1.Text;
            firstText = firstText.Replace('\n', ' ');
            List<string> firstTextWords = new List<string>(firstText.Split(' '));

            // Counting frequencies of words
            Dictionary<string, uint> wordsAndFrequencies = new Dictionary<string, uint>();
            foreach (var word in firstTextWords)
            {
                if (word != string.Empty)
                {
                    if (wordsAndFrequencies.ContainsKey(word))
                    {
                        ++wordsAndFrequencies[word];
                    }
                    else
                    {
                        wordsAndFrequencies.Add(word, 1);
                    }
                }
            }

            // Determining and removing most frequency word
            string wordToRemove = string.Empty;
            try
            {
                uint maxFrequency = wordsAndFrequencies.Max(pair => pair.Value);
                wordToRemove = wordsAndFrequencies.FirstOrDefault(pair => pair.Value == maxFrequency).Key;
                firstTextWords.RemoveAll(s => s == wordToRemove);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            // printing results in first text
            string firstTextWithRemovedMostFrequentlyWord = string.Empty;
            foreach (var word in firstTextWords)
            {
                firstTextWithRemovedMostFrequentlyWord += word + ' ';
            }
            richTextBox1.Text = firstTextWithRemovedMostFrequentlyWord;
            
            int n = 0;
            int selectionStart = -1;
            bool isFounded = false;

            // Direct search algorithm
            for (int i = 0; i < richTextBox2.TextLength; i++)
            {
                if (n < wordToRemove.Length && richTextBox2.Text[i] == wordToRemove[n])
                {
                    ++n;
                    if (selectionStart == -1)
                    {
                        selectionStart = i;
                    }
                    if (n == wordToRemove.Length)
                    {
                        isFounded = true;
                        richTextBox2.SelectionStart = selectionStart;
                        richTextBox2.SelectionLength = wordToRemove.Length;
                        richTextBox2.SelectionColor = Color.Red;
                        selectionStart = -1;
                        n = 0;
                    }
                }
                else
                {
                    selectionStart = -1;
                    n = 0;
                }
            }

            // Result messages
            MessageBox.Show(
                "The most frequently word: \'" + wordToRemove + "\' has been successfully removed from first text",
                "Removed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (!isFounded)
            {
                MessageBox.Show("Word \'" + wordToRemove + "\' not founded in second text", "Not found",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            //Setting black color in the last char in richTextBox2
            richTextBox2.SelectionStart = richTextBox2.TextLength;
            richTextBox2.SelectionColor = Color.Black;
        }
    }
}
