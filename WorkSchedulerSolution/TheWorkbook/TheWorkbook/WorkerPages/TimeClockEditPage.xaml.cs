using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheWorkbook.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TheWorkbook.WorkerPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TimeClockEditPage : ContentPage
    {
        LaborView selectedLabor;
        TimeClockRecord selectedRecord;
        int recordCounter = 0;
        DateTime startTime = DateTime.Now;
        DateTime endTime = DateTime.Now;


        #region Constructor
        public TimeClockEditPage(TimeClockRecord selectedRecord)
        {
            InitializeComponent();
            this.selectedRecord = selectedRecord;
            datePicker.HeaderText = "Start Date";
        }

        public TimeClockEditPage(LaborView selectedLabor)
        {
            InitializeComponent();
            this.selectedLabor = selectedLabor;
            datePicker.HeaderText = "Start Date";
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
            if (recordCounter == 0)
            {
                datePicker.IsEnabled = true;
                datePicker.IsVisible = false;
                timePicker.IsEnabled = true;
                timePicker.IsVisible = true;
                datePicker.HeaderText = "Start Time";

                recordCounter++;
            }
            else if (recordCounter == 1)
            {
                datePicker.IsEnabled = true;
                datePicker.IsVisible = true;
                timePicker.IsEnabled = false;
                timePicker.IsVisible = false;
                datePicker.HeaderText = "End Date";

                recordCounter++;

                startTime = datePicker.Date + timePicker.Time;

                datePicker.MinimumDate = startTime;
            }
            else if (recordCounter == 2)
            {
                datePicker.IsEnabled = true;
                datePicker.IsVisible = false;
                timePicker.IsEnabled = true;
                timePicker.IsVisible = true;
                datePicker.HeaderText = "End Time";

                recordCounter++;
            }
            else
            {
                endTime = datePicker.Date + timePicker.Time;

                if (selectedRecord != null)
                {
                    selectedRecord.ClockedIn = startTime;
                    selectedRecord.ClockedOut = endTime;

                    await App.mobileService.GetTable<TimeClockRecord>().UpdateAsync(selectedRecord);
                }
                else
                {
                    selectedLabor.TimeDateStart = startTime;
                    selectedLabor.TimeDateEnd = endTime;

                    await App.mobileService.GetTable<Labor>().UpdateAsync(selectedLabor);
                }

                await Navigation.PopAsync();
            }
        }
    }
}