using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace DegreePlanner.Model
{
    public class Milestone
    {
        [PrimaryKey, AutoIncrement]
        public int objectiveID { get; set; }

        public int courseID { get; set; }

        [MaxLength(50)]
        public string courseTitle { get; set; }

        [MaxLength(50)]
        public string milestoneName { get; set; }

        public string milestoneType { get; set; }

        public DateTime milestoneStart { get; set; }

        public DateTime milestoneEnd { get; set; }

        public string goalEntry { get; set; }

        public String untilDue 
        {
            get 
            {
                string dateDifference = (milestoneEnd - DateTime.Now).ToString("%d") + " days";
                return dateDifference;
            } 
        }
    }
}
