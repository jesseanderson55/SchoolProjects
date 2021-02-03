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
        bool preCreatedForm = false; //if a form isnt passed need to ID for combobox to clear it
        public int? AppIndx { get; set; } = null;
        public int? ComboBoxIndx { get; set; } = null;
        public DataTable returnedSelectTable { get; set; } = new DataTable { };
        public DataTable returnedIDTestTable { get; set; } = new DataTable { };
        public DataTable comboboxSelectTable { get; set; } = new DataTable { };
        public DataTable combobox2SelectTable { get; set; } = new DataTable { };
        public string appointmentIDPassed { get; set; }

        //constructors with/without passed values
        public AppointmentForm()
        {
            InitializeComponent();
            dateTimePickerLoad();
            AppointmentSelectTableLoad();
            comboBoxSelectTableLoad();
            returnedIDTestTableLoad();
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
            returnedIDTestTableLoad();
        }
        public AppointmentForm(string customerName, string emptyID)
        {
            preCreatedForm = true;
            InitializeComponent();
            dateTimePickerLoad();
            comboBoxSelectTableLoad();
            comboBox1.SelectedIndex = comboBox1.FindStringExact(customerName);
            AppointmentSelectTableLoad(customerName);
            returnedIDTestTableLoad();
        }

        //sends select statements and any arguements to populate DGV
        private void AppointmentSelectTableLoad()
        {
            returnedSelectTable = FormModel.DgvBuild("SELECT appointmentID ID, " + // 0
                "c.customerName Customer, title Title, location Location, " + // 123
                "contact Contact, type Type, url URL, " + // 456 
                "date_format(start, '%Y-%m-%d %H %i') \"Start Time\", " + // 7
                "date_format(end, '%Y-%m-%d %H %i') \"End Time\", " + // 8
                "description Description, a.customerID, " + // 9, 10
                "ad.phone FROM appointment a " + // 11
                "INNER JOIN customer c ON a.customerID=c.customerID INNER JOIN " +
                "address ad ON c.addressID=ad.addressID",
                AppointmentsDGV);
            foreach (DataRow row in returnedSelectTable.Rows)
            {
                DateTime dateTime = new DateTime { };
                dateTime = DateTime.ParseExact(row.ItemArray[7].ToString(), "yyyy-MM-dd HH mm",
                    System.Globalization.CultureInfo.InstalledUICulture).ToLocalTime();
                row.ItemArray[7] = dateTime.ToString();
            }
            foreach (DataRow row in returnedSelectTable.Rows)
            {
                DateTime dateTime = new DateTime { };
                dateTime = DateTime.ParseExact(row.ItemArray[8].ToString(), "yyyy-MM-dd HH mm",
                    System.Globalization.CultureInfo.InstalledUICulture).ToLocalTime();
                row.ItemArray[8] = dateTime.ToString();
            }
            foreach (DataGridViewRow row in AppointmentsDGV.Rows)
            {
                if (row.Index >= 0)
                {
                    DateTime dateTime = new DateTime { };
                    dateTime = DateTime.ParseExact(row.Cells[7].Value.ToString(), "yyyy-MM-dd HH mm",
                        System.Globalization.CultureInfo.InstalledUICulture).ToLocalTime();
                    row.Cells[7].Value = dateTime.ToString();
                }
            }
            foreach (DataGridViewRow row in AppointmentsDGV.Rows)
            {
                if (row.Index >= 0)
                {
                    DateTime dateTime = new DateTime { };
                    dateTime = DateTime.ParseExact(row.Cells[8].Value.ToString(), "yyyy-MM-dd HH mm",
                        System.Globalization.CultureInfo.InstalledUICulture).ToLocalTime();
                    row.Cells[8].Value = dateTime.ToString();
                }
            }
            AppointmentsDGV.Columns[0].Visible = false;
            AppointmentsDGV.Columns[2].Visible = false;
            AppointmentsDGV.Columns[3].Visible = false;
            AppointmentsDGV.Columns[4].Visible = false;
            AppointmentsDGV.Columns[6].Visible = false;
            AppointmentsDGV.Columns[9].Visible = false;
            AppointmentsDGV.Columns[10].Visible = false;
            AppointmentsDGV.Columns[11].Visible = false;
        }
        private void AppointmentSelectTableLoad(string custName)
        {
            returnedSelectTable = FormModel.DgvBuild("SELECT appointmentID ID, " + // 0
                "c.customerName Customer, title Title, location Location, " + // 123
                "contact Contact, type Type, url URL, " + // 456
                "date_format(start, '%Y-%m-%d %H %i') \"Start Time\", " + // 7
                "date_format(end, '%Y-%m-%d %H %i') \"End Time\", " + // 8
                "description Description, a.customerID, " + // 9 10
                "ad.phone FROM appointment a " + // 11
                "INNER JOIN customer c ON a.customerID=c.customerID INNER JOIN " +
                "address ad ON c.addressID=ad.addressID where " +
                $" c.customerName = \"{custName}\"",
                AppointmentsDGV);
            foreach (DataRow row in returnedSelectTable.Rows)
            {
                DateTime dateTime = new DateTime { };
                dateTime = DateTime.ParseExact(row.ItemArray[7].ToString(), "yyyy-MM-dd HH mm",
                    System.Globalization.CultureInfo.InstalledUICulture).ToLocalTime();
                row.ItemArray[7] = dateTime.ToString();
            }
            foreach (DataRow row in returnedSelectTable.Rows)
            {
                DateTime dateTime = new DateTime { };
                dateTime = DateTime.ParseExact(row.ItemArray[8].ToString(), "yyyy-MM-dd HH mm",
                    System.Globalization.CultureInfo.InstalledUICulture).ToLocalTime();
                row.ItemArray[8] = dateTime.ToString();
            }
            foreach (DataGridViewRow row in AppointmentsDGV.Rows)
            {
                if (row.Index >= 0)
                {
                    DateTime dateTime = new DateTime { };
                    dateTime = DateTime.ParseExact(row.Cells[7].Value.ToString(), "yyyy-MM-dd HH mm",
                        System.Globalization.CultureInfo.InstalledUICulture).ToLocalTime();
                    row.Cells[7].Value = dateTime.ToString();
                }
            }
            foreach (DataGridViewRow row in AppointmentsDGV.Rows)
            {
                if (row.Index >= 0)
                {
                    DateTime dateTime = new DateTime { };
                    dateTime = DateTime.ParseExact(row.Cells[8].Value.ToString(), "yyyy-MM-dd HH mm",
                        System.Globalization.CultureInfo.InstalledUICulture).ToLocalTime();
                    row.Cells[8].Value = dateTime.ToString();
                }
            }
            AppointmentsDGV.Columns[0].Visible = false;
            AppointmentsDGV.Columns[2].Visible = false;
            AppointmentsDGV.Columns[3].Visible = false;
            AppointmentsDGV.Columns[4].Visible = false;
            AppointmentsDGV.Columns[6].Visible = false;
            AppointmentsDGV.Columns[9].Visible = false;
            AppointmentsDGV.Columns[10].Visible = false;
            AppointmentsDGV.Columns[11].Visible = false;
            if (AppointmentsDGV.Rows.Count == 0)
            {
                pictureBox1.Visible = true;
                AppointmentsDGV.Visible = false;
            }
        }
        private void AppointmentSelectTableLoad(string typeName, int? emptyIndicator)
        {
            returnedSelectTable = FormModel.DgvBuild("SELECT appointmentID ID, " + // 0
                "c.customerName Customer, title Title, location Location, " + // 123
                "contact Contact, type Type, url URL, " + // 456
                "date_format(start, '%Y-%m-%d %H %i') \"Start Time\", " + // 7
                "date_format(end, '%Y-%m-%d %H %i') \"End Time\", " + // 8
                "description Description, a.customerID, " + // 9 10
                "ad.phone FROM appointment a " + // 11
                "INNER JOIN customer c ON a.customerID=c.customerID INNER JOIN " +
                "address ad ON c.addressID=ad.addressID where " +
                $" a.type = \"{typeName}\"",
                AppointmentsDGV);
            foreach (DataRow row in returnedSelectTable.Rows)
            {
                DateTime dateTime = new DateTime { };
                dateTime = DateTime.ParseExact(row.ItemArray[7].ToString(), "yyyy-MM-dd HH mm",
                    System.Globalization.CultureInfo.InstalledUICulture).ToLocalTime();
                row.ItemArray[7] = dateTime.ToString();
            }
            foreach (DataRow row in returnedSelectTable.Rows)
            {
                DateTime dateTime = new DateTime { };
                dateTime = DateTime.ParseExact(row.ItemArray[8].ToString(), "yyyy-MM-dd HH mm",
                    System.Globalization.CultureInfo.InstalledUICulture).ToLocalTime();
                row.ItemArray[8] = dateTime.ToString();
            }
            foreach (DataGridViewRow row in AppointmentsDGV.Rows)
            {
                if (row.Index >= 0)
                {
                    DateTime dateTime = new DateTime { };
                    dateTime = DateTime.ParseExact(row.Cells[7].Value.ToString(), "yyyy-MM-dd HH mm",
                        System.Globalization.CultureInfo.InstalledUICulture).ToLocalTime();
                    row.Cells[7].Value = dateTime.ToString();
                }
            }
            foreach (DataGridViewRow row in AppointmentsDGV.Rows)
            {
                if (row.Index >= 0)
                {
                    DateTime dateTime = new DateTime { };
                    dateTime = DateTime.ParseExact(row.Cells[8].Value.ToString(), "yyyy-MM-dd HH mm",
                        System.Globalization.CultureInfo.InstalledUICulture).ToLocalTime();
                    row.Cells[8].Value = dateTime.ToString();
                }
            }
            AppointmentsDGV.Columns[0].Visible = false;
            AppointmentsDGV.Columns[2].Visible = false;
            AppointmentsDGV.Columns[3].Visible = false;
            AppointmentsDGV.Columns[4].Visible = false;
            AppointmentsDGV.Columns[6].Visible = false;
            AppointmentsDGV.Columns[9].Visible = false;
            AppointmentsDGV.Columns[10].Visible = false;
            AppointmentsDGV.Columns[11].Visible = false;
            if (AppointmentsDGV.Rows.Count == 0)
            {
                pictureBox1.Visible = true;
                AppointmentsDGV.Visible = false;
            }
        }
        private void returnedIDTestTableLoad()
        {
            DateTime dateTime = new DateTime { };
            returnedIDTestTable = FormModel.SearchQuery("select * from appointment");
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
            combobox2SelectTable = FormModel.SearchQuery("select distinct type from appointment");
            comboBox2.DataSource = combobox2SelectTable;
            comboBox2.DisplayMember = "Type";
            comboBox2.SelectedItem = null;
            comboBox2.Text = "-type select-";
        }

        //sets up the format for the dateTimePicker
        private void dateTimePickerLoad()
        {
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "ddd yyyy-MM-dd hh:mm tt";
            dateTimePicker2.Value = DateTime.Now;
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "ddd yyyy-MM-dd hh:mm tt";
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
                //called 2x in code wasnt enough to make a full method when inline lambda was faster
                Action<string> indicator = appointmentID =>
                {
                    foreach (DataGridViewRow rowSearch in AppointmentsDGV.Rows)
                    {
                        if (rowSearch.Cells[0].Value.ToString().Equals(appointmentID))
                        {
                            rowSearch.Selected = true;
                            AppIndx = rowSearch.Index;
                            AppIDTextBox.Visible = true;
                            ApptIDLabel.Visible = true;
                            break;
                        }
                    }
                };
                indicator(appointmentIDPassed);
            }
        }

        //Fills the textboxes from the index selection
        private void TextboxFillFromSelection()
        {
            if (AppIndx >= 0)
            {
                AppIDTextBox.Text = AppointmentsDGV.Rows[(int)AppIndx].Cells[0].Value.ToString();
                TypeTextbox.Text = AppointmentsDGV.Rows[(int)AppIndx].Cells[5].Value.ToString();
                comboBox1.SelectedIndex = comboBox1.FindStringExact(AppointmentsDGV.Rows[(int)AppIndx].Cells[1].Value.ToString());
                dateTimePicker1.Value = DateTime.ParseExact(AppointmentsDGV.Rows[(int)AppIndx].Cells[7].Value.ToString(), "M/d/yyyy h:mm:ss tt",
                                       System.Globalization.CultureInfo.InvariantCulture);
                dateTimePicker2.Value = DateTime.ParseExact(AppointmentsDGV.Rows[(int)AppIndx].Cells[8].Value.ToString(), "M/d/yyyy h:mm:ss tt",
                                       System.Globalization.CultureInfo.InvariantCulture);
            }
        }

        //Sets the row index of the clicked cell
        private void AppointmentsDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            AppIndx = e.RowIndex;
            TextboxFillFromSelection();
            AppIDTextBox.Visible = true;
            ApptIDLabel.Visible = true;
        }
        private void AppointmentsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            AppointmentsDGV_CellClick(sender, e);
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

        //selects new view based on combo box selection
        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            AppointmentsDGV.Visible = true;
            AppIDTextBox.Visible = false;
            ApptIDLabel.Visible = false;
            AppIndx = null;
            AppIDTextBox.Text = "";
            if (comboBox1.SelectedItem != null)
            {
                string selected = comboBox1.GetItemText(comboBox1.SelectedItem);
                AppointmentSelectTableLoad(selected);
                AppointmentsDGV.ClearSelection();
            }
        }
        private void comboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            AppointmentsDGV.Visible = true;
            AppIDTextBox.Visible = false;
            ApptIDLabel.Visible = false;
            AppIndx = null;
            AppIDTextBox.Text = "";
            if (comboBox2.SelectedItem != null)
            {
                string selected = comboBox2.GetItemText(comboBox2.SelectedItem);
                AppointmentSelectTableLoad(selected, null);
                AppointmentsDGV.ClearSelection();
            }
        }


        //save button sends mySQLupdate or mySQLinsert commands with validation and UTC
        private void SaveButton_Click(object sender, EventArgs e)
        {
            int entryReady = 0;
            bool existsAlready = false;
            foreach (DataRow row in returnedIDTestTable.Rows)
            {
                if (AppIDTextBox != null && AppIDTextBox.Text != "" && row.ItemArray[0].ToString() == AppIDTextBox.Text)
                {
                    existsAlready = true;
                    break;
                }
            }
            if (TypeTextbox.Text.ToString() != "" && TypeTextbox.Text.Length > 3)
            {
                entryReady++;
            }
            else
            {
                MessageBox.Show("Please enter valid type. Must be 3 characters or more.");
            }
            if (dateTimePicker1.Value > DateTime.Now && 
                dateTimePicker2.Value > DateTime.Now &&
                dateTimePicker1.Value < dateTimePicker2.Value)
            {
                entryReady++;
            }
            else
            {
                MessageBox.Show("Dates must be in the future. And end date must be after start date.");
            }
            if (dateTimePicker1.Value.DayOfWeek != DayOfWeek.Saturday &&
                dateTimePicker1.Value.DayOfWeek != DayOfWeek.Sunday &&
                dateTimePicker2.Value.DayOfWeek != DayOfWeek.Saturday &&
                dateTimePicker2.Value.DayOfWeek != DayOfWeek.Sunday &&
                dateTimePicker1.Value.Hour >= 9 &&
                dateTimePicker2.Value.Hour <= 17)
            {
                entryReady++;
            }
            else
            {
                MessageBox.Show("Appointment times must be within business hours.");
            }

            bool overlapAppts = false;
            foreach (DataRow row in returnedIDTestTable.Rows)
            {
                if ((DateTime.Parse(row.ItemArray[9].ToString()).ToLocalTime() <= dateTimePicker1.Value &&
                    dateTimePicker1.Value <= DateTime.Parse(row.ItemArray[10].ToString()).ToLocalTime()) ||
                    (DateTime.Parse(row.ItemArray[9].ToString()).ToLocalTime() <= dateTimePicker2.Value &&
                    dateTimePicker2.Value <= DateTime.Parse(row.ItemArray[10].ToString()).ToLocalTime()) ||
                    (DateTime.Parse(row.ItemArray[9].ToString()).ToLocalTime() >= dateTimePicker1.Value &&
                    dateTimePicker2.Value >= DateTime.Parse(row.ItemArray[10].ToString()).ToLocalTime()))
                {
                    overlapAppts = true;
                    break;
                }
            }
            if (overlapAppts)
            {
                MessageBox.Show("Appointments cannot overlap.");
            }
            else
            {
                entryReady++;
            }
            if (existsAlready && entryReady == 4)
            {
                FormModel.UpdateNonQuery($"UPDATE appointment SET " +
                    $"customerID = '{comboboxSelectTable.Rows[comboBox1.SelectedIndex].ItemArray[1].ToString()}', " +
                    $"userID = '{FormModel.CurrentUserID.ToString()}', " +
                    $"type = '{TypeTextbox.Text}', " +
                    $"start = '{DateTime.Parse(dateTimePicker1.Value.ToString()).ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss tt")}', " +
                    $"end = '{DateTime.Parse(dateTimePicker2.Value.ToString()).ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss tt")}', " +
                    $"lastUpdate = '{DateTime.Now.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss tt")}', " +
                    $"lastupdateby = \"{FormModel.CurrentUser}\" " +
                    $"WHERE (appointmentID = '{AppIDTextBox.Text}');");
                AppointmentSelectTableLoad(comboboxSelectTable.Rows[comboBox1.SelectedIndex].ItemArray[0].ToString());
            }
            else if (entryReady == 4)
            {
                FormModel.InsertNonQuery("insert into appointment " +
                    "(customerID, userID, type, start, end, createDate, createdBy) VALUES " +
                    $"('{comboboxSelectTable.Rows[comboBox1.SelectedIndex].ItemArray[1].ToString()}', " +
                    $"'{FormModel.CurrentUserID}', '{TypeTextbox.Text}', " +
                    $"'{DateTime.Parse(dateTimePicker1.Value.ToString()).ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss tt")}', " +
                    $"'{DateTime.Parse(dateTimePicker2.Value.ToString()).ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss tt")}', " +
                    $"now(), '{FormModel.CurrentUser}');");
                AppointmentSelectTableLoad(comboboxSelectTable.Rows[comboBox1.SelectedIndex].ItemArray[0].ToString());
            }
            else
            {
                MessageBox.Show("Entry Failed");
            }
            comboBox1_SelectionChangeCommitted(new object(), new EventArgs());
            returnedIDTestTableLoad();
            combobox2SelectTable = FormModel.SearchQuery("select distinct type from appointment");
            comboBox2.DataSource = combobox2SelectTable;
            comboBox2.DisplayMember = "Type";
            comboBox2.SelectedItem = null;
            comboBox2.Text = "-type select-";
        }

        //deletes row if found
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Are you sure to delete this Appointment?",
                                    "Confirm Delete?", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                if (AppIndx >= 0)
                {
                    FormModel.DeleteNonQuery($"delete from appointment where appointmentID = '{AppointmentsDGV.Rows[(int)AppIndx].Cells[0].Value.ToString()}';");
                    AppointmentSelectTableLoad();
                }
            }
        }
        private void AppointmentsDGV_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DeleteButton_Click(sender, e);
            }
        }

        //resets for to original settings
        private void resetButton_Click(object sender, EventArgs e)
        {
            AppIDTextBox.Text = "";
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
            TypeTextbox.Text = "";
            AppointmentSelectTableLoad();
            AppointmentsDGV.ClearSelection();
            preCreatedForm = false;
            comboBox1.SelectedItem = null;
            comboBox1.Text = "-customer select-";
            comboBox2.SelectedItem = null;
            comboBox2.Text = "-type select-";
            AppIndx = null;
            pictureBox1.Visible = false;
            AppointmentsDGV.Visible = true;
            AppIDTextBox.Visible = false;
            ApptIDLabel.Visible = false;
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

    }
}
