using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace DegreePlanner.Model
{
    public class Course
    {
        [PrimaryKey, AutoIncrement]
        public int courseID { get; set; }

        [MaxLength(50)]
        public string courseTitle { get; set; }

        public string termName { get; set; }

        public int termID { get; set; }

        public DateTime courseStart { get; set; }

        public DateTime courseEnd { get; set; }

        public string courseStatus { get; set; }



        [MaxLength(30)]
        public string instructorName { get; set; }

        [MaxLength(14)]
        public string instructorPhone { get; set; }

        [MaxLength(50)]
        public string instructorEmail { get; set; }
    }
}
