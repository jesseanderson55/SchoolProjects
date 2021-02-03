using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace DegreePlanner.Model
{
    public class Term
    {

        [PrimaryKey, AutoIncrement]
        public int termID { get; set; }

        [MaxLength (50)]
        public string termName { get; set; }

        public DateTime termStart { get; set; }

        public DateTime termEnd { get; set; }


    }
}
