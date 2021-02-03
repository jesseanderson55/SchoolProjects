using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DegreePlanner.Model;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DegreePlanner
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TermPageAdd : ContentPage
    {
        public TermPageAdd()
        {
            InitializeComponent();
        }

        private void saveTermButton_Pressed(object sender, EventArgs e)
        {
            try
            {
                if (termStartDatePicker.Date < termEndDatePicker.Date)
                {
                    Term term = new Term()
                    {
                        termName = termNameEntry.Text,
                        termStart = termStartDatePicker.Date,
                        termEnd = termEndDatePicker.Date
                    };

                    using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
                    {
                        conn.CreateTable<Term>();
                        int insertRowsSuccess = conn.Insert(term);

                        if (insertRowsSuccess > 0)
                        {
                            DisplayAlert("Success", "Term successfully created", "Ok");
                        }
                        else
                        {
                            DisplayAlert("Failure", "Term failed to be created", "Ok");
                        }
                    }
                    Navigation.PushAsync(new TermPage());
                }
                else
                {
                    DisplayAlert("Error", "Start date must preceed end date", "Ok");
                }
            }
            catch (NullReferenceException)
            {
                DisplayAlert("Error", "Please fill all forms", "Ok");
            }
        }
    }
}