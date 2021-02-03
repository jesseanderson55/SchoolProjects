using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InventoryLibrary;

namespace ManufacturingInvUI
{
    public partial class AddModifyPartScreen : Form
    {
        int ourInhouse = -1;
        bool creatingNewPartID;

        //constructors for form
        public AddModifyPartScreen()
        {
            InitializeComponent();
            inhouseRadio.Checked = true;
            creatingNewPartID = true;
        }

        public AddModifyPartScreen(int part)
        {
            InitializeComponent();
            creatingNewPartID = false;
            mainPartsHeader.Text = "Modify Part";
            textBox1.Text = Inventory.AllParts[part].PartID.ToString();
            textBox2.Text = Inventory.AllParts[part].Name.ToString();
            textBox3.Text = Inventory.AllParts[part].InStock.ToString();
            textBox4.Text = Inventory.AllParts[part].Price.ToString();
            textBox5.Text = Inventory.AllParts[part].Max.ToString();
            textBox7.Text = Inventory.AllParts[part].Min.ToString();
            var castPartType = Inventory.AllParts[part];
            if (Inventory.AllParts[part].GetType().Equals(typeof(Inhouse)))
            {
                Inhouse castToInhouse = (Inhouse)castPartType;
                textBox6.Text = castToInhouse.MachineID.ToString();
                textBox1.ReadOnly = true;
                textBox1.BackColor = Color.DarkGray;
                labelPartMachID.Text = "Machine ID";
                inhouseRadio.Checked = true;
            }
            if (Inventory.AllParts[part].GetType().Equals(typeof(Outsourced)))
            {
                Outsourced castToOutsourced = (Outsourced)castPartType;
                textBox6.Text = castToOutsourced.companyName.ToString();
                textBox1.ReadOnly = true;
                textBox1.BackColor = Color.DarkGray;
                labelPartMachID.Text = "Company Name";
                outsourcedRadio.Checked = true;
            }
        }

        //label activators/deactivators
        private void textBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (textBox1.ReadOnly)
            {
                label1.Visible = true;
            }
        }
        private void textBox1_MouseLeave(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                label1.Visible = false;
            }
        }

        //clears textbox flags when data is entered.
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            label1.Visible = false;
            textBox1.BackColor = default(Color);
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            label2.Visible = false;
            textBox2.BackColor = default(Color);
        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            label3.Visible = false;
            textBox3.BackColor = default(Color);
        }
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            label4.Visible = false;
            textBox4.BackColor = default(Color);
        }
        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            label5.Visible = false;
            textBox5.BackColor = default(Color);
        }
        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            label7.Visible = false;
            textBox7.BackColor = default(Color);
        }
        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            label6.Visible = false;
            textBox6.BackColor = default(Color);
        }

        //radio buttons
        private void inhouseRadio_CheckedChanged(object sender, EventArgs e)
        {
            labelPartMachID.Text = "Machine ID";
            ourInhouse = 0;
        }
        private void outsourcedRadio_CheckedChanged(object sender, EventArgs e)
        {
            labelPartMachID.Text = "Company Name";
            ourInhouse = 1;
        }

        //save button. 
        private void saveButton_Click(object sender, EventArgs e)
        {
            bool entryReady = true;
            bool saveSucess = false;
            int parsedResult;
            int j = 0;

            //if chain testing each textbox for content to see if its EntryReady
            //clear boxes when values are corrected
            if (textBox1.Text != "")
            {
                if (!(int.TryParse(textBox1.Text, out parsedResult)))
                {
                    label1.Text = "PartID must be a number";
                    label1.Show();
                    textBox1.BackColor = Color.DarkRed;
                    entryReady = false;
                }
            }
            else
            {
                label1.Text = "Do not leave blank";
                label1.Show();
                textBox1.BackColor = Color.DarkRed;
                entryReady = false;
            }
            if (textBox2.Text == "")
            {
                label2.Text = "Do not leave blank";
                label2.Show();
                textBox2.BackColor = Color.DarkRed;
                entryReady = false;

            }
            if (textBox3.Text != "")
            {
                if (!(int.TryParse(textBox3.Text, out parsedResult)))
                {
                    label3.Text = "Inventory must be a number";
                    label3.Show();
                    textBox3.BackColor = Color.DarkRed;
                    entryReady = false;
                }
            }
            else
            {
                label3.Text = "Do not leave blank";
                label3.Show();
                textBox3.BackColor = Color.DarkRed;
                entryReady = false;
            }
            if (textBox4.Text != "")
            {
                decimal parsedDecResult;
                if (!(decimal.TryParse(textBox3.Text, out parsedDecResult)))
                {
                    label4.Text = "Price/Cost must be a number";
                    label4.Show();
                    textBox4.BackColor = Color.DarkRed;
                    entryReady = false;
                }
            }
            else
            {
                label4.Text = "Do not leave blank";
                label4.Show();
                textBox4.BackColor = Color.DarkRed;
                entryReady = false;
            }
            if (textBox5.Text != "")
            {
                if (!(int.TryParse(textBox5.Text, out parsedResult)))
                {
                    label5.Text = "Max must be a number";
                    label5.Show();
                    textBox5.BackColor = Color.DarkRed;
                    entryReady = false;
                }
                else
                {
                    if (textBox7.Text != "")
                    {
                        if (int.TryParse(textBox7.Text, out parsedResult))
                        {
                            if (int.Parse(textBox5.Text) < int.Parse(textBox7.Text))
                            {
                                label5.Text = "Max must be greater than Min";
                                label5.Show();
                                textBox5.BackColor = Color.DarkRed;
                                textBox7.BackColor = Color.DarkRed;
                                entryReady = false;
                            }
                            if (textBox3.Text != "" && textBox7.Text != "")
                            {
                                if (int.TryParse(textBox3.Text, out parsedResult) && int.TryParse(textBox7.Text, out parsedResult))
                                {
                                    if (int.Parse(textBox3.Text) < int.Parse(textBox7.Text))
                                    {
                                        label3.Text = "Inventory must be more than Min";
                                        label3.Show();
                                        textBox3.BackColor = Color.DarkRed;
                                        textBox7.BackColor = Color.DarkRed;
                                        entryReady = false;
                                    }
                                }
                            }
                        }
                    }
                    if (textBox3.Text != "")
                    {
                        if (int.TryParse(textBox3.Text, out parsedResult))
                        {
                            if (int.Parse(textBox5.Text) < int.Parse(textBox3.Text))
                            {
                                label3.Text = "Inventory must be less than Max";
                                label3.Show();
                                textBox3.BackColor = Color.DarkRed;
                                textBox5.BackColor = Color.DarkRed;
                                entryReady = false;
                            }
                        }
                    }
                }
            }
            else
            {
                label5.Text = "Do not leave blank";
                label5.Show();
                textBox5.BackColor = Color.DarkRed;
                entryReady = false;
            }
            if (textBox6.Text != "")
            {
                if (!(int.TryParse(textBox6.Text, out parsedResult)) && ourInhouse == 0)
                {
                    label6.Text = "MachineID must be a number";
                    label6.Show();
                    textBox6.BackColor = Color.DarkRed;
                    entryReady = false;
                }
            }
            else
            {
                label6.Text = "Do not leave blank";
                label6.Show();
                textBox6.BackColor = Color.DarkRed;
                entryReady = false;
            }
            if (textBox7.Text != "")
            {
                if (!(int.TryParse(textBox7.Text, out parsedResult)))
                {
                    label7.Text = "Min must be a number";
                    label7.Show();
                    textBox7.BackColor = Color.DarkRed;
                    entryReady = false;
                }
            }
            else
            {
                label7.Text = "Do not leave blank";
                label7.Show();
                textBox7.BackColor = Color.DarkRed;
                entryReady = false;
            }

            //checking to see if a new part already has the same partID
            //deleting part if modifying and allowing to create in next statement
            if (entryReady == true)
            {
                for (int i = 0; i < Inventory.AllParts.Count; i++)
                {
                    j = 0;
                    if (Inventory.AllParts[i].PartID == int.Parse(textBox1.Text) && creatingNewPartID == true)
                    {
                        MessageBox.Show("Please choose another PartID. Cannot duplicate PartID.");
                        j++;
                    }
                    else if (Inventory.AllParts[i].PartID == int.Parse(textBox1.Text) && creatingNewPartID == false)
                    {
                        j = 0;
                        Inventory.deletePart(i);
                        break;
                    }
                    if (j > 0)
                    {
                        break;
                    }
                }
            }

            //creating new sub-part if new part has each Textbox filled (EntryReady)
            //and no partID conflicts for new part. Replaces part if from Modify.
            if (j == 0 && ourInhouse == 0 && entryReady == true)
            {
                var tempPart = new Inhouse(int.Parse(textBox6.Text),
                int.Parse(textBox1.Text), textBox2.Text,
                decimal.Parse(textBox4.Text), int.Parse(textBox3.Text),
                int.Parse(textBox7.Text), int.Parse(textBox5.Text));
                Inventory.addPart(tempPart);
                saveSucess = true;
            }
            else if (j == 0 && ourInhouse == 1 && entryReady == true)
            {
                var tempPart = new Outsourced(textBox6.Text,
                int.Parse(textBox1.Text), textBox2.Text,
                decimal.Parse(textBox4.Text), int.Parse(textBox3.Text),
                int.Parse(textBox5.Text), int.Parse(textBox7.Text));
                Inventory.addPart(tempPart);
                saveSucess = true;
            }
            else
            {
                MessageBox.Show("Record save unsuccessful.");
            }
            if (saveSucess)
            {
                MessageBox.Show("Record saved successfully.");
                this.Hide();
                MainScreen MScreen = new MainScreen();
                MScreen.Show();
            }
        }

        //exit buttons
        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainScreen MScreen = new MainScreen();
            MScreen.Show();
        }

        private void AddModifyPartScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            MainScreen MScreen = new MainScreen();
            MScreen.Show();
        }

    }
}
