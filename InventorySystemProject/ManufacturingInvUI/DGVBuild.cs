using InventoryLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ManufacturingInvUI
{
    public static class DGVBuild
    {
        public static void dgvStyle(DataGridView gridName)
        {
            gridName.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridName.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Yellow;
            gridName.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            gridName.RowHeadersVisible = false;
            gridName.AllowUserToAddRows = false;
        }

        public static void displProdDGV(DataGridView gridName, Product nameProd)
        {
            gridName.AutoGenerateColumns = true;
            gridName.DataSource = nameProd.AssociatedParts;
            gridName.ClearSelection();
        }

        public static void displProdDGV(DataGridView gridName)
        {
            gridName.AutoGenerateColumns = true;
            gridName.ClearSelection();
        }

        public static void displAllPartsDGV(DataGridView gridName)
        {
            gridName.AutoGenerateColumns = true;
            gridName.DataSource = Inventory.AllParts;
            gridName.ClearSelection();
        }
        public static void displProductsDGV(DataGridView gridName)
        {
            gridName.AutoGenerateColumns = true;
            gridName.DataSource = Inventory.Products;
            gridName.ClearSelection();
        }

        public static void displTempAssocPartsDGV(DataGridView gridName)
        {
            gridName.AutoGenerateColumns = true;
            gridName.DataSource = Inventory.TempAssocParts;
            gridName.ClearSelection();
        }
    }
}
