using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TheWorkbook.Model
{
    public class Note
    {
        [PrimaryKey]
        public string ID { get; set; }
        private string jobID;
        private string jobNotes;

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

        public string JobNotes
        {
            get { return jobNotes; }
            set
            {
                if (jobNotes != value)
                {
                    jobNotes = value;
                }
            }
        }
    }
}
