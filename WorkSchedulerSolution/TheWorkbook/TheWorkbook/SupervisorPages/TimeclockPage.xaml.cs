using Syncfusion.DataSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheWorkbook.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TheWorkbook.SupervisorPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TimeclockPage : ContentPage
    {
        private List<TimeClockRecord> timeClockRecordsList = new List<TimeClockRecord>() { };
        bool timeClockInstanceActive = false;

        public TimeclockPage()
        {
            InitializeComponent();
            this.Title = "Time Clock";
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await FormModel.TimeClockAzureDatabaseQuery();
            await TimeClockListViewPopulate();
        }

        private async System.Threading.Tasks.Task TimeClockListViewPopulate()
        {
            bool existsAlreadyTimeClockRecord = false;
            if (App.Customers.Count > 0)
            {
                timeClockRecordsList.Clear();
                foreach (var item in App.timeClockRecords)
                {
                    if (timeClockRecordsList.Count > 0)
                    {
                        existsAlreadyTimeClockRecord = timeClockRecordsList.Where(u => u.ID == item.ID).Any();
                    }

                    if (!existsAlreadyTimeClockRecord)
                    {
                        timeClockRecordsList.Add(item);
                    }
                }

                TimeClockListView.ItemsSource = timeClockRecordsList;
                TimeClockListView.DataSource.SortDescriptors.Clear();

                TimeClockListView.DataSource.SortDescriptors.Add(new SortDescriptor { PropertyName = "clockedIn", Direction = Syncfusion.DataSource.ListSortDirection.Ascending });
            }
        }


        private void editTimeButton_Clicked(object sender, EventArgs e)
        {
            var selectedRecord = TimeClockListView.SelectedItem as TimeClockRecord;

            Navigation.PushAsync(new WorkerPages.TimeClockEditPage(selectedRecord));
        }

        private async void clockOutButton_Clicked(object sender, EventArgs e)
        {
            if (TimeClockListView.SelectedItem != null)
            {
                var selectedRecord = TimeClockListView.SelectedItem as TimeClockRecord;
                var matchedRecordToUpdate = App.timeClockRecords.Where(u => u.ID == selectedRecord.ID).FirstOrDefault();

                matchedRecordToUpdate.ClockedOut = DateTime.Now;

                try
                {
                    await App.mobileService.GetTable<TimeClockRecord>().UpdateAsync(matchedRecordToUpdate);

                    await TimeClockListViewPopulate();
                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", $"{ex}", "Ok.");
                }

                timeClockInstanceActive = false;
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Select the record to clock out.", "Ok.");
            }
        }

        private async void clockInButton_Clicked(object sender, EventArgs e)
        {
            TimeClockRecord timeClockRecord = new TimeClockRecord()
            {
                OwnerID = App.boss.ID,
                EmployeeID = App.user.ID,
                ClockedIn = DateTime.Now
            };
            try
            {
               await App.mobileService.GetTable<TimeClockRecord>().InsertAsync(timeClockRecord);
                App.timeClockRecords.Add(timeClockRecord);

                await TimeClockListViewPopulate();

                timeClockInstanceActive = true;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"{ex}", "Ok.");
            }
        }

        private async void sendHoursButton_Clicked(object sender, EventArgs e)
        {
            if (TimeClockListView.SelectedItem != null)
            {
                if ((TimeClockListView.SelectedItem as TimeClockRecord).ClockedOut != null)
                {
                    bool check = await Application.Current.MainPage.DisplayAlert("Query", "Send this selection to supervisors?", "Yes.", "Cancel.");
                    if (check)
                    {
                        var selectedRecord = TimeClockListView.SelectedItem as TimeClockRecord;
                        string startEndDateToPass = $"My hours. Start Time:{selectedRecord.ClockedIn} End Time:{selectedRecord.ClockedOut}";

                        Message message = new Message()
                        {
                            OwnerID = App.boss.ID,
                            SenderID = App.user.ID,
                            DateTimeSent = DateTime.Now,
                            TextMessage = startEndDateToPass,
                            ToSupervisors = true
                        };

                        try
                        {
                            await App.mobileService.GetTable<Message>().InsertAsync(message);
                        }
                        catch (Exception ex)
                        {
                            await Application.Current.MainPage.DisplayAlert("Error", $"{ex}", "OK");
                        }
                    }
                }
            }
        }
    }
}