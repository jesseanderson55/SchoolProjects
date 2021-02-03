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
    public partial class CustomerForm : Form
    {
        public int? CustIndx { get; set; } = null;
        public DataTable returnedSelectTable { get; set; } = new DataTable { };
        public string CustomerNamePassed { get; set; }

        //constructors with/without passed values
        public CustomerForm()
        {
            InitializeComponent();
            CustomerSelectTableLoad();
        }
        public CustomerForm(string customerName)
        {
            CustomerNamePassed = customerName;
            InitializeComponent();
            CustomerSelectTableLoad();
            TextboxFillFromSelection();
        }

        //On databinding DGV clears selecting or passes the customerName from constructor to the Indicator
        private void customerDGV_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (CustomerNamePassed == null)
            {
                customerDGV.ClearSelection();
            }
            else
            {
                customerDGV.ClearSelection();
                //called 2x in code wasnt enough to make a full method when inline lambda was faster
                Action<string> indicator = customerName =>
                {
                    foreach (DataGridViewRow rowSearch in customerDGV.Rows)
                    {
                        if (rowSearch.Cells[1].Value.ToString().Equals(customerName))
                        {
                            rowSearch.Selected = true;
                            CustIndx = rowSearch.Index;
                            break;
                        }
                    }
                };
                indicator(CustomerNamePassed);
            }
        }

        //sends select statements to populate DGV
        private void CustomerSelectTableLoad()
        {
            returnedSelectTable = FormModel.DgvBuild("SELECT customerID ID, " + //0
                "customerName Name, phone Phone, active Active, " + //123
                "Address Address, city City, postalCode Zip, " + //456
                "country Country, c.customerID, c.addressID, ci.cityID, " +  //7 8 9 10
                "co.countryID FROM customer c inner join address a on " + //11
                "c.addressID=a.addressID inner join city ci on a.cityID=ci.cityID " +
                "inner join country co on ci.countryID=co.countryID",
                customerDGV);
            customerDGV.Columns[8].Visible = false;
            customerDGV.Columns[9].Visible = false;
            customerDGV.Columns[10].Visible = false;
            customerDGV.Columns[11].Visible = false;
        }

        //highlights cells
        private void customerDGV_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                customerDGV.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.LightBlue;
            }
        }
        private void customerDGV_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                customerDGV.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Empty;
            }
        }

        //Sets the row index of the clicked cell
        private void customerDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            CustIndx = e.RowIndex;
            TextboxFillFromSelection();
        }
        private void customerDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CustIndx = e.RowIndex;
            TextboxFillFromSelection();
        }

        //Fills the textboxes from the index selection
        private void TextboxFillFromSelection()
        {
            IDTextBox.Text = returnedSelectTable.Rows[(int)CustIndx].ItemArray[0].ToString(); //id
            IDTextBox.Visible = true;
            UserIDLabel.Visible = true;
            nameTextBox.Text = returnedSelectTable.Rows[(int)CustIndx].ItemArray[1].ToString(); //name
            phoneTextBox.Text = returnedSelectTable.Rows[(int)CustIndx].ItemArray[2].ToString(); //phone
            address1TextBox.Text = returnedSelectTable.Rows[(int)CustIndx].ItemArray[4].ToString(); //address
            cityTextBox.Text = returnedSelectTable.Rows[(int)CustIndx].ItemArray[5].ToString(); //city
            zipTextBox.Text = returnedSelectTable.Rows[(int)CustIndx].ItemArray[6].ToString(); //zip
            countryTextBox.Text = returnedSelectTable.Rows[(int)CustIndx].ItemArray[7].ToString(); //country
            if (returnedSelectTable.Rows[(int)CustIndx].ItemArray[3].Equals(false)) //active
            {
                noRadioButton.Checked = true;
                yesRadioButton.Checked = false;
            }
            else
            {
                noRadioButton.Checked = false;
                yesRadioButton.Checked = true;
            }
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            IDTextBox.Text = ""; //id
            nameTextBox.Text = ""; //name
            phoneTextBox.Text = ""; //phone
            address1TextBox.Text = ""; //address
            cityTextBox.Text = ""; //city
            zipTextBox.Text = ""; //zip
            countryTextBox.Text = ""; //country
            CustIndx = null;
            CustomerSelectTableLoad();
            customerDGV.ClearSelection();
            IDTextBox.Visible = false;
            CustomerIDLabel.Visible = false;
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Are you sure to delete this Customer?",
                                                     "Confirm Delete!", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                foreach (DataRow row in returnedSelectTable.Rows)
                {
                    if (IDTextBox.Text == row.ItemArray[0].ToString())
                    {
                        row.Delete();
                        CustIndx = null;
                        FormModel.DeleteNonQuery($"delete from customer where customerID = '{IDTextBox.Text}'; Commit;");
                    }
                }
            }
        }
        private void customerDGV_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DeleteButton_Click(sender, e);
            }
        }


        private void SaveButton_Click(object sender, EventArgs e)
        {
            DataTable tempTable = new DataTable { };
            string isfoundCustomerID = null;
            string isfoundCountry = null;
            string isfoundCity = null;
            string isfoundAddressPhoneID = null;
            DialogResult confirmResult;
            int readyToEnter = 0;
            if (nameTextBox.Text == "" || countryTextBox.Text == "" ||
                address1TextBox.Text == "" || phoneTextBox.Text == "" ||
                zipTextBox.Text == "" || cityTextBox.Text == "")
            {
                MessageBox.Show("Please be sure to fill all fields.");
            }
            else
            {
                readyToEnter++;
            }
            if (zipTextBox.Text.Length > 10)
            {
                MessageBox.Show("Zip Code is too long.");
            }
            else
            {
                readyToEnter++;
            }
            if (phoneTextBox.Text.Length > 20)
            {
                MessageBox.Show("Phone Number is too long.");
            }
            else
            {
                readyToEnter++;
            }
            if (readyToEnter == 3)
            {
                foreach (DataRow row in returnedSelectTable.Rows)
                {
                    if (IDTextBox.Text == row.ItemArray[0].ToString())
                    {
                        isfoundCustomerID = row.ItemArray[0].ToString();
                    }
                    if (countryTextBox.Text == row.ItemArray[7].ToString())
                    {
                        isfoundCountry = row.ItemArray[11].ToString();
                    }
                    if (cityTextBox.Text == row.ItemArray[5].ToString())
                    {
                        isfoundCity = row.ItemArray[10].ToString();
                    }
                    if (isfoundCustomerID != null && address1TextBox.Text == row.ItemArray[5].ToString() &&
                        phoneTextBox.Text == row.ItemArray[2].ToString())
                    {
                        isfoundAddressPhoneID = row.ItemArray[9].ToString();
                    }
                }

                if (isfoundCustomerID != null)
                {
                    confirmResult = MessageBox.Show("Are you sure to update this Customer?",
                                       "Confirm update?", MessageBoxButtons.YesNo);
                }
                else
                {
                    confirmResult = MessageBox.Show("Are you sure to create this Customer?",
                                       "Confirm creation?", MessageBoxButtons.YesNo);
                }

                if (confirmResult == DialogResult.Yes)
                {

                    if (isfoundCountry == null)
                    {
                        FormModel.InsertNonQuery("Insert into country " +
                            $"(country, createdate, createdby, lastupdate, lastupdateby) " +
                            $"values (\"{countryTextBox.Text}\", " +
                            $"'{DateTime.Now.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss tt")}', " +
                            $"\"{FormModel.CurrentUser}\", " +
                            $"'{DateTime.Now.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss tt")}', " +
                            $"\"{FormModel.CurrentUser}\");");
                        tempTable = FormModel.SearchQuery("select * from country;");
                        foreach (DataRow row in tempTable.Rows)
                        {
                            if (row.ItemArray[1].ToString() == countryTextBox.Text)
                            {
                                isfoundCountry = row.ItemArray[0].ToString();
                                break;
                            }
                        }
                    }
                    if (isfoundCity == null)
                    {
                        FormModel.InsertNonQuery("Insert into city " +
                            $"(city, countryID, createdate, createdby, lastupdate, lastupdateby) " +
                            $"values (\"{cityTextBox.Text}\", {isfoundCountry}, " +
                            $"'{DateTime.Now.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss tt")}', " +
                            $"\"{FormModel.CurrentUser}\", " +
                            $"'{DateTime.Now.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss tt")}', " +
                            $"\"{FormModel.CurrentUser}\");");
                        tempTable = FormModel.SearchQuery("select * from city;");
                        foreach (DataRow row in tempTable.Rows)
                        {
                            if (row.ItemArray[1].ToString() == cityTextBox.Text)
                            {
                                isfoundCity = row.ItemArray[0].ToString();
                                break;
                            }
                        }
                    }
                    if (isfoundAddressPhoneID == null)
                    {
                        FormModel.InsertNonQuery("Insert into address (address, " +
                        "cityID, postalCode, Phone, createdate, createdby, lastupdate, lastupdateby) " +
                        $"values (\"{address1TextBox.Text}\", {isfoundCity}, {zipTextBox.Text}, \"{phoneTextBox.Text}\", " +
                        $"'{DateTime.Now.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss tt")}', " +
                            $"\"{FormModel.CurrentUser}\", " +
                        $"'{DateTime.Now.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss tt")}', " +
                            $"\"{FormModel.CurrentUser}\");");
                        tempTable = FormModel.SearchQuery("select * from address;");
                        foreach (DataRow row in tempTable.Rows)
                        {

                            if (address1TextBox.Text == row.ItemArray[1].ToString() &&
                                phoneTextBox.Text == row.ItemArray[5].ToString() &&
                                DateTime.ParseExact(row.ItemArray[6].ToString(), "M/d/yyyy h:mm:ss tt",
                                System.Globalization.CultureInfo.InstalledUICulture).Hour == DateTime.UtcNow.Hour)
                            {
                                isfoundAddressPhoneID = row.ItemArray[0].ToString();
                                break;
                            }
                        }

                    }
                    if (isfoundCustomerID == null)
                    {
                        FormModel.InsertNonQuery("Insert into customer " +
                            "(customerName, addressID, active, createdate, createdby, lastupdate, lastupdateby) " +
                            $"values (\"{nameTextBox.Text}\", {isfoundAddressPhoneID}, {yesRadioButton.Checked}, " +
                            $"'{DateTime.Now.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss tt")}', " +
                            $"\"{FormModel.CurrentUser}\", " +
                            $"'{DateTime.Now.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss tt")}', " +
                            $"\"{FormModel.CurrentUser}\");");
                    }
                    else
                    {
                        FormModel.UpdateNonQuery("update customer " +
                        $"set customerName = \"{nameTextBox.Text}\", " +
                        $"addressID = {isfoundAddressPhoneID}, " +
                        $"active = {yesRadioButton.Checked}, " +
                        $"lastUpdate = '{DateTime.Now.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss tt")}', " +
                        $"lastupdateby = \"{FormModel.CurrentUser}\" " +
                        $"where customerID = {isfoundCustomerID};");
                    }
                }
                CustomerSelectTableLoad();
                //called 2x in code wasnt enough to make a full method when inline lambda was faster
                Action<string> indicator = customerName =>
                {
                    foreach (DataGridViewRow rowSearch in customerDGV.Rows)
                    {
                        if (rowSearch.Cells[1].Value.ToString().Equals(customerName))
                        {
                            rowSearch.Selected = true;
                            CustIndx = rowSearch.Index;
                            break;
                        }
                    }
                };
                indicator(CustomerNamePassed);
            }
        }

        //Form Switch buttons
        private void CustomerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            Mainscreen mainscreen = new Mainscreen();
            CustIndx = null;
            mainscreen.Show();
        }
        private void appointmentsButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            if (CustIndx != null)
            {
                string customerNameToPass = returnedSelectTable.Rows[(int)CustIndx].ItemArray[1].ToString();
                AppointmentForm appointmentForm = new AppointmentForm(customerNameToPass, null);
                CustIndx = null;
                appointmentForm.Show();
            }
            else
            {
                AppointmentForm appointmentForm = new AppointmentForm();
                appointmentForm.Show();
            }
        }
        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Mainscreen mainscreen = new Mainscreen();
            CustIndx = null;
            mainscreen.Show();
        }
    }
}              