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
using System.Xml;

namespace Contact_List
{
    
    public partial class ContactForm : Form
    {
        private bool newDoc = false;
        private XmlTextReader doc;
        private String filePathName = "../../../XMLSampleNames.xml";
        public ContactForm()
        {
            InitializeComponent();
            

            try {
                doc = new XmlTextReader(filePathName);
                short i = 0;
                DataGridViewCell selectedCell = null;
                dataGridView.Rows.Add();
                while (doc.Read())
                {
                    switch (doc.NodeType)
                    {
                        case XmlNodeType.Element: // The node is an element.
                            if (doc.Name.Equals("PersonName"))
                            {
                                dataGridView.Rows.Add();
                                selectedCell = dataGridView.Rows[i].Cells[0];
                            }
                            else if (doc.Name.Equals("phone"))
                            {
                                selectedCell = dataGridView.Rows[i].Cells[1];
                            }
                            break;
                        case XmlNodeType.Text: //Display the text in each element.
                            selectedCell.Value = doc.Value;
                            break;
                        case XmlNodeType.EndElement: //Display the end of the element.
                            if (doc.Name.Equals("phone"))
                            {
                                i++;
                            }
                            break;
                    }
                }
                
                
                // put the data from this text file intogrid view
               /* String line;
                short i = 0;
                line = doc.ReadLine();
                if (line == null)
                {
                    MessageBox.Show("The test file is empty");
                }
                else
                {
                    while (line != null)
                    {
                        MessageBox.Show(line);
                        // THERE IS AN ERROR HERE
                        dataGridView.Rows[i].Cells[0].Value = line; // this populates the "name column with the lines of file



                        line = doc.ReadLine();
                        i++;
                    }
                }
                */
                    doc.Close();
            } catch (System.IO.FileNotFoundException e) {
                newDoc = true;
            }

            
        }
        private int lastIndex = -1;
        private void button1_Click(object sender, EventArgs e)
        {
            addToList();
        }

        private void addToList()
        {
            lastIndex++;
            dataGridView.Rows.Add();
            dataGridView.Rows[dataGridView.RowCount - 1].Cells[0].Value = PersonTextBox.Text;
            dataGridView.Rows[dataGridView.RowCount - 1].Cells[1].Value = PhoneTextBox.Text;
            PersonTextBox.Text = "";
            PhoneTextBox.Text = "";
        }

        // add to list
        // if add button is pressed
        private void Form1_EnterPress(object sender, System.Windows.Forms.KeyEventArgs e) {
            addToList();
        }
        // if enter is pressed in name box, make user select phone number box
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == '\r')// adds to list if "enter" key is pressed
            {
                //addToList();
                PhoneTextBox.Select();
            }
            
        }
        // if enter is pressed in phone number box, add the person to the list
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')// adds to list if "enter" key is pressed
            {
                addToList();
                PersonTextBox.Select();
            }
        }

        // remove rows
        private int selectedRow = -1;
        private void button2_Click(object sender, EventArgs e)
        {
            // Remove selected row (incomplete)
            if (selectedRow == -1)
            {
                MessageBox.Show("Please select the row to delete");
            } else {
                dataGridView.Rows.RemoveAt(selectedRow);
                if (selectedRow == dataGridView.Rows.Count)
                {
                    selectedRow --;
                }

                lastIndex--;
            }
            
            
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //if (newDoc)
            //{
                // make a new .txt doc and write the chart on it
                StreamWriter write = new StreamWriter(filePathName);
                write.WriteLine("Name\\tPhone");
                for (short i = 0; i < dataGridView.RowCount; i++)
                {
                    write.WriteLine(dataGridView.Rows[i].Cells[0].Value + "\t" + dataGridView.Rows[i].Cells[1].Value);
                }
            write.Close();
            //} else {
                // write the chart on the old doc

            //}
           
        }
    }
}
