using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;



namespace DegreePlanner
{
    public partial class App : Application
    {
        public static string DatabaseLocation = string.Empty;
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new StartPage())
            {
                BarBackgroundColor = Color.FromHex("#003057"),
                BarTextColor = Color.White,
            };
        }

        public App(string databaseLocation)
        {
            InitializeComponent();

            MainPage = new NavigationPage(new StartPage())
            {
                BarBackgroundColor = Color.FromHex("#003057"),
                BarTextColor = Color.White,
            }; 
            
            DatabaseLocation = databaseLocation;
        }

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
