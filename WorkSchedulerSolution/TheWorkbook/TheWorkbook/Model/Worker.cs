using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace TheWorkbook.Model
{
    public class Worker : INotifyPropertyChanged
    {
        [PrimaryKey]
        public string ID { get; set; }
        private string workerUserName;
        private string workerLastName;
        private int workerHourly;
        private string workerEmail;
        private string workerPassword;
        private string workerPhone;
        private string streetAddress;
        private string cityAddress;
        private string stateAddress;
        private string zipCode;
        private bool isSupervisor;
        public string EmployerEmail { get; set; }
        public string CompanyName { get; set; }

        public string WorkerUserName
        {

            get { return workerUserName; }
            set
            {
                if (workerUserName != value)
                {
                    workerUserName = value;
                    this.RaisedOnPropertyChanged("WorkerUserName");
                }
            }
        }

        public string WorkerLastName
        {
            get { return workerLastName; }
            set
            {
                if (workerLastName != value)
                {
                    workerLastName = value;
                    this.RaisedOnPropertyChanged("WorkerLastName");
                }
            }
        }

        public int WorkerHourly
        {
            get { return workerHourly; }
            set
            {
                if (workerHourly != value)
                {
                    workerHourly = value;
                    this.RaisedOnPropertyChanged("WorkerHourly");
                }
            }
        }

        public string WorkerEmail
        {
            get { return workerEmail; }
            set
            {
                if (workerEmail != value)
                {
                    workerEmail = value;
                    this.RaisedOnPropertyChanged("WorkerEmail");
                }
            }
        }

        public string WorkerPassword
        {
            get { return workerPassword; }
            set
            {
                if (workerPassword != value)
                {
                    workerPassword = value;
                    this.RaisedOnPropertyChanged("WorkerPassword");
                }
            }
        }

        public string WorkerPhone
        {
            get { return workerPhone; }
            set
            {
                if (workerPhone != value)
                {
                    workerPhone = value;
                    this.RaisedOnPropertyChanged("WorkerPhone");
                }
            }
        }

        public string StreetAddress
        {
            get { return streetAddress; }
            set
            {
                if (streetAddress != value)
                {
                    streetAddress = value;
                    this.RaisedOnPropertyChanged("StreetAddress");
                }
            }
        }

        public string CityAddress
        {
            get { return cityAddress; }
            set
            {
                if (cityAddress != value)
                {
                    cityAddress = value;
                    this.RaisedOnPropertyChanged("CityAddress");
                }
            }
        }

        public string StateAddress
        {
            get { return stateAddress; }
            set
            {
                if (stateAddress != value)
                {
                    stateAddress = value;
                    this.RaisedOnPropertyChanged("StateAddress");
                }
            }
        }

        public string ZipCode
        {
            get { return zipCode; }
            set
            {
                if (zipCode != value)
                {
                    zipCode = value;
                    this.RaisedOnPropertyChanged("ZipCode");
                }
            }
        }

        public bool IsSupervisor
        {
            get { return isSupervisor; }
            set
            {
                if (isSupervisor != value)
                {
                    isSupervisor = value;
                    this.RaisedOnPropertyChanged("IsSupervisor");
                }
            }
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

    public class WorkerReport : Worker
    {
        private int daysWorked;
        private int daysOff;

        public int DaysWorked
        {
            get { return daysWorked; }
            set
            {
                if (daysWorked != value)
                {
                    daysWorked = value;
                }
            }
        }

        public int DaysOff
        {
            get { return daysOff; }
            set
            {
                if (daysOff != value)
                {
                    daysOff = value;
                }
            }
        }
    }
}
