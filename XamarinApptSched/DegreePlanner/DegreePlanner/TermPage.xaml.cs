using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DegreePlanner.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DegreePlanner
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TermPage : ContentPage
    {
        public Term termSelected;
        public TermPage()
        {
            InitializeComponent();


        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Term>();
                var terms = conn.Table<Term>().ToList();
                termListView.ItemsSource = terms;
            }
            termSelected = null;
            termListView.SelectedItem = null;
        }

        private void termListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            termSelected = termListView.SelectedItem as Term;
        }

        private void addButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TermPageAdd());
        }

        private void editButton_Clicked(object sender, EventArgs e)
        {
            if (termSelected != null)
            {
                Navigation.PushAsync(new TermPageDetails(termSelected));
            }
            else
            {
                DisplayAlert("Error", "Create and Select a Term First", "Ok");
            }
        }

        private void ToMainPage_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }

        private void courseButton_Clicked(object sender, EventArgs e)
        {
            if (termSelected != null)
            {
                Navigation.PushAsync(new CoursesPage(termSelected));
            }
            else
            {
                DisplayAlert("Error", "Create and Select a Term First", "Ok");
            }
        }
    }
}