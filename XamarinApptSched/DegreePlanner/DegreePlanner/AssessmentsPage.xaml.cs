using DegreePlanner.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DegreePlanner
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AssessmentsPage : ContentPage
    {
        public Course selectedCourse;
        public Milestone selectedMilestone;

        public AssessmentsPage(Course passedCourse)
        {
            InitializeComponent();
            selectedCourse = passedCourse;
            assessmentLabel.Text = $"Milestones for {passedCourse.courseTitle}";
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Milestone>();
                var milestone = conn.Table<Milestone>().ToList();
                List<Milestone> milestoneTemp = new List<Milestone> { };
                foreach (Milestone item in milestone)
                {
                    if (item.courseID == selectedCourse.courseID)
                    {
                        milestoneTemp.Add(item);
                    }
                }
                milestonePicker.ItemsSource = milestoneTemp;
            }
        }

        private void milestonePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (milestonePicker.SelectedItem != null)
            {
                selectedMilestone = milestonePicker.SelectedItem as Milestone;
                milestoneName.Text = selectedMilestone.milestoneName;
                milestoneTypePicker.SelectedItem = selectedMilestone.milestoneType;
                milestoneStartDatePicker.Date = selectedMilestone.milestoneStart;
                milestoneEndDatePicker.Date = selectedMilestone.milestoneEnd;
                goalEntry.Text = selectedMilestone.goalEntry;
            }
        }

        private void addButton_Clicked(object sender, EventArgs e)
        {

            try
            {
                if (milestoneStartDatePicker.Date < milestoneEndDatePicker.Date)
                {
                    Milestone milestone = new Milestone()
                    {
                        courseID = selectedCourse.courseID,
                        courseTitle = selectedCourse.courseTitle,
                        milestoneName = milestoneName.Text,
                        milestoneType = milestoneTypePicker.SelectedItem.ToString(),
                        milestoneStart = milestoneStartDatePicker.Date,
                        milestoneEnd = milestoneEndDatePicker.Date,
                        goalEntry = goalEntry.Text
                    };

                    using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
                    {
                        conn.CreateTable<Milestone>();
                        int insertRowsSuccess = conn.Insert(milestone);

                        if (insertRowsSuccess > 0)
                        {
                            DisplayAlert("Success", "Milestone successfully created", "Ok");
                            OnAppearing();
                        }
                        else
                        {
                            DisplayAlert("Failure", "Milestone failed to be created", "Ok");
                        }
                    }
                }
                else
                {
                    DisplayAlert("Error", "Start date must preceed end date", "Ok");
                }
            }
            catch (NullReferenceException)
            {
                DisplayAlert("Error", "Please fill all forms", "Ok");
            }

        }

        private void deleteButton_Clicked(object sender, EventArgs e)
        {
            if (selectedMilestone != null)
            {
                using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
                {
                    conn.CreateTable<Milestone>();
                    int insertRowsSuccess = conn.Delete(selectedMilestone);

                    if (insertRowsSuccess > 0)
                    {
                        DisplayAlert("Success", "Milestone successfully deleted", "Ok");
                    }
                    else
                    {
                        DisplayAlert("Failure", "Milestone failed to be deleted", "Ok");
                    }
                }
            }
            else
            {
                DisplayAlert("Error", "Please select a Milestone to delete", "Ok");
            }
            selectedMilestone = null;
            OnAppearing();
        }

        private void termButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TermPage());
        }

        private void notesButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NotesPage(selectedCourse));
        }

        private void ToMainPage_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }
    }
}