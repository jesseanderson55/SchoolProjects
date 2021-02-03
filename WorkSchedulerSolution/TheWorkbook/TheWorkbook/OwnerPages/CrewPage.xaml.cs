using SQLite;
using Syncfusion.DataSource;
using Syncfusion.DataSource.Extensions;
using Syncfusion.ListView.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheWorkbook.Model;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace TheWorkbook.OwnerPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class CrewPage : ContentPage
    {

        bool crewWarningDisplay = false;

        public CrewPage()
        {
            InitializeComponent();
            this.Title = "Crews";
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //tables populated by a sqlite call then bound
            SQLiteDBWorkerPopulate();
        }

        #region MainPageButtons

        private void NoticesButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new OwnerPages.NoticePage());
        }

        private void addCrewButton_Clicked(object sender, EventArgs e)
        {
            //enables small box overlay to add new crew name. crew name is added with no members
            overlay.IsEnabled = true;
            overlay.IsVisible = true;
            overlayNewCrew.IsVisible = true;
            background.IsEnabled = false;
            overlayNewCrew.IsEnabled = true;
            crewNameLabel.Text = "New Crew";
        }

        private void modifyCrewButton_Clicked(object sender, EventArgs e)
        {
            //opens large listview overlay that allows modifying and deleting of crews
            overlay.IsEnabled = true;
            overlay.IsVisible = true;
            crewNameListView.IsVisible = true;
            crewNameListViewButtons.IsVisible = true;
            background.IsEnabled = false;
            crewNameListView.IsEnabled = true;
            crewNameListViewButtons.IsEnabled = true;
            SQLiteDBWorkerPopulate();

        }
        #endregion

        #region overlayNewCrew New crew addition buttons
        private void cancelNewCrew_Clicked(object sender, EventArgs e)
        {
            //overlay small overlaynewcrew is reused for both listviews to add the name and to rename
            crewEntry.Text = string.Empty;
            //checks to see if the label is setup for new or rename, which determines how it returns
            if (crewNameLabel.Text == "Rename Crew")
            {
                crewNameListView.IsVisible = true;
                crewNameListView.IsEnabled = true;
                crewNameListViewButtons.IsEnabled = true;
                crewNameListViewButtons.IsVisible = true;
            }
            else
            {
                overlay.IsEnabled = false;
                overlay.IsVisible = false;
                background.IsEnabled = true;

            }

            overlayNewCrew.IsVisible = false;
            overlayNewCrew.IsEnabled = false;

            crewNameLabel.Text = "New Crew";

        }

        private async void createNewCrew_Clicked(object sender, EventArgs e)
        {
            //overlay small overlaynewcrew is reused for both listviews to add the name and to rename
            int rows = 0;
            string crewName = crewEntry.Text;
            //checks to see if the string is empty
            if (!string.IsNullOrEmpty(crewName))
            {
                //checks to see which overlay its being sent from and if the warning has been displayed yet
                if (!crewWarningDisplay && crewNameLabel.Text == "New Crew")
                {
                    await Application.Current.MainPage.DisplayAlert("Alert", "New crew will not be saved unless members are added. Crews are also only saved to this device to allow easy addition to jobs.", "Ok");
                    crewWarningDisplay = true;
                }
                using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
                {
                    conn.CreateTable<Crew>();
                    //checks to see if the its sent from the modify overlay crewnamelistview 
                    if (crewNameLabel.Text == "Rename Crew")
                    {
                        var obj2 = conn.Table<Crew>().ToList();
                        var selectedCrewName = (crewNameListView.SelectedItem as Crew).CrewName;


                        //looks through the crew table for members with matching crewname and returns them
                        var crewsTemp = conn.Table<Crew>().Where(u => u.CrewName == selectedCrewName).ToList();

                        //returned group using that crewname is returned and updated
                        foreach (Crew item in crewsTemp)
                        {
                            item.CrewName = crewName;

                            rows = conn.Update(item);
                        }
                        crewNameListView.IsVisible = true;
                        crewNameListView.IsEnabled = true;
                        crewNameListViewButtons.IsEnabled = true;
                        crewNameListViewButtons.IsVisible = true;

                    }
                    else
                    {
                        //if the crew is a part of the new crew overlay it creates a new crew with no members
                        Crew crew = new Crew()
                        {
                            CrewName = crewName
                        };

                        rows = conn.Insert(crew);

                        //returns to the main background content
                        overlay.IsEnabled = false;
                        overlay.IsVisible = false;
                        background.IsEnabled = true;
                    }

                    crewEntry.Text = string.Empty;
           
                    overlayNewCrew.IsVisible = false;
                    overlayNewCrew.IsEnabled = false;

                    if (rows > 0)
                    {
                        await Application.Current.MainPage.DisplayAlert("Notice", "Success.", "Ok");
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Notice", "Failure.", "Ok");
                    }

                    //repopulates lists with new data
                    SQLiteDBWorkerPopulate();
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Please enter a name.", "Ok");
            }
        }
        #endregion

        #region crewNameListView Simple crew list & buttons
        private async void deleteCrewButton_Clicked(object sender, EventArgs e)
        {
            //on the modify listview screen, crewnamelistview, crews can be modified or deleted
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                int rows = 0;
                conn.CreateTable<Crew>();
                if (crewNameListView.SelectedItem != null)
                {
                    var crewToDelete = (crewNameListView.SelectedItem as Crew).CrewName;
                    var crewsTemp = conn.Table<Crew>().Where(u => u.CrewName == crewToDelete).ToList();

                    foreach (Crew crew in crewsTemp)
                    {
                        rows = conn.Delete(crew);
                    }

                    if (rows > 0)
                    {
                        await Application.Current.MainPage.DisplayAlert("Notice", "Success.", "Ok");
                    }

                    //repopulates lists with new data
                    SQLiteDBWorkerPopulate();
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Please select a crew to delete.", "Ok");

                }

            }
        }

        private async void addMembersButton_Clicked(object sender, EventArgs e)
        {
            //on the modify listview screen, crewnamelistview, crews can be modified to add more members
            if (crewNameListView.SelectedItem != null)
            {
                await Navigation.PushAsync(new CrewModifyMembersPage(crewNameListView.SelectedItem as Crew));
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Please select a crew to add members to", "OK");

            }
        }

        private async void copyCrewButton_Clicked(object sender, EventArgs e)
        {
            //on the modify listview screen, crewnamelistview, crews can be copied with addendum to their name
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                int rows = 0;
                conn.CreateTable<Crew>();
                var checkForDoublesCrewList = conn.Table<Crew>().ToList();

                if (crewNameListView.SelectedItem != null)
                {
                    var crewNameToCopy = crewNameListView.SelectedItem as Crew;
                    var crewsTemp = conn.Table<Crew>().Where(u => u.CrewName == crewNameToCopy.CrewName).ToList();
                    var exampleCrewName = $"copy of {crewNameToCopy.CrewName}";
                    if (crewsTemp != null)
                    {
                        if (checkForDoublesCrewList.Where(u => u.CrewName == exampleCrewName).Any())
                        {
                            await Application.Current.MainPage.DisplayAlert("Error", "Please rename the current copy first.", "OK");
                            rows -= 1;
                        }
                        else
                        {
                            foreach (Crew item in crewsTemp)
                            {

                                Crew crew = new Crew()
                                {
                                    CrewName = $"copy of {item.CrewName}",
                                    WorkerID = item.WorkerID
                                };
                                rows += conn.Insert(crew);
                            }
                        }
                    }

                    if (rows > 0)
                    {
                        await Application.Current.MainPage.DisplayAlert("Notice", "Success.", "Ok");
                    }
                    else if (rows == 0)
                    {
                        await Application.Current.MainPage.DisplayAlert("Notice", "Failure.", "Ok");
                    }
                   

                    //repopulates lists with new data
                    SQLiteDBWorkerPopulate();
                }

            }
        }


        private async void renameCrewButton_Clicked(object sender, EventArgs e)
        {
            //overlay small overlaynewcrew is reused for both listviews to add the name and to rename
            if (crewNameListView.SelectedItem != null)
            {
                overlayNewCrew.IsVisible = true;
                overlayNewCrew.IsEnabled = true;
                crewNameListView.IsVisible = false;
                crewNameListView.IsEnabled = false;
                crewNameListViewButtons.IsEnabled = false;
                crewNameListViewButtons.IsVisible = false;
                crewNameLabel.Text = "Rename Crew";
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Please select crew to rename.", "Ok");
            }
        }

        private void cancelCrewButton_Clicked(object sender, EventArgs e)
        {
            //exits the overlay that lists the crews by name.
            overlay.IsEnabled = false;
            overlay.IsVisible = false;
            crewNameListView.IsVisible = false;
            crewNameListViewButtons.IsVisible = false;
            background.IsEnabled = true;
            crewNameListView.IsEnabled = false;
            crewNameListViewButtons.IsEnabled = false;
        }
        #endregion


        private void SQLiteDBWorkerPopulate()
        {
            AllCrewListView.ItemsSource = null;
            crewNameListView.ItemsSource = null;

            List<CrewWorker> joinedWorkersAndCrews = new List<CrewWorker> { };
            List<Worker> justAWorkerList = new List<Worker> { };
            List<Crew> justACrewList = new List<Crew> { };


            //takes the sqlite db and sends it out to a list variable waiting to be bound to a listview
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {

               //makes a table of just crew unfiltered
                conn.CreateTable<Crew>();
                justACrewList = conn.Table<Crew>().ToList();
                
                conn.CreateTable<Worker>();
                justAWorkerList = conn.Table<Worker>().ToList();

                if (justAWorkerList.Count > 0)
                {
                    //had to create a class crewworkers that inherits from crew in order to use filtering/querying with linq queries

                    joinedWorkersAndCrews = justACrewList.Join(justAWorkerList,
                        cr => cr.WorkerID,
                        w => w.ID,
                        (Crew, Worker) => new CrewWorker()
                        {
                            WorkerID = Worker.ID,
                            CrewName = Crew.CrewName,
                            WorkerLastName = Worker.WorkerLastName
                        }).ToList();

                    //shows only crewnames in format to select and delete and add on another page using linq and lambda
                    //is a popup display
                    if (justACrewList.Count > 0)
                    {
                        AllCrewListView.ItemsSource = joinedWorkersAndCrews;

                        var tempGrouping = justACrewList.GroupBy(name => name.CrewName).Select(name => name.First()).ToList();
                        //var tempTable = workerlessCrews;

                        crewNameListView.ItemsSource = tempGrouping;
                    }
                }
            }

            AllCrewListView.DataSource.GroupDescriptors.Clear();
            AllCrewListView.DataSource.GroupDescriptors.Add(new GroupDescriptor()
            {
                PropertyName = "CrewName",
                KeySelector = (object obj1) =>
                {
                    var item = (obj1 as CrewWorker);
                    return item.CrewName;
                },
            });

            AllCrewListView.RefreshView();
            crewNameListView.RefreshView();

        }

    }
}