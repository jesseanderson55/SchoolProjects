using SQLite;
using Syncfusion.DataSource;
using Syncfusion.DataSource.Extensions;
using System;
using System.Collections.Generic;
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
    public partial class JobWorkersPage : ContentPage
    {
        #region properties
        List<CrewWorker> crewWorkersAddedList = new List<CrewWorker>() { };
        List<CrewWorker> CWUnaddedList = new List<CrewWorker>() { };
        List<Labor> laborList = new List<Labor>() { };
        List<JobFunction> matchedJobFunctionList = new List<JobFunction>() { };


        CrewWorker selectedCrewWorkerTop = new CrewWorker();
        CrewWorker selectedCrewWorkerBottom = new CrewWorker();

        JobFunctionView passedJobFunctionView;
        #endregion

        #region constructors
        public JobWorkersPage(JobFunctionView jobFunctionView)
        {
            InitializeComponent();
            passedJobFunctionView = jobFunctionView;

            Title = $"{jobFunctionView.JobName} on {jobFunctionView.DayMonthToGroupBy}";
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            CrewWorkerListViewPopulate();

        }

        private void CrewWorkerListViewPopulate()
        {
            List<Worker> workers = new List<Worker> { };
            List<Crew> crewList = new List<Crew> { };
            List<Labor> tempListOfMatchedLabor = new List<Labor>() { };


            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                allWorkersCrewListView.ItemsSource = null;
                WorkersLaborListView.ItemsSource = null;

                //makes a table of just crew without workers
                conn.CreateTable<Crew>();
                crewList = conn.Table<Crew>().ToList();

                //seperate table of workers
                conn.CreateTable<Worker>();
                workers = conn.Table<Worker>().ToList();

                if (workers != null)
                {
                    //had to create a class crewworkers that inherits from crew in order to use filtering/querying with linq queries
                    //left outer join was easier with a fill linq query with no lambdas

                    CWUnaddedList = (from Worker in App.WorkerList
                                     join Crew in crewList on Worker.ID equals Crew.WorkerID
                                     into CrewWorker
                                     from subcrew in CrewWorker.DefaultIfEmpty()
                                     select new CrewWorker() { ID = subcrew?.ID ?? null, WorkerID = Worker.ID, CrewName = subcrew?.CrewName ?? string.Empty, WorkerLastName = Worker.WorkerLastName, IsSupervisor = Worker.IsSupervisor }).ToList();

                    if (CWUnaddedList != null)
                    {
                        allWorkersCrewListView.ItemsSource = CWUnaddedList;
                    }
                }

                //finds the jobfunctions created for this job based on the one jobfunction that was passed
                matchedJobFunctionList = App.JobFunctions.Where(u => u.JobID == passedJobFunctionView.JobID && u.DateScheduled == passedJobFunctionView.DateScheduled).ToList();

                if (matchedJobFunctionList.Count > 0)
                {
                    //go through each jobfunction
                    foreach (JobFunction jobFunction in matchedJobFunctionList)
                    {
                        if (App.Labors.Count > 0)
                        {
                            //get the labor list that is for this job function
                            var tempToAdd = App.Labors.Where(u => u.JobFunctionID == jobFunction.ID).ToList();
                            if (tempToAdd.Count > 0)
                            {
                                tempListOfMatchedLabor.AddRange(tempToAdd);
                            }
                        }
                    }
                    //go through each labor that was returned from this function
                    foreach (Labor item in tempListOfMatchedLabor)
                    {
                        if (CWUnaddedList != null)
                        {
                            //get the workers that are paired with this labor //should be only 1, foreach here and linq query might be unnecessary
                            bool alreadyPresent = false;
                            var tempCrewWorker = CWUnaddedList.Where(u => u.WorkerID == item.EmployeeID).FirstOrDefault();

                            //Check to see if the worker for this labor is already present in the top group and if it isnt add them.

                            alreadyPresent = crewWorkersAddedList.Where(u => u.WorkerID == tempCrewWorker.WorkerID).Any();
                            if (!alreadyPresent)
                            {
                                crewWorkersAddedList.Add(tempCrewWorker);
                            }

                        }
                    }
                }

                WorkersLaborListView.ItemsSource = crewWorkersAddedList;

                allWorkersCrewListView.DataSource.SortDescriptors.Clear();
                allWorkersCrewListView.DataSource.SortDescriptors.Add(new SortDescriptor()
                {
                    PropertyName = "CrewName",
                    Direction = ListSortDirection.Ascending
                });

                WorkersLaborListView.DataSource.SortDescriptors.Clear();
                WorkersLaborListView.DataSource.SortDescriptors.Add(new SortDescriptor()
                {
                    PropertyName = "IsSupervisor",
                    Direction = ListSortDirection.Ascending
                });

                WorkersLaborListView.RefreshView();
                allWorkersCrewListView.RefreshView();
            }
        }
        #endregion

        #region listview tapped events

        private void WorkersLaborListView_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            selectedCrewWorkerTop = e.ItemData as CrewWorker;
        }

        private void allWorkersCrewListView_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            selectedCrewWorkerBottom = e.ItemData as CrewWorker;
        }
        #endregion

        #region listview add/remove events
        private async void addWorkerToTheJobButton_Clicked(object sender, EventArgs e)
        {
            if (selectedCrewWorkerBottom != null)
            {
                bool checkExistingAlreadyInJob = crewWorkersAddedList.Where(u => u.WorkerID == selectedCrewWorkerBottom.WorkerID).Any();
                if (!checkExistingAlreadyInJob)
                {
                    crewWorkersAddedList.Add(selectedCrewWorkerBottom);
                    CrewWorkerListViewPopulate();
                    selectedCrewWorkerBottom = null;
                    allWorkersCrewListView.SelectedItem = null;
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Alert", "Select a worker below to add to the job above.", "Ok");
            }
        }

        private async void removeWorkerFromJobButton_Clicked(object sender, EventArgs e)
        {
            if (selectedCrewWorkerTop != null)
            {
                crewWorkersAddedList.Remove(selectedCrewWorkerTop);
                CrewWorkerListViewPopulate();
                selectedCrewWorkerTop = null;
                WorkersLaborListView.SelectedItem = null;
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Alert", "Select a worker above to remove from the job.", "Ok");
            }
        }

        private async void addCrewToTheJobButton_Clicked(object sender, EventArgs e)
        {
            if (selectedCrewWorkerBottom != null)
            {
                foreach (CrewWorker item in CWUnaddedList)
                {
                    if (selectedCrewWorkerBottom.CrewName != null && item.CrewName != null)
                    {
                        if (item.CrewName == selectedCrewWorkerBottom.CrewName)
                        {
                            bool checkExistingAlreadyInJob = crewWorkersAddedList.Where(u => u.WorkerID == item.WorkerID).Any();
                            if (!checkExistingAlreadyInJob)
                            {
                                crewWorkersAddedList.Add(item);
                            }
                        }
                    }
                }
                CrewWorkerListViewPopulate();
                selectedCrewWorkerBottom = null;
                allWorkersCrewListView.SelectedItem = null;
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Alert", "Select a worker of a crew below to add to the job.", "Yes");
            }
        }

        private async void removeCrewFromJobButton_Clicked(object sender, EventArgs e)
        {
            if (selectedCrewWorkerTop != null)
            {
                foreach (CrewWorker item in crewWorkersAddedList.ToList())
                {
                    if (selectedCrewWorkerTop.CrewName != null && item.CrewName != null)
                    {
                        if (item.CrewName == selectedCrewWorkerTop.CrewName)
                        {
                            crewWorkersAddedList.Remove(item);
                        }
                    }
                }
                CrewWorkerListViewPopulate();
                selectedCrewWorkerTop = null;
                WorkersLaborListView.SelectedItem = null;
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Alert", "Select a worker of a crew above to remove from the job.", "Yes");
            }
        }
        #endregion

        #region navigation buttons
        private void cancelButton_Clicked(object sender, EventArgs e)
        {
            crewWorkersAddedList.Clear();
            CWUnaddedList.Clear();
            Navigation.PopAsync();
        }
        private void NoticesButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new OwnerPages.NoticePage());
        }

        private void employeesButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new OwnerPages.EmployeePage());
        }
        #endregion

        private async void saveButton_Clicked(object sender, EventArgs e)
        {
            List<TaskTool> taskTools = new List<TaskTool>() { };
            laborList.Clear();
            matchedJobFunctionList.Clear();

            int successCounter = 0;

            List<CrewWorker> listToUseExceptMethod = new List<CrewWorker>() { };

            if (crewWorkersAddedList.Count > 0)
            {
                //added this so the below except() would work
                foreach (var item in crewWorkersAddedList)
                {
                    var tempWorker = CWUnaddedList.Where(u => u.ID == item.ID).FirstOrDefault();
                    listToUseExceptMethod.Add(tempWorker);
                }

                //find out which ones arent added using except(). Broke up the chain for simplicity.
                var ocCrewWorkersAddedList = listToUseExceptMethod.ToObservableCollection();
                var ocJoinedCrewWorkersList = CWUnaddedList.ToObservableCollection();
                var ocExceptList = ocJoinedCrewWorkersList.Except(ocCrewWorkersAddedList);
                var notSelected = ocExceptList.ToList();

                //get all tasktool combinations that could be used
                using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
                {
                    conn.CreateTable<TaskTool>();
                    taskTools = conn.Table<TaskTool>().ToList();
                }

                //get the jobfunctions that match what was passed for date and job
                matchedJobFunctionList = App.JobFunctions.Where(u => u.JobID == passedJobFunctionView.JobID && u.DateScheduled == passedJobFunctionView.DateScheduled).ToList();

                //go through the job functions to create a labor per jobfunction
                foreach (JobFunction jobFunction in matchedJobFunctionList)
                {
                    if (App.Labors.Count > 0)
                    {
                        //get a list of labors that are already created for this jobfunction
                        laborList = App.Labors.Where(u => u.JobFunctionID == jobFunction.ID).ToList();

                        //delete any labors (workers doing labor) that were in the list but now arent
                        foreach (CrewWorker notSelectedWorker in notSelected)
                        {
                            //get any labors for this specific jobfunction for the employees not in the list
                            var laborToDelete = App.Labors.Where(u => u.EmployeeID == notSelectedWorker.WorkerID && u.JobFunctionID == jobFunction.ID).FirstOrDefault();

                            if (laborToDelete != null)
                            {
                                try
                                {
                                    App.Labors.Remove(laborToDelete);
                                    await App.mobileService.GetTable<Labor>().DeleteAsync(laborToDelete);
                                    successCounter++;
                                }
                                catch (Exception)
                                {
                                    throw;
                                }
                            }
                        }
                    }

                    //get the specific tasktool for this jobfunction
                    var tempTaskToolToInsert = taskTools.Where(u => u.ID == jobFunction.TaskID).FirstOrDefault();

                    //go through each crewworker added to this job to make a labor for each worker on each jobfunction
                    foreach (CrewWorker crewWorker in crewWorkersAddedList)
                    {
                        Labor laborOfOneEmployee = null;

                        //check to see if the labor for this jobfunction already exists
                        if (laborList.Count > 0)
                        {
                            laborOfOneEmployee = laborList.Where(u => u.EmployeeID == crewWorker.WorkerID).FirstOrDefault();
                        }

                        //if the labor doesnt already exist, make it
                        if (laborOfOneEmployee == null)
                        {
                            Labor labor = new Labor()
                            {
                                EmployeeID = crewWorker.WorkerID,
                                ToolID = tempTaskToolToInsert.toolID,
                                JobFunctionID = jobFunction.ID
                            };

                            try
                            {
                                App.Labors.Add(labor);
                                await App.mobileService.GetTable<Labor>().InsertAsync(labor);
                                successCounter++;
                            }
                            catch (Exception)
                            {
                                throw;
                            }
                        }
                    }
                }
                if (successCounter > 0)
                {
                    await Navigation.PopAsync();
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Alert", "Select a worker below to add to the job above to save.", "Ok");
            }
        }
    }
}
