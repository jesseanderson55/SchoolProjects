using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace DegreePlanner.Model
{
    public class Note
    {
        [PrimaryKey, AutoIncrement]
        public int notesID { get; set; }

        public string notesName { get; set; }

        public int courseID { get; set; }


        public string courseTitle { get; set; }

        public string notesForCourse { get; set; }
    }
}
