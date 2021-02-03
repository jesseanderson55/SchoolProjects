using System;
using System.Collections.Generic;
using Microsoft.WindowsAzure.MobileServices;
using TheWorkbook.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TheWorkbook
{
    public partial class App : Application
    {
        //todo for continuing project
        //Add hash tables and encryption to passwords to and from the database
        //sqlite is only partially set up for offline capability. For adding later. 
        //Save username and password to sqlite db so automatic login occurs. Also inclulde a logout button.
        //Email verification for employees to prevent anyone from using an employers address to add themselves to the workforce
        //approval through the messaging system to view
        //prevent matching emails in the same business
        //activated the enter button for login
        //export to pdf for ios

        #region properties
        //azure db strings
        public static string DatabaseLocation = string.Empty;
        public static MobileServiceClient mobileService = new MobileServiceClient("https://workschedulerxam.azurewebsites.net");

        //set upon login to the db for use filtering queries from the azure db
        public static Worker user { get; set; }
        public static Worker boss { get; set; }

        //lists built from the azure db
        public static List<Worker> WorkerList = new List<Worker>() { };
        public static List<Labor> Labors = new List<Labor>() { };
        public static List<Tool> Tools = new List<Tool>() { };
        public static List<Customer> Customers = new List<Customer>() { };
        public static List<Job> Jobs = new List<Job>() { };
        public static List<Task> Tasks = new List<Task>() { };
        public static List<Note> Notes = new List<Note>() { };
        public static List<CustomerJob> CustomerJobs = new List<CustomerJob>() { };
        public static List<JobFunction> JobFunctions = new List<JobFunction>() { };
        public static List<Request> Requests = new List<Request>() { };
        public static List<Message> MessageList = new List<Message>() { };
        public static List<TimeClockRecord> timeClockRecords = new List<TimeClockRecord>() { };



        //variable set to prevent changes to the underlying sqlite db until the connection to azure is reestablished
        //public static bool online = true;
        #endregion

        #region constructors
        public App()
        {
            InitializeComponent();
        }

        public App(string databaseLocation)
        {
            InitializeComponent();

            //project discontinued. Syncfusion key removed.




            MainPage = new NavigationPage(new LoginPage())
            {
                BarBackgroundColor = Color.FromHex("683F32")
            };

            DatabaseLocation = databaseLocation;
        }
        #endregion


        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
