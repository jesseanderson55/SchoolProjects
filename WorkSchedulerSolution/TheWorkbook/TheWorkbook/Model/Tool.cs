using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TheWorkbook.Model
{
    public class Tool
    {
        [PrimaryKey]
        public string ID { get; set; }
        private string toolName;
        private int toolCostPerHour;
        private string employeeID;

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
}
