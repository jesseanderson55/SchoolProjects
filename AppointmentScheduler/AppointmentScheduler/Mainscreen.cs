using MySql.Data.MySqlClient;
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
    public partial class Mainscreen : Form
    {
        public int? MainIndx { get; set; } = null;
        public int? ComboBoxIndx { get; set; } = null;
        public bool WeekSelector { get; set; } = false;
        public DataTable returnedSelectTable { get; set; } = new DataTable { };
        public DataTable comboboxSelectTable { get; set; } = new DataTable { };

        public Mainscreen()
        {
            InitializeComponent();
            MonthlyRadio.Checked = true;
            comboBoxSelectTableLoad();
        }

        //Populates the combobox with the names from the sql server
        private void comboBoxSelectTableLoad()
        {
            comboboxSelectTable = FormModel.SearchQuery("select distinct customerName" +
                " Customer from customer");
            comboBox1.DataSource = comboboxSelectTable;
            comboBox1.DisplayMember = "Customer";
            comboBox1.SelectedItem = null;
            comboBox1.Text = "-customer select-";
        }

        //sends select statements and any arguements to populate DGV
        private void MainscreenSelectTableLoad()
        {
            returnedSelectTable = FormModel.DgvBuild("SELECT date_format(start, '%Y-%m-%d') Day," +
                " date_format(start, '%H %i') Time, appointmentID, phone Phone," +
                " customerName Customer FROM appointment a inner join customer c on" +
                " a.customerID=c.customerID inner join user u on a.userID=u.userID" +
                " inner join address ad on c.addressID=ad.addressID",
                CalenderDGV);
        }
        private void MainscreenSelectTableLoad(DateTime startDate, DateTime endDate)
        {
            FormModel.DgvBuild("SELECT date_format(start, '%Y-%m-%d') Day," +
                " date_format(start, '%H %i') Time, appointmentID, userName User," +
                " customerName Customer FROM appointment a inner join customer c on" +
                " a.customerID=c.customerID inner join user u on a.userID=u.userID where " +
                $"a.start>=STR_TO_DATE('{startDate}', '%c/%e/%Y') and " +
                $"a.end<= STR_TO_DATE('{endDate}', '%c/%e/%Y')",
                CalenderDGV);
            if (CalenderDGV.Rows.Count == 0)
            {
                pictureBox1.Visible = true;
                CalenderDGV.Visible = false;
            }
        }
        private void MainscreenSelectTableLoad(string custName)
        {
            returnedSelectTable = FormModel.DgvBuild("SELECT date_format(start, '%Y-%m-%d') Day," +
                " date_format(start, '%H %i') Time, appointmentID, userName User," +
                " customerName Customer FROM appointment a inner join customer c on" +
                " a.customerID=c.customerID inner join user u on a.userID=u.userID where " +
                $" c.customerName = \"{custName}\"",
                CalenderDGV);
            if (CalenderDGV.Rows.Count == 0)
            {
                pictureBox1.Visible = true;
                CalenderDGV.Visible = false;
            }
        }

        //when the DGV finishing databinding it clears selecting the first row.
        private void CalenderDGV_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            CalenderDGV.ClearSelection();
        }

        //highlights cells
        private void CalenderDGV_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                CalenderDGV.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.LightBlue;
            }
        }
        private void CalenderDGV_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                CalenderDGV.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Empty;
            }
        }

        //Sets the row index of the clicked cell
        private void CalenderDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            MainIndx = e.RowIndex;
        }
        private void CalenderDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CalenderDGV_CellClick(sender, e);
        }

        //radio buttons choosing selectable range for DGV
        private void MonthlyRadio_CheckedChanged(object sender, EventArgs e)
        {
            WeekSelector = false;
            MainscreenSelectTableLoad();
            monthCalendar1.BoldedDates = FormModel.CalendarBuild(returnedSelectTable).ToArray();
            monthCalendar1.SelectionRange = new SelectionRange(DateTime.Today, DateTime.Today);
        }
        private void WeeklyRadio_CheckedChanged(object sender, EventArgs e)
        {
            WeekSelector = true;
            DateTime baseDate = DateTime.Today;
            CalanderWeekHighlighter(baseDate);
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            CalenderDGV.Visible = true;
            MainIndx = null;
            if (comboBox1.SelectedItem != null)
            {
                string selected = comboBox1.GetItemText(comboBox1.SelectedItem);
                MainscreenSelectTableLoad(selected);
                CalenderDGV.ClearSelection();
            }
        }

        //highlights the week when the week radio button is selected
        private void CalanderWeekHighlighter(DateTime date)
        {
            DateTime thisWeekStart = date.AddDays(-(int)date.DayOfWeek);
            DateTime thisWeekEnd = thisWeekStart.AddDays(7).AddSeconds(-1);
            monthCalendar1.SelectionRange = new SelectionRange(thisWeekStart, thisWeekEnd);
            MainscreenSelectTableLoad(monthCalendar1.SelectionRange.Start,
              monthCalendar1.SelectionRange.End.AddDays(1));
        }

        ///when a date or week is selected this calls the highlighter 
        ///CalanderWeekHighlighter() or populates with a SQL statement
        ///MainscreenSelectTableLoad()
        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            pictureBox1.Visible = false;
            CalenderDGV.Visible = true;
            var endTime = monthCalendar1.SelectionRange.End.AddDays(1);
            if (WeekSelector == true)
            {
                DateTime select = monthCalendar1.SelectionRange.Start;
                CalanderWeekHighlighter(select);
                MainscreenSelectTableLoad(monthCalendar1.SelectionRange.Start, 
                    monthCalendar1.SelectionRange.End.AddDays(1));
            }
            if (WeekSelector == false)
            {
                MainscreenSelectTableLoad(monthCalendar1.SelectionRange.Start, 
                    monthCalendar1.SelectionRange.End.AddDays(1));
            }
        }

        //resets the DGV to display the full list.
        private void clearButton_Click(object sender, EventArgs e)
        {
            MainscreenSelectTableLoad();
            ComboBoxIndx = null;
            MainIndx = null;
            comboBox1.SelectedItem = null;
            comboBox1.Text = "-customer select-";
            pictureBox1.Visible = false;
            CalenderDGV.Visible = true;
        }

        //Form Switch buttons
        private void AppointmentButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            if (MainIndx >= 0)
            {
                string appointmentIDToPass = returnedSelectTable.Rows[(int)MainIndx].ItemArray[2].ToString();
                AppointmentForm appointmentForm = new AppointmentForm(appointmentIDToPass);
                ComboBoxIndx = null;
                MainIndx = null;
                appointmentForm.Show();
            }
            else
            {
                AppointmentForm appointmentForm = new AppointmentForm();
                ComboBoxIndx = null;
                appointmentForm.Show();
            }
        }
        private void CustomerButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            if (MainIndx >= 0)
            {
                string customerNameToPass = returnedSelectTable.Rows[(int)MainIndx].ItemArray[4].ToString();
                CustomerForm customerForm = new CustomerForm(customerNameToPass);
                customerForm.Show();
            }
            else
            {
                CustomerForm customerForm = new CustomerForm();
                customerForm.Show();
            }
        }

        //Program Exit Buttons
        private void LogoutButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Dashboard loginForm = new Dashboard();
            loginForm.Show();
        }
        private void Mainscreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            Dashboard loginForm = new Dashboard();
            loginForm.Show();
        }
    }
}
