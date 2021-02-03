using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.Threading;

namespace AppointmentScheduler
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            //Log file is under JesseAndersonScheduler\AppointmentScheduler\bin\Debug
            //I provided a shortcut in the main file.

            //Changing the region, culture and default language to russian then following the 
            //recommended reboot caused russian to appear. 
            //It was not enough to change just the windows region.
            CultureInfo currentCulture = Thread.CurrentThread.CurrentUICulture;
            if (currentCulture.TwoLetterISOLanguageName == "RU")
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("ru-RU");
            }
            InitializeComponent();
        }

        //Form Switch buttons
        private void Loginbutton_Click(object sender, EventArgs e)
        {
            string userName = UsertextBox.Text.ToString();
            string userPass = PasswordtextBox.Text.ToString();
            List<string> lines = new List<string> { " " };
            var userCreds = FormModel.SearchQuery("select * from user");
            //called 2x in code wasnt enough to make a full method when inline lambda was faster
            Action<List<String>> logging = writeToLog =>
            {
                foreach (string line in lines)
                {
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"loginlogs.txt", true))
                    {
                        file.WriteLine(line);
                    }
                }
            };
            bool nameTrue = false;
            bool passTrue = false;
            foreach (DataRow row in userCreds.Rows)
            {

                if (userName == row.ItemArray[1].ToString())
                {
                    nameTrue = true;
                }
                if (userPass == row.ItemArray[2].ToString())
                {
                    passTrue = true;
                }
                if (nameTrue && passTrue)
                {
                    FormModel.CurrentUserID = row.ItemArray[0].ToString();
                    FormModel.CurrentUser = row.ItemArray[1].ToString();
                    lines.Add($"UserID = \"{row.ItemArray[0].ToString()}\"");
                    lines.Add($"UserName = \"{row.ItemArray[1].ToString()}\"");
                    lines.Add($"Time = \"{DateTime.UtcNow}\"");
                    lines.Add($"Success = \"True\"");
                    lines.Add($"");
                    logging(lines);
                    var tempTable = FormModel.SearchQuery("select c.customerName, a.start, a.type from appointment a inner join customer c on a.customerID = c.customerID;");
                    foreach (DataRow nextAppt in tempTable.Rows)
                    {
                        if (DateTime.UtcNow.AddHours(2) >= DateTime.Parse(nextAppt.ItemArray[1].ToString()) && 
                            DateTime.UtcNow <= DateTime.Parse(nextAppt.ItemArray[1].ToString()))
                        {
                            MessageBox.Show($"Urgent! There is an upcoming appointment with " +
                                $"{nextAppt.ItemArray[0].ToString()} at " +
                                $"{nextAppt.ItemArray[1].ToString()} for " +
                                $"{nextAppt.ItemArray[2].ToString()}.");
                        }
                    }
                    this.Hide();
                    Mainscreen mainscreen = new Mainscreen();
                    mainscreen.Show();
                    break;
                }
            }
            if (!(nameTrue && passTrue))
            {
                lines.Add($"UserName = \"{UsertextBox.Text}\"");
                lines.Add($"UserPass = \"{PasswordtextBox.Text}\"");
                lines.Add($"Time = \"{DateTime.UtcNow}\"");
                lines.Add($"Success = \"Failed\"");
                lines.Add($"");
                MessageBox.Show($"{Properties.Resources.BadLogin}");
                logging(lines);
            }
        }
        private void UsertextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Loginbutton_Click(this, new EventArgs());
            }
        }
        private void PasswordtextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Loginbutton_Click(this, new EventArgs());
            }
        }

        //Exit Buttons 
        private void ExitButton_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
        private void Dashboard_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
