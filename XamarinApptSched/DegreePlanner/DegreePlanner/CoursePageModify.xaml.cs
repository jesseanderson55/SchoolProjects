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
    public partial class CoursePageModify : ContentPage
    {
        Course selectedCourse;
        Term selectedTerm;

        public CoursePageModify(Course passedCourse, Term passedTerm)
        {
            InitializeComponent();

            
            this.selectedTerm = passedTerm;
            this.selectedCourse = passedCourse;
            termNameEntry.Text = passedCourse.termName;
            courseNameEntry.Text = passedCourse.courseTitle;
            courseStartDatePicker.Date = passedCourse.courseStart;
            courseEndDatePicker.Date = passedCourse.courseEnd;
            courseStatusEntry.SelectedItem = passedCourse.courseStatus;
            instructorEmailEntry.Text = passedCourse.instructorEmail;
            instructorNameEntry.Text = passedCourse.instructorName;
            instructorPhoneEntry.Text = passedCourse.instructorPhone;
        }

        private void deleteButton_Clicked(object sender, EventArgs e)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Course>();
                int insertRowsSuccess = conn.Delete(selectedCourse);

                if (insertRowsSuccess > 0)
                {
                    DisplayAlert("Success", "Course successfully deleted", "Ok");
                }
                else
                {
                    DisplayAlert("Failure", "Course failed to be deleted", "Ok");
                }
            }
            Navigation.PushAsync(new CoursesPage(selectedTerm));
        }

        private void updateCourseButton_Pressed(object sender, EventArgs e)
        {
            try
            {
                if (courseStartDatePicker.Date < courseEndDatePicker.Date)
                {


                    selectedCourse.courseStart = courseStartDatePicker.Date;
                    selectedCourse.courseStatus = courseStatusEntry.SelectedItem.ToString();
                    selectedCourse.courseEnd = courseEndDatePicker.Date;
                    selectedCourse.instructorName = instructorNameEntry.Text;
                    selectedCourse.instructorEmail = instructorEmailEntry.Text;
                    selectedCourse.instructorPhone = instructorPhoneEntry.Text;
                }
                else
                {
                    DisplayAlert("Error", "Start date must preceed end date", "Ok");
                }
            }
            catch (NullReferenceException)
            {
                DisplayAlert("Failure", "Course failed to be created", "Ok");
            }


            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Course>();
                int insertRowsSuccess = conn.Update(selectedCourse);

                if (insertRowsSuccess > 0)
                {
                    DisplayAlert("Success", "Course successfully updated", "Ok");
                }
                else
                {
                    DisplayAlert("Failure", "Course failed to be updated", "Ok");
                }
            }
            Navigation.PushAsync(new CoursesPage(selectedTerm));
        }

        private void examButton_Clicked(object sender, EventArgs e)
        {
                Navigation.PushAsync(new AssessmentsPage(selectedCourse));
        }

        private void ToMainPage_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }

        private void termButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TermPage());
        }

        private void notesButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NotesPage(selectedCourse));
        }

        private void notes2Button_Pressed(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NotesPage(selectedCourse));
        }
    }
}