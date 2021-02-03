using SQLite;
using Syncfusion.SfCalendar.XForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace TheWorkbook.Model
{
    public class Message : INotifyPropertyChanged
    {
        [PrimaryKey]
        public string ID { get; set; }

        private string senderID;
        private string textMessage;
        private string ownerID;
        private DateTime dateTimeSent;
        private bool toSupervisors;

        public string SenderID
        {
            get { return senderID; }
            set
            {
                if (senderID != value)
                {
                    senderID = value;
                    this.RaisedOnPropertyChanged("SenderID");
                }
            }
        }

        public string TextMessage
        {
            get { return textMessage; }
            set
            {
                if (textMessage != value)
                {
                    textMessage = value;
                    this.RaisedOnPropertyChanged("TextMessage");
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
                    this.RaisedOnPropertyChanged("OwnerID");
                }
            }
        }

        public DateTime DateTimeSent
        {
            get { return dateTimeSent; }
            set
            {
                if (dateTimeSent != value)
                {
                    dateTimeSent = value;
                    this.RaisedOnPropertyChanged("DateTimeSent");
                }
            }
        }

        public bool ToSupervisors
        {
            get { return toSupervisors; }
            set
            {
                if (toSupervisors != value)
                {
                    toSupervisors = value;
                    this.RaisedOnPropertyChanged("ToSupervisors");
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
}
