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
    public partial class TermPageDetails : ContentPage
    {
        Term selectedTerm;
        public TermPageDetails(Term selectedTerm)
        {
            InitializeComponent();

            this.selectedTerm = selectedTerm;

            termNameEntry.Text = selectedTerm.termName;
            termStartDatePicker.Date = selectedTerm.termStart;
            termEndDatePicker.Date = selectedTerm.termEnd;
        }

        private void updateTermButton_Pressed(object sender, EventArgs e)
        {
            if (termStartDatePicker.Date < termEndDatePicker.Date)
            {
                selectedTerm.termStart = termStartDatePicker.Date;
                selectedTerm.termEnd = termEndDatePicker.Date;

                using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
                {
                    conn.CreateTable<Term>();
                    int insertRowsSuccess = conn.Update(selectedTerm);

                    if (insertRowsSuccess > 0)
                    {
                        DisplayAlert("Success", "Term successfully updated", "Ok");
                    }
                    else
                    {
                        DisplayAlert("Failure", "Term failed to be updated", "Ok");
                    }
                }
                Navigation.PushAsync(new TermPage());
            }
            else
            {
                DisplayAlert("Error", "Start date must preceed end date", "Ok");
            }
        }

        private void deleteButton_Clicked(object sender, EventArgs e)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Term>();
                int insertRowsSuccess = conn.Delete(selectedTerm);

                if (insertRowsSuccess > 0)
                {
                    DisplayAlert("Success", "Term successfully deleted", "Ok");
                }
                else
                {
                    DisplayAlert("Failure", "Term failed to be deleted", "Ok");
                }
            }
            Navigation.PushAsync(new TermPage());
        }

        private void ToMainPage_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }

        private void termButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TermPage());
        }
    }
}