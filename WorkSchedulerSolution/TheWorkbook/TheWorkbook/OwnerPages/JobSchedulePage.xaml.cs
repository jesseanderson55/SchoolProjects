using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheWorkbook.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TheWorkbook.OwnerPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class JobSchedulePage : ContentPage
    {
        Job selectedJob;

        #region Constructor
        public JobSchedulePage(Job selectedJob)
        {
            InitializeComponent();
            this.selectedJob = selectedJob;
            datePicker.MinimumDate = DateTime.Today;
        }
        #endregion

        #region Navigation Button
        private void cancelNewDate_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PopAsync();
        }
        #endregion

        private async void okNewDate_Clicked(object sender, EventArgs e)
        {
            if (timePicker.IsEnabled != true)
            {
                datePicker.IsEnabled = true;
                datePicker.IsVisible = false;
                timePicker.IsEnabled = true;
                timePicker.IsVisible = true;
            }
            else
            {
                var timeToPass = datePicker.Date;

                timeToPass = timeToPass + timePicker.Time; 
                
                
                await Application.Current.MainPage.DisplayAlert("Success", "Now please select the task.", "OK");

                await Navigation.PushAsync(new JobTaskToolPage(selectedJob, timeToPass));
            }
            
        }
    }
}