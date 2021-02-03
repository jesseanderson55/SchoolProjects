using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheWorkbook.Model
{
    public class Job
    {
        [PrimaryKey]
        public string ID { get; set; }
        private int jobDiscount;
        private string jobName;
        private string jobStreetAddress;
        private string jobCity;
        private string jobState;
        private string jobZipCode;
        private string customerID;

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

        public int JobDiscount
        {
            get { return jobDiscount; }
            set
            {
                if (jobDiscount != value)
                {
                    jobDiscount = value;
                }
            }
        }

        public string JobStreetAddress
        {
            get { return jobStreetAddress; }
            set
            {
                if (jobStreetAddress != value)
                {
                    jobStreetAddress = value;
                }
            }
        }

        public string JobCity
        {
            get { return jobCity; }
            set
            {
                if (jobCity != value)
                {
                    jobCity = value;
                }
            }
        }

        public string JobState
        {
            get { return jobState; }
            set
            {
                if (jobState != value)
                {
                    jobState = value;
                }
            }
        }

        public string JobZipCode
        {
            get { return jobZipCode; }
            set
            {
                if (jobZipCode != value)
                {
                    jobZipCode = value;
                }
            }
        }

        public string CustomerID
        {
            get { return customerID; }
            set
            {
                if (customerID != value)
                {
                    customerID = value;
                }
            }
        }

    }

    public class JobReport : Job
    {
        
        private string customerName;

        private int baseIncome;

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

        public int BaseIncome
        {
            get { return baseIncome; }
            set
            {
                if (baseIncome != value)
                {
                    baseIncome = value;
                }
            }
        }
    }
}
