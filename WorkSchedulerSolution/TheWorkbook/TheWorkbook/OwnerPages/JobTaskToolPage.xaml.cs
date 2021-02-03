using System;
using System.Collections.Generic;
using System.Linq;
using Syncfusion.XForms.Pickers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TheWorkbook.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;

namespace TheWorkbook.OwnerPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class JobTaskToolPage : ContentPage
    {
        #region Properties
        List<TaskTool> taskTools = new List<TaskTool>() { };
        List<TaskTool> addedTaskTools = new List<TaskTool>() { };

        Job passedJob;
        DateTime passedDate;
        TaskTool selectedTopTaskTool = new TaskTool();
        TaskTool selectedBottomTaskTool = new TaskTool();
        #endregion

        #region Constructor and populator
        public JobTaskToolPage(Job passedjob, DateTime dateTime)
        {
            InitializeComponent();
            Title = $"Tasks for {passedjob.JobName}";
            this.passedJob = passedjob;
            passedDate = dateTime;
            ListviewPickerPopulate();
        }

        private async void ListviewPickerPopulate()
        {

            try
            {
                await FormModel.TaskToolsAzureDatabaseQuery();
            }
            catch (Exception)
            {

                throw;
            }
            TasksForThisJobListView.ItemsSource = null;
            TotalTaskListview.ItemsSource = null;
            listOfTask.ItemsSource = null;
            listOfTools.ItemsSource = null;
            listOfTask.ItemsSource = App.Tasks;
            listOfTools.ItemsSource = App.Tools;
            TasksForThisJobListView.ItemsSource = addedTaskTools;
            var totalsCostsPerHour = addedTaskTools.Sum(tools => tools.ToolCostPerHour) + addedTaskTools.Sum(tasks => tasks.TaskPricePer);
            if (addedTaskTools.Count > 0)
            {
                Title = $"Tasks for {passedJob.JobName} total/hour: {totalsCostsPerHour}";
            }
            else
            {
                Title = $"Tasks for {passedJob.JobName}";
            }

            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                //makes a table of just crew unfiltered
                conn.CreateTable<TaskTool>();
                taskTools = conn.Table<TaskTool>().ToList();
                TotalTaskListview.ItemsSource = taskTools;
            }

            TasksForThisJobListView.RefreshView();
            TotalTaskListview.RefreshView();
        }
        #endregion

        #region Overlay Buttons
        private void NewTaskToolButton_Clicked(object sender, EventArgs e)
        {
            overlay.IsEnabled = true;
            overlay.IsVisible = true;
            overlayNewTask.IsVisible = true;
            background.IsEnabled = false;
            overlayNewTask.IsEnabled = true;
        }

        private void cancelNewTask_Clicked(object sender, EventArgs e)
        {
            overlay.IsEnabled = false;
            overlay.IsVisible = false;
            overlayNewTask.IsVisible = false;
            background.IsEnabled = true;
            overlayNewTask.IsEnabled = false;
            taskNameEntry.Text = string.Empty;
            toolNameEntry.Text = string.Empty;
            pricePerTaskHourEntry.Text = string.Empty;
            pricePerToolHourEntry.Text = string.Empty;
        }
        #endregion

        #region Functional Buttons
        private void TasksForThisJobListView_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            selectedTopTaskTool = e.ItemData as TaskTool;
        }

        private void TotalTaskListview_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            selectedBottomTaskTool = e.ItemData as TaskTool;
        }

        private async void DeleteToolTask_Clicked(object sender, EventArgs e)
        {

            try
            {
                if (listOfTools.SelectedItem != null)
                {
                    bool checkDelete = await Application.Current.MainPage.DisplayAlert("Alert", "Delete Tool?", "Yes", "No");
                    if (checkDelete)
                    {
                        await App.mobileService.GetTable<Tool>().DeleteAsync(listOfTools.SelectedItem as Tool);
                    }

                }
                if (listOfTask.SelectedItem != null)
                {
                    bool checkDelete = await Application.Current.MainPage.DisplayAlert("Alert", "Delete Task?", "Yes", "No");
                    if (checkDelete)
                    {
                        await App.mobileService.GetTable<Model.Task>().DeleteAsync(listOfTask.SelectedItem as Model.Task);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            ListviewPickerPopulate();
        }

        private async void DeleteTaskToolButton_Clicked(object sender, EventArgs e)
        {
            if (selectedBottomTaskTool != null)
            {
                using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
                {
                    conn.CreateTable<TaskTool>();

                    conn.Delete(selectedBottomTaskTool);
                }
                ListviewPickerPopulate();
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Please select a Task to delete.", "OK");
            }
        }

        private async void addTaskToTheJobButton_Clicked(object sender, EventArgs e)
        {
            if (selectedBottomTaskTool != null)
            {
                addedTaskTools.Add(selectedBottomTaskTool);
                ListviewPickerPopulate();
                selectedBottomTaskTool = null;
                TotalTaskListview.SelectedItem = null;
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Alert", "Select a task below to add to the job above.", "Yes");
            }
        }

        private async void removeTaskFromJobButton_Clicked(object sender, EventArgs e)
        {
            if (selectedTopTaskTool != null)
            {
                addedTaskTools.Remove(selectedTopTaskTool);
                ListviewPickerPopulate();
                selectedTopTaskTool = null;
                TasksForThisJobListView.SelectedItem = null;
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Alert", "Select a task above to remove from the job.", "Yes");
            }
        }

        private async void saveNewTask_Clicked(object sender, EventArgs e)
        {
            Model.Task task = new Model.Task() { };
            Model.TaskTool taskTool = new Model.TaskTool() { };

            bool entrySuccessandExit = false;

            //checks to see if a task name was entered. Cost isnt necessary.
            if (!string.IsNullOrEmpty(taskNameEntry.Text))
            {
                //checks to see if anything is in the cost form and if its a number
                if (!int.TryParse(pricePerTaskHourEntry.Text, out int output))
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Please enter valid hourly cost for Tasks", "OK");
                }
                else
                {
                    //creates a temporary list of TASK objects
                    List<Model.Task> tasklist = new List<Model.Task>() { };

                    if (App.user != null)
                    {

                        var taskMatch = App.Tasks.Where(u => u.TaskName == taskNameEntry.Text).FirstOrDefault();
                        if (taskMatch != null)
                        {
                            if (!string.IsNullOrEmpty(taskMatch.EmployeeID))
                            {
                                bool response = await Application.Current.MainPage.DisplayAlert("Alert", "Update Task?", "Yes", "No");

                                if (response)
                                {
                                    taskMatch.TaskPricePer = int.Parse(pricePerTaskHourEntry.Text);
                                    await App.mobileService.GetTable<Model.Task>().UpdateAsync(taskMatch);
                                    var templist = await App.mobileService.GetTable<Model.Task>().Where(u => u.EmployeeID == App.user.ID).ToListAsync();
                                    if (pricePerTaskHourEntry.Text != null)
                                    {
                                        taskTool.ID = templist.Where(u => u.TaskName == taskNameEntry.Text && u.TaskPricePer == taskMatch.TaskPricePer).FirstOrDefault().ID;
                                    }
                                    else
                                    {
                                        taskTool.ID = templist.Where(u => u.TaskName == taskNameEntry.Text).FirstOrDefault().ID;
                                    }

                                    taskTool.EmployeeID = App.user.ID;
                                    taskTool.TaskName = taskNameEntry.Text;
                                    taskTool.TaskPricePer = int.Parse(pricePerTaskHourEntry.Text);
                                }
                            }

                        }
                        else
                        {
                            task.EmployeeID = App.user.ID;
                            task.TaskName = taskNameEntry.Text;
                            task.TaskPricePer = int.Parse(pricePerTaskHourEntry.Text);
                            taskTool.EmployeeID = App.user.ID;
                            taskTool.TaskName = taskNameEntry.Text;
                            taskTool.TaskPricePer = int.Parse(pricePerTaskHourEntry.Text);

                            await App.mobileService.GetTable<Model.Task>().InsertAsync(task);

                            var templist = await App.mobileService.GetTable<Model.Task>().Where(u => u.EmployeeID == App.user.ID).ToListAsync();
                            if (pricePerTaskHourEntry.Text != null)
                            {
                                taskTool.ID = templist.Where(u => u.TaskName == taskNameEntry.Text && u.TaskPricePer == taskTool.TaskPricePer).FirstOrDefault().ID;
                            }
                            else
                            {
                                taskTool.ID = templist.Where(u => u.TaskName == taskNameEntry.Text).FirstOrDefault().ID;
                            }
                        }

                        await Application.Current.MainPage.DisplayAlert("Alert", "Success, task saved!", "OK");
                        App.Tasks = await App.mobileService.GetTable<Model.Task>().Where(u => u.EmployeeID == App.user.ID).ToListAsync();

                        entrySuccessandExit = true;
                    }
                }
            }
            if (!string.IsNullOrEmpty(toolNameEntry.Text))
            {
                if (!int.TryParse(pricePerToolHourEntry.Text, out int output))
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Please enter valid hourly cost for Tool", "OK");
                }
                else
                {
                    List<Tool> toolList = new List<Tool>() { };

                    if (App.user != null)
                    {

                        var toolMatch = App.Tools.Where(u => u.ToolName == toolNameEntry.Text).FirstOrDefault();

                        if (toolMatch != null)
                        {
                            if (!string.IsNullOrEmpty(toolMatch.EmployeeID))
                            {
                                bool response = await Application.Current.MainPage.DisplayAlert("Alert", "Update Tool?", "Yes", "No");

                                if (response)
                                {
                                    toolMatch.ToolCostPerHour = int.Parse(pricePerToolHourEntry.Text);
                                    await App.mobileService.GetTable<Tool>().UpdateAsync(toolMatch);

                                    var templist = await App.mobileService.GetTable<Tool>().Where(u => u.EmployeeID == App.user.ID).ToListAsync();
                                    if (pricePerToolHourEntry.Text != null)
                                    {
                                        taskTool.toolID = templist.Where(u => u.ToolName == toolNameEntry.Text && u.ToolCostPerHour == toolMatch.ToolCostPerHour).FirstOrDefault().ID;
                                    }
                                    else
                                    {
                                        taskTool.toolID = templist.Where(u => u.ToolName == toolNameEntry.Text).FirstOrDefault().ID;
                                    }

                                    taskTool.ToolName = toolNameEntry.Text;
                                    taskTool.ToolCostPerHour = int.Parse(pricePerToolHourEntry.Text);

                                }
                            }
                        }
                        else
                        {
                            Tool tool = new Tool()
                            {
                                EmployeeID = App.user.ID,
                                ToolName = toolNameEntry.Text,
                                ToolCostPerHour = int.Parse(pricePerToolHourEntry.Text)
                            };

                            taskTool.ToolName = toolNameEntry.Text;
                            taskTool.ToolCostPerHour = int.Parse(pricePerToolHourEntry.Text);

                            await App.mobileService.GetTable<Tool>().InsertAsync(tool);

                            var templist = await App.mobileService.GetTable<Tool>().Where(u => u.EmployeeID == App.user.ID).ToListAsync();
                            if (pricePerToolHourEntry.Text != null)
                            {
                                taskTool.toolID = templist.Where(u => u.ToolName == toolNameEntry.Text && u.ToolCostPerHour == taskTool.ToolCostPerHour).FirstOrDefault().ID;
                            }
                            else
                            {
                                taskTool.toolID = templist.Where(u => u.ToolName == toolNameEntry.Text).FirstOrDefault().ID;
                            }
                        }

                        await Application.Current.MainPage.DisplayAlert("Alert", "Success, tool saved!", "OK");
                        App.Tools = await App.mobileService.GetTable<Tool>().Where(u => u.EmployeeID == App.user.ID).ToListAsync();
                    }
                }
            }
            if (!string.IsNullOrEmpty(taskTool.TaskName))
            {
                using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
                {
                    conn.CreateTable<TaskTool>();

                    bool isCreated = taskTools.Where(u => u.TaskName == taskTool.TaskName && u.ToolName == taskTool.ToolName).Any();
                    try
                    {
                        if (isCreated)
                        {
                            conn.Update(taskTool);
                        }
                        else
                        {
                            conn.Insert(taskTool);
                        }
                    }
                    catch (SQLiteException ex)
                    {
                        string bob = ex.Result.ToString();

                        if (ex.Result.ToString() == "Constraint")
                        {
                            await Application.Current.MainPage.DisplayAlert("Error", $"Please select a new Task name if not paired with a tool", "Ok");
                            entrySuccessandExit = false;
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert("Error", $"{ex}", "Ok");
                            entrySuccessandExit = false;
                        }

                    }

                    ListviewPickerPopulate();
                }
            }
            if (entrySuccessandExit)
            {
                overlay.IsEnabled = false;
                overlay.IsVisible = false;
                overlayNewTask.IsVisible = false;
                background.IsEnabled = true;
                overlayNewTask.IsEnabled = false;
                taskNameEntry.Text = string.Empty;
                toolNameEntry.Text = string.Empty;
                pricePerTaskHourEntry.Text = string.Empty;
                pricePerToolHourEntry.Text = string.Empty;
            }
        }

        private void listOfTools_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listOfTools.SelectedItem != null)
            {
                toolNameEntry.Text = (listOfTools.SelectedItem as Model.Tool).ToolName;
                pricePerToolHourEntry.Text = (listOfTools.SelectedItem as Model.Tool).ToolCostPerHour.ToString();
            }
        }

        private void listOfTask_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listOfTask.SelectedItem != null)
            {
                taskNameEntry.Text = (listOfTask.SelectedItem as Model.Task).TaskName;
                pricePerTaskHourEntry.Text = (listOfTask.SelectedItem as Model.Task).TaskPricePer.ToString();
            }
        }

        private async void SaveTaskToolButton_Clicked(object sender, EventArgs e)
        {
            if (addedTaskTools.Count > 0)
            {
                bool successfulInsert = false;

                foreach (var item in addedTaskTools)
                {
                    JobFunction jobFunction = new JobFunction()
                    {
                        JobID = passedJob.ID,
                        TaskID = item.ID,
                        DateScheduled = passedDate
                    };
                    try
                    {
                        await App.mobileService.GetTable<JobFunction>().InsertAsync(jobFunction);
                        successfulInsert = true;
                    }
                    catch (Exception)
                    {

                        throw;

                    }
                }
                if (successfulInsert)
                {
                    this.Navigation.RemovePage(this.Navigation.NavigationStack[this.Navigation.NavigationStack.Count - 2]);

                    await this.Navigation.PopAsync();

                    await Application.Current.MainPage.DisplayAlert("Success", "Job scheduled! Dont forget to assign workers for them to see this job you scheduled!", "Ok");


                    await FormModel.FullAzureDatabaseQuery();

                    await Navigation.PushAsync(new OwnerPages.JobPage());
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Job must have at least one task.", "Ok");
            }
        }
        #endregion
    }
}