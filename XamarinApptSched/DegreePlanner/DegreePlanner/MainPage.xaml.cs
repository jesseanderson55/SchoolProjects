using DegreePlanner.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DegreePlanner
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Milestone>();
                var milestone = conn.Table<Milestone>().ToList();
                milestoneListView.ItemsSource = milestone;
            }
        }

        private void ToTermPage_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TermPage());
        }
    }
}
