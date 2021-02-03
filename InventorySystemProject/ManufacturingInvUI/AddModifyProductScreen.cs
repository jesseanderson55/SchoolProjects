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
    public partial class AddModifyProductScreen : Form
    {
        public int PartIndx { get; set; }
        public int ProdIndx { get; set; }
        public int ProdIDLookup { get; set; }
        bool creatingNewProdID;
        bool removeOldProd = false;
        int removeOldProdID;


        //constructors for the screen
        public AddModifyProductScreen()
        {
            InitializeComponent();
            DGVBuild.displProdDGV(dataGridView2);
            creatingNewProdID = true;
        }

        public AddModifyProductScreen(Product product)
        {
            InitializeComponent();
            creatingNewProdID = false;
            mainProductHeader.Text = "Modify Product";
            productIDTextBox.Text = product.ProductID.ToString();
            productNameTextBox.Text = product.Name;
            productInvTextBox.Text = product.InStock.ToString();
            productPriceTextBox.Text = product.Price.ToString();
            productMaxTextBox.Text = product.Max.ToString();
            productMinTextBox.Text = product.Min.ToString();
            //making the product ID read-only mode for modify
            productIDTextBox.ReadOnly = true;
            productIDTextBox.BackColor = Color.DarkGray;
            DGVBuild.displProdDGV(dataGridView2, product);
            ProdIDLookup = product.ProductID;
        }

        private void AddModifyProductScreen_Load(object sender, EventArgs e)
        {
            DGVBuild.dgvStyle(dataGridView1);
            DGVBuild.dgvStyle(dataGridView2);
            DGVBuild.displAllPartsDGV(dataGridView1);
        }

        //When part or products are clicked, save the row index of the part clicked
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            PartIndx = e.RowIndex;
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            PartIndx = e.RowIndex;
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ProdIndx = e.RowIndex;
        }
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ProdIndx = e.RowIndex;
        }

        //label activators/deactivators
        private void productIDTextBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (productIDTextBox.ReadOnly)
            {
                label1.Visible = true;
            }
        }
        private void productIDTextBox_MouseLeave(object sender, EventArgs e)
        {
            if (productIDTextBox.Text != "")
            {
                label1.Visible = false;
            }
        }

        //clears textbox flags when data is entered.
        private void productIDTextBox_TextChanged(object sender, EventArgs e)
        {
            label1.Visible = false;
            productIDTextBox.BackColor = default(Color);
        }
        private void productNameTextBox_TextChanged(object sender, EventArgs e)
        {
            label2.Visible = false;
            productNameTextBox.BackColor = default(Color);
        }
        private void productInvTextBox_TextChanged(object sender, EventArgs e)
        {
            label3.Visible = false;
            productInvTextBox.BackColor = default(Color);
        }
        private void productPriceTextBox_TextChanged(object sender, EventArgs e)
        {
            label4.Visible = false;
            productPriceTextBox.BackColor = default(Color);
        }
        private void productMaxTextBox_TextChanged(object sender, EventArgs e)
        {
            label5.Visible = false;
            productMaxTextBox.BackColor = default(Color);
        }
        private void productMinTextBox_TextChanged(object sender, EventArgs e)
        {
            label6.Visible = false;
            productMinTextBox.BackColor = default(Color);
        }

        //usage buttons
        private void partSearchButton_Click(object sender, EventArgs e)
        {
            bool iffound = false;
            if (partSearchTextBox.Text != "")
            {
                for (int j = 0; j < Inventory.AllParts.Count; j++)
                {
                    if (Inventory.AllParts[j].Name.ToUpper().Contains(partSearchTextBox.Text.ToUpper()))
                    {
                        iffound = true;
                        int duplicateSearch = 0;
                        for (int i = 0; i < Inventory.SearchPartList.Count; i++)
                        {
                            if (Inventory.SearchPartList[i].Name.ToUpper() == Inventory.AllParts[j].Name.ToUpper())
                            {
                                duplicateSearch++;
                            }
                        }
                        if (duplicateSearch == 0)
                        {
                            Inventory.SearchPartList.Add(Inventory.lookupPart(j));
                        }
                        dataGridView1.DataSource = Inventory.SearchPartList;
                    }
                }
            }
            if (!iffound)
            {
                MessageBox.Show("Nothing found. View Reset.");
                dataGridView1.DataSource = Inventory.AllParts;
                Inventory.SearchPartList.Clear();
            }
        }
        private void partSearchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                partSearchButton_Click(this, new EventArgs());
            }
        }

        private void productAddButton_Click(object sender, EventArgs e)
        {
            if (PartIndx >= 0)
            {
                if (!(creatingNewProdID))
                {
                    for (int j = 0; j < Inventory.Products.Count; j++)
                    {
                        if (Inventory.Products[j].ProductID == ProdIDLookup)
                        {
                            for (int m = 0; m < Inventory.AllParts.Count; m++)
                            {
                                if (Inventory.AllParts[m].PartID == (int)dataGridView1.Rows[PartIndx].Cells[0].Value)
                                {
                                    Inventory.Products[j].addAssociatedPart(Inventory.AllParts[m]);
                                }
                            }
                        }
                    }
                    DGVBuild.displProdDGV(dataGridView2);
                    ProdIndx = -1;
                }
                else
                {
                    for (int m = 0; m < Inventory.AllParts.Count; m++)
                    {
                        if (Inventory.AllParts[m].PartID == (int)dataGridView1.Rows[PartIndx].Cells[0].Value)
                        {
                            Inventory.TempAssocParts.Add(Inventory.AllParts[m]);
                        }
                    }
                    DGVBuild.displTempAssocPartsDGV(dataGridView2);
                    ProdIndx = -1;
                }
            }
            else
            {
                MessageBox.Show("Select an associated part row");
            }
            dataGridView2.ClearSelection();

        }

        private void productSaveButton_Click(object sender, EventArgs e)
        {
            bool entryReady = true;
            bool saveSucess = false;
            int parsedResult;
            decimal parsedDecResult;
            int j = 0;

            //if chain testing each textbox for content to see if its EntryReady
            //clear boxes when values are corrected
            if (productIDTextBox.Text != "")
            {
                if (!(int.TryParse(productIDTextBox.Text, out parsedResult)))
                {
                    label1.Text = "ProductID must be a number";
                    label1.Show();
                    productIDTextBox.BackColor = Color.DarkRed;
                    entryReady = false;
                }
            }
            else
            {
                label1.Text = "Do not leave blank";
                label1.Show();
                productIDTextBox.BackColor = Color.DarkRed;
                entryReady = false;
            }
            if (productNameTextBox.Text == "")
            {
                label2.Text = "Do not leave blank";
                label2.Show();
                productNameTextBox.BackColor = Color.DarkRed;
                entryReady = false;

            }
            if (productInvTextBox.Text != "")
            {
                if (!(int.TryParse(productInvTextBox.Text, out parsedResult)))
                {
                    label3.Text = "Inventory must be a number";
                    label3.Show();
                    productInvTextBox.BackColor = Color.DarkRed;
                    entryReady = false;
                }
            }
            else
            {
                label3.Text = "Do not leave blank";
                label3.Show();
                productInvTextBox.BackColor = Color.DarkRed;
                entryReady = false;
            }
            if (productPriceTextBox.Text != "")
            {
                if (!(decimal.TryParse(productPriceTextBox.Text, out parsedDecResult)))
                {
                    label4.Text = "Price/Cost must be a number";
                    label4.Show();
                    productPriceTextBox.BackColor = Color.DarkRed;
                    entryReady = false;
                }
            }
            else
            {
                label4.Text = "Do not leave blank";
                label4.Show();
                productPriceTextBox.BackColor = Color.DarkRed;
                entryReady = false;
            }
            if (productMaxTextBox.Text != "")
            {
                if (!(int.TryParse(productMaxTextBox.Text, out parsedResult)))
                {
                    label5.Text = "Max must be a number";
                    label5.Show();
                    productMaxTextBox.BackColor = Color.DarkRed;
                    entryReady = false;
                }
                else
                {
                    if (productMinTextBox.Text != "")
                    {
                        if (int.TryParse(productMaxTextBox.Text, out parsedResult))
                        {
                            if (int.Parse(productMaxTextBox.Text) < int.Parse(productMinTextBox.Text))
                            {
                                label5.Text = "Max must be greater than Min";
                                label5.Show();
                                productMaxTextBox.BackColor = Color.DarkRed;
                                productMinTextBox.BackColor = Color.DarkRed;
                                entryReady = false;
                            }
                            if (productInvTextBox.Text != "" && productMinTextBox.Text != "")
                            {
                                if (int.Parse(productInvTextBox.Text) < int.Parse(productMinTextBox.Text))
                                {
                                    label3.Text = "Inventory must be more than Min";
                                    label3.Show();
                                    productInvTextBox.BackColor = Color.DarkRed;
                                    productMinTextBox.BackColor = Color.DarkRed;
                                    entryReady = false;
                                }
                            }
                        }
                    }
                    if (productInvTextBox.Text != "")
                    {
                        if (int.TryParse(productInvTextBox.Text, out parsedResult))
                        {
                            if (int.Parse(productMaxTextBox.Text) < int.Parse(productInvTextBox.Text))
                            {
                                label3.Text = "Inventory must be less than Max";
                                label3.Show();
                                productInvTextBox.BackColor = Color.DarkRed;
                                productMaxTextBox.BackColor = Color.DarkRed;
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
                productMaxTextBox.BackColor = Color.DarkRed;
                entryReady = false;
            }
            if (productMinTextBox.Text != "")
            {
                if (!(int.TryParse(productMaxTextBox.Text, out parsedResult)))
                {
                    label6.Text = "Do not leave blank";
                    label6.Show();
                    productMinTextBox.BackColor = Color.DarkRed;
                    entryReady = false;
                }
            }
            else
            {
                label6.Text = "Do not leave blank";
                label6.Show();
                productMinTextBox.BackColor = Color.DarkRed;
                entryReady = false;
            }

            //checking to see if a new part already has the same partID
            //deleting part if modifying and allowing to create in next statement
            if (entryReady == true)
            {
                for (int i = 0; i < Inventory.Products.Count; i++)
                {
                    j = 0;
                    if (Inventory.Products[i].ProductID == int.Parse(productIDTextBox.Text) && creatingNewProdID == true)
                    {
                        MessageBox.Show("Please choose another Product ID. Cannot duplicate Product ID.");
                        j++;
                    }
                    else if (Inventory.Products[i].ProductID == int.Parse(productIDTextBox.Text) && creatingNewProdID == false)
                    {
                        j = 0;
                        removeOldProd = true;
                        removeOldProdID = i;
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
            if (j == 0 && entryReady == true)
            {
                
                Product tempProd = new Product(int.Parse(productIDTextBox.Text), productNameTextBox.Text,
                decimal.Parse(productPriceTextBox.Text), int.Parse(productInvTextBox.Text),
                int.Parse(productMinTextBox.Text), int.Parse(productMaxTextBox.Text));

                if (creatingNewProdID)
                {
                    for (int i = 0; i < Inventory.TempAssocParts.Count; i++)
                    {
                        tempProd.addAssociatedPart(Inventory.TempAssocParts[i]);
                    }
                }
                if (!(creatingNewProdID))
                {
                    for (int i = 0; i < Inventory.Products[removeOldProdID].AssociatedParts.Count; i++)
                    {
                        tempProd.addAssociatedPart(Inventory.Products[removeOldProdID].AssociatedParts[i]);
                    }
                }
                Inventory.addProduct(tempProd);
                saveSucess = true;
            }
            else
            {
                MessageBox.Show("Record save unsuccessful.");
            }
            if (saveSucess)
            {
                MessageBox.Show("Record saved successfully.");
                Inventory.TempAssocParts.Clear();
                if (removeOldProd)
                {
                    Inventory.RemoveProduct(removeOldProdID);
                }
                this.Hide();
                MainScreen MScreen = new MainScreen();
                MScreen.Show();
            }
        }

        private void productDeleteButton_Click(object sender, EventArgs e)
        {
            if (ProdIndx >= 0)
            {
                if (!(creatingNewProdID))
                {
                    for (int j = 0; j < Inventory.Products.Count; j++)
                    {
                        if (Inventory.Products[j].ProductID == ProdIDLookup)
                        {
                            Inventory.Products[j].removeAssociatedPart((int)dataGridView2.Rows[ProdIndx].Cells[0].Value);
                        }
                    }
                    DGVBuild.displProdDGV(dataGridView2);
                    ProdIndx = -1;
                }
                else
                {
                    for (int j = 0; j < Inventory.TempAssocParts.Count; j++)
                    {
                        if (Inventory.TempAssocParts[j].PartID  == (int)dataGridView2.Rows[PartIndx].Cells[0].Value)
                        {
                            Inventory.TempAssocParts.RemoveAt(j);
                        }
                    }
                    DGVBuild.displTempAssocPartsDGV(dataGridView2);
                    ProdIndx = -1;
                }
            }
            else
            {
                MessageBox.Show("Select an associated part row");
            }
            dataGridView2.ClearSelection();
        }


        //exit buttons
        private void productCancelButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainScreen MScreen = new MainScreen();
            MScreen.Show();
        }

        private void AddModifyProductScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            MainScreen MScreen = new MainScreen();
            MScreen.Show();
        }


    }
}
