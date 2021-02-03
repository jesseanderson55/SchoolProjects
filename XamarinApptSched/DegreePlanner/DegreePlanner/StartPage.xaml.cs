using DegreePlanner.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DegreePlanner
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class StartPage : ContentPage
    {
        public StartPage()
        {
            InitializeComponent();

            var assemble = typeof(StartPage);

            owlImage.Source = ImageSource.FromResource("DegreePlanner.Assets.Images.nightowl.png", assemble);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                
                conn.CreateTable<Milestone>();
                var milestone = conn.Table<Milestone>().ToList();
                foreach (Milestone item in milestone)
                {
                    if ((item.milestoneEnd - DateTime.Now).Days <= 1 && (item.milestoneEnd - DateTime.Now).Hours > 0)
                    {
                        DisplayAlert("Alert", $"{item.milestoneName} due today", "Ok");
                        continue;
                    }
                    if ((item.milestoneEnd - DateTime.Now).Days <= 7 && (item.milestoneEnd - DateTime.Now).Hours > 0)
                    {
                        DisplayAlert("Alert", $"{item.milestoneName} due in {item.untilDue}", "Ok");
                    }
                }
                conn.CreateTable<Course>();
                var course = conn.Table<Course>().ToList();
                int sampleDataCounter = 0;
                foreach (Course item in course)
                {
                    if ((item.courseStart - DateTime.Now).Days <= 7 && (item.courseStart - DateTime.Now).Hours > 0)
                    {
                        DisplayAlert("Alert", $"{item.courseTitle} starts in {(item.courseStart - DateTime.Now).Days}", "Ok");
                        continue;
                    }
                    if ((item.courseEnd - DateTime.Now).Days <= 7 && (item.courseEnd - DateTime.Now).Hours > 0)
                    {
                        DisplayAlert("Alert", $"{item.courseTitle} due in {(item.courseEnd - DateTime.Now).Days}", "Ok");
                    }
                    if (item.instructorName == "Jesse Anderson" && item.instructorEmail == "fakeEmail@aol.com") 
                    {
                        sampleDataCounter++;
                    }
                }
                if (sampleDataCounter == 0)
                {
                    SampleDataLoad();
                }
            }
        }

        private void SampleDataLoad()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                Term myterm = new Term()
                {
                    termName = "Summer 2020",
                    termStart = DateTime.Today.AddDays(3),
                    termEnd = DateTime.Today.AddDays(180)
                };
                conn.CreateTable<Term>();
                conn.Insert(myterm);

                var tempTerm = conn.Table<Term>().ToList();
                int tempTermID = 0;
                foreach (Term item in tempTerm)
                {
                    if (item.termName == "Summer 2020")
                    {
                        tempTermID = item.termID;
                    }
                }

                Course mycourse = new Course()
                {
                    termID = tempTermID,
                    termName = "Summer 2020",
                    courseTitle = "Random Computers",
                    courseStart = DateTime.Today.AddDays(3),
                    courseStatus = "In Progress",
                    courseEnd = DateTime.Today.AddDays(30),
                    instructorName = "Jesse Anderson",
                    instructorEmail = "fakeEmail@aol.com",
                    instructorPhone = "4349440580"
                };
                conn.CreateTable<Course>();
                conn.Insert(mycourse);

                var tempCourse = conn.Table<Course>().ToList();
                int tempCourseID = 0;
                foreach (Course item in tempCourse)
                {
                    if (item.courseTitle == "Random Computers")
                    {
                        tempCourseID = item.courseID;
                    }
                }

                conn.CreateTable<Milestone>();
                Milestone tempMilestone = new Milestone()
                {
                    courseID = tempCourseID,
                    courseTitle = "Random Computers",
                    milestoneName = "Test 081",
                    milestoneType = "Objective Assessment",
                    milestoneStart = DateTime.Today.AddDays(3),
                    milestoneEnd = DateTime.Today.AddDays(30),
                    goalEntry = "Complete the OA for Random Computers"
                };
                Milestone tempMilestone2 = new Milestone()
                {
                    courseID = tempCourseID,
                    courseTitle = "Random Computers",
                    milestoneName = "Chapter 1-6",
                    milestoneType = "Chapter Completed",
                    milestoneStart = DateTime.Today.AddDays(3),
                    milestoneEnd = DateTime.Today.AddDays(15),
                    goalEntry = "Complete the first chapters for Random Computers"
                };
                Milestone tempMilestone3 = new Milestone()
                {
                    courseID = tempCourseID,
                    courseTitle = "Random Computers",
                    milestoneName = "Task 1",
                    milestoneType = "Performance Assessment",
                    milestoneStart = DateTime.Today.AddDays(3),
                    milestoneEnd = DateTime.Today.AddDays(15),
                    goalEntry = "Complete the PA for Random Computers"
                };
                conn.Insert(tempMilestone);
                conn.Insert(tempMilestone2);
                conn.Insert(tempMilestone3);

                conn.CreateTable<Note>();
                Note tempNote = new Note()
                {
                    notesName = "Note 1",
                    courseID = tempCourseID,
                    courseTitle = "Random Computers",
                    notesForCourse = "There is nothing actually random about computers"
                };
                conn.Insert(tempNote);
            }
        }

            //Navigation Buttons
            private void splashPageButton_Pressed(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }

        private void ToTermPage_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TermPage());
        }

        
    }
}
