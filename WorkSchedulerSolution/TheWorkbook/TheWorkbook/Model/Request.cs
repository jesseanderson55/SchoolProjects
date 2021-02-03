using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace TheWorkbook.Model
{
    public class Request 
    {
        [PrimaryKey]
        public string ID { get; set; }
        private string employeeID;
        private DateTime requestDate;
        private string reasonForRequest;
        private string ownerID;
        private string employeeName;
        private bool approved;

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

        public bool Approved
        {
            get { return approved; }
            set
            {
                if (approved != value)
                {
                    if (value != true)
                    {
                        approved = false;
                    }
                    else
                    {
                        approved = value;
                    }
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

        public DateTime RequestDate
        {
            get { return requestDate; }
            set
            {
                if (requestDate != value)
                {
                    requestDate = value;
                }
            }
        }

        public string ReasonForRequest
        {
            get { return reasonForRequest; }
            set
            {
                if (reasonForRequest != value)
                {
                    reasonForRequest = value;
                }
            }
        }

        public string EmployeeName
        {
            get { return employeeName; }
            set
            {
                if (employeeName != value)
                {
                    employeeName = value;
                }
            }
        }

    }
}
