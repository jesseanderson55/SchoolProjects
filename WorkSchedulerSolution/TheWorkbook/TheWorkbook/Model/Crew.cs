using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace TheWorkbook.Model
{
    public class Crew : INotifyPropertyChanged
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        private string crewName;
      
        private string workerID;
        public string CrewName
        {
            get { return crewName; }
            set
            {
                if (crewName != value)
                {
                    crewName = value;
                    this.RaisedOnPropertyChanged("CrewName");
                }
            }
        }

        public string WorkerID 
        {
            get { return workerID; }
            set
            {
                if (workerID != value)
                {
                    workerID = value;
                    this.RaisedOnPropertyChanged("WorkerID");
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

    //extension class created for when the above crew.cs class is called from sqlite and joined with worker.cs to find the workerlastname
    public class CrewWorker : Crew , INotifyPropertyChanged
    {
        public new int? ID;
        public string WorkerLastName { get; set; }

        public bool IsSupervisor { get; set; }
    }
}
