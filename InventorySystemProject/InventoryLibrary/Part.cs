using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryLibrary
{
    public abstract class Part
    {
        public int PartID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int InStock { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }

        //why have empty default constructors?
        public Part() { }

        public Part (int IDpart, string partName, decimal partPrice, 
            int stock, int minimum, int max)
        {
            PartID = IDpart;
            Name = partName;
            Price = partPrice;
            InStock = stock;
            Min = minimum;
            Max = max;
        }
    }
}
