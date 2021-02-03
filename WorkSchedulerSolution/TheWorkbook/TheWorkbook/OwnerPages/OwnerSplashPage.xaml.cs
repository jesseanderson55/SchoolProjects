using Syncfusion.DataSource;
using Syncfusion.ListView.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TheWorkbook.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TheWorkbook.OwnerPages
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OwnerSplashPage : ContentPage
    {
        private List<JobFunctionView> jobFunctionViews = new List<JobFunctionView>() { };

        #region constructor, on appearing, population function
        public OwnerSplashPage()
        {
            InitializeComponent();
 
            this.Title = App.user.CompanyName;


        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            
            JobFuncListViewPopulate();

        }

        private async void JobFuncListViewPopulate()
        {
            bool existsAlreadyJobFuncV = false;

            jobFunctionViews.Clear();

            List<JobFunction> todaysJobFunctions = App.JobFunctions.Where(u => u.DateScheduled.Date == DateTime.Today).ToList();

            foreach (var item in todaysJobFunctions)
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

                    bool laborAssigned = false;

                    if (App.Labors.Count > 0)
                    {
                        laborAssigned = App.Labors.Where(u => u.JobFunctionID == jobFunctionView.ID).Any();
                    }
                    if (laborAssigned)
                    {
                        jobFunctionView.WorkersAssigned = true;
                    }
                    else
                    {
                        jobFunctionView.WorkersAssigned = false;
                    }

                    jobFunctionViews.Add(jobFunctionView);
                }
            }

            if (jobFunctionViews.Count == 0)
            {
                await Application.Current.MainPage.DisplayAlert("Alert", "No Jobs Scheduled for today.", "OK");
            }
            listView.ItemsSource = jobFunctionViews;
            listView.DataSource.SortDescriptors.Clear();

            listView.DataSource.SortDescriptors.Add(new SortDescriptor { PropertyName = "HourToSortBy", Direction = Syncfusion.DataSource.ListSortDirection.Ascending });
        }
        #endregion

        #region navigation buttons
        private void employeesButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new OwnerPages.EmployeePage());
        }

        private void jobsButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new OwnerPages.JobPage());
        }

        private void CalendarButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new OwnerPages.CalendarPage());
        }

        private void noticeButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new OwnerPages.NoticePage());
        }

        private void reportsButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new OwnerPages.ReportPage());
        }

        private void userDetailsButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new userDetails());
        }

        private void NoticesButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new OwnerPages.NoticePage());
        }
        #endregion
    }
}