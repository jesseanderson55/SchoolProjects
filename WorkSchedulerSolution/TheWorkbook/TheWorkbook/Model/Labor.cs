using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TheWorkbook.Model
{
    public class Labor
    {
        [PrimaryKey]
        public string ID { get; set; }
        private string employeeID;
        private string toolID;
        private string jobFunctionID;
        private DateTime? timeDateStart;
        private DateTime? timeDateEnd;

        public string EmployeeID
        {
            get { return employeeID; }
            set
            {
                if (employeeID != value)
                {
                    employeeID = value;
                }
            }
        }

        public string ToolID
        {
            get { return toolID; }
            set
            {
                if (toolID != value)
                {
                    toolID = value;
                }
            }
        }
        public DateTime? TimeDateStart
        {
            get { return timeDateStart; }
            set
            {
                if (timeDateStart != value)
                {
                    timeDateStart = value;
                }
            }
        }
        public DateTime? TimeDateEnd
        {
            get { return timeDateEnd; }
            set
            {
                if (timeDateEnd != value)
                {
                    timeDateEnd = value;
                }
            }
        }

        public string JobFunctionID
        {
            get { return jobFunctionID; }
            set
            {
                if (jobFunctionID != value)
                {
                    jobFunctionID = value;
                }
            }
        }
    }

    public class LaborView : Labor
    {
        private string taskName;
        private string toolName;

        public string ToolName 
        {
            get { return toolName; }
            set
            {
                if (toolName != value)
                {
                    toolName = value;
                }
            }
        }

        public string TaskName
        {
            get { return taskName; }
            set
            {
                if (taskName != value)
                {
                    taskName = value;
                }
            }
        }
    }
}
