using SQLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace TheWorkbook.Model
{
    public class TimeClockRecord
    {
        [PrimaryKey]
        public string ID { get; set; }
        private string employeeID;
        private DateTime clockedIn;
        private DateTime? clockedOut;
        private string ownerID;

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

        public DateTime ClockedIn
        {
            get { return clockedIn; }
            set
            {
                if (clockedIn != value)
                {
                    clockedIn = value;
                }
            }
        }

        public DateTime? ClockedOut
        {
            get { return clockedOut; }
            set
            {
                if (clockedOut != value)
                {
                    clockedOut = value;
                }
            }
        }
        public string OwnerID
        {
            get { return ownerID; }
            set
            {
                if (ownerID != value)
                {
                    ownerID = value;
                }
            }
        }
    }
}
