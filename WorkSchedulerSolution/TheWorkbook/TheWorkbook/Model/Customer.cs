using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TheWorkbook.Model
{
    public class Customer
    {
        [PrimaryKey]
        public string ID { get; set; }
        private string customerName;
        private string customerPhone;
        private string customerPhone2;
        private string customerStreetAddress;
        private string customerCity;
        private string customerState;
        private string customerZipCode;
        private string customerEmail;
        private string employeeID;

        public string CustomerName
        {
            get { return customerName; }
            set
            {
                if (customerName != value)
                {
                    customerName = value;
                }
            }
        }

        public string CustomerPhone
        {
            get { return customerPhone; }
            set
            {
                if (customerPhone != value)
                {
                    customerPhone = value;
                }
            }
        }

        public string CustomerPhone2
        {
            get { return customerPhone2; }
            set
            {
                if (customerPhone2 != value)
                {
                    customerPhone2 = value;
                }
            }
        }

        public string CustomerStreetAddress
        {
            get { return customerStreetAddress; }
            set
            {
                if (customerStreetAddress != value)
                {
                    customerStreetAddress = value;
                }
            }
        }

        public string CustomerCity
        {
            get { return customerCity; }
            set
            {
                if (customerCity != value)
                {
                    customerCity = value;
                }
            }
        }

        public string CustomerState
        {
            get { return customerState; }
            set
            {
                if (customerState != value)
                {
                    customerState = value;
                }
            }
        }

        public string CustomerZipCode
        {
            get { return customerZipCode; }
            set
            {
                if (customerZipCode != value)
                {
                    customerZipCode = value;
                }
            }
        }

        public string CustomerEmail
        {
            get { return customerEmail; }
            set
            {
                if (customerEmail != value)
                {
                    customerEmail = value;
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

    public class CustomerJob : Customer
    {
        private string jobID;
        private string jobName;

        public string JobName
        {
            get { return jobName; }
            set
            {
                if (jobName != value)
                {
                    jobName = value;
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
    }
}
