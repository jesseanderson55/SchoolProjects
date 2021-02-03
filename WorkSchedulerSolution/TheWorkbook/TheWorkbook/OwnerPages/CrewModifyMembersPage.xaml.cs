using SQLite;
using Syncfusion.DataSource.Extensions;
using Syncfusion.ListView.XForms;
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
    public partial class CrewModifyMembersPage : ContentPage
    {
        Crew crewPassed;

        public CrewModifyMembersPage()
        {
            InitializeComponent();
        }

        public CrewModifyMembersPage(Crew crewPassed)
        {
            InitializeComponent();

            this.crewPassed = crewPassed;
            Title = "Add to " + crewPassed.CrewName + " crew.";
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            listviewPopulate();
        }

        private async void saveWorkersToCrew_Clicked(object sender, EventArgs e)
        {
            List<Crew> crewList = new List<Crew>() { };

            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Crew>();
                crewList = conn.Table<Crew>().ToList();

                if (App.WorkerList != null)
                {
                    int rows = 0;
                    //any worker in the workerlist that isnt selected is put into notselected
                    var notSelected = App.WorkerList.ToObservableCollection().Except(listView.SelectedItems).ToList();



                    //selected workers are added
                    foreach (Worker item in listView.SelectedItems)
                    {
                        var selectedCrewList = crewList.Where(u => u.CrewName == crewPassed.CrewName).ToList();
                        var workerExistsInCrew = selectedCrewList.Where(u => u.WorkerID == item.ID).Any();

                        if (!workerExistsInCrew)
                        {
                            Crew crew = new Crew()
                            {
                                CrewName = crewPassed.CrewName,
                                WorkerID = item.ID
                            };
                            rows = conn.Insert(crew);
                        }
                    }

                    //goes through the array of workers not selected and deletes them from the crew if they are in the crew
                    foreach (Worker item in notSelected)
                    {
                        var nonCrewMember = conn.Table<Crew>().Where(u => u.CrewName == crewPassed.CrewName && u.WorkerID == item.ID).FirstOrDefault();

                        if (nonCrewMember != null)
                        {
                            rows = conn.Delete(nonCrewMember);
                        }
                    }

                    if (rows > 0)
                    {
                        await Application.Current.MainPage.DisplayAlert("Success", "Workers Updated", "OK");
                        await Navigation.PopAsync();
                    }
                }
            }
        }

        private void cancelExit_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        public void listviewPopulate()
        {
            if (App.WorkerList != null)
            {
                listView.ItemsSource = App.WorkerList;


                using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
                {
                    conn.CreateTable<Crew>();

                    var crewList = conn.Table<Crew>().Where(u => u.CrewName == crewPassed.CrewName).ToList();

                    foreach (Crew crew in crewList)
                    {
                        var stuffy = App.WorkerList.Where(u => u.ID == crew.WorkerID).FirstOrDefault();
                        if (stuffy != null)
                        {
                            listView.SelectedItems.Add(stuffy);
                        }
                    }
                }
            }
            else
            {
                Title = $"Create workers first.";
            }
        }
    }
}