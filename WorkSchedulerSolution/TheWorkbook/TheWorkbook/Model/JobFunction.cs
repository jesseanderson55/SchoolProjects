using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace TheWorkbook.Model
{
    public class JobFunction
    {
        [PrimaryKey]
        public string ID { get; set; }
        private string taskID;
        private string jobID;
        private DateTime dateScheduled;

        public string TaskID
        {
            get { return taskID; }
            set
            {
                if (taskID != value)
                {
                    taskID = value;
                }
            }
        }

        public string JobID
        {
            get { return jobID; }
            set
            {
                if (jobID != value)
                {
                    jobID = value;
                }
            }
        }

        public DateTime DateScheduled
        {
            get { return dateScheduled; }
            set
            {
                if (dateScheduled != value)
                {
                    dateScheduled = value;
                }
            }
        }
    }

    public class JobFunctionView : JobFunction, INotifyPropertyChanged
    {
        private string customerName;
        private string jobName;
        private bool workersAssigned;
        private string yearToGroupBy;
        private string monthToGroupBy;
        private string dayMonthToGroupBy;
        private string hourToSortBy;

        public JobFunctionView(string passedJobID, DateTime passedDateScheduled)
        {
            JobID = passedJobID;
            DateScheduled = passedDateScheduled;

            var connectedJob = App.Jobs.Where(u => u.ID == JobID).FirstOrDefault();
            jobName = connectedJob.JobName;

            customerName = App.Customers.Where(u => u.ID == connectedJob.CustomerID).FirstOrDefault().CustomerName;

            yearToGroupBy = DateScheduled.ToString("yyyy");
            monthToGroupBy = DateScheduled.ToString("MM-MMMM");
            dayMonthToGroupBy = DateScheduled.ToString("MM/dd");
            hourToSortBy = DateScheduled.ToString("HH");
        }

        public string CustomerName
        {
            get { return customerName; }
        }

        public string JobName
        {
            get { return jobName; }
        }

        public bool WorkersAssigned
        {
            get { return workersAssigned; }
            set
            {
                if (workersAssigned != value)
                {
                    workersAssigned = value;
                }
            }
        }

        public string YearToGroupBy
        {
            get { return yearToGroupBy; }
        }
        public string MonthToGroupBy
        {
            get { return monthToGroupBy; }
        }
        public string DayMonthToGroupBy
        {
            get { return dayMonthToGroupBy; }
        }
        public string HourToSortBy
        {
            get { return hourToSortBy; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisedOnPropertyChanged(string _PropertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(_PropertyName));
            }
        }

    }
}
