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
    public partial class CoursesPage : ContentPage
    {
        public Course courseSelected;
        public Term termSelected;

        public CoursesPage(Term termSelected)
        {
            InitializeComponent();

            this.termSelected = termSelected;

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Course>();
                var course = conn.Table<Course>().ToList();
                List<Course> termCourses = new List<Course> { };
                foreach (Course item in course)
                {
                    if (item.termID == termSelected.termID)
                    {
                        termCourses.Add(item);
                    }
                }
                courseListView.ItemsSource = termCourses;
                courseSelected = null;
                courseListView.SelectedItem = null;
            }
        }

        private void courseListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            courseSelected = courseListView.SelectedItem as Course;
        }

        private void addButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CoursePageAdd(termSelected));
        }

        private void editButton_Clicked(object sender, EventArgs e)
        {
            if (courseSelected != null)
            {
                Navigation.PushAsync(new CoursePageModify(courseSelected, termSelected));
            }
            else
            {
                DisplayAlert("Error", "Create and Select a Course First", "Ok");
            }

        }

        private void notesButton_Clicked(object sender, EventArgs e)
        {
            if (courseSelected != null)
            {
                Navigation.PushAsync(new NotesPage(courseSelected));
            }
            else
            {
                DisplayAlert("Error", "Create and Select a Course First", "Ok");
            }
        }

        private void ToMainPage_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }

        private void ToExamPage_Clicked(object sender, EventArgs e)
        {
            if (courseSelected != null)
            {
                Navigation.PushAsync(new AssessmentsPage(courseSelected));
            }
            else
            {
                DisplayAlert("Error", "Create and Select a Course First", "Ok");
            }
        }

        private void ToTermPage_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TermPage());
        }
    }
}