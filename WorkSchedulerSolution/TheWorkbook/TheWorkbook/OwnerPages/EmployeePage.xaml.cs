using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheWorkbook.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TheWorkbook.OwnerPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EmployeePage : ContentPage
    {
        #region Constructor
        public EmployeePage()
        {
            InitializeComponent();
            listView.ItemsSource = App.WorkerList;
            this.Title = "Employees";
        }
        #endregion

        #region Navigation Buttons
        private void calendarButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new OwnerPages.CalendarPage());
        }

        private void NoticesButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new OwnerPages.NoticePage());
        }
        private void addEmployeeButton_Pressed(object sender, EventArgs e)
        {
            Navigation.PushAsync(new OwnerPages.EmployeeAddEditPage());
        }

        private void CrewsButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new OwnerPages.CrewPage());
        }
        #endregion

        #region Function Buttons
        private async void editEmployeeButton_Pressed(object sender, EventArgs e)
        {
            if (listView.SelectedItem != null)
            {
                await Navigation.PushAsync(new OwnerPages.EmployeeAddEditPage(listView.SelectedItem as Worker));
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Please select user to edit first.", "OK");
            }
        }

        private void listView_ItemHolding(object sender, Syncfusion.ListView.XForms.ItemHoldingEventArgs e)
        {
            editEmployeeButton_Pressed(sender, e);
        }
        #endregion
    }
}