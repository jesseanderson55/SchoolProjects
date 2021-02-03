using Syncfusion.ListView.XForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using Syncfusion.XForms.PopupLayout;
using System.Text;
using System.Threading.Tasks;
using TheWorkbook.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Syncfusion.DataSource.Extensions;
using System.Collections;
using Syncfusion.DataSource;
using Xamarin.Essentials;
using System.ComponentModel;

namespace TheWorkbook.OwnerPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class JobPage : ContentPage
    {
        private List<JobFunctionView> jobFunctionViews = new List<JobFunctionView>() { };

        #region constructors and onappearing modifying button accessability
        public JobPage()
        {
            InitializeComponent();
            this.Title = "Scheduled Jobs";
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await FormModel.FullAzureDatabaseQuery();
            await GetTheClosestDate();
        }
        #endregion

        #region navigation buttons
        private void NoticesButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new OwnerPages.NoticePage());
        }

        private void allJobsButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new OwnerPages.JobAllPage());
        }

        private void assignWorkersButton_Clicked(object sender, EventArgs e)
        {
            var tempJobFuncView = JobFuncListView.SelectedItem as JobFunctionView;
            Navigation.PushAsync(new OwnerPages.JobWorkersPage(tempJobFuncView));
        }
        #endregion

        #region google & services on toolbar
        private async void refreshItem_Clicked(object sender, EventArgs e)
        {
            await FormModel.FullAzureDatabaseQuery();

            await GetTheClosestDate();
        }

        private async void navigateItem_Clicked(object sender, EventArgs e)
        {

            if (JobFuncListView.SelectedItem != null)
            {
                JobFunctionView jobViewSelected = JobFuncListView.SelectedItem as JobFunctionView;
                Job jobSelected = App.Jobs.Where(u => u.ID == jobViewSelected.JobID).FirstOrDefault();


                if (Device.RuntimePlatform == Device.iOS)
                {
                    //https://developer.apple.com/library/ios/featuredarticles/iPhoneURLScheme_Reference/MapLinks/MapLinks.html;
                    await Launcher.OpenAsync($"http://maps.apple.com/?q={jobSelected.JobStreetAddress}+{jobSelected.JobCity}+{jobSelected.JobState}");
                }
                else if (Device.RuntimePlatform == Device.Android)
                {
                    // open the maps app directly
                    await Launcher.OpenAsync($"geo:0,0?q={jobSelected.JobStreetAddress}+{jobSelected.JobCity}+{jobSelected.JobState}");
                }
                else if (Device.RuntimePlatform == Device.UWP)
                {
                    await Launcher.OpenAsync($"http://maps.apple.com/?q={jobSelected.JobStreetAddress}+{jobSelected.JobCity}+{jobSelected.JobState}");
                }
            }

        }
        private async void callItem_Clicked(object sender, EventArgs e)
        {
            if (JobFuncListView.SelectedItem != null)
            {
                string number = string.Empty;

                JobFunctionView jobViewSelected = JobFuncListView.SelectedItem as JobFunctionView;
                Job jobSelected = App.Jobs.Where(u => u.ID == jobViewSelected.JobID).FirstOrDefault();
                Customer custSelected = App.Customers.Where(u => u.ID == jobSelected.CustomerID).FirstOrDefault();

                if (custSelected.CustomerPhone2 != null)
                {
                    bool choice = await Application.Current.MainPage.DisplayAlert("Selection", $"Choose which number", $"{custSelected.CustomerPhone}", $"{custSelected.CustomerPhone2}");
                    if (choice)
                    {
                        number = custSelected.CustomerPhone;
                    }
                    else
                    {
                        number = custSelected.CustomerPhone2;
                    }

                }
                else
                {
                    number = custSelected.CustomerPhone;
                }

                try
                {
                    PhoneDialer.Open(number);
                }
                catch (ArgumentNullException anEx)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", $"{anEx}", "OK");
                    // Number was null or white space
                }
                catch (FeatureNotSupportedException ex)
                {
                    // Phone Dialer is not supported on this device.
                    await Application.Current.MainPage.DisplayAlert("Error", $"{ex}", "OK");
                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", $"{ex}", "OK");
                    // Other error has occurred.
                }

            }
        }
        #endregion

        private async void DeleteButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (JobFuncListView.SelectedItem != null)
                {

                    var tempJobFunctionView = JobFuncListView.SelectedItem as JobFunctionView;
                    var tempJobFunction = App.JobFunctions.Where(u => u.DateScheduled == tempJobFunctionView.DateScheduled && u.JobID == tempJobFunctionView.JobID).ToList();

                    foreach (JobFunction item in tempJobFunction)
                    {
                        App.JobFunctions.Remove(item);


                        await App.mobileService.GetTable<JobFunction>().DeleteAsync(item);
                    }

                    JobFuncListViewPopulate();
                    await Application.Current.MainPage.DisplayAlert("Success", $"{tempJobFunctionView.JobName} scheduled {tempJobFunctionView.DayMonthToGroupBy} deleted.", "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"{ex}", "OK");
            }
        }

        #region notes overlay and resources
        private void noteJobsButton_Clicked(object sender, EventArgs e)
        {
            if (JobFuncListView.SelectedItem != null)
            {
                overlay.IsEnabled = true;
                overlay.IsVisible = true;
                background.IsEnabled = false;
                NotesListViewPopulate();
            }
        }
        private void cancelNewNote_Clicked(object sender, EventArgs e)
        {
            overlay.IsEnabled = false;
            overlay.IsVisible = false;
            background.IsEnabled = true;
        }

        private async void saveNote_Clicked(object sender, EventArgs e)
        {
            bool insertSuccess = false;
            if (!string.IsNullOrEmpty(noteEditor.Text))
            {
                var tempJobID = (JobFuncListView.SelectedItem as JobFunctionView).JobID;

                if (notesListView.SelectedItem != null)
                {
                    var updateCheck = await Application.Current.MainPage.DisplayAlert("Query", $"Update Note?", "Yes", "No");

                    try
                    {
                        if (updateCheck == true)
                        {
                            var tempNote = (notesListView.SelectedItem as Note);
                            tempNote.JobNotes = noteEditor.Text;
                            await App.mobileService.GetTable<Note>().UpdateAsync(tempNote);
                            var tempAppNoteToUpdate = App.Notes.Where(u => u.ID == tempNote.ID).FirstOrDefault();
                            tempAppNoteToUpdate.JobNotes = noteEditor.Text;

                            insertSuccess = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", $"{ex}", "Yes");
                    }
                }
                else
                {
                    try
                    {
                        Note note = new Note()
                        {
                            JobNotes = noteEditor.Text,
                            JobID = tempJobID
                        };

                        await App.mobileService.GetTable<Note>().InsertAsync(note);
                        App.Notes.Add(note);
                        insertSuccess = true;
                        NotesListViewPopulate();
                    }
                    catch (Exception ex)
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", $"{ex}", "Yes");
                    }
                }
                if (insertSuccess)
                {
                    await Application.Current.MainPage.DisplayAlert("Success", $"Note for {(JobFuncListView.SelectedItem as JobFunctionView).CustomerName} saved.", "OK");
                }
            }
        }

        private async void deleteNewNote_Clicked(object sender, EventArgs e)
        {
            if (notesListView.SelectedItem != null)
            {
                var tempNoteToDelete = notesListView.SelectedItem as Note;
                await App.mobileService.GetTable<Note>().DeleteAsync(tempNoteToDelete);
                App.Notes.Remove(tempNoteToDelete);
                NotesListViewPopulate();
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Success", $"Note for {(JobFuncListView.SelectedItem as JobFunctionView).CustomerName} saved.", "OK");
            }
        }

        private void NotesListViewPopulate()
        {
            var tempJobFuncsToGetNotes = JobFuncListView.SelectedItem as JobFunctionView;
            var tempNotesList = App.Notes.Where(u => u.JobID == tempJobFuncsToGetNotes.JobID).ToList();
            notesListView.ItemsSource = tempNotesList;
        }
        #endregion

        #region setup listviews, make lists, get closest date
        private async System.Threading.Tasks.Task GetTheClosestDate()
        {
            DateTime todaysDate = DateTime.Today;

            //ArrayList theDates;
            
            JobFuncListViewPopulate();
            if (jobFunctionViews.Count > 0)
            {
                long min = Math.Abs(todaysDate.Ticks - jobFunctionViews[0].DateScheduled.Date.Ticks);


                foreach (JobFunctionView item in jobFunctionViews)
                {
                    long diff = Math.Abs(todaysDate.Ticks - item.DateScheduled.Date.Ticks);

                    if (diff < min)
                    {
                        min = diff;

                        JobFuncListView.SelectedItem = item;
                    }
                }


                var selectedItemIndex = JobFuncListView.DataSource.DisplayItems.IndexOf(JobFuncListView.SelectedItem);
                selectedItemIndex += (JobFuncListView.HeaderTemplate != null && !JobFuncListView.IsStickyHeader || !JobFuncListView.IsStickyGroupHeader) ? 1 : 0;
                selectedItemIndex -= (JobFuncListView.GroupHeaderTemplate != null && JobFuncListView.IsStickyGroupHeader) ? 1 : 0;
                if ((selectedItemIndex - 2) >= 0)
                {
                    (JobFuncListView.LayoutManager as LinearLayout).ScrollToRowIndex(selectedItemIndex - 2);
                }
            }
        }

        //cant make the joins between jobs, tasks and labor to make jobfunction
        private void JobFuncListViewPopulate()
        {
            bool existsAlreadyJobFuncV = false;
            if (App.Customers.Count > 0)
            {
                jobFunctionViews.Clear();
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

                JobFuncListView.ItemsSource = jobFunctionViews;
                JobFuncListView.DataSource.SortDescriptors.Clear();

                JobFuncListView.DataSource.GroupDescriptors.Clear();
                JobFuncListView.DataSource.GroupDescriptors.Add(new GroupDescriptor()
                {
                    PropertyName = "YearToGroupBy",
                    KeySelector = (object obj1) =>
                    {
                        var item = (obj1 as JobFunctionView);
                        return item.YearToGroupBy;
                    },
                });
                JobFuncListView.DataSource.GroupDescriptors.Add(new GroupDescriptor()
                {
                    PropertyName = "MonthToGroupBy",
                    KeySelector = (object obj1) =>
                    {
                        var item = (obj1 as JobFunctionView);
                        return item.MonthToGroupBy;
                    },
                });
                JobFuncListView.DataSource.GroupDescriptors.Add(new GroupDescriptor()
                {
                    PropertyName = "DayMonthToGroupBy",
                    KeySelector = (object obj1) =>
                    {
                        var item = (obj1 as JobFunctionView);
                        return item.DayMonthToGroupBy;
                    },
                });
                JobFuncListView.DataSource.SortDescriptors.Add(new SortDescriptor { PropertyName = "YearToGroupBy", Direction = Syncfusion.DataSource.ListSortDirection.Ascending });
                JobFuncListView.DataSource.SortDescriptors.Add(new SortDescriptor { PropertyName = "MonthToGroupBy", Direction = Syncfusion.DataSource.ListSortDirection.Ascending });
                JobFuncListView.DataSource.SortDescriptors.Add(new SortDescriptor { PropertyName = "DayMonthToGroupBy", Direction = Syncfusion.DataSource.ListSortDirection.Ascending });
                JobFuncListView.DataSource.SortDescriptors.Add(new SortDescriptor { PropertyName = "HourToSortBy", Direction = Syncfusion.DataSource.ListSortDirection.Ascending });


            }
        }

        private void listView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedItem")
            {
                var selectedItemIndex = JobFuncListView.DataSource.DisplayItems.IndexOf(JobFuncListView.SelectedItem);
                selectedItemIndex += (JobFuncListView.HeaderTemplate != null && !JobFuncListView.IsStickyHeader || !JobFuncListView.IsStickyGroupHeader) ? 1 : 0;
                selectedItemIndex -= (JobFuncListView.GroupHeaderTemplate != null && JobFuncListView.IsStickyGroupHeader) ? 1 : 0;
                if ((selectedItemIndex - 2) >= 0)
                {
                    (JobFuncListView.LayoutManager as LinearLayout).ScrollToRowIndex(selectedItemIndex - 1);
                }
            }
        }

        #endregion
    }
}