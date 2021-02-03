using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppointmentScheduler
{
    public partial class AppointmentForm : Form
    {
        bool preCreatedForm = false; //if a form isnt passed need to ID for combobox2 to clear it
        public int? AppIndx { get; set; } = null;
        public int? ComboBoxIndx { get; set; } = null;
        public DataTable returnedSelectTable { get; set; } = new DataTable { };
        public DataTable returnedIDTestTable { get; set; } = new DataTable { };
        public DataTable comboboxSelectTable { get; set; } = new DataTable { };
        public string appointmentIDPassed { get; set; }

        //constructors with/without passed values
        public AppointmentForm()
        {
            InitializeComponent();
            dateTimePickerLoad();
            AppointmentSelectTableLoad();
            comboBoxSelectTableLoad();
        }
        public AppointmentForm(string appointmentID)
        {
            preCreatedForm = true;
            appointmentIDPassed = appointmentID;
            InitializeComponent();
            dateTimePickerLoad();
            comboBoxSelectTableLoad();
            AppointmentSelectTableLoad();
            TextboxFillFromSelection();
        }
        public AppointmentForm(string customerName, string emptyID)
        {
            preCreatedForm = true;
            InitializeComponent();
            dateTimePickerLoad();
            comboBoxSelectTableLoad();
            comboBox1.SelectedIndex = comboBox1.FindStringExact(customerName);
            AppointmentSelectTableLoad(customerName);
        }

        //sends select statements and any arguements to populate DGV
        private void AppointmentSelectTableLoad()
        {
            returnedSelectTable = FormModel.DgvBuild("SELECT appointmentID ID, " + // 0
                "c.customerName Customer, title Title, location Location, " + // 123
                "contact Contact, type Type, url URL, " + // 456 
                "date_format(start, '%Y-%m-%d') Day, " + // 7
                "date_format(start, '%H %i') \"Start Time\", " + // 8
                "date_format(end, '%H %i') \"End Time\", " + // 9
                "description Description, a.customerID, date_format(end, '%Y-%m-%d') \"End Day\", " + // 10 11 12
                "ad.phone FROM appointment a " + // 13
                "INNER JOIN customer c ON a.customerID=c.customerID INNER JOIN " + 
                "address ad ON c.addressID=ad.addressID", 
                AppointmentsDGV);
            AppointmentsDGV.Columns[0].Visible = false;
            AppointmentsDGV.Columns[12].Visible = false;
            AppointmentsDGV.Columns[11].Visible = false;
            AppointmentsDGV.Columns[2].Visible = false;
            AppointmentsDGV.Columns[3].Visible = false;
            AppointmentsDGV.Columns[10].Visible = false;
            AppointmentsDGV.Columns[4].Visible = false;
            AppointmentsDGV.Columns[6].Visible = false;
            returnedIDTestTableLoad();
        }
        private void AppointmentSelectTableLoad(string custName)
        {
            returnedSelectTable = FormModel.DgvBuild("SELECT appointmentID ID, " + // 0
                "c.customerName Customer, title Title, location Location, " + // 123
                "contact Contact, type Type, url URL, " + // 456
                "date_format(start, '%Y-%m-%d') Day, " + // 7
                "date_format(start, '%H %i') \"Start Time\", " + // 8
                "date_format(end, '%H %i') \"End Time\", " + // 9
                "description Description, a.customerID, date_format(end, '%Y-%m-%d') \"End Time\", " + // 10 11 12
                "ad.phone FROM appointment a " + // 13
                "INNER JOIN customer c ON a.customerID=c.customerID INNER JOIN " + 
                "address ad ON c.addressID=ad.addressID where " + 
                $" c.customerName = \"{custName}\"", 
                AppointmentsDGV);
            AppointmentsDGV.Columns[0].Visible = false;
            AppointmentsDGV.Columns[12].Visible = false;
            AppointmentsDGV.Columns[11].Visible = false;
            AppointmentsDGV.Columns[2].Visible = false;
            AppointmentsDGV.Columns[3].Visible = false;
            AppointmentsDGV.Columns[10].Visible = false;
            AppointmentsDGV.Columns[4].Visible = false;
            AppointmentsDGV.Columns[6].Visible = false;
            if (AppointmentsDGV.Rows.Count == 0)
            {
                pictureBox1.Visible = true;
                AppointmentsDGV.Visible = false;
            }
            returnedIDTestTableLoad();
        }
        private void returnedIDTestTableLoad()
        {
            returnedIDTestTable = FormModel.SearchQuery("select * from appointment");
        }

        private void dateTimePickerLoad()
        {
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "ddd yyyy-MM-dd hh:mm tt";
            dateTimePicker2.Value = DateTime.Now;
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "ddd yyyy-MM-dd hh:mm tt";
        }

        //Populates the combobox with the names from the sql server
        private void comboBoxSelectTableLoad()
        {
            comboboxSelectTable = FormModel.SearchQuery("select distinct customerName" +
                " Customer, customerID from customer");
            comboBox1.DataSource = comboboxSelectTable;
            comboBox1.DisplayMember = "Customer";
            if (preCreatedForm == false)
            {
                comboBox1.SelectedItem = null;
                comboBox1.Text = "-customer select-";
            }
        }

        //Fills the textboxes from the index selection
        private void TextboxFillFromSelection()
        {
            if (AppIndx >= 0)
            {
                TypeTextbox.Text = AppointmentsDGV.Rows[(int)AppIndx].Cells[5].Value.ToString();
                comboBox1.SelectedIndex = comboBox1.FindStringExact(AppointmentsDGV.Rows[(int)AppIndx].Cells[1].Value.ToString());
                dateTimePicker1.Value = DateTime.ParseExact($"{AppointmentsDGV.Rows[(int)AppIndx].Cells[7].Value.ToString()}" + $" {AppointmentsDGV.Rows[(int)AppIndx].Cells[8].Value.ToString()}", "yyyy-MM-dd HH mm",
                                       System.Globalization.CultureInfo.InvariantCulture);
                dateTimePicker2.Value = DateTime.ParseExact($"{AppointmentsDGV.Rows[(int)AppIndx].Cells[12].Value.ToString()}" + $" {AppointmentsDGV.Rows[(int)AppIndx].Cells[9].Value.ToString()}", "yyyy-MM-dd HH mm",
                                       System.Globalization.CultureInfo.InvariantCulture);
            }
        }

        //Form Switch buttons
        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Mainscreen mainscreen = new Mainscreen();
            mainscreen.Show();
        }
        private void AppointmentForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            Mainscreen mainscreen = new Mainscreen();
            mainscreen.Show();
        }
        private void customerButton_Click(object sender, EventArgs e)
        {
            {
                this.Hide();
                if (AppIndx != null)
                {
                    string customerNameToPass = returnedSelectTable.Rows[(int)AppIndx].ItemArray[1].ToString();
                    CustomerForm customerForm = new CustomerForm(customerNameToPass);
                    customerForm.Show();
                }
                else
                {
                    CustomerForm customerForm = new CustomerForm();
                    customerForm.Show();
                }
            }
        }

        //Sets the row index of the clicked cell
        private void AppointmentsDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            AppIndx = e.RowIndex;
            TextboxFillFromSelection();
        }
        private void AppointmentsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            AppointmentsDGV_CellClick(sender, e);
        }
        
        //highlights current cell
        private void AppointmentsDGV_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                AppointmentsDGV.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.LightBlue;
            }
        }
        private void AppointmentsDGV_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                AppointmentsDGV.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Empty;
            }
        }

        //On databinding DGV clears selecting or passes the appointmentID from constructor to the Indicator
        private void AppointmentsDGV_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (appointmentIDPassed == null)
            {
                AppointmentsDGV.ClearSelection();
            }
            else
            {
                AppointmentsDGV.ClearSelection();
                Action<string> indicator = appointmentID =>
                {
                    foreach (DataGridViewRow rowSearch in AppointmentsDGV.Rows)
                    {
                        if (rowSearch.Cells[0].Value.ToString().Equals(appointmentID))
                        {
                            rowSearch.Selected = true;
                            AppIndx = rowSearch.Index;
                            break;
                        }
                    }
                };
                indicator(appointmentIDPassed);
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            bool existsAlready = false;
            if (AppIndx >= 0)
            {
                foreach (DataRow row in returnedIDTestTable.Rows)
                {
                    if (row.ItemArray[0].ToString() == AppointmentsDGV.Rows[(int)AppIndx].Cells[0].Value.ToString())
                    {
                        existsAlready = true;
                        break;
                    }
                }
                // FIX 
                //if (existsAlready)
                //{
                //    FormModel.UpdateNonQuery($"UPDATE appointment SET " +
                //        $"customerID = '{comboboxSelectTable.Rows[comboBox1.SelectedIndex].ItemArray[1].ToString()}', " +
                //        $"userID = '{FormModel.CurrentUserID.ToString()}', " +
                //        $"type = '{TypeTextbox.Text.ToString()}', start = '{dateTimePicker1.Value}', " +
                //        $"end = '{dateTimePicker2.Value}' " +
                //        $"WHERE (appointmentID = '{AppointmentsDGV.Rows[(int)AppIndx].Cells[0].Value.ToString()}');");
                //}
                //else
                //{
                //    FormModel.InsertNonQuery("insert into appointment" +
                //        " (customerID, userID, type, start, end, createDate, createdBy) VALUES " +
                //        $"('{comboboxSelectTable.Rows[comboBox1.SelectedIndex].ItemArray[1].ToString()}', " +
                //        //$"('{AppointmentsDGV.Rows[(int)AppIndx].Cells[11].Value.ToString()}', " +
                //        $"'{FormModel.CurrentUserID}', '{TypeTextbox.Text}', " +
                //        $"'{dateTimePicker1.Value.ToString("yyyy-MM-dd hh:mm")}', " +
                //        $"'{dateTimePicker2.Value.ToString("yyyy-MM-dd hh:mm")}', " +
                //        $"now(), '{FormModel.CurrentUser}'; " +
                //        $"Commit;");
                //}

            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {

        }


        private void resetButton_Click(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
            TypeTextbox.Text = ""; 
            AppointmentSelectTableLoad();
            AppointmentsDGV.ClearSelection();
            preCreatedForm = false;
            comboBox1.SelectedItem = null;
            comboBox1.Text = "-customer select-";
            AppIndx = null;
            pictureBox1.Visible = false;
            AppointmentsDGV.Visible = true;
        }

        //selects new view based on combo box selection
        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            AppointmentsDGV.Visible = true;
            AppIndx = null;
            if (comboBox1.SelectedItem != null)
            {
                string selected = comboBox1.GetItemText(comboBox1.SelectedItem);
                AppointmentSelectTableLoad(selected);
                AppointmentsDGV.ClearSelection();
            }
        }

        //allows DGV sort with no row selected
        private void AppointmentsDGV_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            AppointmentsDGV.ClearSelection();
        }

        //cell double click selects that customer only. 
        private void AppointmentsDGV_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
         {
            if (AppIndx >= 0)
            {
                comboBox1.SelectedIndex = comboBox1.FindStringExact(AppointmentsDGV.Rows[(int)AppIndx].Cells[1].Value.ToString());
                AppointmentSelectTableLoad(AppointmentsDGV.Rows[(int)AppIndx].Cells[1].Value.ToString());
            }
        }
        private void AppointmentsDGV_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            AppointmentsDGV_CellContentDoubleClick(sender, e);
        }
    }
}
