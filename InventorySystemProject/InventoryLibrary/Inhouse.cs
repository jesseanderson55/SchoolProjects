using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryLibrary
{
    public class Inhouse : Part
    {
        public int MachineID { get; set; }

        //why have empty default constructors?
        public Inhouse() { }

        public Inhouse (int machID, int IDpart, string partName, 
            decimal partPrice, int stock, int minimum, int max)
            : base (IDpart, partName, partPrice, stock, minimum, max)
        {
            MachineID = machID;
        }
    }
}
