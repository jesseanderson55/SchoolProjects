using InventoryLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManufacturingInvUI
{
    public partial class MainScreen : Form
    {
        public int? PartIndx { get; set; } = null;
        public int? ProdIndx { get; set; } = null;

        private void MainScreen_Load(object sender, EventArgs e)
        {
            DGVBuild.dgvStyle(partListGrid);
            DGVBuild.dgvStyle(productListGrid);
            DGVBuild.displAllPartsDGV(partListGrid);
            DGVBuild.displProductsDGV(productListGrid);
        }

        public MainScreen()
        {
            InitializeComponent();
        }

        //When part or products are clicked, save the row index of the part clicked
        private void partListGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            PartIndx = e.RowIndex;
        }
        private void partListGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            PartIndx = e.RowIndex;
        }

        private void productListGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ProdIndx = e.RowIndex;
        }
        private void productListGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ProdIndx = e.RowIndex;
        }

        //delete buttons
        private void partDeleteButton_Click(object sender, EventArgs e)
        {
            if (PartIndx != null)
            {
                for (int j = 0; j < Inventory.AllParts.Count; j++)
                {

                    if (Inventory.AllParts[j].PartID == (int)partListGrid.Rows[(int)PartIndx].Cells[0].Value)
                    {
                        var confirmResult = MessageBox.Show("Are you sure to delete this item?",
                                     "Confirm Delete!", MessageBoxButtons.YesNo);
                        if (confirmResult == DialogResult.Yes)
                        {
                            Inventory.deletePart(j);
                        }
                    }
                }
                DGVBuild.displAllPartsDGV(partListGrid);
                PartIndx = null;
            }
            else
            {
                MessageBox.Show("Select a row");
            }
            partListGrid.ClearSelection();
        }

        private void productDeleteButton_Click(object sender, EventArgs e)
        {
            if (ProdIndx != null)
            {
                for (int j = 0; j < Inventory.Products.Count; j++)
                {
                    if (Inventory.Products[j].ProductID == (int)productListGrid.Rows[(int)ProdIndx].Cells[0].Value)
                    {
                        int productPartCount = Inventory.Products[j].AssociatedParts.Count;
                        if (productPartCount > 0)
                        {
                            MessageBox.Show("Remove associated parts before attempting to delete the product. ");
                        }
                        else
                        {
                            var confirmResult = MessageBox.Show("Are you sure to delete this item?",
                                     "Confirm Delete!", MessageBoxButtons.YesNo);
                            if (confirmResult == DialogResult.Yes)
                            {
                                Inventory.RemoveProduct(j);
                            }
                        }
                    }
                }
                DGVBuild.displProductsDGV(productListGrid);
                ProdIndx = null;
            }
            else
            {
                MessageBox.Show("Select a row");
            }
            productListGrid.ClearSelection();
        }

        //Search buttons. 
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
                        partListGrid.DataSource = Inventory.SearchPartList;
                    }
                }
            }
            if (!iffound)
            {
                MessageBox.Show("Nothing found. View Reset.");
                partListGrid.DataSource = Inventory.AllParts;
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

        private void productSearchButton_Click(object sender, EventArgs e)
        {
            bool iffound = false;
            if (productSearchTextBox.Text != "")
            {
                for (int j = 0; j < Inventory.Products.Count; j++)
                {
                    if (Inventory.Products[j].Name.ToUpper().Contains(productSearchTextBox.Text.ToUpper()))
                    {
                        iffound = true;
                        int duplicateSearch = 0;
                        for (int i = 0; i < Inventory.SearchProductList.Count; i++)
                        {
                            if (Inventory.SearchProductList[i].Name.ToUpper() == Inventory.Products[j].Name.ToUpper())
                            {
                                duplicateSearch++;
                            }
                        }
                        if (duplicateSearch == 0)
                        {
                            Inventory.SearchProductList.Add(Inventory.lookupProduct(j));
                        }
                        productListGrid.DataSource = Inventory.SearchProductList;
                    }
                }
            }
            if (!iffound)
            {
                MessageBox.Show("Nothing found. View Reset.");
                productListGrid.DataSource = Inventory.Products;
                Inventory.SearchProductList.Clear();
            }
        }
        private void productSearchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                productSearchButton_Click(this, new EventArgs());
            }
        }

        //Screen switchers. 
        private void partAddButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            AddModifyPartScreen addModifyPartScreen = new AddModifyPartScreen();
            ProdIndx = null;
            PartIndx = null;
            addModifyPartScreen.Show();
        }

        private void partModifyButton_Click(object sender, EventArgs e)
        {
            if (PartIndx != null)
            {
                this.Hide();
                AddModifyPartScreen addModifyPartScreen = new AddModifyPartScreen((int)PartIndx);
                PartIndx = null;
                ProdIndx = null;
                addModifyPartScreen.Show();
            }
            else
            {
                MessageBox.Show("Please select Part to Modify.");
            }
        }

        private void productAddButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            AddModifyProductScreen addModifyProductScreen = new AddModifyProductScreen();
            ProdIndx = null;
            PartIndx = null;
            addModifyProductScreen.Show();
        }

        private void productModifyButton_Click(object sender, EventArgs e)
        {
            if (ProdIndx != null)
            {
                this.Hide();
                AddModifyProductScreen addModifyProductScreen = new AddModifyProductScreen(Inventory.Products[(int)ProdIndx]);
                ProdIndx = null;
                PartIndx = null;
                addModifyProductScreen.Show();
            }
            else
            {
                MessageBox.Show("Please select Product to Modify.");
            }
        }

        //Application Exit Buttons
        private void MainScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }


    }
}
