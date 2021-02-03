using Syncfusion.SfCalendar.XForms;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TheWorkbook.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TheWorkbook.OwnerPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalendarPage : ContentPage
    {
        #region properties
        private List<JobFunctionView> jobFunctionViews = new List<JobFunctionView>() { };
        private CalendarEventCollection CalendarInlineEvents { get; set; } = new CalendarEventCollection();


        private List<string> pickerList = new List<string>() { };

        List<Request> todaysRequests = new List<Request>() { };
        #endregion

        #region constructors and setup
        public CalendarPage()
        {
            InitializeComponent();
            this.Title = "Work Calendar";
            pickerList.Add("Vacation");
            pickerList.Add("Family Emergency");
            pickerList.Add("Sick");
            pickerList.Add("Personal Day");
            pickerList.Add("Religious Holiday");
            pickerList.Add("Other");
            reasonPicker.ItemsSource = pickerList;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await CalendarPopulate();
            await FormModel.FullAzureDatabaseQuery();
        }

        private async System.Threading.Tasks.Task CalendarPopulate()
        {
            calendar.DataSource = null;
            bool existsAlreadyJobFuncV = false;
            if (App.Customers.Count > 0)
            {
                jobFunctionViews.Clear();
                CalendarInlineEvents.Clear();
                calendar.DataSource = null;

                foreach (var item in App.JobFunctions)
                {
                    if (jobFunctionViews.Count > 0)
                    {
                        existsAlreadyJobFuncV = jobFunctionViews.Where(u => u.JobID == item.JobID && u.DateScheduled == item.DateScheduled).Any();
                    }

                    if (!existsAlreadyJobFuncV)
                    {
                        JobFunctionView jobFunctionView = new JobFunctionView(item.JobID, item.DateScheduled)
                        {
                            ID = item.ID,
                            TaskID = item.TaskID,
                            DateScheduled = item.DateScheduled,
                        };

                        jobFunctionViews.Add(jobFunctionView);
                    }
                }

                foreach (JobFunctionView item in jobFunctionViews)
                {
                    CalendarInlineEvent workEvents = new CalendarInlineEvent();

                    workEvents.StartTime = item.DateScheduled;
                    workEvents.EndTime = item.DateScheduled.AddMinutes(30);
                    var findTaskName = App.Tasks.Where(u => u.ID == item.TaskID).FirstOrDefault();
                    workEvents.Subject = item.JobName + " " + findTaskName.TaskName;
                    workEvents.Color = (Color)Application.Current.Resources["buttonscolor"];

                    CalendarInlineEvents.Add(workEvents);
                }

                if (App.Requests.Count > 0)
                {
                    foreach (Request item in App.Requests)
                    {
                        if (item.ReasonForRequest == "Blocked Date")
                        {
                            CalendarInlineEvent blockedEvents = new CalendarInlineEvent();

                            blockedEvents.StartTime = item.RequestDate.Date;
                            blockedEvents.EndTime = item.RequestDate.AddDays(1).AddHours(-1);
                            blockedEvents.IsAllDay = true;
                            blockedEvents.Subject = "Date blocked from requests.";
                            blockedEvents.Color = Color.Black;
                        
                            CalendarInlineEvents.Add(blockedEvents);
                        }
                        else
                        {
                            CalendarInlineEvent employeeEvents = new CalendarInlineEvent();

                            employeeEvents.StartTime = item.RequestDate.Date;
                            employeeEvents.EndTime = item.RequestDate.AddDays(1).AddHours(-1);
                            employeeEvents.IsAllDay = true;
                            if (item.Approved == true)
                            {
                                employeeEvents.Subject = item.EmployeeName + " " + item.ReasonForRequest + " - Approved";
                                employeeEvents.Color = (Color)Application.Current.Resources["headerfootercolor"];
                            }
                            else
                            {
                                employeeEvents.Subject = item.EmployeeName + " " + item.ReasonForRequest + "- Not Approved";
                                employeeEvents.Color = (Color)Application.Current.Resources["highlightscolor"];
                            }
                            CalendarInlineEvents.Add(employeeEvents);
                        }
                    }
                }

                calendar.DataSource = CalendarInlineEvents;
           
            }
        }
        #endregion

        #region Navigation buttons
        private void NoticesButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new OwnerPages.NoticePage());
        }

        private async void cancelButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }


        private void jobsButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new OwnerPages.JobPage());
        }

        private void employeeButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new OwnerPages.EmployeePage());
        }
        #endregion

        #region Functional buttons
        private async void blockDatesButton_Clicked(object sender, EventArgs e)
        {
            if (calendar.SelectedDate != null)
            {
                Request requestAlready = App.Requests.Where(u => u.ReasonForRequest == "Blocked Date" && u.RequestDate.Date == calendar.SelectedDate.Value.Date).FirstOrDefault();

                if (requestAlready != null)
                {
                    try
                    {
                        await App.mobileService.GetTable<Request>().DeleteAsync(requestAlready);
                        App.Requests.Remove(requestAlready);
                        await CalendarPopulate();

                    }
                    catch (Exception ex)
                    {
                        await Application.Current.MainPage.DisplayAlert("Alert", $"{ex}", "Ok");
                    }
                }
                else
                {
                    bool query = await Application.Current.MainPage.DisplayAlert("Query", $"Blocking {calendar.SelectedDate.Value.Date.ToString("MMMM/dd/yyyy")} will prevent further requests but not scheduling.", "Ok", "Cancel");
                    if (query)
                    {
                        Request blackoutDate = new Request()
                        {
                            EmployeeID = App.user.ID,
                            OwnerID = App.user.ID,
                            Approved = true,
                            RequestDate = calendar.SelectedDate.Value,
                            EmployeeName = App.user.WorkerLastName,
                            ReasonForRequest = "Blocked Date"
                        };

                        try
                        {
                            await App.mobileService.GetTable<Request>().InsertAsync(blackoutDate);
                            App.Requests.Add(blackoutDate);
                            await CalendarPopulate();
                        }
                        catch (Exception ex)
                        {
                            await Application.Current.MainPage.DisplayAlert("Alert", $"{ex}", "Ok");
                        }
                    }
               
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Alert", "Please select a date.", "Ok");
            }
        }

        private async void viewRequestsButton_Clicked(object sender, EventArgs e)
        {
            if (calendar.SelectedDate != null)
            {
                var checkForRequestThisDate = App.Requests.Where(u => u.RequestDate == calendar.SelectedDate).Any();
                if (checkForRequestThisDate)
                {
                    overlay.IsEnabled = true;
                    overlay.IsVisible = true;
                    background.IsEnabled = false;

                    todaysRequests = App.Requests.Where(u => u.RequestDate == calendar.SelectedDate).ToList();

                    dateScheduledLabel.Text = todaysRequests[0].RequestDate.ToString("MMMM/dd/yyyy");
                    workerPicker.ItemsSource = todaysRequests.Select(u => u.EmployeeName).ToList();
                    workerPicker.SelectedIndex = 0;
                }
                else
                {
                    bool query = await Application.Current.MainPage.DisplayAlert("Query", "There are no requests at this date. " +
                        "New request?", "Yes", "No");
                    if (query)
                    {
                        newRequestButton.Text = "Save";
                        workerPicker.ItemsSource = App.WorkerList.Select(u => u.WorkerLastName).ToList();
                        overlay.IsEnabled = true;
                        overlay.IsVisible = true;
                        background.IsEnabled = false;
                        dateScheduledLabel.Text = calendar.SelectedDate.Value.ToString("MMMM/dd/yyyy");
                    }
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Alert", "Please select a date.", "Ok");
            }
        }
        #endregion

        #region overlay events
        private async void approveButton_Clicked(object sender, EventArgs e)
        {
            if (newRequestButton.Text == "Save")
            {
                await Application.Current.MainPage.DisplayAlert("Alert", "Select save to create and approve the request.", "Ok");
            }
            else
            {
                try
                {
                    var todaysRequests = App.Requests.Where(u => u.RequestDate == calendar.SelectedDate).ToList();

                    var tempToUpdate = todaysRequests.Where(u => u.EmployeeName == workerPicker.SelectedItem.ToString()).FirstOrDefault();
                    tempToUpdate.Approved = true;

                    await App.mobileService.GetTable<Request>().UpdateAsync(tempToUpdate);
                    App.Requests.Where(u => u.ID == tempToUpdate.ID).FirstOrDefault().Approved = true;

                    await CalendarPopulate();
                    workerPicker.ItemsSource = null;

                    overlay.IsEnabled = false;
                    overlay.IsVisible = false;
                    background.IsEnabled = true;
                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Alert", $"{ex}", "Ok");
                }
            }
        }

        private void workerPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (newRequestButton.Text != "Save")
            {
                if (workerPicker.SelectedItem != null)
                {
                    var selectedRequest = todaysRequests.Where(u => u.EmployeeName == workerPicker.SelectedItem.ToString()).FirstOrDefault();

                    reasonPicker.SelectedItem = selectedRequest.ReasonForRequest;
                }
            }
        }

        private async void newRequestButton_Clicked(object sender, EventArgs e)
        {
            if (newRequestButton.Text == "Save")
            {
                if (workerPicker.SelectedItem != null)
                {
                    if (reasonPicker.SelectedItem != null)
                    {
                        Request request = new Request()
                        {
                            EmployeeID = App.WorkerList.Where(u => u.WorkerLastName == workerPicker.SelectedItem.ToString()).FirstOrDefault().ID,
                            OwnerID = App.user.ID,
                            Approved = true,
                            RequestDate = calendar.SelectedDate.Value,
                            EmployeeName = workerPicker.SelectedItem.ToString(),
                            ReasonForRequest = reasonPicker.SelectedItem.ToString()
                        };

                        bool requestAlready = App.Requests.Where(u => u.EmployeeID == request.EmployeeID && u.RequestDate == request.RequestDate).Any();

                        if (!requestAlready)
                        {
                            try
                            {
                                await App.mobileService.GetTable<Request>().InsertAsync(request);
                                App.Requests.Add(request);
                            }
                            catch (Exception ex)
                            {
                                await Application.Current.MainPage.DisplayAlert("Alert", $"{ex}", "Ok");
                            }
                            await Application.Current.MainPage.DisplayAlert("Success", "Request created and approved.", "Ok");
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert("Failed", "Employee has already requested that day off.", "Ok");
                        }

                        await CalendarPopulate();
                        newRequestButton.Text = "New";
                        overlay.IsEnabled = false;
                        overlay.IsVisible = false;
                        background.IsEnabled = true;
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Alert", "Select a reason.", "Ok");
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Alert", "Select a worker.", "Ok");
                }
            }
            else
            {
                newRequestButton.Text = "Save";
                workerPicker.ItemsSource = App.WorkerList.Select(u => u.WorkerLastName).ToList();
            }
        }

        private async void denyButton_Clicked(object sender, EventArgs e)
        {
            if (newRequestButton.Text == "Save")
            {
                overlay.IsEnabled = false;
                overlay.IsVisible = false;
                background.IsEnabled = true;
                newRequestButton.Text = "New";
                await Application.Current.MainPage.DisplayAlert("Alert", "Choose a request you'd like to deny.", "Ok");
            }
            else
            {
                try
                {
                    var todaysRequests = App.Requests.Where(u => u.RequestDate == calendar.SelectedDate).ToList();

                    var tempToDelete = todaysRequests.Where(u => u.EmployeeName == workerPicker.SelectedItem.ToString()).FirstOrDefault();

                    try
                    {
                        await App.mobileService.GetTable<Request>().UpdateAsync(tempToDelete);
                    }
                    catch (Exception ex)
                    {
                        await Application.Current.MainPage.DisplayAlert("Alert", $"{ex}", "Ok");
                    }
                    App.Requests.Remove(tempToDelete);

                    await CalendarPopulate();
                    workerPicker.ItemsSource = null;

                    overlay.IsEnabled = false;
                    overlay.IsVisible = false;
                    background.IsEnabled = true;
                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Alert", $"{ex}", "Ok");
                }
            }
        }

        private void cancelOverlayButton_Clicked(object sender, EventArgs e)
        {
            workerPicker.ItemsSource = null;

            overlay.IsEnabled = false;
            overlay.IsVisible = false;
            background.IsEnabled = true;
            newRequestButton.Text = "New";
        }
        #endregion
    }
}