using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryLibrary
{
    public class Outsourced : Part
    {
        public string companyName { get; set; }

        public Outsourced() { }

        public Outsourced(string compName, int IDpart, string partName,
            decimal partPrice, int stock, int minimum, int max)
            : base(IDpart, partName, partPrice, stock, minimum, max)
        {
            companyName = compName;
        }
    }
}
