using DegreePlanner.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DegreePlanner
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotesPage : ContentPage
    {
        public Course passedCourse;
        public Note noteSelected;

        public NotesPage(Course coursePassed)
        {
            InitializeComponent();

            passedCourse = coursePassed;
            notesLabel.Text = $"Notes for {passedCourse.courseTitle}";
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Note>();
                var note = conn.Table<Note>().ToList();
                List<Note> courseNotes = new List<Note> { };
                foreach (Note item in note)
                {
                    if (item.courseID == passedCourse.courseID)
                    {
                        courseNotes.Add(item);
                    }
                }
                notesPicker.ItemsSource = courseNotes;
                noteSelected = null;
            }
        }

        private void notesPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            noteSelected = notesPicker.SelectedItem as Note;
            if (noteSelected != null)
            {
                notesTitle.Text = noteSelected.notesName;
                notesEntry.Text = noteSelected.notesForCourse;
            }
        }

        private void addButton_Clicked(object sender, EventArgs e)
        {
            try
            {

                Note note = new Note()
                {
                    notesName = notesTitle.Text,
                    courseID = passedCourse.courseID,
                    courseTitle = passedCourse.courseTitle,
                    notesForCourse = notesEntry.Text
                };

                using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
                {
                    conn.CreateTable<Note>();
                    int insertRowsSuccess = conn.Insert(note);

                    if (insertRowsSuccess > 0)
                    {
                        DisplayAlert("Success", "Note successfully created", "Ok");
                    }
                    else
                    {
                        DisplayAlert("Failure", "Note failed to be created", "Ok");
                    }
                }
            }
            catch (NullReferenceException)
            {
                DisplayAlert("Error", "Please fill all fields", "Ok");
            }
            OnAppearing();
        }

        private void deleteButton_Clicked(object sender, EventArgs e)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Note>();
                int insertRowsSuccess = conn.Delete(noteSelected);

                if (insertRowsSuccess > 0)
                {
                    DisplayAlert("Success", "Note successfully deleted", "Ok");
                }
                else
                {
                    DisplayAlert("Failure", "Note failed to be deleted", "Ok");
                }
            }
            notesTitle.Text = string.Empty;
            notesEntry.Text = string.Empty;
            OnAppearing();
        }

        private void termButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TermPage());
        }

        private void ToMainPage_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }

         private async void shareButton_Pressed(object sender, EventArgs e)
        {
            try
            {
                await Share.RequestAsync(new ShareTextRequest
                {
                    Text = $"{noteSelected.notesName} {noteSelected.courseTitle} {noteSelected.notesForCourse}",
                    Title = "Share Text"
                });
            }
            catch (NullReferenceException)
            {
                await DisplayAlert("Error", "Please select a saved note first.", "Ok");
            }




        }

    }
}