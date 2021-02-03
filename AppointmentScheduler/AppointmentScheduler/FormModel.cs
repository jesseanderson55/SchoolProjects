using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppointmentScheduler
{
    public static class FormModel
    {
        public static string CurrentUser { get; set; } = null;
        public static string CurrentUserID { get; set; } = null;

        //application discontinued. Connection string invalid. 
        public static MySqlConnection myConnection { get; } = new MySqlConnection("Server = 0; Database = 0; Uid = 0; Pwd = 0"); // Your Connection String here

        //Searches DB and returns a datatable
        public static DataTable SearchQuery(string selectStatement)
        {
            var select = $"{selectStatement}";

            var dataAdapter = new MySqlDataAdapter(select, myConnection);
            var commandBuilder = new MySqlCommandBuilder(dataAdapter);

            var queryTable = new DataTable();
            dataAdapter.Fill(queryTable);
            return queryTable;
        }

        //Searches DB and returns into a DGV
        public static DataTable DgvBuild(string selectStatement, DataGridView gridName)
        {
            var select = $"{selectStatement}";
            var dataAdapter = new MySqlDataAdapter(select, myConnection);

            var commandBuilder = new MySqlCommandBuilder(dataAdapter);
            var returnedSelectTable = new DataTable();
            dataAdapter.Fill(returnedSelectTable);
            DgvStyle(gridName);
            gridName.DataSource = returnedSelectTable;
            return returnedSelectTable;
        }

        public static void InsertNonQuery(string insert)
        {
            using (MySqlCommand InsertSelection = new MySqlCommand(insert, myConnection))
                try
                {
                    myConnection.Open();
                    InsertSelection.ExecuteNonQuery();
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            myConnection.Close();
        }

        public static void UpdateNonQuery(string update)
        {
            using (MySqlCommand UpdateSelection = new MySqlCommand(update, myConnection))
                try
                {
                    myConnection.Open();
                    UpdateSelection.ExecuteNonQuery();
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            myConnection.Close();
        }

        public static void DeleteNonQuery(string delete)
        {
            using (MySqlCommand DeleteSelection = new MySqlCommand(delete, myConnection))
                try
                {
                    myConnection.Open();
                    DeleteSelection.ExecuteNonQuery();
                    MessageBox.Show("Delete successful");
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            myConnection.Close();
        }

        //Styles DGV and does highlighting of rows
        public static void DgvStyle(DataGridView gridName)
        {
            gridName.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridName.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Yellow;
            gridName.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            gridName.RowHeadersVisible = false;
            gridName.AllowUserToAddRows = false;
            gridName.ReadOnly = true;
        }

        public static List<DateTime> CalendarBuild(DataTable dataTable)
        {
            List<DateTime> appointmentList = new List<DateTime>();
            DateTime[] appointmentDates = { };
            foreach (DataRow row in dataTable.Rows)
            {
                if (row.ItemArray[0] != null)
                {
                    DateTime date = Convert.ToDateTime(row.ItemArray[0].ToString());
                    appointmentList.Add(date);
                }
            }
            return appointmentList;
        }
    }
}
