using Syncfusion.Drawing;
using Syncfusion.Pdf;
using Syncfusion.SfDataGrid.XForms;
using Syncfusion.SfDataGrid.XForms.Exporting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheWorkbook.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace TheWorkbook.OwnerPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReportPage : ContentPage
    {
        #region properties
        ObservableCollection<JobReport> jobReports = new ObservableCollection<JobReport>() { };
        ObservableCollection<WorkerReport> employeeReports = new ObservableCollection<WorkerReport>() { };
        #endregion

        #region constructor, onappearing
        public ReportPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            GenerateJobReport();
            GenerateWorkerReport();
            reportsDataGrid.AutoGenerateColumns = false;
            PopulateWithJobs();
        }
        #endregion

        #region methods for populating
        public void GenerateJobReport()
        {
            List<JobFunction> jfListForJob = new List<JobFunction>() { };
            List<Model.Task> taskListForJob = new List<Model.Task>() { };
            List<Labor> laborListForJob = new List<Labor>() { };
            List<Tool> toolListForJob = new List<Tool>() { };

            jobReports.Clear();

            foreach (Job item in App.Jobs)
            {
                if (App.JobFunctions.Count > 0)
                {
                    if (true)
                    {
                        jfListForJob = App.JobFunctions.Where(u => u.JobID == item.ID).ToList();
                    }
                    if (jfListForJob.Count > 0)
                    {
                        foreach (JobFunction jobFunction in jfListForJob)
                        {
                            var tempTaskList = App.Tasks.Where(u => u.ID == jobFunction.TaskID).ToList();
                            if (tempTaskList.Count > 0)
                            {
                                taskListForJob.AddRange(tempTaskList);
                            }

                            var tempLaborList = App.Labors.Where(u => u.JobFunctionID == jobFunction.ID).ToList();
                            if (tempLaborList.Count > 0)
                            {
                                laborListForJob.AddRange(tempLaborList);
                            }
                        }
                    }
                    if (laborListForJob.Count > 0)
                    {
                        foreach (Labor labor in laborListForJob)
                        {
                            var tempToolList = App.Tools.Where(u => u.ID == labor.ToolID).ToList();
                            if (tempToolList != null)
                            {
                                toolListForJob.AddRange(tempToolList);
                            }
                        }
                    }
                }

                int startingPossibleIncome = 0;

                if (toolListForJob.Count > 0)
                {
                    startingPossibleIncome = startingPossibleIncome + toolListForJob.Sum(u => u.ToolCostPerHour);
                }
                if (taskListForJob.Count > 0)
                {
                    startingPossibleIncome = startingPossibleIncome + taskListForJob.Sum(u => u.TaskPricePer);
                }

                var thisCustomerName = App.Customers.Where(u => u.ID == item.CustomerID).FirstOrDefault().CustomerName;

                JobReport jobReport = new JobReport()
                {
                    CustomerName = thisCustomerName,
                    JobName = item.JobName,
                    BaseIncome = startingPossibleIncome
                };

                jobReports.Add(jobReport);
            }
        }

        public void GenerateWorkerReport()
        {
            employeeReports.Clear();

            int toPassDaysOff = 0;
            int toPassDaysWorked = 0;

            foreach (Worker item in App.WorkerList)
            {
                if (App.Requests.Count > 0)
                {
                    var workersRequestList = App.Requests.Where(u => u.EmployeeID == item.ID).ToList();
                    if (workersRequestList != null)
                    {
                        toPassDaysOff = workersRequestList.Where(u => u.Approved == true).Select(u => u.Approved).Count();
                    }
                }

                if (App.Labors.Count > 0)
                {
                    var workersLaborList = App.Labors.Where(u => u.EmployeeID == item.ID).ToList();
                    if (workersLaborList != null)
                    {
                        toPassDaysWorked = workersLaborList.Where(u => u.TimeDateStart != null).Select(u => u.TimeDateStart.Value.Date).GroupBy(u => u.Date).Count();
                    }
                }


                WorkerReport workerReport = new WorkerReport()
                {
                    WorkerLastName = item.WorkerLastName,
                    DaysOff = toPassDaysOff,
                    DaysWorked = toPassDaysWorked
                };

                employeeReports.Add(workerReport);
            }
        }


        public void PopulateWithJobs()
        {
            reportsDataGrid.Columns.Clear();

            reportsDataGrid.ItemsSource = jobReports;

            GridTextColumn jobNameColumn = new GridTextColumn();
            jobNameColumn.MappingName = "JobName";
            jobNameColumn.HeaderText = "Job Name";

            reportsDataGrid.Columns.Add(jobNameColumn);

            GridTextColumn baseNameColumn = new GridTextColumn();
            baseNameColumn.MappingName = "BaseIncome";
            baseNameColumn.HeaderText = "Base Cost/HR";

            reportsDataGrid.GroupColumnDescriptions.Add(new GroupColumnDescription() { ColumnName = "CustomerName" });

            reportsDataGrid.Columns.Add(baseNameColumn);
        }

        public void PopulateWithWorkers()
        {
            reportsDataGrid.Columns.Clear();

            reportsDataGrid.ItemsSource = employeeReports;

            GridTextColumn workerNameColumn = new GridTextColumn();
            workerNameColumn.MappingName = "WorkerLastName";
            workerNameColumn.HeaderText = "Worker";

            reportsDataGrid.Columns.Add(workerNameColumn);

            GridTextColumn daysOffColumn = new GridTextColumn();
            daysOffColumn.MappingName = "DaysOff";
            daysOffColumn.HeaderText = "Days Off";

            reportsDataGrid.Columns.Add(daysOffColumn);

            GridTextColumn daysWorkedColumn = new GridTextColumn();
            daysWorkedColumn.MappingName = "DaysWorked";
            daysWorkedColumn.HeaderText = "Days Scheduled";

            reportsDataGrid.Columns.Add(daysWorkedColumn);
        }
        #endregion

        #region Functional Buttons
        private void jobsButton_Clicked(object sender, EventArgs e)
        {
            PopulateWithJobs();
        }

        private void employeesButton_Clicked(object sender, EventArgs e)
        {
            PopulateWithWorkers();
        }

        private async void sendButton_Clicked(object sender, EventArgs e)
        {
            bool confirmed = await Application.Current.MainPage.DisplayAlert("Query", "Confirm toexport the current form into a PDF. ", "Confirm", "Cancel");

            if (confirmed)
            {
            
                DataGridPdfExportingController pdfExport = new DataGridPdfExportingController();
                MemoryStream stream = new MemoryStream();
                var doc = pdfExport.ExportToPdf(this.reportsDataGrid, new DataGridPdfExportOption() { FitAllColumnsInOnePage = true });
                doc.Save(stream);
                doc.Close(true);
                if (Device.OS == TargetPlatform.WinPhone || Device.OS == TargetPlatform.Windows)
                    Xamarin.Forms.DependencyService.Get<ISaveWindowsPhone>().Save("DataGrid.pdf", "application/pdf", stream);
                else
                    Xamarin.Forms.DependencyService.Get<ISave>().Save("DataGrid.pdf", "application/pdf", stream);


                //I have NO IDEA why the code the offered to print to PDF didnt work but I found a replacement deep in the forums.
                //DataGridPdfExportingController pdfExport = new DataGridPdfExportingController();
                //MemoryStream stream = new MemoryStream();
                //var pdfDoc = new PdfDocument();
                //PdfPage page = pdfDoc.Pages.Add();
                //var exportToPdfGrid = pdfExport.ExportToPdfGrid(this.reportsDataGrid, this.reportsDataGrid.View, new DataGridPdfExportOption()
                //{
                //    FitAllColumnsInOnePage = true,

                //}, pdfDoc);
                //exportToPdfGrid.Draw(page, new PointF(10, 10));
                //pdfDoc.Save(stream);
                //pdfDoc.Close(true);
                //if (Device.OS == TargetPlatform.WinPhone || Device.OS == TargetPlatform.Windows)
                //    await Xamarin.Forms.DependencyService.Get<ISaveWindows>().Save("DataGrid.pdf", "application/pdf", stream);
                //else
                //    Xamarin.Forms.DependencyService.Get<ISave>().Save("DataGrid.pdf", "application/pdf", stream);

            }   

        }
        #endregion

        private void NoticesButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new OwnerPages.NoticePage());
        }
    }
}