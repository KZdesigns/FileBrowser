using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace FileBrowser.UI
{
    public partial class Form1 : Form
    {

        // fields needed
        string filePath = "";
        List<string> list = new List<string>();

        public Form1()
        {
            InitializeComponent();
        }

        private void buttonOpenFile_Click(object sender, EventArgs e)
        {
            // show the open file dialog window
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // reset form fields
                lblFileName.Text = "";
                textBoxSearchPhrase.Text = "";
                listBoxResult.Items.Clear();

                // add the file path to the label
                filePath = openFileDialog1.FileName;
                lblFileName.Text = filePath;

                // create a file stream object & stream reader
                var fileStream = openFileDialog1.OpenFile();
                StreamReader reader = new StreamReader(fileStream);

                // add all the lines into a list
                while(!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    list.Add(line);
                }

            }
            else
            {
                // if cancel is pressed reset form
                lblFileName.Text = "";
                textBoxSearchPhrase.Text = "";
                listBoxResult.Items.Clear();
            }
        }



        private void buttonSearch_Click(object sender, EventArgs e)
        {
            // clears the listbox
            listBoxResult.Items.Clear();

            // try catch block
            try
            {
                // calls findMatches
                findMatches();
            }
            catch (ArgumentException)
            {
                // shows message box if findmatches returns an exception
                MessageBox.Show("Please enter a search word or phrase.");
            }
        }

        private void findMatches()
        {
            // check for invalid input from user
            if(textBoxSearchPhrase.Text == "" || textBoxSearchPhrase.Text == null)
            {
                throw new ArgumentException();
            } 

            // loop through the list and if the line contains the textbox text add it to the listbox
            foreach(var item in list)
            {
                if(item.Contains(textBoxSearchPhrase.Text))
                {
                    listBoxResult.Items.Add(item);
                }
            }
        }
    }
}
