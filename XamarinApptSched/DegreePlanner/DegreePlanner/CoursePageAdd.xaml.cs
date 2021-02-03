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
    public partial class CoursePageAdd : ContentPage
    {
        public Term termSelected;

        public CoursePageAdd(Term passedTerm)
        {
            InitializeComponent();
            this.termSelected = passedTerm;
            termNameEntry.Text = termSelected.termName;
        }

        private void saveCourseButton_Pressed(object sender, EventArgs e)
        {
            try
            {
                if (courseStartDatePicker.Date < courseEndDatePicker.Date)
                {
                    Course course = new Course()
                    {
                        termID = termSelected.termID,
                        termName = termSelected.termName,
                        courseTitle = courseNameEntry.Text,
                        courseStart = courseStartDatePicker.Date,
                        courseStatus = courseStatusEntry.SelectedItem.ToString(),
                        courseEnd = courseEndDatePicker.Date,
                        instructorName = instructorNameEntry.Text,
                        instructorEmail = instructorEmailEntry.Text,
                        instructorPhone = instructorPhoneEntry.Text
                    };

                    using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
                    {
                        conn.CreateTable<Course>();
                        int insertRowsSuccess = conn.Insert(course);

                        if (insertRowsSuccess > 0)
                        {
                            DisplayAlert("Success", "Course successfully created", "Ok");
                        }
                        else
                        {
                            DisplayAlert("Failure", "Course failed to be created", "Ok");
                        }
                    }
                    Navigation.PushAsync(new CoursesPage(termSelected));
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
    }
}