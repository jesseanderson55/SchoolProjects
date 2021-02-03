using Syncfusion.DataSource;
using Syncfusion.XlsIO.Parser.Biff_Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheWorkbook.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TheWorkbook.SupervisorPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaskPage : ContentPage
    {
        public JobFunctionView passedJobFunctionView;
        private List<LaborView> laborViews = new List<LaborView>() { };
        bool jobsHere = true;

        public TaskPage(JobFunctionView jobFunctionView)
        {
            InitializeComponent();
            passedJobFunctionView = jobFunctionView;
            Title = $"{passedJobFunctionView.JobName}";
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await FormModel.LaborAndTasksAzureDatabaseQuery();
            await ListViewPopulate();

            List<JobFunction> jobfunctionsForThisUser = new List<JobFunction>() { };

            var thisUsersLabors = App.Labors.Where(u => u.EmployeeID == App.user.ID).ToList();

            foreach (Labor labor in thisUsersLabors)
            {
                jobfunctionsForThisUser = App.JobFunctions.Where(u => u.ID == labor.JobFunctionID && u.JobID == passedJobFunctionView.JobID
            && u.DateScheduled == passedJobFunctionView.DateScheduled).ToList();
            }

            if (jobfunctionsForThisUser.Count == 0)
            {
                jobsHere = false;
                await Application.Current.MainPage.DisplayAlert("Alert.", "There are no jobs here assigned to you. " +
                    "You will not be able to edit times.", "Ok");
            }
        }

        public async System.Threading.Tasks.Task ListViewPopulate()
        {
            laborViews.Clear();

            var groupOfPassedJobFunctions = App.JobFunctions.Where(u => u.JobID == passedJobFunctionView.JobID
            && u.DateScheduled == passedJobFunctionView.DateScheduled).ToList();

            foreach (JobFunction item in groupOfPassedJobFunctions)
            {
                var linqQueriedLaborViews = App.Labors.Where(u => u.JobFunctionID == item.ID).ToList();

                foreach (Labor labor in linqQueriedLaborViews)
                {
                    var tempTask = App.Tasks.Where(u => u.ID == item.TaskID).FirstOrDefault();
                    var tempTool = App.Tools.Where(u => u.ID == labor.ToolID).FirstOrDefault();


                    LaborView laborView = new LaborView()
                    {
                        ID = labor.ID,
                        EmployeeID = labor.EmployeeID,
                        TimeDateEnd = labor.TimeDateEnd,
                        TimeDateStart = labor.TimeDateStart,
                        JobFunctionID = labor.JobFunctionID,
                        TaskName = tempTask.TaskName,
                        ToolName = tempTool.ToolName,
                        ToolID = labor.ToolID
                    };

                    laborViews.Add(laborView);
                }
            }
            LaborClockListView.ItemsSource = laborViews;

            LaborClockListView.DataSource.SortDescriptors.Clear();

            LaborClockListView.DataSource.SortDescriptors.Add(new SortDescriptor { PropertyName = "TimeDateStart", Direction = Syncfusion.DataSource.ListSortDirection.Ascending });

        }

        private async void editTimeButton_Clicked(object sender, EventArgs e)
        {
            if (jobsHere == false)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "This job isnt assigned to you.", "Ok.");
            }
            else
            {
                var selectedRecord = LaborClockListView.SelectedItem as LaborView;

                Navigation.PushAsync(new WorkerPages.TimeClockEditPage(selectedRecord));
            }
        }

        private async void clockInButton_Clicked(object sender, EventArgs e)
        {
            if (jobsHere == false)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "This job isnt assigned to you.", "Ok.");
            }
            else
            {
                if (LaborClockListView.SelectedItem != null)
                {
                    var selectedLaborView = LaborClockListView.SelectedItem as LaborView;
                    if (selectedLaborView.TimeDateStart == null)
                    {
                        var selectedLabor = App.Labors.Where(u => u.ID == selectedLaborView.ID).FirstOrDefault();
                        selectedLabor.TimeDateStart = DateTime.Now;

                        try
                        {
                            await App.mobileService.GetTable<Labor>().UpdateAsync(selectedLabor);

                            await FormModel.LaborAndTasksAzureDatabaseQuery();
                            await ListViewPopulate();
                        }
                        catch (Exception ex)
                        {
                            await Application.Current.MainPage.DisplayAlert("Error", $"{ex}", "Ok.");
                        }
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Alert", "Select a task to begin", "Ok.");
                }
            }
        }

        private async void clockOutButton_Clicked(object sender, EventArgs e)
        {
            if (jobsHere == false)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "This job isnt assigned to you.", "Ok.");
            }
            else
            {
                if (LaborClockListView.SelectedItem != null)
                {
                    var selectedLabor = LaborClockListView.SelectedItem as LaborView;
                    var matchedLaborToUpdate = App.Labors.Where(u => u.ID == selectedLabor.ID).FirstOrDefault();

                    matchedLaborToUpdate.TimeDateEnd = DateTime.Now;

                    try
                    {
                        await App.mobileService.GetTable<Labor>().UpdateAsync(matchedLaborToUpdate);

                        await ListViewPopulate();
                    }
                    catch (Exception ex)
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", $"{ex}", "Ok.");
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Select the task to clock out.", "Ok.");
                }
            }
        }

        private async void sendHoursButton_Clicked(object sender, EventArgs e)
        {
            if (jobsHere == false)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "This job isnt assigned to you.", "Ok.");
            }
            else
            {
                bool allRecordsClockedOut = true;

                foreach (LaborView item in laborViews)
                {
                    if (item.TimeDateEnd == null && item.TimeDateStart != null)
                    {
                        allRecordsClockedOut = false;
                    }
                }

                if (!allRecordsClockedOut)
                {
                    await Application.Current.MainPage.DisplayAlert("Alert", "Please clock out of all active tasks first.", "Ok.");
                }
                else
                {
                    bool check = await Application.Current.MainPage.DisplayAlert("Query", "Send notice of completed job to supervisors?", "Yes.", "Cancel.");
                    if (check)
                    {
                        DateTime todaysDate = DateTime.Today;
                        long min = Math.Abs(todaysDate.Ticks - laborViews[0].TimeDateStart.Value.Ticks);
                        long max = Math.Abs(todaysDate.Ticks - laborViews[0].TimeDateStart.Value.Ticks);

                        LaborView firstJob = laborViews[0];
                        LaborView lastJob = laborViews[0];

                        foreach (LaborView item in laborViews)
                        {
                            long diff = Math.Abs(todaysDate.Ticks - item.TimeDateStart.Value.Ticks);

                            if (diff < min)
                            {
                                min = diff;

                                firstJob = item;
                            }

                            if (diff > min)
                            {
                                max = diff;

                                lastJob = item;
                            }
                        }

                        var jobCompleted = App.Jobs.Where(u => u.ID == passedJobFunctionView.JobID).FirstOrDefault();
                        string datesToPass = $"Hours for {jobCompleted.JobName}. Start Time:{firstJob.TimeDateStart.Value} End Time:{lastJob.TimeDateStart.Value}";

                        Message message = new Message()
                        {
                            OwnerID = App.boss.ID,
                            SenderID = App.user.ID,
                            DateTimeSent = DateTime.Now,
                            TextMessage = datesToPass,
                            ToSupervisors = true
                        };

                        try
                        {
                            await App.mobileService.GetTable<Message>().InsertAsync(message);
                            await Navigation.PopAsync();
                        }
                        catch (Exception ex)
                        {
                            await Application.Current.MainPage.DisplayAlert("Error", $"{ex}", "OK");
                        }
                    }
                }
            }
        }

        private void NoticesButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new WorkerPages.NoticePage());
        }
    }
}