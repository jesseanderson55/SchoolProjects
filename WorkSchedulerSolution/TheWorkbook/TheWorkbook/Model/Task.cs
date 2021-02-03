using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TheWorkbook.Model
{
    public class Task
    {
        [PrimaryKey]
        public string ID { get; set; }
        private string taskName;
        private int taskPricePer;
        private string employeeID;

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

        public int TaskPricePer
        {
            get { return taskPricePer; }
            set
            {
                if (taskPricePer != value)
                {
                    taskPricePer = value;
                }
            }
        }

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
    }

    public class TaskTool : Model.Task  
    {

        public string toolID { get; set; }
        private string toolName;
        private int toolCostPerHour;
        private int totalCost;
        public int TotalCost
        {
            get { return totalCost; }
            set
            {
                totalCost = toolCostPerHour + TaskPricePer;
            }
        }

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

        public int ToolCostPerHour
        {
            get { return toolCostPerHour; }
            set
            {
                if (toolCostPerHour != value)
                {
                    toolCostPerHour = value;
                }
            }
        }
    }
}
